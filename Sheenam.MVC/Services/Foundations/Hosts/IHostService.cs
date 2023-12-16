//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Linq;
using System.Threading.Tasks;
using System;
using Sheenam.MVC.Models.Foundations.Hosts;

namespace Sheenam.MVC.Services.Foundations.Hosts
{
    public interface IHostService
    {
        ValueTask<Host> AddHostAsync(Host host);
        IQueryable<Host> RetrieveAllHosts();
        ValueTask<Host> RetrieveHostByIdAsync(Guid id);
        ValueTask<Host> ModifyHostAsync(Host host);
        ValueTask<Host> RemoveHostByIdAsync(Guid id);
    }
}
