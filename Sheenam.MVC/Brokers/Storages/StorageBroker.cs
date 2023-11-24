//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Linq;
using System.Threading.Tasks;
using EFxceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Sheenam.MVC.Brokers.Storages
{
    public partial class StorageBroker : EFxceptionsContext, IStoragebroker
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment webHostEnvironment;

        public StorageBroker(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            this.configuration = configuration;
            this.Database.Migrate();
            this.webHostEnvironment = webHostEnvironment;
        }

        public async ValueTask<T> InsertAsync<T>(T @object)
        {
            using var broker = new StorageBroker(this.configuration, webHostEnvironment);
            broker.Entry(@object).State = EntityState.Added;
            await broker.SaveChangesAsync();
            return @object;
        }

        public IQueryable<T> SelectAll<T>() where T : class
        {
            var broker = new StorageBroker(this.configuration, webHostEnvironment);

            return broker.Set<T>();
        }

        public async ValueTask<T> SelectAsync<T>(params object[] objectsId) where T : class
        {
            var broker = new StorageBroker(this.configuration, webHostEnvironment);

            return await broker.FindAsync<T>(objectsId);
        }

        public async ValueTask<T> UpdateAsync<T>(T @object)
        {
            var broker = new StorageBroker(this.configuration, webHostEnvironment);
            broker.Entry(@object).State = EntityState.Modified;
            await broker.SaveChangesAsync();

            return @object;
        }

        public async ValueTask<T> DeleteAsync<T>(T @object)
        {
            var broker = new StorageBroker(this.configuration, webHostEnvironment);
            broker.Entry(@object).State = EntityState.Deleted;
            await broker.SaveChangesAsync();

            return @object;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = configuration.GetConnectionString(name: "DefaultConnection");
            optionsBuilder.UseSqlite(connectionString);
        }

        public override void Dispose() { }
    }
}
