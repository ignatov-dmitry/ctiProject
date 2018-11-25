using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logics
{
    interface ILogic<T>
    {
        void Remove(int id);
        void Add(T name);
        void Edit(T name);
        T ViewsObj(int id);
        List<T> ViewsListObj();
    }
}
