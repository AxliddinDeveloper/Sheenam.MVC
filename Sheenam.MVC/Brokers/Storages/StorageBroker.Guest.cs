//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sheenam.MVC.Models.Foundations.Guests;

namespace Sheenam.MVC.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Guest> Guests { get; set; }

        public async ValueTask<Guest> InsertGuestAsync(Guest guest) =>
            await InsertAsync(guest);

        public IQueryable<Guest> SelectAllGuests() =>
            SelectAll<Guest>();
        public ValueTask<Guest> SelectGuestByIdAsync(Guid id) =>
            SelectAsync<Guest>(id);
        public ValueTask<Guest> UpdateGuestAsync(Guest guest) =>
            UpdateAsync<Guest>(guest);

        public ValueTask<Guest> DeleteGuestAsync(Guest guest) =>
            DeleteAsync<Guest>(guest);
    }
}
