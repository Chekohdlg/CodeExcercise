using CodeExercise.DataAcccess.Data;
using CodeExercise_Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodeExcercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public ProductsController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<ActionResult>  Get()
        {
            try
            {
                return Ok(await _db.Products.ToListAsync());
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
           
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct(int id)
        {
            try
            {
                if (id<=0)
                {
                    return BadRequest("The id must be grater than zero");
                }
                
                var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
                
                if(product is null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _db.Products.Add(product);
                await _db.SaveChangesAsync();

                return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);

            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromRoute]int id, [FromBody] Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _db.Products.Update(product);

                await _db.SaveChangesAsync();

                return Ok(product);

            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("The id must be grater than zero");
                }

                var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product is null)
                {
                    return BadRequest();
                }

                _db.Products.Remove(product);
                await _db.SaveChangesAsync();

                return Ok(product);

            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
