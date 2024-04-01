using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    public class HolidayController : Controller
    {
    private readonly List<Holiday> _holidays = new List<Holiday>(); // You may replace this with a database context if needed~
    private readonly ColaboratorService _colaboratorService;
        public HolidayController(ColaboratorService colaboratorService)
    {
        _colaboratorService = colaboratorService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateHolidayAsync([FromBody] int id)
    {
        try
        {
            var colaborator = await _colaboratorService.GetColaboratorById(id);
            if (colaborator == null)
            {
                return NotFound($"Colaborator with id {id} not found.");
            }
            
            var holiday = new Holiday(colaborator);
            _holidays.Add(holiday);
            return Ok(holiday);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    }
}