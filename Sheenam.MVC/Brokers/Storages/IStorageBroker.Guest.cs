//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Linq;
using System.Threading.Tasks;
using Sheenam.MVC.Models.Foundations.Guests;

namespace Sheenam.MVC.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Guest>  InsertGuestAsync(Guest guest);
        IQueryable<Guest> SelectAllGuestsAsync(Guest guest);
    }
}
