using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using CarPark.Api.Infrastructure.EF_Core.DBContext;

namespace CarPark.Api.xUnitTest.DBContext
{
    class InMemoryContext
    {
        CarParkDbContext Context;

        public InMemoryContext()
        {
            try
            {
                var connection = new SQLiteConnection("Datasource=:memory:");
                var options = new DbContextOptionsBuilder<CarParkDbContext>().UseSqlite(connection).Options;
                Context = new CarParkDbContext(options);


                using (Context)
                {
                    Context.Database.EnsureCreated();
                }

            }
            catch (Exception)
            {

                throw;
            }



        }

        public CarParkDbContext GetContext()
        {
            return Context;
        }
    }
}
