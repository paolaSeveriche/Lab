using Lab.API.Data;
using Lab.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Lab.API.Controllers
{
    [ApiController]
    [Route("/api/physicochemicals")]
    public class PhysicochemicalsController : ControllerBase
    {
        private readonly DataContext _context;

        public PhysicochemicalsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _context.Physicochemilcal.ToListAsync());
        }

        //Método GET con parámetro

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {

            var country = await _context.Physicochemilcal
                .FirstOrDefaultAsync(x => x.Id == id);

            if (country is null)
            {
                return NotFound(); //404
            }

            return Ok(country);

        }


        //CREAR DATO
        [HttpPost]
        public async Task<ActionResult> Post(Physicochemical physicochemical)
        {
            _context.Add(physicochemical);
            await _context.SaveChangesAsync();
            return Ok(physicochemical);

            //_context.Add(physicochemical);
            //try
            //{

            //    await _context.SaveChangesAsync();
            //    return Ok(physicochemical);
            //}
            //catch (DbUpdateException dbUpdateException)
            //{
            //    //valida que los datos no este duplicados
            //    if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
            //    {
            //        return BadRequest("Ya existe una muestra con el mismo nombre.");
            //    }
            //    else
            //    {
            //        return BadRequest(dbUpdateException.InnerException.Message);
            //    }
            //}
            //catch (Exception exception)
            //{
            //    return BadRequest(exception.Message);
            //}
        }




        
        // Método DELETE-- Eliminar
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var afectedRows = await _context.Physicochemilcal
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();

            if (afectedRows == 0)
            {
                return NotFound(); //404
            }

            return NoContent(); //204
        }


    }
}
