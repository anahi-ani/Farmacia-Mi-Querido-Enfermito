using Farmacia.COMMON;
using Farmacia.COMMON.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.DAL
{
    public class ClienteRepository
    {
        ManejdorDeArchivos archivo;
        List<Cliente> _clientes;
        public ClienteRepository()
        {
            archivo = new ManejdorDeArchivos("Clientes.txt");
            _clientes = new List<Cliente>();
        }

        //Create=Crear,Agregar,Insertar
        public bool Crear(Cliente cliente)
        {
            _clientes = LeerCliente();
            _clientes.Add(cliente);
            bool resultado = ActualizarArchivo();
            _clientes = LeerCliente();
            return resultado;
        }
        
        //Read=Leer
        public List<Cliente> LeerCliente()
        {
            string contenido = archivo.Leer();
            string[] lineas = contenido.Split('\n');
            List<Cliente> resultado = new List<Cliente>();
            for (int i = 0; i < lineas.Length; i++)
            {
                if (lineas[i].Length > 3)
                {
                    Cliente c = new Cliente();
                    string[] datos = lineas[i].Split('|');
                    c.Nombre = datos[0];
                    c.Direccion = datos[1];
                    c.Rfc = datos[2];
                    c.Telefono = datos[3];
                    c.Correo = datos[4];
                    resultado.Add(c);
                }
            }
            _clientes = resultado;
            return resultado;
        }
        
        //Update=Actualizar
        public bool Actualizar(Cliente original, Cliente modificado)
        {
            Cliente temp = new Cliente();
            foreach (var item in _clientes)
            {
                if (item.Nombre == original.Nombre && item.Rfc == original.Rfc)
                {
                    temp = item;
                    break;
                }
            }
            temp.Nombre = modificado.Nombre;
            temp.Direccion = modificado.Direccion;
            temp.Rfc = modificado.Rfc;
            temp.Telefono = modificado.Telefono;
            temp.Correo = modificado.Correo;
            bool resultado = ActualizarArchivo();
            _clientes = LeerCliente();
            return resultado;
        }
        
        //Delete=Eliminar
        public bool Eliminar(Cliente original)
        {
            Cliente temp = new Cliente();
            foreach (var item in _clientes)
            {

                if (item.Nombre == original.Nombre && item.Rfc == original.Rfc)
                {
                    temp = item;
                    break;
                }
            }
            _clientes.Remove(temp);
            bool resultado = ActualizarArchivo();
            _clientes = LeerCliente();
            return ActualizarArchivo();
        }

        private bool ActualizarArchivo()
        {
            string contenido = "";
            foreach (var item in _clientes)
            {
                contenido += string.Format("{0}|{1}|{2}|{3}|{4}\n", item.Nombre, item.Direccion, item.Rfc, item.Telefono, item.Correo);
            }
            return archivo.Guardar(contenido);
        }
    }
}
