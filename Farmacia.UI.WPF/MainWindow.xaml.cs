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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Farmacia.COMMON;
using Farmacia.DAL;

namespace Farmacia.UI.WPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EmpleadoRepository empleado;
        Empleado _e;
        public MainWindow()
        {
            InitializeComponent();
            _e = new Empleado();
            empleado = new EmpleadoRepository();
            cmbPuesto.Items.Add("Administrador");
            cmbPuesto.Items.Add("Empleado");
            
        }

        private void HabilitarCajas(bool habilitadas)
        {
            txbusuario.Clear();
            pswbContrasenia.Clear();
            txbusuario.IsEnabled = habilitadas;
            pswbContrasenia.IsEnabled = habilitadas;
            cmbPuesto.IsEnabled = habilitadas;
        }

        private void HabilitarBotones(bool habilitados)
        {
            btnEntrar.IsEnabled = habilitados;
            btnSalir.IsEnabled = habilitados;

        }

        private void btnEntrar_Click(object sender, RoutedEventArgs e)
      {
            try
            {
                if (!string.IsNullOrEmpty(txbusuario.Text) || !string.IsNullOrEmpty(pswbContrasenia.Password) || !string.IsNullOrEmpty(cmbPuesto.SelectedItem.ToString()))
                {
                    List<Empleado> em = empleado.LeerEmpleado();
                    _e = em.Where(d=>d.Nombre==txbusuario.Text).SingleOrDefault();//este liena me trea el primer objeto de empleado que encuentre con el nombre de usuario que le pase
                    if (_e.Contrasenia == pswbContrasenia.Password && _e.TipoEmpleado == cmbPuesto.SelectedItem.ToString())
                    {
                        if (_e.TipoEmpleado=="Administrador"|| _e.TipoEmpleado == "Empleado")
                        {
                            if (_e.TipoEmpleado == "Administrador")
                            {
                                //ventana administrador
                                MenuAdministrador admon = new MenuAdministrador(_e);
                                admon.Show();
                                this.Close();
                            }
                            if (_e.TipoEmpleado == "Empleado")
                            {
                                //ventana empleado
                                MenuEmpleado v = new MenuEmpleado(_e);
                                v.Show();
                                this.Close();
                            }
                        }
                        else
                        {
                            throw new Exception("Seleccione un tipo de empleado");
                        }

                    }
                    else
                    {
                        throw new Exception("usuario y/o contraseña invalidos");
                    }
                }
                else
                {
                    throw new Exception("Verifique campos");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
