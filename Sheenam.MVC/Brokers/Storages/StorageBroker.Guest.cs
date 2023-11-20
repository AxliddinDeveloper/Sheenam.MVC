//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sheenam.MVC.Models.Foundations.Guests;

namespace Sheenam.MVC.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Guest> Guests { get; set; }

        public async ValueTask<Guest> InsertGuestAsync(Guest guest) =>
            await InsertGuestAsync(guest);

        public async ValueTask<Guest> SelectAllGuestsAsync(Guest guest) =>
            await SelectAllGuestsAsync(guest);

    }
}
