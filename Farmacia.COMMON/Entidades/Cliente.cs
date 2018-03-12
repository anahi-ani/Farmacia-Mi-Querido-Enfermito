using System;
using System.Collections.Generic;
using System.Text;

namespace Farmacia.COMMON
{
    public class Cliente:Objeto
    {
        public string Direccion { get; set; }
        public string Rfc { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public override string ToString()
        {
            return string.Format("{0}", Nombre);
        }
    }
}
