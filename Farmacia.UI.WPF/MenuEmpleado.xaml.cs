using Farmacia.COMMON;
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
    /// Lógica de interacción para MenuEmpleado.xaml
    /// </summary>
    public partial class MenuEmpleado : Window
    {
        Empleado _usuario;
        public MenuEmpleado(Empleado usuario)
        {
            InitializeComponent();
            _usuario = usuario;
        }

        private void btnNuevaVenta_Click(object sender, RoutedEventArgs e)
        {
            NuevaVenta v = new NuevaVenta(_usuario);
            v.Show();
            this.Close();
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Principal = new MainWindow();
            Principal.Show();
            this.Close();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
