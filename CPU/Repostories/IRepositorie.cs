using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Part_2_Lesson_6.CPU.Repostories
{
   public  interface IRepositorie<T> where T:class
    {
        IList<T> GetAll();
        T GEtById(int id);
        void  Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
