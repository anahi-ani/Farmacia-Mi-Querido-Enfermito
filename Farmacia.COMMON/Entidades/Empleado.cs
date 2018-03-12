using System;
using System.Collections.Generic;
using System.Text;

namespace Farmacia.COMMON
{
    public class Empleado:Objeto
    {
        public string Contrasenia { get; set; }
        public string TipoEmpleado { get; set; }
        public override string ToString()
        {
            return string.Format("{0}|{1}|{2}", Nombre,TipoEmpleado,Contrasenia);
        }
    }
}
