using Android.Content;
using Android.Net;
using Java.Net;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace LIP.Services
{
   public static class Utilidades
    {
        public static Boolean RevisarConexion()
        {
            ConnectivityManager connectivityManager = (ConnectivityManager)Android.App.Application.Context.GetSystemService(Context.ConnectivityService);
            NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
            var r = (activeConnection.Type == Android.Net.ConnectivityType.Wifi) && activeConnection.IsConnected;
            if (!r) {
                r = ConexionServerAsync();
            }
            return r;
        }

        public static bool ConexionServerAsync()
        {
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
                URL myUrl = new URL("http://"+ direccion);
                URLConnection connection = myUrl.OpenConnection();
                connection.ConnectTimeout = 3000;
                connection.ConnectAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

      

    }
}
