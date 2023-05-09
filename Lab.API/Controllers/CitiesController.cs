using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab.Shared.Entities;
using Lab.API.Helpers;
using Lab.API.Data;
using Lab.Shared.DTOs;


namespace Lab.API.Controllers
{
    ////indica que esta protegido con token 
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("/api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly DataContext _context;

        public CitiesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Cities // RECORRIDO DE PAGINAS DE 1 A 10
                .AsQueryable();


            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains
                (pagination.Filter.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(pagination.StateId))
            {
                queryable = queryable.Where(x => x.StateId == int.Parse(pagination.StateId));
            }

            return Ok(await queryable
                .OrderBy(x => x.Name)
                .Paginate(pagination)
                .ToListAsync());
        }



        [HttpGet("totalPages")] // Contar numeros de paginas 
        public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Cities.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(pagination.StateId))
            {
                queryable = queryable.Where(x => x.StateId == int.Parse(pagination.StateId));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }



        // Get FULL hace 
        [HttpGet("full")]
        public async Task<ActionResult> GetFull()
        {
            return Ok(await _context.Cities
                .ToListAsync());
        }



        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var city = await _context.Cities
                .FirstOrDefaultAsync(x => x.Id == id);
            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(City city)
        {
            try
            {
                _context.Add(city);
                await _context.SaveChangesAsync();
                return Ok(city);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe una ciudad con el mismo nombre.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(City city)
        {
            try
            {
                _context.Update(city);
                await _context.SaveChangesAsync();
                return Ok(city);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe una ciudad con el mismo nombre.");
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
            var city = await _context.Cities.FirstOrDefaultAsync(x => x.Id == id);
            if (city == null)
            {
                return NotFound();
            }

            _context.Remove(city);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}




//using Lab.API.Data;
//using Lab.Shared.DTOs;
//using Lab.Shared.Entities;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Lab.API.Helpers;

//namespace Lab.API.Controllers
//{
//    [ApiController]
//    [Route("/api/cities")]
//    public class CitiesController : ControllerBase
//    {
//        private readonly DataContext _context;

//        public CitiesController(DataContext context)
//        {
//            _context = context;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAsync()
//        {
//            return Ok(await _context.Cities.ToListAsync());
//        }

//        //[HttpGet]
//        //public async Task<ActionResult> Get([FromQuery] PaginationDTO pagination)
//        //{
//        //    var queryable = _context.Cities
//        //        .Where(x => x.State!.Id == pagination.Id)
//        //        .AsQueryable();

//        //    return Ok(await queryable
//        //        .OrderBy(x => x.Name)
//        //        .Paginate(pagination)
//        //        .ToListAsync());
//        //}


//        //[HttpGet("totalPages")]
//        //public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)
//        //{
//        //    var queryable = _context.Cities
//        //        .Where(x => x.State!.Id == pagination.Id)
//        //        .AsQueryable();

//        //    double count = await queryable.CountAsync();
//        //    double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
//        //    return Ok(totalPages);
//        //}



//        [HttpGet("{id:int}")]
//        public async Task<IActionResult> GetAsync(int id)
//        {
//            var city = await _context.Cities.FirstOrDefaultAsync(x => x.Id == id);
//            if (city == null)
//            {
//                return NotFound();
//            }

//            return Ok(city);
//        }

//        [HttpPost]
//        public async Task<ActionResult> PostAsync(City city)
//        {
//            try
//            {
//                _context.Add(city);
//                await _context.SaveChangesAsync();
//                return Ok(city);
//            }
//            catch (DbUpdateException dbUpdateException)
//            {
//                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
//                {
//                    return BadRequest("Ya existe una ciudad con el mismo nombre.");
//                }

//                return BadRequest(dbUpdateException.Message);
//            }
//            catch (Exception exception)
//            {
//                return BadRequest(exception.Message);
//            }
//        }

//        [HttpPut]
//        public async Task<ActionResult> PutAsync(City city)
//        {
//            try
//            {
//                _context.Update(city);
//                await _context.SaveChangesAsync();
//                return Ok(city);
//            }
//            catch (DbUpdateException dbUpdateException)
//            {
//                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
//                {
//                    return BadRequest("Ya existe una ciudad con el mismo nombre.");
//                }

//                return BadRequest(dbUpdateException.Message);
//            }
//            catch (Exception exception)
//            {
//                return BadRequest(exception.Message);
//            }
//        }

//        [HttpDelete("{id:int}")]
//        public async Task<IActionResult> DeleteAsync(int id)
//        {
//            var city = await _context.Cities.FirstOrDefaultAsync(x => x.Id == id);
//            if (city == null)
//            {
//                return NotFound();
//            }

//            _context.Remove(city);
//            await _context.SaveChangesAsync();
//            return NoContent();
//        }
//    }
//}

