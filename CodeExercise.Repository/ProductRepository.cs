using CodeExercise.DataAcccess.Data;
using CodeExercise_Model.Interfaces;
using CodeExercise_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeExercise.Repository
{
    

    public class ProductRepository : GenericRepositry<Product>, IProduct
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
