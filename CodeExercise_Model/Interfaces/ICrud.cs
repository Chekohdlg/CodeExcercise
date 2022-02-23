using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeExercise_Model.Interfaces
{
    public interface ICrud<T> where T : class
    {
        Task<List<T>> GetList();
        Task<T> GetById(int id);
        Task<T> DeleteById(int id);

        Task<T> Update(T entity);
    }
}
