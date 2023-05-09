using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab.Shared.Entities;
using Lab.API.Data;
using Lab.API.Helpers;
using Lab.Shared.DTOs;


namespace Lab.API.Controllers
{
    [ApiController]
    [Route("/api/states")]
    public class StatesController : ControllerBase
    {
        private readonly DataContext _context;

        public StatesController(DataContext context)
        {
            _context = context;
        }


        [HttpGet] //Nuevo
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.States
                .Include(x => x.Cities) // RECORRIDO DE PAGINAS 
                .AsQueryable();


            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains
                (pagination.Filter.ToLower()));
            }

            //Filtra estados por Id de país
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }



            return Ok(await queryable
                .OrderBy(x => x.Name)
                .Paginate(pagination)
                .ToListAsync());
        }


        [HttpGet("totalPages")] // Contar numeros de paginas --> nuevo 
        public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.States.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }


            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }



        // Get FULL --> nuevo
        [HttpGet("full")]
        public async Task<ActionResult> GetFull()
        {
            return Ok(await _context.States
                .Include(x => x.Cities!)
                .ToListAsync());
        }




        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var state = await _context.States
                .Include(x => x.Cities)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (state == null)
            {
                return NotFound();
            }

            return Ok(state);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(State state)
        {
            try
            {
                _context.Add(state);
                await _context.SaveChangesAsync();
                return Ok(state);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un estado/departamento con el mismo nombre.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(State state)
        {
            try
            {
                _context.Update(state);
                await _context.SaveChangesAsync();
                return Ok(state);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un estado/departamento con el mismo nombre.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var state = await _context.States.FirstOrDefaultAsync(x => x.Id == id);
            if (state == null)
            {
                return NotFound();
            }

            _context.Remove(state);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

