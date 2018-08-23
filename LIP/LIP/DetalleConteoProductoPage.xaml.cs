using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LIP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleConteoProductoPage : ContentPage
    {
        List<Entidades.DetalleEstante> Items = new List<Entidades.DetalleEstante>();
        public Entidades.DetalleLevantadoTemp Producto = new Entidades.DetalleLevantadoTemp();
        Services.ProductosServices Servicios = new Services.ProductosServices();
        Entidades.Respuesta resp = new Entidades.Respuesta();

        public DetalleConteoProductoPage()
        {
            InitializeComponent();
            this.Title = "Conteo por estantes";
        }

        public void Cargar()
        {
            try
            {
                resp = Servicios.TraerDetalleEstantes(Producto);
                if (resp.Code == 1)
                {
                    if (resp.Lista.Count > 0)
                    {

                        foreach (object i in resp.Lista)
                        {
                            Items.Add(JsonConvert.DeserializeObject<Entidades.DetalleEstante>(i.ToString()));
                        }

                        this.MyListView.ItemsSource = Items;
                        this.BindingContext = Items;
                        this.lblFooter.Text = "Total en todos los estantes : " + Items.Sum(r => Decimal.Parse(r.Resultado)).ToString();

                    }
                    else
                    {
                        this.Title = "No hay conteo para este Producto! ";

                    }

                }

                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
            }
            catch (Exception)
            {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                return;
                // throw;
            }

        }

         void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //if (e.Item == null)
            //    return;

            //await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            ////Deselect Item
            //((ListView)sender).SelectedItem = null;
        }
    }
}
