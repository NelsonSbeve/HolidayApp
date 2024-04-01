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
        InitializeData();
    }

    public virtual DbSet<Colaborator> Colaborators { get; set; } = null!;
    private void InitializeData()
        {
            // Check if there are any collaborators in the database
            if (!Colaborators.Any())
            {
                // Add a sample collaborator
                var sampleColaborator = new Colaborator
                {
                    Id = 1,
                    _strName = "Sample Colaborator",
                    _strEmail = "sample@example.com"
                };
                Colaborators.Add(sampleColaborator);
                SaveChanges();
            }
        }

}