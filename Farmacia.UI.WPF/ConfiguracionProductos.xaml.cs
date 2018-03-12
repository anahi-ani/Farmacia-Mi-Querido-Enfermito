using Farmacia.COMMON.Entidades;
using Farmacia.DAL;
using System.ComponentModel.Design;
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
    /// Lógica de interacción para ConfiguracionProductos.xaml
    /// </summary>
    public partial class ConfiguracionProductos : Window
    {
        ProductoEnAlmacenRepository _repProducto;
        CategoriaRepository _repCategoria;
        bool esNuevo;
        public ConfiguracionProductos()
        {
            InitializeComponent();
            _repProducto = new ProductoEnAlmacenRepository();
            _repCategoria = new CategoriaRepository();
            dtgProductos.ItemsSource = _repProducto.LeerProductoAlmacenado();
            cmbCategoria.ItemsSource = _repCategoria.LeerCategorias();
            HabilitarCajas(false);
            HabilitarBotones(true);
        }
        
        private void HabilitarBotones(bool v)
        {

            btnNuevo.IsEnabled = v;
            brnEditar.IsEnabled = v;
            btnBorrar.IsEnabled = v;
            btnGuardar.IsEnabled = !v;
        }

        private void HabilitarCajas(bool v)
        {
            txbNombre.Clear();
            txbDescripcion.Clear();
            txbP_Compra.Clear();
            txbP_Venta.Clear();
            txbNombre.IsEnabled = v;
            txbPresentacion.Clear();
            txbNombre.Clear();
            txbDescripcion.IsEnabled = v;
            txbP_Compra.IsEnabled = v;
            txbP_Venta.IsEnabled = v;
            txbPresentacion.IsEnabled = v;
        }

        private void ActualizarTabla()
        {
            dtgProductos.ItemsSource = null;
            dtgProductos.ItemsSource = _repProducto.LeerProductoAlmacenado();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HabilitarCajas(true);
                HabilitarBotones(false);
                esNuevo = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void brnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_repProducto.LeerProductoAlmacenado().Count == 0)
                {
                    MessageBox.Show("Actualmente no tiene productos disponibles", "Productos-Vacio", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (dtgProductos.SelectedItem != null)
                    {
                        COMMON.Entidades.ProductoEnAlmacen _p = dtgProductos.SelectedItem as COMMON.Entidades.ProductoEnAlmacen;
                        HabilitarCajas(true);
                        txbNombre.Text = _p.Nombre;
                        txbDescripcion.Text = _p.Descripcion;
                        txbPresentacion.Text = _p.Presentacion;
                        txbP_Compra.Text = Convert.ToString(_p.PrecioCompra);
                        txbP_Venta.Text = Convert.ToString(_p.PrecioVenta);
                        cmbCategoria.Text = Convert.ToString(_p._Categoria);
                        HabilitarBotones(false);
                        esNuevo = false;
                    }
                    else
                    {
                        MessageBox.Show("No se ha seleccionado un Producto aún", "Clientes", MessageBoxButton.OK, MessageBoxImage.Question);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_repProducto.LeerProductoAlmacenado().Count == 0)
                {
                    MessageBox.Show("No hay Productos que eliminar", "Atención", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    if (dtgProductos.SelectedItem != null)
                    {
                        COMMON.Entidades.ProductoEnAlmacen _pr = dtgProductos.SelectedItem as COMMON.Entidades.ProductoEnAlmacen;
                        if (MessageBox.Show($"¿Realmente deseas eliminar { _pr.Nombre} de la lista de productos?", "Eliminar?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            if (_repProducto.Eliminar(_pr))
                            {
                                ActualizarTabla();
                                MessageBox.Show("Este Producto ha sido Eliminado", "Empleao", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                MessageBox.Show($"Error al eliminar a {_pr.Nombre} de los productos", "Productos-Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("No se ha seleccionado ningún elemento para eliminar", "Atención", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txbNombre.Text) || string.IsNullOrEmpty(txbDescripcion.Text) || string.IsNullOrEmpty(txbPresentacion.Text) || string.IsNullOrEmpty(txbP_Compra.Text) || string.IsNullOrEmpty(txbP_Venta.Text) || string.IsNullOrEmpty(cmbCategoria.Text))
                {
                    MessageBox.Show("Faltan datos", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if (esNuevo)
                {

                    COMMON.Categoria _Cat = new COMMON.Categoria();
                    _Cat.Nombre = cmbCategoria.SelectedItem.ToString();
                    COMMON.Entidades.ProductoEnAlmacen _pr = new COMMON.Entidades.ProductoEnAlmacen()
                    {
                        Nombre = txbNombre.Text,
                        Descripcion = txbDescripcion.Text,
                        Presentacion = txbPresentacion.Text,
                        PrecioCompra = float.Parse(txbP_Compra.Text),
                        PrecioVenta = float.Parse(txbP_Venta.Text),
                        _Categoria = _Cat
                    };
                    if (_repProducto.Crear(_pr))
                    {
                        HabilitarBotones(true);
                        HabilitarCajas(false);
                        ActualizarTabla();
                        MessageBox.Show($"{_pr.Nombre} agregado con exito", "Producto", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error al intentar guardar al Producto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    COMMON.Categoria _Cat = new COMMON.Categoria();
                    _Cat.Nombre = cmbCategoria.SelectedItem.ToString();

                    COMMON.Entidades.ProductoEnAlmacen original = dtgProductos.SelectedItem as COMMON.Entidades.ProductoEnAlmacen;
                    COMMON.Entidades.ProductoEnAlmacen pr = new COMMON.Entidades.ProductoEnAlmacen();
                    pr.Nombre = txbNombre.Text;
                    pr.Descripcion = txbDescripcion.Text;
                    pr.Presentacion = txbPresentacion.Text;
                    pr.PrecioCompra= float.Parse(txbP_Compra.Text);
                    pr.PrecioVenta = float.Parse(txbP_Venta.Text);
                    pr._Categoria = _Cat;
                    if (_repProducto.Actualizar(original, pr))
                    {
                        HabilitarBotones(true);
                        HabilitarCajas(false);
                        ActualizarTabla();
                        MessageBox.Show($"Los datos de {pr.Nombre} han sido actualizados", "Producto", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show($"No se ha podido modificar  al Producto({pr.Nombre})", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void btmReturn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
