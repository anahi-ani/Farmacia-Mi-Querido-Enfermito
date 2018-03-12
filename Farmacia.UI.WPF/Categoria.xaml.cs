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
    /// Lógica de interacción para Categoria.xaml
    /// </summary>
    public partial class Categoria : Window
    {
        CategoriaRepository _repo;
        public Categoria()
        {
            InitializeComponent();
            _repo = new CategoriaRepository();
            dtgCategorias.ItemsSource = _repo.LeerCategorias();
            HabilitarCaja(false);
            HabilitarBotones(true);
        }
        private void HabilitarCaja(bool habilitada)
        {
            txbNombre.Clear();
            txbNombre.IsEnabled = habilitada;

        }
        private void HabilitarBotones(bool habilitado)
        {
            btnAgregar.IsEnabled = habilitado;
            btnEliminar.IsEnabled = habilitado;
            btnGuardar.IsEnabled = !habilitado;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            HabilitarCaja(true);
            HabilitarBotones(false);
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_repo.LeerCategorias().Count == 0)
                {
                    MessageBox.Show("No hay Categorías que eliminar", "Atención", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    if (dtgCategorias != null)
                    {
                        COMMON.Categoria _cat = dtgCategorias.SelectedItem as COMMON.Categoria;
                        if (MessageBox.Show("Confirma la eliminación de la categoría", "Eliminar?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            if (_repo.Eliminar(_cat))
                            {
                                ActualizarTabla();
                                MessageBox.Show("Categoría Eliminada con éxito", "Categorías", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                MessageBox.Show("Error al eliminarCategoría", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selecciona la categoría que quieres Eliminar", "Atención", MessageBoxButton.OK, MessageBoxImage.Exclamation);

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
                if (string.IsNullOrEmpty(txbNombre.Text))
                {
                    MessageBox.Show("Verifica los Datos", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                COMMON.Categoria _cat = new COMMON.Categoria()
                {
                    Nombre = txbNombre.Text
                };
                if (_repo.Crear(_cat))
                {
                    ActualizarTabla();
                    MessageBox.Show("Guardado con Éxito", "Categoria", MessageBoxButton.OK, MessageBoxImage.Information);
                    HabilitarBotones(true);
                    HabilitarCaja(false);
                }
                else
                {
                    MessageBox.Show("Error al guardar Categoría", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ActualizarTabla()
        {
            dtgCategorias.ItemsSource = null;
            dtgCategorias.ItemsSource = _repo.LeerCategorias();
        }

        private void btmRegresar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}