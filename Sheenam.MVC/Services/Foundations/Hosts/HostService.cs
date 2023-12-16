//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.Linq;
using System.Threading.Tasks;
using Sheenam.MVC.Brokers.Loggings;
using Sheenam.MVC.Brokers.Storages;
using Sheenam.MVC.Models.Foundations.Hosts;

namespace Sheenam.MVC.Services.Foundations.Hosts
{
    public class HostService : IHostService
    {
        private readonly IStorageBroker storagebroker;
        private readonly ILoggingBroker loggingbroker;

        public HostService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
        {
            this.storagebroker = storageBroker;
            this.loggingbroker = loggingBroker;
        }

        public async ValueTask<Host> AddHostAsync(Host host)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Host> RetrieveAllHosts()
        {
            return this.storagebroker.SelectAllHosts();
        }

        public async ValueTask<Host> RetrieveHostByIdAsync(Guid id)
        {
            return await this.storagebroker.SelectHostByIdAsync(id);
        }

        public async ValueTask<Host> ModifyHostAsync(Host host)
        {
            return await this.storagebroker.UpdateHostAsync(host);
        }

        public async ValueTask<Host> RemoveHostByIdAsync(Guid id)
        {
            Host mayBeHost = await this.storagebroker.SelectHostByIdAsync(id);

            return await this.storagebroker.DeleteHostAsync(mayBeHost);
        }
    }
}
