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
    /// Lógica de interacción para MenuAdministrador.xaml
    /// </summary>
    public partial class MenuAdministrador : Window
    {
        Empleado _usuario;
        public MenuAdministrador(Empleado usuario)
        {
            InitializeComponent();
            _usuario = usuario;
        }

        private void btmCategorias_Click(object sender, RoutedEventArgs e)
        {
            Categoria cat= new Categoria();
            cat.Show();
        }

        private void btmCliente_Click(object sender, RoutedEventArgs e)
        {
            ConfigurarClientes cl = new ConfigurarClientes();
            cl.Show();
        }

        private void btmEmpleado_Click(object sender, RoutedEventArgs e)
        {
            ConfiguracionEmpleados empl = new ConfiguracionEmpleados();
            empl.Show();
        }

        private void btmProducto_Click(object sender, RoutedEventArgs e)
        {
            ConfiguracionProductos prod = new ConfiguracionProductos();
            prod.Show();
        }

        private void btmVenta_Click(object sender, RoutedEventArgs e)
        {
            NuevaVenta venta = new NuevaVenta(_usuario);
            venta.Show();
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow prin = new MainWindow();
            prin.Show();
            this.Close();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
