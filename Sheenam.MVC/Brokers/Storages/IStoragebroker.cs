//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.MVC.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<T> InsertAsync<T>(T @object);

        IQueryable<T> SelectAll<T>() where T : class;

        ValueTask<T> SelectAsync<T>(params object[] objectsId) where T : class;

        ValueTask<T> UpdateAsync<T>(T @object);

        ValueTask<T> DeleteAsync<T>(T @object);
    }
}
