
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
        ShowToastPopUp t = new ShowToastPopUp();

        public MainPage()
		{
			InitializeComponent();
            var c = new Conexion();

           
        }
        public void CargarDatos() {
            try
            {
                if (bEnSession)
                {
                    Entidades.Auth item = new Entidades.Auth();
                    Entidades.Respuesta Respuesta = new Entidades.Respuesta();
                    BuscarProductoPage f = new BuscarProductoPage();

                    var Conteo = string.Empty;
                    Conteo = Servicio.Conteo(Usuario);
                    Respuesta = Servicio.TraerUbicaciones(Usuario);
                    DataAccess bd = new DataAccess();

                    if (Conteo != Usuario.Conteo.ToString())
                    {
                        
                        if (Respuesta.Lista != null)
                            {
                                var user = new Entidades.Auth();
                                user = JsonConvert.DeserializeObject<Entidades.Auth>(Respuesta.Objeto.ToString());
                                Lista = Respuesta.Lista;
                                bd.EjecutarQueryScalar(string.Format("UPDATE Conteo={0}, Codigo_Ubicacion={1}  WHERE Codigo_Usuario ={2}", user.Conteo, user.Codigo_Ubicacion, user.Codigo_Usuario));
                         }

                    }
                    else //si el conteo Es el mismo lo enviamos a estante actual
                     {

                        EstanteActivo = true;
                        this.Estantes.IsVisible = false;
                            
                      }
                    //else
                    //{
                    //    if (Respuesta.Code == 0)
                    //    {
                    //        t.ShowToastMessage("Ocurrio un error");
                    //        Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Iniciando Session!", Acr.UserDialogs.MaskType.Black);
                    //        //this.Navigation.RemovePage(this);
                    //    }
                    //}

                }

                    foreach (object i in Lista)
                    {
                        l.Add(JsonConvert.DeserializeObject<Entidades.Lista>(i.ToString()));
                    }

             
               

                //this.Estantes.ItemsSource = l;
                //Entidades.ListModel listaModel = new Entidades.ListModel();
                //listaModel.listas = l;
                this.BindingContext = l;

                //this.Estantes.ItemDisplayBinding 
                this.Estantes.ItemsSource = l;
                this.lblBodega.Text = Usuario.Bodega.ToString();
                this.lblSucursal.Text = Usuario.Sucursal.ToString();
                this.lblUsuario.Text = Usuario.Nombre;
                if (this.EstanteActivo)
                {
                    this.btnContar.Text = "Seguir Contando ";
                }
                else
                {
                    this.btnContar.Text = "Iniciar Conteo";
                }
            }
            catch (Exception)
            {

                // throw;
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
                    var t = new ShowToastPopUp();
                    t.ShowToastMessage("Error de Conexion");
                }                }

            }
                  
        }
    }
