using Newtonsoft.Json;
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
        private int buttonSelect;

        List<Productos> listContado =  new List<Productos>();

        List<Productos> listDiferencias = new List<Productos>();

        public BuscarProductoPage()
        {
            InitializeComponent();
           this.lvwProductos.ItemsSource = db.GetAllProd();
            buttonSelect = 1;
        }

        public void Load()
        {
            this.tbDatos.Text = "Conteo: " + Usuario.Conteo;
              //  + " Estante : " + Usuario.Codigo_Ubicacion;
            tap = 0;
                this.btnDiferencias.IsVisible = Usuario.Conteo > 0 ? true:false;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {


            Device.BeginInvokeOnMainThread(async () =>
            {
                var respuesta = await DisplayAlert("Cerrar Estantes", "Seguro que desea Cerrar el Estante Actual", "Aceptar", "Cancelar");
                if (respuesta == true)
                {
                    var resp = await Acr.UserDialogs.UserDialogs.Instance.PromptAsync("Ingrese su credencial para confirmar", "LIP", "Cerrar Estante", "Cancelar", "Tus Credenciales",Acr.UserDialogs.InputType.Default);
                    if (resp.Text.ToUpper() == Usuario.Cedula.ToUpper())
                    {
                        var estantes = new Services.EstantesServices();
                        Usuario.IsCerrado = true;
                        Usuario.Codigo_Ubicacion = 0;
                        if (db.CerrarEstante(Usuario) == 1)
                        {
                            var res = new Entidades.Respuesta();
                            res = estantes.CerrarUbicacion(Usuario);
                            await Navigation.PopAsync(true);
                        }
                    }
                    else {

                        Acr.UserDialogs.UserDialogs.Instance.Toast(new Acr.UserDialogs.ToastConfig("Credenciales no validas!"));
                    }
                   
                }

            });
        }

        private void CargarProductosContados() {
            try
            {
                var servicios = new Services.ProductosServices();
                var Lista = new List<Productos>();
                var respuesta = servicios.TraerListaProductosContados(Usuario);

                if (respuesta.Code == 1) {
                    if (respuesta.Lista.Count > 0)
                    {
                        List<Entidades.ListaProductos> lp = new List<Entidades.ListaProductos>();

                        foreach (var i in respuesta.Lista)
                        {
                            lp.Add(JsonConvert.DeserializeObject<Entidades.ListaProductos>(i.ToString()));
                        }
                        this.listContado.Clear();
                        foreach (var i in lp)
                        {

                            Lista.Add(new Productos
                            {
                                Codigo = i.Codigo_Producto,
                                Nombre = i.Nombre,
                                Estado = i.Estado,
                                Resultado = i.Resultado

                            });
                        }

                        if (this.buttonSelect == 2) {

                            listContado = Lista;
                            this.lvwProductos.ItemsSource = listContado;
                        }
                        if (this.buttonSelect == 3)
                        {
                            if (listDiferencias.Count <= 0) {
                                listDiferencias = Lista.Where(x => x.Estado == "0").ToList();
                            }
                            this.lvwProductos.ItemsSource = listDiferencias; 
                        }

                    }
                    else {
                        this.lvwProductos.ItemsSource = new List<Productos>();
                    }
                 
                }
            }
            catch (Exception)
            {
                return;
              // throw;
            }


        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {

                if (this.buttonSelect == 1) //Filtrar lista de Productos Contados
                {
                    var l = new List<Productos>();
                    l = db.FindProductos(e.NewTextValue.ToUpper());
                    this.lvwProductos.ItemsSource = l;
                }
                if (this.buttonSelect == 2 ) //Lista de Productos de Inventario
                {
                    var result = listContado.Where(c => c.Nombre.ToUpper().Contains(e.NewTextValue.ToString().ToUpper()));
                    if (string.IsNullOrEmpty(e.NewTextValue))
                    {
                        this.lvwProductos.ItemsSource = listContado;
                    }
                    else
                    {
                        this.lvwProductos.ItemsSource = result;
                    }
                    this.lvwProductos.ItemsSource = result;
                }
                if (this.buttonSelect == 3) {
                    var result = listDiferencias.Where(c => c.Nombre.ToUpper().Contains(e.NewTextValue.ToString().ToUpper()));
                    if (string.IsNullOrEmpty(e.NewTextValue)){
                        this.lvwProductos.ItemsSource = listDiferencias;
                    }
                    else{
                        this.lvwProductos.ItemsSource = result;
                    }
                    
                }



            }
            catch (Exception ex)
            {
                return;
                //throw;
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
                f.ProductosDiferencias = buttonSelect != 1 ? true : false;
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
            try
            {
                tap = 0;
                Entidades.Respuesta Respuesta = new Entidades.Respuesta();
                Usuario = db.GetAllLevantado(Usuario.Cedula);
                if (Usuario.Codigo_Ubicacion == 0)
                {
                    Navigation.PopAsync(true);
                }

                if (Usuario.Conteo > 0)
                {
                    if (listDiferencias.Count > 0) {
                        listDiferencias.Remove(p); //eliminamos de la lista de Conteo con dif.
                        if (buttonSelect != 1) {
                            this.lvwProductos.ItemsSource = listDiferencias;
                        }
                    }

                }
            }
            catch (Exception)
            {

                //throw;
            }
           
           

        }


        private void btnDiferencias_Clicked(object sender, EventArgs e)
        {
            buttonSelect = 3;
            CargarProductosContados();
        }
    

        private void btnContados_Clicked(object sender, EventArgs e)
        {
            buttonSelect = 2;
            CargarProductosContados();    
        }

        private void btnInventario_Clicked(object sender, EventArgs e)
        {
            buttonSelect = 1;
            this.lvwProductos.ItemsSource = db.GetAllProd();
        }
    }
}