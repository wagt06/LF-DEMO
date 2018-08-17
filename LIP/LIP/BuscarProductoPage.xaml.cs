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
        Productos p = new Productos();
        private  int tap;

        public BuscarProductoPage()
        {
            InitializeComponent();
            this.lvwProductos.ItemsSource = db.GetAllProd();
        }

        public void Load()
        {
            this.tbDatos.Text = "Conteo: " + Usuario.Conteo + " Estante : " + Usuario.Codigo_Ubicacion;
            tap = 0;
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
        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {
            Productos p2 = new Productos();
            tap += 1;
            p2 = p;
           
            p = (Productos)this.lvwProductos.SelectedItem;
            if (p2 != p)
            {
                tap = 1;
            }

            if (tap == 2) {
           
                var f = new IgresarProductosPage();
                f.CodigoProducto = p.Codigo;
                f.NombreProducto = p.Nombre;
                f.Usuario = Usuario;
                f.Cargar();
                Navigation.PushAsync(f, true);
            }

        }
        private void ClickVerDetalle(object sender, EventArgs e) {
            try
            {
                
                var DetalleProducto = new DetalleConteoProductoPage();
                p = (Productos)this.lvwProductos.SelectedItem;
                if (p != null)
                {
                    Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Descargando Detalle", Acr.UserDialogs.MaskType.Clear);
                    Device.BeginInvokeOnMainThread(async () =>
                {
                   
                        DetalleProducto.Producto.CodigoSucursal = Usuario.Sucursal;
                        DetalleProducto.Producto.Codigo_Factura = Usuario.Parcial;
                        DetalleProducto.Producto.Bodega = Usuario.Bodega;
                        DetalleProducto.Producto.Codigo_Producto = p.Codigo;
                        DetalleProducto.Cargar();

                        // Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                        await Navigation.PushAsync(DetalleProducto, true);
                    
                });
                }
            }
            catch (Exception)
            {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                DisplayAlert("LIP", "Seleccione un Producto", "OK");
                return;
               // throw;
            }
          
           
        }

        private void lvwProductos_ItemTapped(object sender, ItemTappedEventArgs e)
        {


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            tap = 0;
            Entidades.Respuesta Respuesta = new Entidades.Respuesta();
            Usuario = db.GetAllLevantado(Usuario.Cedula);
            if  (Usuario.Codigo_Ubicacion == 0){
                 Navigation.PopAsync(true);
            }

        }

 
    }
}