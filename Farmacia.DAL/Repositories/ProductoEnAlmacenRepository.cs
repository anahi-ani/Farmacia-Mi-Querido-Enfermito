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
    public class ProductoEnAlmacenRepository
    {
        ManejdorDeArchivos archivo;
        List<ProductoEnAlmacen> _producto;
        public ProductoEnAlmacenRepository()
        {
            archivo = new ManejdorDeArchivos("Producto.txt");
            _producto = new List<ProductoEnAlmacen>();
        }

        //Create=Crear,Agregar,Insertar
        public bool Crear(ProductoEnAlmacen entidad)
        {
            _producto = LeerProductoAlmacenado();
            _producto.Add(entidad);
            bool resultado = ActualizarArchivo();
            _producto = LeerProductoAlmacenado();
            return resultado;
        }

        //Read=Leer
        public List<ProductoEnAlmacen> LeerProductoAlmacenado()
        {
            string contenido = archivo.Leer();
            string[] lineas = contenido.Split('\n');
            List<ProductoEnAlmacen> resultado = new List<ProductoEnAlmacen>();
            for (int i = 0; i < lineas.Length; i++)
            {
                if (lineas[i].Length > 3)
                {
                    ProductoEnAlmacen _p = new ProductoEnAlmacen();
                    Categoria _c = new Categoria();
                    string[] datos = lineas[i].Split('|');
                    _p.Nombre = datos[0];
                    _p.Descripcion = datos[1];
                    _p.PrecioCompra = float.Parse(datos[2]);
                    _p.PrecioVenta = float.Parse(datos[3]);
                    _p.Presentacion = datos[4];
                    _c.Nombre = datos[5];
                    _p._Categoria = _c;
                    resultado.Add(_p);
                }
            }
            _producto = resultado;
            return resultado;

        }

        //Update=Actualizar
        public bool Actualizar(ProductoEnAlmacen original, ProductoEnAlmacen modificado)
        {
            ProductoEnAlmacen temp = new ProductoEnAlmacen();
            foreach (var item in _producto)
            {
                if (item.Nombre == original.Nombre && item.Descripcion == original.Descripcion)
                {
                    temp = item;
                    break;
                }
            }
            temp.Nombre = modificado.Nombre;
            temp.Descripcion = modificado.Descripcion;
            temp.PrecioCompra = modificado.PrecioCompra;
            temp.PrecioVenta = modificado.PrecioVenta;
            temp.Presentacion = modificado.Presentacion;
            temp._Categoria = modificado._Categoria;
            bool resultado = ActualizarArchivo();
            _producto = LeerProductoAlmacenado();
            return resultado;
        }

        //Delete=Eliminar
        public bool Eliminar(ProductoEnAlmacen original)
        {
            ProductoEnAlmacen temp = new ProductoEnAlmacen();
            foreach (var item in _producto)
            {

                if (item.Nombre == original.Nombre && item.Descripcion == original.Descripcion)
                {
                    temp = item;
                    break;
                }
            }
            _producto.Remove(temp);
            bool resultado = ActualizarArchivo();
            _producto = LeerProductoAlmacenado();
            return resultado;
        }

        private bool ActualizarArchivo()
        {
            string contenido = "";
            foreach (var item in _producto)
            {
                contenido += string.Format("{0}|{1}|{2}|{3}|{4}|{5}\n", item.Nombre, item.Descripcion, item.PrecioCompra, item.PrecioVenta, item.Presentacion, item._Categoria.Nombre);
            }
            return archivo.Guardar(contenido);
        }
    }

}