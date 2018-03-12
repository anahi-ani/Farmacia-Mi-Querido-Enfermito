using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.COMMON
{
   public class Categoria:Objeto
    {
        public override string ToString()
        {
            return string.Format("{0}", Nombre);
        }
    }
}
