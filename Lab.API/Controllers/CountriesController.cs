using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab.API.Helpers;
using Lab.Shared.DTOs;
using Lab.API.Data;
using Lab.Shared.Entities;

namespace Lab.API.Controllers
{

    [ApiController]
    [Route("/api/countries")]
    public class CountriesController : ControllerBase
    {
        private readonly DataContext _context;


        public CountriesController(DataContext context)
        {
            _context = context;
        }


        //Método GET LIST

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Countries
                .Include(x => x.States) // RECORRIDO DE PAGINAS DE 1 A 10
                .AsQueryable();


            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains
                (pagination.Filter.ToLower()));
            }


            return Ok(await queryable
                .OrderBy(x => x.Name)
                .Paginate(pagination)
                .ToListAsync());
        }

        [HttpGet("totalPages")] // Contar numeros de paginas 
        public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Countries.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }



        // Get FULL hace un join con estados y ciudades una union entre ellas 
        [HttpGet("full")]
        public async Task<ActionResult> GetFull()
        {
            return Ok(await _context.Countries
                .Include(x => x.States!)
                .ThenInclude(x => x.Cities)
                .ToListAsync());
        }


        //´Método GET con parámetro

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {

            var country = await _context.Countries
                .Include(x => x.States)
                .ThenInclude(x => x.Cities)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (country is null)
            {
                return NotFound(); //404
            }

            return Ok(country);

        }

        // Método POST -- CREAR
        [HttpPost]
        public async Task<ActionResult> Post(Country country)
        {
            _context.Add(country);
            try
            {

                await _context.SaveChangesAsync();
                return Ok(country);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un país con el mismo nombre.");
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
        public async Task<ActionResult> Put(Country country)
        {
            _context.Update(country);
            await _context.SaveChangesAsync();
            return Ok(country);
        }



        // Método DELETE-- Eliminar
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var afectedRows = await _context.Countries
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();

            if (afectedRows == 0)
            {
                return NotFound(); //404
            }

            return NoContent(); //204
        }

        ///*1*/[HttpPost]
        //public async Task<ActionResult> Post(Country country)
        //{
        //    _context.Add(country);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //        return Ok(country);
        //    }
        //    catch (DbUpdateException dbUpdateException)
        //    {
        //        if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
        //        {
        //            return BadRequest("Ya existe un país con el mismo nombre.");
        //        }
        //        else
        //        {
        //            return BadRequest(dbUpdateException.InnerException.Message);
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        return BadRequest(exception.Message);
        //    }
        //}

        ///*LISTA*/

        ///*2*/[HttpGet]
        //[HttpGet]
        //public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        //{
        //    var queryable = _context.Countries
        //        .Include(x => x.States)
        //        .AsQueryable();

        //    //if (!string.IsNullOrWhiteSpace(pagination.Filter))
        //    //{
        //    //    queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
        //    //}

        //    return Ok(await queryable
        //        .OrderBy(x => x.Name)
        //        .Paginate(pagination)
        //        .ToListAsync());
        //}


        ///*3*/
        //[HttpGet("totalPages")]
        //public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)
        //{
        //    var queryable = _context.Countries.AsQueryable();

        //    //if (!string.IsNullOrWhiteSpace(pagination.Filter))
        //    //{
        //    //    queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
        //    //}

        //    double count = await queryable.CountAsync();
        //    double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
        //    return Ok(totalPages);
        //}


        /////*4*/[HttpGet("full")]
        ////public async Task<ActionResult> GetFull()
        ////{
        ////    return Ok(await _context.Countries
        ////        .Include(x => x.States!)
        ////        .ThenInclude(x => x.Cities)
        ////        .ToListAsync());
        ////}

        ////Búsqueda por parámetro
        ///*5*/
        //[HttpGet("{id:int}")]  ///api/countries/1
        //public async Task<ActionResult> Get(int id)
        //{
        //    var country = await _context.Countries
        //      .Include(x => x.States!)
        //      .ThenInclude(x => x.Cities!)
        //        .FirstOrDefaultAsync(x => x.Id == id);
        //    if (country is null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(country);
        //}
        //// Actualización

        ///*6*/[HttpPut]
        //public async Task<ActionResult> Put(Country country)
        //{

        //    try
        //    {

        //        _context.Update(country);
        //        await _context.SaveChangesAsync();


        //        return Ok(country);

        //    }
        //    catch (DbUpdateException dbUpdateException)
        //    {
        //        if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
        //        {
        //            return BadRequest("Ya existe un registro con el mismo nombre.");
        //        }
        //        else
        //        {
        //            return BadRequest(dbUpdateException.InnerException.Message);
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        return BadRequest(exception.Message);
        //    }
        //}
        ///*7*/
        //[HttpDelete("{id:int}")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    var afectedRows = await _context.Countries
        //        .Where(x => x.Id == id)
        //        .ExecuteDeleteAsync();

        //    if (afectedRows == 0)
        //    {
        //        return NotFound();
        //    }

        //    return NoContent();
        //}


    }
}
