using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IGenericDAL<T> where T : class 
    {

        void Add(T item);   
        void Remove(T item);
        void Update (T item);

        T GetT (int index);

        List<T> GetAll();

        List<T> GetbyFilter(Expression<Func<T, bool>> filter);


    }

}
