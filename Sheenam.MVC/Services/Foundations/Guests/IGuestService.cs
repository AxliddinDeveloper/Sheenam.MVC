//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Sheenam.MVC.Models.Foundations.Guests;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace Sheenam.MVC.Services.Foundations.Guests
{
    public interface IGuestService
    {
        ValueTask<Guest> AddGuestAsync(Guest guest);
        IQueryable<Guest> RetrieveAllGuests();
        ValueTask<Guest> RetrieveGuestByIdAsync(Guid Id);
        ValueTask<Guest> ModifyGuestAsync(Guest guest);
        ValueTask<Guest> RemoveGuestByIdAsync(Guid id);
    }
}
