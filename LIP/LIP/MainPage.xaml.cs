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
		public MainPage()
		{
			InitializeComponent();
            var c = new Conexion();
            this.Sucursales.ItemsSource = c.ListaElementos();
        }
        private void Iniciar()
        {

            var f = new BuscarProductoPage();
           this.Navigation.PushAsync(f,true);
        
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Iniciar();
        }
    }

   
}
