using LIP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LIP.ConfPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfPage : ContentPage
    {
        DataAccess bd = new DataAccess();
        Entidades.Conf c = new Entidades.Conf();
        public ConfPage()
        {
            InitializeComponent();
            //TraerDireccion

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        
            c = bd.TraerDireccion();
            if (c != null) {
                this.txtDireccion.Text = c.Direccion;
            }
            
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                int r;
                if (string.IsNullOrEmpty(this.txtDireccion.Text)) {
                    DisplayAlert("LIP - PAISAS","Ingrese una dirección valida!","ok");
                    this.txtDireccion.Focus();
                    return;
                }
                if (c == null)
                {
                   r = bd.SaveConf(new Entidades.Conf { Direccion = this.txtDireccion.Text });
                }
                else
                {
                    c.Direccion = this.txtDireccion.Text;
                   r = bd.UpdateConf(c);
                }

                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Revisando Conexión!", Acr.UserDialogs.MaskType.Clear);
                Task.Run(()=>RevisarConexion());
            }
            catch (Exception)
            {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                return;
            }
        }

        private void RevisarConexion()
        {
                 if (!Utilidades.ConexionServerAsync())
                {
                    Device.BeginInvokeOnMainThread( () =>
                    {
                        Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                        Acr.UserDialogs.UserDialogs.Instance.Toast("Revise que la direccion este Correcta, no hay conexión !");
                        return;
                    });
                }
                else {
                    Device.BeginInvokeOnMainThread( () =>
                    {
                        Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                        Acr.UserDialogs.UserDialogs.Instance.Toast("Se guardo la Dirección y la conexion fue realizada con exito!");
                        return;
                    });
               
                }
        }

    }
}