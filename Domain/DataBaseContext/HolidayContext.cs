using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Domain.DataBaseContext
{
    public class HolidayContext : DbContext
    {
        public HolidayContext(DbContextOptions<HolidayContext> options)
        : base(options)
        {
        }

    public virtual DbSet<Holiday> Holidays { get; set; } = null!;

    }
}