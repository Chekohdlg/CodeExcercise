using CodeExercise_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeExercise.Services.Interfaces
{
    public interface IProductService
    {
        Task<IList<Product>> GetProducts();
    }
}
