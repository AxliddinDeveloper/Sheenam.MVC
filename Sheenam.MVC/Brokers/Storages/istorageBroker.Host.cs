//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Linq;
using System.Threading.Tasks;
using System;
using Sheenam.MVC.Models.Foundations.Hosts;

namespace Sheenam.MVC.Brokers.Storages
{
    public partial  interface IStorageBroker
    {
        ValueTask<Host> InsertHostAsync(Host host);
        IQueryable<Host> SelectAllHosts();
        ValueTask<Host> SelectHostByIdAsync(Guid id);
        ValueTask<Host> UpdateHostAsync(Host host);
        ValueTask<Host> DeleteHostAsync(Host host);
    }
}
