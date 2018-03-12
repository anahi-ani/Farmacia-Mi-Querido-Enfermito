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
    /// Lógica de interacción para ConfigurarClientes.xaml
    /// </summary>
    public partial class ConfigurarClientes : Window
    {
        ClienteRepository _repositorio;
        bool esNuevo;

        public ConfigurarClientes()
        {
            InitializeComponent();
            _repositorio = new ClienteRepository();
            dtgClientes.ItemsSource = _repositorio.LeerCliente();
            HabilitarCajas(false);
            HabilitarBotones(true);
        }

        private void HabilitarCajas(bool habilitada)
        {
            txbNombre.Clear();
            txbDireccion.Clear();
            txbRFC.Clear();
            txbTelefono.Clear();
            txbemail.Clear();
            txbNombre.IsEnabled = habilitada;
            txbDireccion.IsEnabled = habilitada;
            txbRFC.IsEnabled = habilitada;
            txbTelefono.IsEnabled = habilitada;
            txbemail.IsEnabled= habilitada;
        }

        private void HabilitarBotones(bool habilitados)
        {
            btnAgregar.IsEnabled = habilitados;
            btnEditar.IsEnabled = habilitados;
            btnEliminar.IsEnabled = habilitados;
            btnGuardar.IsEnabled = !habilitados;
           
        }

        private void ActualizarTabla()
        {
            dtgClientes.ItemsSource = null;
            dtgClientes.ItemsSource = _repositorio.LeerCliente();
        }

        private void btmRegresar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
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

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_repositorio.LeerCliente().Count == 0)
                {
                    MessageBox.Show("Actualmente no tiene clientes disponibles", "Clientes-Vacio", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (dtgClientes.SelectedItem != null)
                    {
                        COMMON.Cliente c = dtgClientes.SelectedItem as COMMON.Cliente;
                        HabilitarCajas(true);
                        txbNombre.Text = c.Nombre;
                        txbDireccion.Text = c.Direccion;
                        txbRFC.Text = c.Rfc;
                        txbTelefono.Text = c.Telefono;
                        txbemail.Text = c.Correo;
                        HabilitarBotones(false);
                        esNuevo = false;
                    }
                    else
                    {
                        MessageBox.Show("No se ha seleccionado un cliente aún", "Cliente", MessageBoxButton.OK, MessageBoxImage.Question);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_repositorio.LeerCliente().Count == 0)
                {
                    MessageBox.Show("No hay Clientes que eliminar", "Clientes", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    if (dtgClientes.SelectedItem != null)
                    {
                        COMMON.Cliente c = dtgClientes.SelectedItem as COMMON.Cliente;
                        if (MessageBox.Show("Realmente deseas eliminar a " + c.Nombre + "de tus Clientes?", "Eliminar?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            if (_repositorio.Eliminar(c))
                            {
                                ActualizarTabla();
                                MessageBox.Show("Este Cliente ha sido Eliminado", "Cliente", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                MessageBox.Show($"Error al eliminar a {c.Nombre} de tus Clientes", "Clientes-Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Selecciona al Cliente que quieres Eliminar", "Atención", MessageBoxButton.OK, MessageBoxImage.Exclamation);

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
                if (string.IsNullOrEmpty(txbNombre.Text)|| string.IsNullOrEmpty(txbDireccion.Text)||string.IsNullOrEmpty(txbRFC.Text) || string.IsNullOrEmpty(txbTelefono.Text) || string.IsNullOrEmpty(txbemail.Text))
                {
                    MessageBox.Show("Faltan datos", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if (esNuevo)
                {
                    COMMON.Cliente _c = new COMMON.Cliente()
                    {
                        Nombre = txbNombre.Text,
                        Direccion=txbDireccion.Text,
                        Rfc=txbRFC.Text,
                        Telefono=txbTelefono.Text,
                        Correo=txbemail.Text
                    };
                    if (_repositorio.Crear(_c))
                    {
                        ActualizarTabla();
                        MessageBox.Show($"{_c.Nombre} agregado con exito", "Cliente", MessageBoxButton.OK, MessageBoxImage.Information);
                        HabilitarBotones(true);
                        HabilitarCajas(false);
                    }
                    else
                    {
                        MessageBox.Show("Error al intentar guardar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    COMMON.Cliente original = dtgClientes.SelectedItem as COMMON.Cliente;
                    COMMON.Cliente c = new COMMON.Cliente();
                    c.Nombre = txbNombre.Text;
                    c.Direccion = txbDireccion.Text;
                    c.Rfc = txbRFC.Text;
                    c.Telefono = txbTelefono.Text;
                    c.Correo = txbemail.Text;
                    if (_repositorio.Actualizar(original, c))
                    {
                        HabilitarBotones(true);
                        HabilitarCajas(false);
                        ActualizarTabla();
                        MessageBox.Show($"Los datos de {c.Nombre} han sido actualizados", "Compas", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show($"No se ha podido modificar a { c.Nombre}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show($"Error al intentar guardar a {txbNombre}", "Atención", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }


        
        
    }
}
