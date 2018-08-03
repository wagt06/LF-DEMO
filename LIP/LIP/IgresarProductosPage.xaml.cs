using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
namespace LIP
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IgresarProductosPage : ContentPage
	{
        public String codigo;
        public String Nombre = "";

		public IgresarProductosPage ()
		{
			InitializeComponent ();
         
		}
        public void Cargar() {
            this.btnCodigo.Text = codigo;
            this.lblNombre.Text = Nombre;
        }

        async void btnEscanear_Clicked(object sender, EventArgs e)
        {
            var scann = new ZXingBarcodeImageView();
            var pagina = new ZXingScannerPage();
            pagina.AutoFocus();
            pagina.HasTorch = true;
            pagina.Title = "Escaneando codigo de barra";

            //setup options
            var options = new ZXing.Mobile.MobileBarcodeScanningOptions
            {
                AutoRotate = false,
                UseFrontCameraIfAvailable = true,
                TryHarder = true,
                PossibleFormats = new List<ZXing.BarcodeFormat>
                        {
                           ZXing.BarcodeFormat.EAN_8, ZXing.BarcodeFormat.EAN_13
                        }
                };

             var opciones = new ZXingScannerPage(options);  
            

            await Navigation.PushAsync(pagina);

            pagina.OnScanResult += (resultado) =>
            {
                pagina.IsScanning = false;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    lblResultado.Text = resultado.Text;
                });
            };
        }

        void btnGuardar_Clicked(object sender, EventArgs e) {

            DisplayAlert("LIP", "Se a guardado el conteo", "Aceptar");
        }
    }
}