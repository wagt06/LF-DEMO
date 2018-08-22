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
        private int tap;
        private int buttonSelect;

        List<Productos> listContado = new List<Productos>();

        List<Productos> listDiferencias = new List<Productos>();

        public BuscarProductoPage()
        {
            InitializeComponent();
            this.lvwProductos.ItemsSource = db.GetAllProd();
            buttonSelect = 1;
        }

        public void Load()
        {
            var sizeBtn = new double();
            var conteo = Usuario.Conteo.ToString() == "0" ? " Inicial " : Usuario.Conteo.ToString();
            this.tbDatos.Text = "Conteo : " + conteo + " Estante : " + Usuario.NombreUbicacion;
            tap = 0;
            this.btnDiferencias.IsVisible = Usuario.Conteo > 0 ? true : false;
            if (this.btnDiferencias.IsEnabled == false)
            {
                sizeBtn = (App.Current.MainPage.Width / 3) - 8;
            }
            else
            {
                sizeBtn = (App.Current.MainPage.Width / 3) - 8;
            }

            btnInventario.WidthRequest = sizeBtn;
            btnContados.WidthRequest = sizeBtn;
            btnDiferencias.WidthRequest = sizeBtn;
            btnInventario_Clicked(null, null);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {


            Device.BeginInvokeOnMainThread(async () =>
            {
                if (this.listDiferencias.Count != 0 && Usuario.Conteo > 0)
                {
                    await DisplayAlert("Cerrar Estantes", "No se puede cerrar el estante hasta nque todos los productos con diferencias esten contados, la lista debe de estar limpia.", "Ok");
                    return;
                }

                var respuesta = await DisplayAlert("Cerrar Estantes", "Seguro que desea Cerrar el Estante Actual", "Aceptar", "Cancelar");
                if (respuesta == true)
                {
                    var resp = await Acr.UserDialogs.UserDialogs.Instance.PromptAsync("Ingrese su credencial para confirmar", "LIP", "Cerrar Estante", "Cancelar", "Tus Credenciales", Acr.UserDialogs.InputType.Default);
                    if (resp.Text.ToUpper().Trim() == Usuario.Cedula.ToUpper().Trim())
                    {
                        var estantes = new Services.EstantesServices();
                        var res = new Entidades.Respuesta();
                        res = estantes.CerrarUbicacion(Usuario);
                        Usuario.IsCerrado = true;
                        Usuario.Codigo_Ubicacion = 0;
                        if (res.Code == 1)
                        {
                            db.CerrarEstante(Usuario);
                            await Navigation.PopAsync(true);
                        }
                        if (res.Code == 6)
                        {
                            await DisplayAlert("LIP", res.Response, "Aceptar");
                            Usuario.Codigo_Ubicacion = 0;
                            Usuario.NombreUbicacion = "";
                            Usuario.IsCerrado = true;
                            db.UpdateLevantado(Usuario);
                            await Navigation.PopAsync();
                        }
                    }
                    else
                    {

                        Acr.UserDialogs.UserDialogs.Instance.Toast(new Acr.UserDialogs.ToastConfig("Credenciales no validas!"));
                    }

                }

            });
        }

        private void CargarProductosContados()
        {
            try
            {
                var servicios = new Services.ProductosServices();
                var Lista = new List<Productos>();
                var respuesta = servicios.TraerListaProductosContados(Usuario);

                if (respuesta.Code == 1)
                {
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
                                Resultado = i.Resultado,
                                Conteo1 = i.Conteo1,
                                Conteo2 = i.Conteo2,
                                Conteo3 = i.Conteo3
                            });
                        }

                        if (this.buttonSelect == 2)
                        {

                            listContado = Lista;
                            Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                this.lvwProductos.ItemsSource = listContado;
                            });

                        }
                        if (this.buttonSelect == 3)
                        {
                            if (Usuario.Conteo == 1) { listDiferencias = Lista.Where(x => x.Estado == "0" && x.Conteo1 == 0).ToList(); }
                            if (Usuario.Conteo == 2) { listDiferencias = Lista.Where(x => x.Estado == "0" && x.Conteo2 == 0).ToList(); }
                            if (Usuario.Conteo == 3) { listDiferencias = Lista.Where(x => x.Estado == "0" && x.Conteo3 == 0).ToList(); }

                            Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                this.lvwProductos.ItemsSource = listDiferencias;
                            });

                        }

                    }
                    else
                    {
                        Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            this.lvwProductos.ItemsSource = new List<Productos>();
                        });

                    }

                }
            }
            catch (Exception)
            {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
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
                if (this.buttonSelect == 2) //Lista de Productos de Inventario
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
                if (this.buttonSelect == 3)
                {

                    var result = listDiferencias.Where(c => c.Nombre.ToUpper().Contains(e.NewTextValue.ToString().ToUpper()));


                    if (string.IsNullOrEmpty(e.NewTextValue))
                    {
                        this.lvwProductos.ItemsSource = listDiferencias;
                    }
                    else
                    {
                        this.lvwProductos.ItemsSource = result;
                    }

                }



            }
            catch (Exception)
            {
                return;
                //throw;
            }
        }


        private void ViewCell_Tapped(object sender, EventArgs e)
        {
            if (buttonSelect == 2)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Aqui no podes guardar nada, Buscar en inventario o en los productos que tienen diferencias!");
                return;
            }

            Productos p2 = new Productos();
            tap += 1;
            p2 = p;

            p = (Productos)this.lvwProductos.SelectedItem;
            if (p2 != p)

            {
                tap = 1;
            }

            if (tap == 2)
            {

                var f = new IgresarProductosPage();
                f.CodigoProducto = p.Codigo;
                f.NombreProducto = p.Nombre;
                f.Usuario = Usuario;
                f.ProductosDiferencias = buttonSelect != 1 ? true : false;
                f.Cargar();
                Navigation.PushAsync(f, true);
            }

        }
        private void ClickVerDetalle(object sender, EventArgs e)
        {
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
                    this.btnBusqueda.Text = "";
                    Navigation.PopAsync(true);
                }

                if (buttonSelect == 3)
                {
                    this.btnBusqueda.Text = "";
                    btnDiferencias_Clicked(null, null);
                }
                if (buttonSelect == 2)
                {
                    this.btnBusqueda.Text = "";
                    btnContados_Clicked(null, null);
                }
            }
            catch (Exception)
            {
                return;
                //throw;
            }
        }


        private void btnDiferencias_Clicked(object sender, EventArgs e)
        {
            buttonSelect = 3;
            var colorDefecto = btnDiferencias.BackgroundColor;
            btnDiferencias.BackgroundColor = Color.DarkRed;
            btnContados.BackgroundColor = Color.LightGray;
            btnInventario.BackgroundColor = Color.LightGray;
            Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Refrescando!", Acr.UserDialogs.MaskType.Clear);
            Task.Run(() => CargarProductosContados());
        }


        private void btnContados_Clicked(object sender, EventArgs e)
        {
            buttonSelect = 2;
            var colorDefecto = btnContados.BackgroundColor;
            btnContados.BackgroundColor = Color.DarkRed;
            btnDiferencias.BackgroundColor = Color.LightGray;
            btnInventario.BackgroundColor = Color.LightGray;

            Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Refrescando!", Acr.UserDialogs.MaskType.Clear);
            Task.Run(() => CargarProductosContados());

        }

        private void btnInventario_Clicked(object sender, EventArgs e)
        {
            buttonSelect = 1;
            var colorDefecto = btnInventario.BackgroundColor;
            btnInventario.BackgroundColor = Color.DarkRed;
            btnDiferencias.BackgroundColor = Color.LightGray;
            btnContados.BackgroundColor = Color.LightGray;
            var list = new List<Productos>();
            this.lvwProductos.ItemsSource = this.db.GetAllProd();
        }

    }

}