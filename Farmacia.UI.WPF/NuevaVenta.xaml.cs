using Farmacia.COMMON;
using Farmacia.COMMON.Entidades;
using Farmacia.COMMON.Tools;
using Farmacia.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Farmacia.UI.WPF
{
    /// <summary>
    /// Lógica de interacción para NuevaVenta.xaml
    /// </summary>
    public partial class NuevaVenta : Window
    {
        Empleado _vendedor;
        ClienteRepository cliente_repo;
        ProductoVendidoRepository pVendido_repo;
        ProductoEnAlmacenRepository pAlmacen_repo;
        TicketRepository ticket_repo;
        public NuevaVenta(Empleado vendedor)
        {
            InitializeComponent();
            _vendedor = vendedor;
            pVendido_repo = new ProductoVendidoRepository();
            pAlmacen_repo = new ProductoEnAlmacenRepository();
            ticket_repo = new TicketRepository();
            cliente_repo = new ClienteRepository();
            dpkFecha.Text = DateTime.Now.ToString("dd-MM-yyyy  hh:mm:ss");
            txbVendedor.Text = _vendedor.Nombre;
            
            HabilitarCajas(false);
            cmbCliente.ItemsSource = cliente_repo.LeerCliente();
            cmbProducto.ItemsSource = pAlmacen_repo.LeerProductoAlmacenado();
        }
        private void HabilitarCajas(bool v)
        {
            txbVendedor.IsEnabled = v;
            cmbCliente.IsEnabled = !v;
            cmbProducto.IsEnabled = !v;
            txbCantidad.IsEnabled = !v;
            txbIva.IsEnabled = v;
            txbTotal.IsEnabled = v;
            txbPagoEf.IsEnabled = !v;
            txbCambio.IsEnabled = !v;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbProducto.Text) || string.IsNullOrEmpty(txbCantidad.Text))
                {
                    MessageBox.Show("Faltan datos", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                ProductoVendido _pv = new ProductoVendido();
                _pv.CantidaDeProductos = int.Parse(txbCantidad.Text);
                _pv.Nombre = cmbProducto.Text;
                float _total=0;
                foreach (var item in pAlmacen_repo.LeerProductoAlmacenado())
                {
                    if (item.Nombre == cmbProducto.Text)
                    {
                         float precio = item.PrecioVenta;
                        _pv.PrecioVenta = precio;
                    }
                }

                _pv.TotalAPagar = CalcularTotalPorProducto(_pv.PrecioVenta, _pv.CantidaDeProductos);

                if (pVendido_repo.Crear(_pv))
                {
                    ActualizarTabla();

                }
                else
                {
                    MessageBox.Show("Error al registrar venta del producto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }

                //List<ProductoVendido> pv = pVendido_repo.LeerProductoVendido();
                foreach (var item in pVendido_repo.LeerProductoVendido())
                {
                    if (item.Nombre == cmbProducto.Text)
                    {
                        txbTotal.Text = Convert.ToString(TotalPorVenta(_pv.TotalAPagar));
                    }
                }
                _total = float.Parse(txbTotal.Text);
                txbIva.Text = Convert.ToString(GenerarIVA(_total));
            }
            catch (Exception)
            {

                MessageBox.Show($"No se ha podido guardar a {cmbProducto.Text}", "Atención", MessageBoxButton.OK, MessageBoxImage.Exclamation);

            }

        }

        //private void btnEliminar_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (pVendido_repo.LeerProductoVendido().Count == 0)
        //        {
        //            MessageBox.Show("Aun no has agregado ningun Producto para vender", "Productos", MessageBoxButton.OK, MessageBoxImage.Error);
        //        }
        //        else
        //        {
        //            if (dtgProductosVendidos.SelectedItem != null)
        //            {
        //                ProductoVendido pv = dtgProductosVendidos.SelectedItem as ProductoVendido;
        //                if (MessageBox.Show("¿Realmente deseas eliminar a " + pv.Nombre + " de la lista?", "Eliminar????", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        //                {
        //                    if (pVendido_repo.Eliminar(pv))
        //                    {
        //                        ActualizarTabla();
        //                        MessageBox.Show("Se ha quitado un producto de la lista a vender", "Productos", MessageBoxButton.OK, MessageBoxImage.Information);

        //                    }
        //                    else
        //                    {
        //                        MessageBox.Show("Error al intentar Eliminar el producto de la lista", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                MessageBox.Show("No has seleccionado un elemento de la lista", "Productos", MessageBoxButton.OK, MessageBoxImage.Question);
        //            }
        //        }
        //        //ProductoVendido actualizada = new ProductoVendido();
        //        ProductoVendido _pv = new ProductoVendido();
        //        _pv.CantidaDeProductos = int.Parse(txbCantidad.Text);
        //        _pv.Nombre = cmbProducto.Text;
        //        float _total = 0;
        //        foreach (var item in pAlmacen_repo.LeerProductoAlmacenado())
        //        {
        //            if (item.Nombre == cmbProducto.Text)
        //            {
        //                float precio = item.PrecioVenta;
        //                _pv.PrecioVenta = precio;
        //            }
        //        }

        //        _pv.TotalAPagar = CalcularTotalPorProducto(_pv.PrecioVenta, _pv.CantidaDeProductos);
        //        foreach (var item in pVendido_repo.LeerProductoVendido())
        //        {
        //            if (item.Nombre == cmbProducto.Text)
        //            {
        //                txbTotal.Text = Convert.ToString(TotalPorVenta(_pv.TotalAPagar));
        //            }
        //        }
        //        _total = float.Parse(txbTotal.Text);
        //        txbIva.Text = Convert.ToString(GenerarIVA(_total));
        //    }
        //    catch (Exception)
        //    {

        //        MessageBox.Show($"No se ha podido eliminar a {cmbProducto.Text}", "Atención", MessageBoxButton.OK, MessageBoxImage.Exclamation);

        //    }
        //}

        //Métodos para Calcular Precios

        float totalv = 0;

        public float CalcularTotalPorProducto(float precioVenta, int cant)
        {
            return precioVenta * cant;
        }

        private float TotalPorVenta(float TotalAPagar)
        {
            totalv = totalv + TotalAPagar;
            return totalv;
        }

        private float GenerarIVA(float total)
        {
            
            return (total * 16) / 100;
        }
        //

        private void ActualizarTabla()
        {
            dtgProductosVendidos.ItemsSource = null;
            dtgProductosVendidos.ItemsSource = pVendido_repo.LeerProductoVendido();
        }

        private void btnGenerarVenta_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Ticket _ticket = new Ticket();
                if (string.IsNullOrEmpty(txbPagoEf.Text) )
                {
                    MessageBox.Show("Favor de ingresar El monto en efectivo", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                    _ticket.TotalVenta = float.Parse(txbTotal.Text);
                    _ticket.PagoEfectivo = float.Parse(txbPagoEf.Text);
                if (_ticket.PagoEfectivo < _ticket.TotalVenta)
                {
                    MessageBox.Show("Lo siento, monto a pagar por debajo del total de la venta", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if (string.IsNullOrEmpty(cmbProducto.Text))
                {
                    _ticket.Cliente = "";
                }
                else
                {
                    _ticket.ProductosVendidos = pVendido_repo.LeerProductoVendido();
                    _ticket.Empleado = txbVendedor.Text;
                    _ticket.Cliente = cmbCliente.Text;
                    _ticket.IVA = float.Parse(txbIva.Text);
                    txbCambio.Text = Convert.ToString(CalcularCambio(_ticket.PagoEfectivo, _ticket.TotalVenta));
                    _ticket.Cambio = float.Parse(txbCambio.Text);
                    ticket_repo.Crear(_ticket);
                    MessageBox.Show("Venta Generada con éxito", "Venta Generada", MessageBoxButton.OK, MessageBoxImage.None);
                    this.Close();
                }

            }
            catch(Exception)
            {
                MessageBox.Show($"No se ha podido Generar el Ticket...", "Atención", MessageBoxButton.OK, MessageBoxImage.Exclamation);

            }
        }

        private float CalcularCambio(float pagoEf, float totalVenta)
        {
            return pagoEf - totalVenta;
        }

        private void btmRegresar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
