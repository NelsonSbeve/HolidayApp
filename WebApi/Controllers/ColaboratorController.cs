using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using Domain.DataBaseContext;
using Newtonsoft.Json;


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
    private bool ColaboratorExists(int id)
    {
        return _context.Colaborators.Any(e => e.Id == id);
    }
    // [HttpPost]
    // public async Task<ActionResult<Colaborator>> CreateColaborator(string name, string email)
    // {
    // try
    //     {
    //         Colaborator collaborator = new Colaborator(name, email);
    //         _context.Colaborators.Add(collaborator);
    //         await _context.SaveChangesAsync();

    //         return Ok(collaborator);
    //     }
    //     catch (ArgumentException ex)
    //     {
    //         return BadRequest(ex.Message);
    //     }
    //     catch (Exception ex)
    //     {
    //         return StatusCode(500, ex.Message); // Return 500 if any unexpected error occurs
    //     }
    // }

    [HttpPost]
    public async Task<ActionResult<Colaborator>> CreateColaboratorWithObject([FromBody] Colaborator colaborator)
    {
        try
        {
            if (string.IsNullOrEmpty(colaborator._strName) || string.IsNullOrEmpty(colaborator._strEmail))
            {
                return BadRequest("Name and email must not be empty.");
            }
            if (!colaborator.IsValidEmailAddres(colaborator._strEmail))
            {
                return BadRequest("Invalid email format.");
            }
            
            _context.Colaborators.Add(colaborator);
            await _context.SaveChangesAsync();

            return Ok(colaborator);
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

    // [HttpGet("{name}")]
    // public async Task<ActionResult<Colaborator>> GetColaboratorByName(string name)
    // {
    //     try
    //     {
    //         Colaborator collaborator = await _context.Colaborators.FirstOrDefaultAsync(c => c._strName.Equals(name, StringComparison.OrdinalIgnoreCase));

    //         if (collaborator == null)
    //         {
    //             return NotFound(); // Return 404 if collaborator with given name is not found
    //         }

    //         // Return only name and email of the collaborator
    //         return Ok(new { Name = collaborator._strName, Email = collaborator._strEmail });
    //     }
    //     catch (Exception ex)
    //     {
    //         return StatusCode(500, ex.Message); // Return 500 if any unexpected error occurs
    //     }
    // }
    [HttpGet("{id}")]
    public async Task<ActionResult<Colaborator>> GetColaboratorById(int id)
    {
        try
        {
            var collaborator = await _context.Colaborators.FindAsync(id);

            if (collaborator == null)
            {
                return NotFound(); // Return 404 if collaborator with given ID is not found
            }

            // Return collaborator with 200 OK status code
            return Ok(collaborator);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message); // Return 500 if any unexpected error occurs
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Colaborator>>> GetAllColaborators()
    {
        try
        {
            var collaborators = await _context.Colaborators.ToListAsync();

            if (collaborators == null || collaborators.Count == 0)
            {
                return NotFound(); // Return 404 if no collaborators are found
            }

            // Return collaborators with 200 OK status code
            return Ok(collaborators);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message); // Return 500 if any unexpected error occurs
        }
    }



    [HttpPut("{id}")]
    public async Task<IActionResult> PutColaborator(int id, string name, string email)
    {
        var existingColaborator = await _context.Colaborators.FindAsync(id);
        if (existingColaborator == null)
        {
            return NotFound();
        }

        var colaboratorInstance = new Colaborator(name, email);
        if (!colaboratorInstance.isValidParameters(name, email))
        {
            return BadRequest("Invalid format.");
        }

        existingColaborator._strName = name;
        existingColaborator._strEmail = email;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ColaboratorExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return Ok(colaboratorInstance);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteColaborator(int id)
    {
        var colaborator = await _context.Colaborators.FindAsync(id);
        if (colaborator == null)
        {
            return NotFound();
        }

        _context.Colaborators.Remove(colaborator);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    
    }
}
