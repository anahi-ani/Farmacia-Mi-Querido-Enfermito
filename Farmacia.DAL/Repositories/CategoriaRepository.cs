using Farmacia.COMMON;
using Farmacia.COMMON.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.DAL
{
    public class CategoriaRepository
    {
        ManejdorDeArchivos archivo;
        List<Categoria> _Categoria;
        public CategoriaRepository()
        {
            archivo = new ManejdorDeArchivos("Categorias.txt");
            _Categoria = new List<Categoria>();
        }

        //Create=Crear,Agregar,Insertar
        public bool Crear(Categoria Entidad)
        {
            _Categoria = LeerCategorias();
            _Categoria.Add(Entidad);
            bool _resultado = ActualizarArchivo();
            _Categoria = LeerCategorias();
            return _resultado;
        }

        private bool ActualizarArchivo()
        {
            string contenido = "";
            foreach (var item in _Categoria)
            {
                contenido += string.Format("{0}\n", item.Nombre);
            }
            return archivo.Guardar(contenido);
        }

        //Read=Leer
        public List<Categoria> LeerCategorias()
        {
            string contenido = archivo.Leer();
            string[] lineas = contenido.Split('\n');
            List<Categoria> resultado = new List<Categoria>();
            for (int i = 0; i < lineas.Length - 1; i++)
            {
                if (lineas[i].Length > 3)
                {
                    Categoria c = new Categoria();
                    c.Nombre = lineas[i];
                    resultado.Add(c);
                }
            }
            _Categoria = resultado;
            return resultado;

        }
        //Update=Actualizar
        public bool Actualizar(Categoria original, Categoria modificado)
        {
            Categoria temp = new Categoria();
            foreach (var item in _Categoria)
            {
                if (item.Nombre == original.Nombre)
                {
                    temp = item;
                    break;
                }
            }
            temp.Nombre = modificado.Nombre;
            bool resultado = ActualizarArchivo();
            _Categoria = LeerCategorias();
            return resultado;
        }
        //Delete=Eliminar
        public bool Eliminar(Categoria original)
        {
            Categoria temp = new Categoria();
            foreach (var item in _Categoria)
            {

                if (item.Nombre == original.Nombre)
                {
                    temp = item;
                    break;
                }
            }
            _Categoria.Remove(temp);
            bool _resultado = ActualizarArchivo();
            _Categoria = LeerCategorias();
            return _resultado;
        }
    }
}
