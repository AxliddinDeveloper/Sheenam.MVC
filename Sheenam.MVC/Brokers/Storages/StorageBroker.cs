//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using EFxceptions;

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = configuration.GetConnectionString(name: "DefaultConnection");
            optionsBuilder.UseSqlite(connectionString);
        }

        public override void Dispose() { }
    }
}
