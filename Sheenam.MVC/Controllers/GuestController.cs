//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Sheenam.MVC.Models.Foundations.Guests;
using Sheenam.MVC.Models.Foundations.Guests.Exceptions;
using Sheenam.MVC.Services.Foundations.Guests;

namespace Sheenam.MVC.Controllers
{
    public class GuestController : Controller
    {
        private readonly IGuestService guestService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public GuestController(IGuestService guestService, IWebHostEnvironment webHostEnvironment)
        {
            this.guestService = guestService;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult PostGuest()
        {
            return View("PostGuest");
        }
        
        [HttpPost]
        public async ValueTask<ActionResult<Guest>> PostGuest(Guest guest)
        {
            try
            {
                Guest postedGuest =
                    await this.guestService.AddGuestAsync(guest);

                return RedirectToAction("GetAllGuests");
            }
            catch (GuestValidationException GuestValidationException)
            {
                return BadRequest(GuestValidationException.InnerException);
            }
            catch (GuestDependencyValidationException guestDependencyValidationException)
                when (guestDependencyValidationException.InnerException is AlreadyExistsGuestException)
            {
                return Conflict(guestDependencyValidationException.InnerException);
            }
            catch (GuestDependencyValidationException guestDependencyValidationException)
            {
                return Conflict(guestDependencyValidationException.InnerException);
            }
            catch (GuestDependencyException guestDependencyException)
            {
                return BadRequest(guestDependencyException.InnerException);
            }
            catch (GuestServiceException guestServiceException)
            {
                return BadRequest(guestServiceException.InnerException);
            }
        }

        [HttpPost]
        public IActionResult UpdateGuest(Guest guest)
        {
            this.guestService.ModifyGuestAsync(guest);

            return RedirectToAction("GetAllGuests");
        }

        [HttpGet]
        public ActionResult<IQueryable<Guest>> GetAllGuests()
        {
            try
            {
                IQueryable<Guest> guests = this.guestService.RetrieveAllGuests();

                return View(guests);
            }
            catch (GuestDependencyException guestDependencyException)
            {
                return BadRequest(guestDependencyException.InnerException);
            }
            catch (GuestServiceException guestServiceException)
            {
                return BadRequest(guestServiceException);
            }
        }

        [HttpGet]
        public async ValueTask<ActionResult<Guest>> Edit(Guid id)
        {
            Guest guest = await this.guestService.RetrieveGuestByIdAsync(id);

            return View(guest);
        }

        [HttpGet]
        public IActionResult DeleteGuest(Guid id)
        {
            this.guestService.RemoveGuestByIdAsync(id);

            return RedirectToAction("GetAllGuests");
        }

        [HttpGet] 
        public async ValueTask<ActionResult<Guest>> GetProfileInformation(Guid id)
        {
            Guest guest = await  this.guestService.RetrieveGuestByIdAsync(id);

            return View(guest);
        }
    }
}
