//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Threading.Tasks;

namespace Sheenam.MVC.Brokers.Storages
{
    public partial interface IStoragebroker
    {
        ValueTask<T> InsertAsync<T>(T @object);
    }
}
