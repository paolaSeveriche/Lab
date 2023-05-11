using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab.API.Helpers;
using Lab.Shared.DTOs;
using Lab.API.Data;
using Lab.Shared.Entities;

namespace Lab.API.Controllers
{

    [ApiController]
    [Route("/api/dataSamples")]
    public class DataSamplesController : ControllerBase
    {
        private readonly DataContext _context;


        public DataSamplesController(DataContext context)
        {
            _context = context;
        }


        //Método GET LIST
        //Paginación
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.DataSample
                /*.Include(x => x.)*/ // RECORRIDO DE PAGINAS DE 1 A 10
                .AsQueryable();


            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.CollectorDate.ToLower().Contains
                (pagination.Filter.ToLower()));
            }


            return Ok(await queryable
                .OrderBy(x => x.CollectorDate)
                .Paginate(pagination)
                .ToListAsync());
        }

        [HttpGet("totalPages")] // Contar numeros de paginas 
        public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Physicochemical.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.CollectorDate.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }



        // Get FULL hace un join con estados y ciudades una union entre ellas 
        [HttpGet("full")]
        public async Task<ActionResult> GetFull()
        {
            return Ok(await _context.DataSample
                .ToListAsync());
        }


        //´Método GET con parámetro

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {

            var dataSample = await _context.DataSample
                .FirstOrDefaultAsync(x => x.Id == id);

            if (dataSample is null)
            {
                return NotFound(); //404
            }

            return Ok(dataSample);

        }

        // Método POST -- CREAR
        [HttpPost]
        public async Task<ActionResult> Post(DataSample dataSample)
        {
            _context.Add(dataSample);
            try
            {

                await _context.SaveChangesAsync();
                return Ok(dataSample);
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
        public async Task<ActionResult> Put(DataSample dataSample)
        {
            _context.Update(dataSample);
            await _context.SaveChangesAsync();
            return Ok(dataSample);
        }



        // Método DELETE-- Eliminar
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var afectedRows = await _context.Physicochemical
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

