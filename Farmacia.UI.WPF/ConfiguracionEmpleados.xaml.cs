using Farmacia.COMMON;
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
    /// Lógica de interacción para ConfiguracionEmpleados.xaml
    /// </summary>
    public partial class ConfiguracionEmpleados : Window
    {
        EmpleadoRepository _repositorio;
        bool esNuevo;
        public ConfiguracionEmpleados()
        {
            InitializeComponent();
            _repositorio = new EmpleadoRepository();
            dtgEmpleados.ItemsSource = _repositorio.LeerEmpleado();
            HabilitarCajas(false);
            HabilitarBotones(true);
            cmbPuesto.Items.Add("Administrador");
            cmbPuesto.Items.Add("Empleado");
        }

        private void HabilitarCajas(bool habilitada)
        {
            txbNombre.Clear();
            txbContrasenia.Clear();
            txbNombre.IsEnabled = habilitada;
            txbContrasenia.IsEnabled = habilitada;
            cmbPuesto.IsEnabled = habilitada;

        }

        private void HabilitarBotones(bool habilitado)
        {
            btnCNew.IsEnabled = habilitado;
            btnEdit.IsEnabled = habilitado;
            btnDelete.IsEnabled = habilitado;
            btnSave.IsEnabled = !habilitado;
        }

        private void ActualizarTabla()
        {
            dtgEmpleados.ItemsSource = null;
            dtgEmpleados.ItemsSource = _repositorio.LeerEmpleado();
        }

        private void btmRegresar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCNew_Click(object sender, RoutedEventArgs e)
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

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_repositorio.LeerEmpleado().Count == 0)
                {
                    MessageBox.Show("Actualmente no tiene Empleados disponibles", "Empleado-Vacio", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (dtgEmpleados.SelectedItem != null)
                    {
                        COMMON.Empleado _e = dtgEmpleados.SelectedItem as COMMON.Empleado;
                        HabilitarCajas(true);
                        txbNombre.Text = _e.Nombre;
                        txbContrasenia.Text = _e.Contrasenia;
                        cmbPuesto.SelectedItem = _e.TipoEmpleado;
                        HabilitarBotones(false);
                        esNuevo = false;
                    }
                    else
                    {
                        MessageBox.Show("No se ha seleccionado un Empleado aún", "Empleado", MessageBoxButton.OK, MessageBoxImage.Question);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_repositorio.LeerEmpleado().Count == 0)
                {
                    MessageBox.Show("No hay Empleados que eliminar", "Atención", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    if (dtgEmpleados.SelectedItem != null)
                    {
                        COMMON.Empleado _e = dtgEmpleados.SelectedItem as COMMON.Empleado;
                        if (MessageBox.Show($"¿Realmente deseas eliminar a { _e.Nombre} de tus Empleados?", "Eliminar?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            if (_repositorio.Eliminar(_e))
                            {
                                ActualizarTabla();
                                MessageBox.Show("Este Empleado ha sido Eliminado", "Empleado", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                MessageBox.Show($"Error al eliminar a {_e.Nombre} de tus Empleados", "Clientes-Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Selecciona al Empleado que quieres Eliminar", "Atención", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txbNombre.Text) || string.IsNullOrEmpty(txbContrasenia.Text) || string.IsNullOrEmpty(cmbPuesto.Text))
                {
                    MessageBox.Show("Faltan datos", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if (esNuevo)
                {
                    COMMON.Empleado _c = new COMMON.Empleado()
                    {
                        Nombre = txbNombre.Text,
                        Contrasenia = txbContrasenia.Text,
                        TipoEmpleado = cmbPuesto.Text
                    };
                    if (_repositorio.Crear(_c))
                    {
                        HabilitarBotones(true);
                        HabilitarCajas(false);
                        ActualizarTabla();
                        MessageBox.Show($"{_c.Nombre} agregado con exito", "Cliente", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error al intentar guardar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    COMMON.Empleado original = dtgEmpleados.SelectedItem as COMMON.Empleado;
                    COMMON.Empleado _e = new COMMON.Empleado();
                    _e.Nombre = txbNombre.Text;
                    _e.Contrasenia = txbContrasenia.Text;
                    _e.TipoEmpleado = cmbPuesto.Text;

                    if (_repositorio.Actualizar(original, _e))
                    {
                        HabilitarBotones(true);
                        HabilitarCajas(false);
                        ActualizarTabla();
                        MessageBox.Show($"Los datos de {_e.Nombre} han sido actualizados", "Producto", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    { 
                        MessageBox.Show($"No se ha podido modificar al Empleado ({_e.Nombre}) ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show($"No se ha podido guardar a {txbNombre}", "Atención", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
