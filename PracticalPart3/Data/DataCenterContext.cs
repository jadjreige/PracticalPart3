using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PracticalPart3.Models;

namespace PracticalPart3.Data
{
    public class DataCenterContext : DbContext
    {
        public DataCenterContext (DbContextOptions<DataCenterContext> options)
            : base(options)
        {
        }

        public static DataCenterContext CreateContext()
        {
            var connection = "Server=(localdb)\\mssqllocaldb;Database=DataCenterContext-6fc4806a-3279-44a2-96f4-e45810052184;Trusted_Connection=True;";
            var optionsBuilder = new DbContextOptionsBuilder<DataCenterContext>();
            optionsBuilder.UseSqlServer(connection);

            return new DataCenterContext(optionsBuilder.Options);
        }

        public DbSet<PracticalPart3.Models.DataCenter> DataCenter { get; set; } = default!;

    }
}
