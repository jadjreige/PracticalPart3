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

        public DbSet<PracticalPart3.Models.DataCenter> DataCenter { get; set; } = default!;
    }
}
