using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implenment
{
    public class ExcetionHanler : IExcetionHanler
    {
        public async Task<int> TestExcetionHanler()
        {
            var a = int.Parse("");
            return a;
        }
    }
}
