using System;
using System.Linq;
using System.Threading.Tasks;
using Sheenam.MVC.Brokers.Loggings;
using Sheenam.MVC.Brokers.Storages;
using Sheenam.MVC.Models.Foundations.Guests;

namespace Sheenam.MVC.Services.Foundations.Guests
{
    public class GuestService : IGuestService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public GuestService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<Guest> AddGuestAsync(Guest guest) =>
            await this.storageBroker.InsertGuestAsync(guest);

        public IQueryable<Guest> RetrieveAllGuests() =>
            this.storageBroker.SelectAllGuests();
            
        public async ValueTask<Guest> RetrieveGuestByIdAsync(Guid Id) =>
            await this.storageBroker.SelectGuestByIdAsync(Id);

        public async ValueTask<Guest> ModifyGuestAsync(Guest guest) =>
            await this.storageBroker.UpdateGuestAsync(guest);

        public async ValueTask<Guest> RemoveGuestByIdAsync(Guid id)
        {
            Guest mayBeGuest = await this.RetrieveGuestByIdAsync(id);

            return await this.storageBroker.DeleteGuestAsync(mayBeGuest);
        }
    }
}
