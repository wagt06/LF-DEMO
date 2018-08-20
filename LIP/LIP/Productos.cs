using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace LIP
{
    public class Productos
    {

        [PrimaryKey]
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public double Resultado { get; set; }
        public string Estado { get; set; }
        public double Conteo1 { get; set; }
        public double Conteo2 { get; set; }
        public double Conteo3 { get; set; }
    }
}