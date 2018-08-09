using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Toast;
using Newtonsoft.Json;

namespace LIP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void btnLogin_Clicked(object sender, EventArgs e)
        {
            //Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Iniciando Session!", Acr.UserDialogs.MaskType.Black);
            this.txtCedula.Text = "0011810860021C";
            this.btnLogin.Text = ".........";
            var Servicios = new Services.LoginServices();
            Entidades.Respuesta Resultado;
            Entidades.Auth usuario = new Entidades.Auth();


            Resultado =  AsyncHelper.RunSync<Entidades.Respuesta>(()=> Servicios.LoginAsync(txtCedula.Text));

            //var toast = new ShowToastPopUp();
            if (Resultado.Objeto == null)
            {
                if (Resultado.Response == "")
                {
                    DisplayAlert("LIP", " Error de Conexion", "Aceptar");
                }
                else
                {
                    if (Resultado.Response == "Este Usuario tiene session activa")
                    {
                        var bd = new DataAccess();
                        usuario = bd.GetAllLevantado().FirstOrDefault();
                       // Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                        if (usuario != null)
                        {

                            var f = new MainPage();
                            f.bEnSession = true;
                            f.Usuario = usuario;
                            f.CargarDatos();
                            this.Navigation.PushAsync(f, true);
                        }
                    }
                    else
                    {
                        DisplayAlert("LIP", Resultado.Response, "Aceptar");
                        //Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                        this.txtCedula.Focus();
                    }


                }

            }
            else
            {
                usuario = JsonConvert.DeserializeObject<Entidades.Auth>(Resultado.Objeto.ToString());
                // JObject rss = JObject.Parse(Resultado.Lista.ToString());


                DisplayAlert("LIP", "Bienvenido :" + usuario.Nombre, "Aceptar");
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                var f = new MainPage();
                f.Usuario = usuario;
                f.Lista = Resultado.Lista;
                f.CargarDatos();
                this.Navigation.PushAsync(f, true);
            }


            //toast.ShowToastMessage(Resultado.Nombre);
        }
    }
}