using DbProjekt.src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbProjekt.DAO
{
    /// <summary>
    /// Interface for base methods for dao classes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDAO<T> where T : IBaseClass
    {
        T? GetByID(int id);
        IEnumerable<T> GetAll();
        void Save(T element);
        void Delete(T element);
    }
}
