//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Microsoft.AspNetCore.Hosting;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using Sheenam.MVC.Services.Foundations.Hosts;
using Sheenam.MVC.Models.Foundations.Hosts;
using Sheenam.MVC.Models.Foundations.Hosts.Exceptions;

namespace Sheenam.MVC.Controllers
{
    public class HostController : Controller
    {
        private readonly IHostService hostService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public HostController(IHostService hostService, IWebHostEnvironment webHostEnvironment)
        {
            this.hostService = hostService;
            this.webHostEnvironment = webHostEnvironment;
        }
        
        [HttpGet]
        public IActionResult PostHost()
        {
            return View("PostHost");
        }

        [HttpPost]
        public async ValueTask<ActionResult<Host>> PostHost(Host host)
        {
            try
            {
                Host postedHost =
                    await this.hostService.AddHostAsync(host);

                return RedirectToAction("GetAllHosts");
            }
            catch (HostValidationException hostValidationException)
            {
                return BadRequest(hostValidationException.InnerException);
            }
            catch (HostDependencyValidationException hostDependencyValidationException)
                when (hostDependencyValidationException.InnerException is AlreadyExistsHostException)
            {
                return Conflict(hostDependencyValidationException.InnerException);
            }
            catch (HostDependencyValidationException hostDependencyValidationException)
            {
                return Conflict(hostDependencyValidationException.InnerException);
            }
            catch (HostDependencyException hostDependencyException)
            {
                return BadRequest(hostDependencyException.InnerException);
            }
            catch (HostServiceException hostServiceException)
            {
                return BadRequest(hostServiceException.InnerException);
            }
        }

        [HttpPost]
        public IActionResult UpdateHost(Host host)
        {
            this.hostService.ModifyHostAsync(host);

            return RedirectToAction("GetAllHosts");
        }

        [HttpGet]
        public ActionResult<IQueryable<Host>> GetAllHosts()
        {
            try
            {
                IQueryable<Host> hosts = this.hostService.RetrieveAllHosts();

                return View(hosts);
            }
            catch (HostDependencyException hostDependencyException)
            {
                return BadRequest(hostDependencyException.InnerException);
            }
            catch (HostServiceException hostServiceException)
            {
                return BadRequest(hostServiceException);
            }
        }

        [HttpGet]
        public async ValueTask<ActionResult<Host>> Edit(Guid id)
        {
            Host guest = await this.hostService.RetrieveHostByIdAsync(id);

            return View(guest);
        }

        [HttpGet]
        public IActionResult DeleteHost(Guid id)
        {
            this.hostService.RemoveHostByIdAsync(id);

            return RedirectToAction("GetAllHosts");
        }

        [HttpGet]
        public async ValueTask<ActionResult<Host>> GetProfileInformation(Guid id)
        {
            Host host = await this.hostService.RetrieveHostByIdAsync(id);

            return View(host);
        }
    }
}
