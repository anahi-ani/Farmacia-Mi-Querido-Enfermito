using Farmacia.COMMON.Entidades;
using Farmacia.COMMON.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.DAL
{
    public class TicketRepository
    {
        ManejdorDeArchivos archivo;
        List<ProductoVendido> _ListTicket;
        Ticket _ticket;
        string nombrearchivo;
        public TicketRepository( )
        {
            nombrearchivo=DateTime.Now.ToString("dd.MM.yy[hh][mm][ss]");
            archivo= new ManejdorDeArchivos($"Ticket.{nombrearchivo}.txt");
            _ListTicket = new List<ProductoVendido>();
            _ticket = new Ticket();
        }

        //Create=Crear,Agregar,Insertar
        public bool Crear(Ticket ticket)
        {
            _ListTicket = ticket.ProductosVendidos;
            _ticket.Empleado = ticket.Empleado;
            _ticket.Cliente = ticket.Cliente;
            _ticket.IVA = ticket.IVA;
            _ticket.TotalVenta = ticket.TotalVenta;
            _ticket.PagoEfectivo = ticket.PagoEfectivo;
            _ticket.Cambio = ticket.Cambio;
            bool _resultado = ActualizarArchivo();
            //_ListTicket = LeerProductosVendidos();
            return _resultado;
        }

        private bool ActualizarArchivo()
        {
            string contenido = null;
            contenido = $"\n       -FARMACIAS MI QUERIDO ENFERMITO SA DE CV-\r\rSUCURSAL HUICHAPAN\nCalle Jorge Rojo Lugo\nTel: 7731097482\nE-mail:miqueridoe@gmail.com\n\nFecha: {nombrearchivo}\n\nCliente: {_ticket.Cliente}\nAtendio: {_ticket.Empleado}\n\n _________________________________________________________________\n";
            foreach (var item in _ListTicket)
            {
                contenido += string.Format("{0}\t|\t{1}\t|\t${2}\n", item.CantidaDeProductos, item.Nombre, item.TotalAPagar);
            }
            contenido += $"\n\nIVA: {_ticket.IVA}\nTotal: ${_ticket.TotalVenta}\nPago Efectivo: ${_ticket.PagoEfectivo}\nCambio: {_ticket.Cambio}\n_________________________________________________________________\n     FUE UN PLACER ATENDERLE";
            return archivo.Guardar(contenido);
        }

        //Read=Leer
        public List<ProductoVendido> LeerProductosVendidos()
        {
            string contenido = archivo.Leer();
            string[] lineas = contenido.Split('\n');
            List<ProductoVendido> resultado = new List<ProductoVendido>();
            for (int i = 0; i < lineas.Length - 1; i++)
            {
                if (lineas[i].Length > 3)
                {
                    ProductoVendido pv = new ProductoVendido();
                    pv.Nombre = lineas[i];
                    resultado.Add(pv);
                }
            }
            _ListTicket = resultado;
            return resultado;

        }
    }
}
