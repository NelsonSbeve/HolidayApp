using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.DataBaseContext;
using Microsoft.EntityFrameworkCore;

namespace Domain.Service
{
    public class ColaboratorService
    {
        private readonly ColaboratorContext _context;

        public ColaboratorService(ColaboratorContext context)
        {
            _context = context;
        }

        public async Task<IColaborator> GetColaboratorById(int id)
        {
            // Retrieve the Colaborator by id from the database
            return await _context.Colaborators.FirstOrDefaultAsync(c => c.Id == id);
        }
    }

}