using System;
using System.Collections.Generic;
using System.Text;

namespace Farmacia.COMMON.Entidades
{
    public class ProductoVendido:Producto
    {
        public int CantidaDeProductos { get; set; }
        public float TotalAPagar { get; set; }
        public override string ToString()
        {
            return string.Format("{0}|{1}|{2}|{3}", CantidaDeProductos,Nombre,PrecioVenta,TotalAPagar);
        }
    }
}
