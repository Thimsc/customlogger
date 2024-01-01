using DevOnLogger.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DevOnLogger
{

    /// <summary>
    /// ApplicationDBContext: Entityframework database contet class
    /// </summary>
    public class ApplicationDBContext: DbContext
    {
        public DbSet<LogTable> LogTable { get; set; }

        public ApplicationDBContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<LogTable>().HasIndex(c => c.Id);
        }
    }

}
