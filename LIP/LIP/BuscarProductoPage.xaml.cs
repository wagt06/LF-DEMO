using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LIP
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BuscarProductoPage : ContentPage
	{
        ObservableCollection<Productos> o = new ObservableCollection<Productos>();
        Conexion c = new Conexion();
        DataAccess db = new DataAccess();
        public Entidades.Auth Usuario = new Entidades.Auth();
        public BuscarProductoPage ()
		{
			InitializeComponent ();
            this.lvwProductos.ItemsSource = db.GetAllProd();
        }

        public void Load() {
            this.tbDatos.Text = "Conteo: " + Usuario.Conteo + "Estante : " + Usuario.Codigo_Ubicacion;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Seleccion de Items", "Se ha seleccionado un Nuevo Items", "Cancelar");
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var l = new List<Productos>();
                //l = c.ListaProductos();// this.Navigation.PushAsync(f,true);
                l = db.FindProductos(e.NewTextValue.ToUpper());
                //var result = l.Where(c => c.Nombre.ToUpper().Contains(e.NewTextValue.ToString().ToUpper()));
                this.lvwProductos.ItemsSource = l;
                //Navigation.PushModalAsync(f, true);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void lvwProductos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Productos p = new Productos();
            p = (Productos)e.SelectedItem;
            var f = new IgresarProductosPage();
            f.Nombre = p.Nombre;
            f.codigo = p.Codigo;
            f.Cargar();
            Navigation.PushAsync(f, true);
        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {
           
        }
    }
}