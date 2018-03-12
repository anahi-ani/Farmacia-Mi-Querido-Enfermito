using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.COMMON.Entidades
{
    public class Ticket
    {
        public string Empleado { get; set; }
        public string Cliente { get; set; }
        public List<ProductoVendido> ProductosVendidos { get; set; }
        public float TotalVenta { get; set; }
        public float PagoEfectivo { get; set; }
        public float Cambio { get; set; }
        public float IVA { get; set; }
    }
}
