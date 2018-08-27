using Android.Content;
using Android.Net;
using Java.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using Xamarin.Forms;

namespace LIP.Services
{
   public static class Utilidades
    {
        
        public static Boolean RevisarConexion()
        {
            ConnectivityManager connectivityManager = (ConnectivityManager)Android.App.Application.Context.GetSystemService(Context.ConnectivityService);
            NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
            var r = (activeConnection.Type == Android.Net.ConnectivityType.Wifi) && activeConnection.IsConnected;
            return r;
        }

        public static bool ConexionServerAsync()
        {
            Boolean Respuesta = new Boolean();
            Services.ServicesApi Ser = new Services.ServicesApi();
            try
            {
                var bd = new DataAccess();
                var direccion = bd.TraerDireccion().Direccion;
                if (!string.IsNullOrEmpty(direccion)){
                    if(!App.Current.Properties.ContainsKey("Direccion"))
                        App.Current.Properties.Add("Direccion", direccion);
                    else {
                        App.Current.Properties["Direccion"] = direccion;
                    }
                }
                    var resp = JsonConvert.DeserializeObject<Entidades.Respuesta>(Ser.PeticionPost("/lip/api/login/login", ""));
                    Respuesta = resp != null ?true:false;
                return Respuesta;
            }
            catch
            {
                return Respuesta;
            }
        }

        public static decimal TruncateDecimal(decimal value, int precision)
        {
            decimal step = (decimal)Math.Pow(10, precision);
            decimal tmp = Math.Truncate(step * value);
            return tmp / step;
        }


    }
}
