
using Android.Widget;
using Newtonsoft.Json;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace LIP
{
	public partial class MainPage : ContentPage
	{
        public Entidades.Auth Usuario = new Entidades.Auth();
        public List<object>  Lista = new List<object>();
        public Boolean bEnSession = new Boolean();
        public List<Entidades.Lista> l = new List<Entidades.Lista>();
        Services.EstantesServices Servicio = new Services.EstantesServices();
        Boolean EstanteActivo = new Boolean();
        Boolean VienededeLogin = new Boolean();
        ShowToastPopUp t = new ShowToastPopUp();
        DataAccess bd = new DataAccess();

        public MainPage()
		{
			InitializeComponent();
            var c = new Conexion();

           
        }
        public void CargarDatos() {
            try
            {
                
                    Entidades.Auth item = new Entidades.Auth();
                    Entidades.Respuesta Respuesta = new Entidades.Respuesta();
                    BuscarProductoPage f = new BuscarProductoPage();

                    var Conteo = string.Empty;
                 Conteo = Servicio.Conteo(Usuario);
                 Respuesta = Servicio.TraerUbicaciones(Usuario);

                 if (Respuesta.Code == 1) {
                        Lista = Respuesta.Lista;
                 }
          

                if (Usuario.IsCerrado)
                {
                    Usuario.Codigo_Ubicacion = 0;
                }

                if ((int.Parse(Conteo)) -1 != Usuario.Conteo)
                    {
                                Lista = Respuesta.Lista;
                                Usuario.Conteo = (int.Parse(Conteo)) - 1;
                                Usuario.Codigo_Ubicacion = 0;
                                bd.EjecutarQueryScalar(string.Format("UPDATE Usuario SET  Conteo={0}, isCerrado= 0  WHERE Codigo_Usuario ={1}", (int.Parse(Conteo) - 1), Usuario.Codigo_Usuario));
                    }

                l.Clear();
               // this.Estantes.ItemsSource = null;
                this.BindingContext = null;
                //if (this.Estantes.Items.Count > 0) {
                //    this.Estantes.Items.Clear();
                //}

                foreach (object i in Lista)
                {
                  l.Add(JsonConvert.DeserializeObject<Entidades.Lista>(i.ToString()));
                }
                
                this.BindingContext = l;

                this.Estantes.ItemsSource = l;
                this.lblBodega.Text = Usuario.Bodega.ToString();
                this.lblSucursal.Text = Usuario.Sucursal.ToString();
                this.lblUsuario.Text = Usuario.Nombre;


                if (this.Usuario.Codigo_Ubicacion != 0)
                {
                    this.EstanteActivo = true;
                }
                else
                {
                    this.EstanteActivo = false;
                }

                if (this.EstanteActivo)
                {
                    this.btnContar.Text = "Seguir Contando ";
                    this.btnContar.IsVisible = true;
                    this.Estantes.IsVisible = false;
                    this.lblEstante.Text = Usuario.Codigo_Ubicacion.ToString();
                }
                else
                {
                    this.Estantes.IsVisible = true;
                    this.btnContar.IsVisible = false;
                    this.btnContar.Text = "Iniciar Conteo";
                    this.lblEstante.Text = "Estante No Seleccionado";
                }

            }
            catch (Exception)
            {

                // throw;
                DisplayAlert("LIP", " Error de Conexion", "Aceptar");
                return;
            }
          
        }

        private void Iniciar()
        {

            // var f = new BuscarProductoPage();
            //this.Navigation.PushAsync(f,true);
           // Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Iniciando Session!", Acr.UserDialogs.MaskType.Black);

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            BuscarProductoPage f = new BuscarProductoPage();
            f.Usuario = Usuario;
            f.Load();
            this.Navigation.PushAsync(f, true);
            if (f.isCerrardo == true)
            {
                this.Estantes.IsVisible = true;
                this.btnContar.IsVisible = false;
            }
            else {
                this.btnContar.IsVisible = true;

            }
        }

        private void Estantes_SelectedIndexChanged(object sender, EventArgs e)
        {
           

            Entidades.Auth item = new Entidades.Auth();
            Entidades.Respuesta Respuesta = new Entidades.Respuesta();
            var r = (Entidades.Lista)this.Estantes.SelectedItem;
         

            item = Usuario;
            item.Codigo_Ubicacion = r.Codigo_Ubicacion;

            Respuesta = Servicio.SeleccionarEstantes(item);
            if (Respuesta.Code == 1)
            {
                BuscarProductoPage f = new BuscarProductoPage();
                f.Usuario = item;
                f.Load();
                this.Navigation.PushAsync(f, true);
            }
            else {
                if (Respuesta.Code == 0)
                {


                }
                else
                {
                    //var t = new ShowToastPopUp();
                    //t.ShowToastMessage("Error de Conexion");
                }
            }

            }

     
        protected override void OnAppearing()
        {
            base.OnAppearing();
           
            try
            {
                if (!this.VienededeLogin) //si viene desde el login no entra
                {
                    Entidades.Respuesta Respuesta = new Entidades.Respuesta();
                    Usuario = bd.GetAllLevantado(Usuario.Cedula);
                    if (Usuario.IsCerrado) {
                        Usuario.Codigo_Ubicacion = 0;
                    }
                    CargarDatos();
                }
            }
            catch (Exception)
            {
                DisplayAlert("LIP", " Error de Conexion", "Aceptar");
                throw;
            }
        }

        private void ActEstantes_Clicked(object sender, EventArgs e)
        {
            CargarDatos();
        }
    }
    }
