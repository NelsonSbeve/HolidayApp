using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Domain.DataBaseContext
{
public class TestColaboratorContext : ColaboratorContext
{
    public override DbSet<Colaborator> Colaborators { get; set; }

    public TestColaboratorContext(DbContextOptions<ColaboratorContext> options) : base(options)
    {
    }
}

}