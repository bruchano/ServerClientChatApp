using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pie.EntityFramework
{
    public class PieDbContextFactory : IDesignTimeDbContextFactory<PieDbContext>
    {
        public PieDbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<PieDbContext>();
            options.UseSqlServer("Server=A\\SQLEXPRESS;Database=PieDB;Trusted_Connection=True;");

            return new PieDbContext(options.Options);
        }
    }
}
