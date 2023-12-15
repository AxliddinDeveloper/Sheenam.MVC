//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.Linq;
using System.Threading.Tasks;
using Sheenam.MVC.Brokers.Loggings;
using Sheenam.MVC.Brokers.Storages;
using Sheenam.MVC.Models.Foundations.Guests;

namespace Sheenam.MVC.Services.Foundations.Guests
{
    public partial class GuestService : IGuestService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public GuestService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Guest> AddGuestAsync(Guest guest) =>
        TryCatch(async () =>
        {
            ValidateGuest(guest);

            return await this.storageBroker.InsertGuestAsync(guest);
        });

        public IQueryable<Guest> RetrieveAllGuests() =>
            this.storageBroker.SelectAllGuests();

        public ValueTask<Guest> RetrieveGuestByIdAsync(Guid Id) =>
        TryCatch(async () =>
        {
            ValidateGuestId(Id);

            Guest maybeGuest =
                await this.storageBroker.SelectGuestByIdAsync(Id);

            ValidateStorageGuestExists(maybeGuest, Id);

            return maybeGuest;
        });

        public ValueTask<Guest> ModifyGuestAsync(Guest guest) =>
         TryCatch(async () =>
        {
            ValidateGuestOnModify(guest);

            Guest mayBeGuest =
                await this.storageBroker.SelectGuestByIdAsync(guest.Id);

            ValidateAgainstStorageGuestOnModify(inputGuest: guest, storageGuest: mayBeGuest);

            return await this.storageBroker.UpdateGuestAsync(guest);
        });

        public async ValueTask<Guest> RemoveGuestByIdAsync(Guid id)
        {
            Guest mayBeGuest = await this.storageBroker.SelectGuestByIdAsync(id);

            return await this.storageBroker.DeleteGuestAsync(mayBeGuest);
        }
    }
}
