﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Toast;
using Newtonsoft.Json;

namespace LIP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        ShowToastPopUp toast = new ShowToastPopUp();
        Boolean Session = new Boolean();
        public LoginPage()
        {
            InitializeComponent();
        }

        private void btnLogin_ClickedAsync(object sender, EventArgs e)
        {
            if (this.txtCedula.Text != "" || this.txtCedula.Text.Length > 0)
            {
                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Iniciando Session!", Acr.UserDialogs.MaskType.Clear);
                Task.Run(() => this.IniciarSessionAsync());
            }
            else {
                toast.ShowToastMessage("Escriba sus datos para iniciar Sessión!");
                this.txtCedula.Focus();
            }
          

        }
        private  void IniciarSessionAsync() {
            try
            {

                var Servicios = new Services.LoginServices();
                Entidades.Respuesta Resultado;
                Entidades.Auth usuario = new Entidades.Auth();


                Resultado = Servicios.LoginAsync(txtCedula.Text);


                if (Resultado.Code == 1) //Repuesta desde Servidor
                {

                    if (Resultado.Objeto != null) {
                  
                        usuario = JsonConvert.DeserializeObject<Entidades.Auth>(Resultado.Objeto.ToString());
                        usuario.Conteo = usuario.Conteo - 1;
                        var f = new MainPage();
                        f.Usuario = usuario;

                        f.Lista = Resultado.Lista;
                       // f.CargarDatos();

                        Device.BeginInvokeOnMainThread(async () =>
                        {
                           // Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                            await DisplayAlert("LIP", "Bienvenido :" + usuario.Nombre, "Aceptar");
                            await this.Navigation.PushAsync(f, true);
                        });
                    }
                    else
                    {
                        if (Resultado.Response == "Este Usuario tiene session activa")
                        {
                            var bd = new DataAccess();
                            var usu = new Entidades.Auth();
                            usu = bd.GetAllLevantado(txtCedula.Text);

                            
                            if (usuario != null)
                            {
                                var f = new MainPage();
                                f.bEnSession = true;
                                f.Usuario = usuario;
                               // f.CargarDatos();

                               // Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    await this.Navigation.PushAsync(f, true);
                                });
                            }
                            else 
                            {
                                Session = false;
                                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    await DisplayAlert("LIP", "Este Usuario Tienen una session Abierta en un dispositivo, favor cerrar la sesión", "Ok");
                                    return;

                                });
                            }
                        }
                    }
                    
                }
                if (Resultado.Code == 4)//Encontrado en BD
                {
                    var f = new MainPage();
                    f.bEnSession = true;
                    f.Usuario = (Entidades.Auth)Resultado.Objeto;
                    //f.CargarDatos();

                    //Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await this.Navigation.PushAsync(f, true);
                    });
                }

                if (Resultado.Code == 3) //Error de Conexion BD
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await DisplayAlert("LIP", " Error de Conexion", "Aceptar");
                        return;
                    });
                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                }
                if (Resultado.Code == 0) //Error de Conexion Servidor
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await DisplayAlert("LIP",Resultado.Response , "Aceptar");
                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                    return;
                    });
                }

            }
            catch (Exception)
            {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                Device.BeginInvokeOnMainThread(async () =>
                {
                   await  DisplayAlert("LIP", "Ocurrio un Error", "Ok");
                    return;

                });
               
               // throw;
            }

        }
    }
}