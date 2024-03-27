using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Domain.DataBaseContext;

public class ColaboratorContext : DbContext
{
    public ColaboratorContext(DbContextOptions<ColaboratorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Colaborator> Colaborators { get; set; } = null!;
}