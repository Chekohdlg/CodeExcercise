using CodeExercise.DataAcccess.Data;
using CodeExercise.Services.Interfaces;
using CodeExercise_Model.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeExercise.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }
    }
}
