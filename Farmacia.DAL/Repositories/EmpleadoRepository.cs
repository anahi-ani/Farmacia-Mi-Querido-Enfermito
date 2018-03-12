using Farmacia.COMMON;
using Farmacia.COMMON.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.DAL
{
    public class EmpleadoRepository
    {
        ManejdorDeArchivos archivo;
        List<Empleado> _empleado;
        public EmpleadoRepository()
        {
            archivo = new ManejdorDeArchivos("Empleado2.txt");
            _empleado = new List<Empleado>();
        }

        //Create=Crear,Agregar,Insertar
        public bool Crear(Empleado entidad)
        {
            _empleado = LeerEmpleado();
            _empleado.Add(entidad);
            bool resultado = ActualizarArchivo();
            _empleado = LeerEmpleado();
            return resultado;
        }

        //Read=Leer
        public List<Empleado> LeerEmpleado()
        {
            string contenido = archivo.Leer();
            if (contenido!=null)
            {
                string[] lineas = contenido.Split('\n');
                List<Empleado> resultado = new List<Empleado>();
                for (int i = 0; i < lineas.Length; i++)
                {
                    if (lineas[i].Length > 3)
                    {
                        Empleado e = new Empleado();
                        string[] datos = lineas[i].Split('|');
                        e.Nombre = datos[0];
                        e.Contrasenia = datos[1];
                        e.TipoEmpleado = datos[2];
                        resultado.Add(e);
                    }

                }
                _empleado = resultado;
                return resultado; 
            }
            else
            {
                return null;
            }
        }

        //Update=Actualizar
        public bool Actualizar(Empleado original, Empleado modificado)
        {
            Empleado temp = new Empleado();
            foreach (var item in _empleado)
            {
                if (item.Nombre == original.Nombre && item.Contrasenia == original.Contrasenia)
                {
                    temp = item;
                    break;
                }
            }
            temp.Nombre = modificado.Nombre;
            temp.Contrasenia = modificado.Contrasenia;
            temp.TipoEmpleado = modificado.TipoEmpleado;
            bool resultado = ActualizarArchivo();
            _empleado = LeerEmpleado();
            return resultado;
        }

        //Delete=Eliminar
        public bool Eliminar(Empleado original)
        {
            Empleado temp = new Empleado(); ;
            foreach (var item in _empleado)
            {

                if (item.Nombre == original.Nombre && item.Contrasenia == original.Contrasenia)
                {
                    temp = item;
                    break;
                }
            }
            _empleado.Remove(temp);
            bool resultado = ActualizarArchivo();
            _empleado = LeerEmpleado();
            return resultado;
        }

        private bool ActualizarArchivo()
        {
            string contenido = "";
            foreach (var item in _empleado)
            {
                contenido += string.Format("{0}|{1}|{2}\n", item.Nombre, item.Contrasenia, item.TipoEmpleado);
            }
            return archivo.Guardar(contenido);
        }
    }

}

