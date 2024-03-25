using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using Domain.DataBaseContext;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColaboratorController : ControllerBase
    {
    private readonly ColaboratorContext _context;

    public ColaboratorController(ColaboratorContext context)
    {
        _context = context;
    }
    [HttpPost]
    public async Task<ActionResult<Colaborator>> CreateColaborator(string name, string email)
    {
    try
        {
            Colaborator collaborator = new Colaborator(name, email);
            _context.Colaborators.Add(collaborator);
            await _context.SaveChangesAsync();

            return Ok(collaborator);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message); // Return 500 if any unexpected error occurs
        }
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<Colaborator>> GetColaboratorByName(string name)
    {
        try
        {
            Colaborator collaborator = await _context.Colaborators.FirstOrDefaultAsync(c => c._strName.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (collaborator == null)
            {
                return NotFound(); // Return 404 if collaborator with given name is not found
            }

            // Return only name and email of the collaborator
            return Ok(new { Name = collaborator._strName, Email = collaborator._strEmail });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message); // Return 500 if any unexpected error occurs
        }
    }

    [HttpPut("{name}")]
    public async Task<IActionResult> UpdateColaborator(string name, string email)
    {
        try
        {
            var collaborator = await _context.Colaborators.FirstOrDefaultAsync(c => c._strName.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (collaborator == null)
            {
                return NotFound(); // Return 404 if collaborator with given name is not found
            }

            collaborator._strEmail = email;
            await _context.SaveChangesAsync();

            return NoContent(); // Return 204 No Content upon successful update
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message); // Return 500 if any unexpected error occurs
        }
    }

    [HttpDelete("{name}")]
    public async Task<IActionResult> DeleteColaborator(string name)
    {
        try
        {
            var collaborator = await _context.Colaborators.FirstOrDefaultAsync(c => c._strName.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (collaborator == null)
            {
                return NotFound(); // Return 404 if collaborator with given name is not found
            }

            _context.Colaborators.Remove(collaborator);
            await _context.SaveChangesAsync();

            return NoContent(); // Return 204 No Content upon successful deletion
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message); // Return 500 if any unexpected error occurs
        }
    }
    }
}
