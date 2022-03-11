using CodeExercise.DataAcccess.Data;
using CodeExercise_Model.Interfaces;
using CodeExercise_Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using CodeExercise.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodeExcercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericService<Product> productRepository;
       
        //private readonly IMapper mapper;
        public ProductsController(
            IGenericService<Product>  productRespository       
            )
        {
            productRepository = productRespository;
           
            //this.mapper = mapper;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await productRepository.GetAllAsync());
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("The id must be grater than zero");
                }

                var product = await productRepository.GetAsync(id);

                if (product is null)
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

                await productRepository.AddAsync(product);
                
                return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromRoute] int id, [FromBody] Product product)
        {
            try
            {
                if (id != product.Id)
                {
                    return BadRequest("The id and the product id is not the same");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await productRepository.UpdateAsync(product);

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

               var productDeleted = await productRepository.DeleteAsync(id);
            
               return Ok(productDeleted);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


    }
}
