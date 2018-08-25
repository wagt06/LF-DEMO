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
             
                if (r == 0) {
                    Acr.UserDialogs.UserDialogs.Instance.Toast("Ocurrio Un error al guardar");
                }
                else {
                    Acr.UserDialogs.UserDialogs.Instance.Toast("Se guardo la Dirección");
                }

            }
            catch (Exception)
            {
                return;
            }
        }
    }
}