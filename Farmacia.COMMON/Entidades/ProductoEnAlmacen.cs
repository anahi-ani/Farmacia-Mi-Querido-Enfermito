
using System;
using System.Collections.Generic;
using System.Text;

namespace Farmacia.COMMON.Entidades
{
   public class ProductoEnAlmacen:Producto
    {
        public string Descripcion { get; set; }
        public float PrecioCompra { get; set; }
        public string Presentacion { get; set; }
        public Categoria _Categoria { get; set; }
        public override string ToString()
        {
            return string.Format("{0}", Nombre);
        }
    }
}
