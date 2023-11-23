//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.Linq;
using System.Threading.Tasks;
using Sheenam.MVC.Models.Foundations.Hosts;

namespace Sheenam.MVC.Brokers.Storages
{
    public partial class StorageBroker
    {
        public async ValueTask<Host> InsertHostAsync(Host host) =>
            await InsertAsync(host);

        public IQueryable<Host> SelectAllHosts() =>
            SelectAll<Host>();

        public async ValueTask<Host> SelectHostByIdAsync(Guid id) =>
            await SelectAsync<Host>(id);

        public async ValueTask<Host> UpdateHostAsync(Host host) =>
            await UpdateAsync<Host>(host);

        public async ValueTask<Host> DeleteHostAsync(Host host) =>
            await DeleteAsync(host);
    }
}
