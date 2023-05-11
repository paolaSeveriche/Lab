using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab.API.Helpers;
using Lab.Shared.DTOs;
using Lab.API.Data;
using Lab.Shared.Entities;

namespace Lab.API.Controllers
{

    [ApiController]
    [Route("/api/microbiologicals")]
    public class MicrobiologicalController : ControllerBase
    {
        private readonly DataContext _context;


        public MicrobiologicalController(DataContext context)
        {
            _context = context;
        }


        //Método GET LIST
        //Paginación
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Microbiological
                /*.Include(x => x.)*/ // RECORRIDO DE PAGINAS DE 1 A 10
                .AsQueryable();


            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.ColiformCount.ToLower().Contains
                (pagination.Filter.ToLower()));
            }


            return Ok(await queryable
                .OrderBy(x => x.ColiformCount)
                .Paginate(pagination)
                .ToListAsync());
        }

        [HttpGet("totalPages")] // Contar numeros de paginas 
        public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Microbiological.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.ColiformCount.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }



        // Get FULL hace un join con estados y ciudades una union entre ellas 
        [HttpGet("full")]
        public async Task<ActionResult> GetFull()
        {
            return Ok(await _context.Microbiological
                .ToListAsync());
        }


        //´Método GET con parámetro

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {

            var microbiological = await _context.Microbiological
                .FirstOrDefaultAsync(x => x.Id == id);

            if (microbiological is null)
            {
                return NotFound(); //404
            }

            return Ok(microbiological);

        }

        // Método POST -- CREAR
        [HttpPost]
        public async Task<ActionResult> Post(Microbiological microbiological)
        {
            _context.Add(microbiological);
            try
            {

                await _context.SaveChangesAsync();
                return Ok(microbiological);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe una nuestra con el mismo nombre.");
                }
                else
                {
                    return BadRequest(dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        //Método PUT --- UPDATE

        [HttpPut]
        public async Task<ActionResult> Put(Microbiological microbiological)
        {
            _context.Update(microbiological);
            await _context.SaveChangesAsync();
            return Ok(microbiological);
        }



        // Método DELETE-- Eliminar
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var afectedRows = await _context.Microbiological
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
