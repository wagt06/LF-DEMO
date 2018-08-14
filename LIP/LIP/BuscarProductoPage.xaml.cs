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
        public Boolean isCerrardo = new Boolean();
        public Entidades.Auth Usuario = new Entidades.Auth();

        TapGestureRecognizer g = new TapGestureRecognizer();

       
      
    public BuscarProductoPage()
        {
            InitializeComponent();
            this.lvwProductos.ItemsSource = db.GetAllProd();
            g.NumberOfTapsRequired = 2;
            g.Tapped += (Sender,args) => GestureEnvento(Sender,args);
            this.lvwProductos.GestureRecognizers.Add(g);
        }

        public void Load()
        {
            this.tbDatos.Text = "Conteo: " + Usuario.Conteo + " Estante : " + Usuario.Codigo_Ubicacion;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {


            Device.BeginInvokeOnMainThread(async () =>
            {
                var respuesta = await DisplayAlert("Cerrar Estantes", "Seguro que desea Cerrar el Estante Actual", "Aceptar", "Cancelar");
                if (respuesta == true)
                {
                    var estantes = new Services.EstantesServices();
                    Usuario.IsCerrado = true;
                    if (db.CerrarEstante(Usuario) == 1)
                    {
                        var res = new Entidades.Respuesta();
                        res = estantes.CerrarUbicacion(Usuario);
                        await Navigation.PopAsync(true);
                    }

                }

            });
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
            //Productos p = new Productos();
            //p = (Productos)e.SelectedItem;
            //var f = new IgresarProductosPage();
            //f.CodigoProducto = p.Codigo;
            //f.NombreProducto = p.Nombre;
            //f.Usuario = Usuario;
            //f.Cargar();
            //Navigation.PushAsync(f, true);
        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {
            Productos p = new Productos();
            p = (Productos)this.lvwProductos.SelectedItem;
            var f = new IgresarProductosPage();
            f.CodigoProducto = p.Codigo;
            f.NombreProducto = p.Nombre;
            f.Usuario = Usuario;
            f.Cargar();
            Navigation.PushAsync(f, true);
        }

        private void lvwProductos_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Entidades.Respuesta Respuesta = new Entidades.Respuesta();
            Usuario = db.GetAllLevantado(Usuario.Cedula);
            if  (Usuario.Codigo_Ubicacion == 0){
                 Navigation.PopAsync(true);
            }

        }

        private void GestureEnvento(object sender, EventArgs e) {

        }

    }
}