using Farmacia.COMMON;
using Farmacia.COMMON.Entidades;
using Farmacia.COMMON.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.DAL
{
    public class ProductoVendidoRepository
    {
        ManejdorDeArchivos _pVendido;
        List<ProductoVendido> _ProductoVendido;
        string nombrearchivo;
        public ProductoVendidoRepository()
        {
            nombrearchivo = DateTime.Now.ToString("dd.MM.yy[hh][mm][ss]");
            _pVendido = new ManejdorDeArchivos($"Venta.{nombrearchivo}.txt");
            _ProductoVendido = new List<ProductoVendido>();
        }
        //($"C:\\Ventas{Archivo}.txt")

        //Create=Crear,Agregar,Insertar
        public bool Crear(ProductoVendido ProductoVendido)
        {
            _ProductoVendido.Add(ProductoVendido);
            bool resultado = ActualizarArchivo();
            _ProductoVendido = LeerProductoVendido();
            return resultado;
        }

        //Read=Leer
        public List<ProductoVendido> LeerProductoVendido()
        {
            string contenido = _pVendido.Leer();
            List<ProductoVendido> resultado = new List<ProductoVendido>();
            string[] lineas = contenido.Split('\n');
            for (int i = 0; i < lineas.Length; i++)
            {
                if (lineas[i].Length > 3)
                {
                    ProductoVendido _pv = new ProductoVendido();
                    string[] datos = lineas[i].Split('|');
                    _pv.CantidaDeProductos = int.Parse(datos[0]);
                    _pv.Nombre = datos[1];
                    _pv.PrecioVenta = float.Parse(datos[2]);
                    _pv.TotalAPagar = float.Parse(datos[3]);
                    resultado.Add(_pv);
                }
            }
            _ProductoVendido = resultado;
            return resultado;
        }

        //Update=Actualizar
        public bool Actualizar(ProductoVendido original, ProductoVendido modificado)
        {
            ProductoVendido temp = new ProductoVendido();
            foreach (var item in _ProductoVendido)
            {
                if (item.Nombre==original.Nombre)
                {
                    temp = item;
                    break;
                }
            }
            temp.CantidaDeProductos = modificado.CantidaDeProductos;
            temp.Nombre = modificado.Nombre;
            temp.PrecioVenta = modificado.PrecioVenta;
            temp.TotalAPagar = modificado.TotalAPagar;
            bool resultado = ActualizarArchivo();
            _ProductoVendido = LeerProductoVendido();
            return resultado;
        }

        //Delete=Eliminar
        public bool Eliminar(ProductoVendido original)
        {
            ProductoVendido temp = new ProductoVendido();
            foreach (var item in _ProductoVendido)
            {

                if ((item.Nombre == original.Nombre && item.TotalAPagar == original.TotalAPagar))
                {
                    temp = item;
                    break;
                }
            }
            _ProductoVendido.Remove(temp);
            bool resultado = ActualizarArchivo();
            _ProductoVendido = LeerProductoVendido();
            return resultado;
        }

        private bool ActualizarArchivo()
        {
            string contenido = "";
            foreach (var item in _ProductoVendido)
            {
                contenido += string.Format("{0}|{1}|{2}|{3}\n", item.CantidaDeProductos, item.Nombre, item.PrecioVenta, item.TotalAPagar);
            }
            return _pVendido.Guardar(contenido);
        }
    }
}