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
        public int CodSucursal { get; set; }
        public int CodBodega { get; set; }
        public Single Conteo1 { get; set; }
        public Single Conteo2 { get; set; }
        public Single Conteo3 { get; set; }
        public Single Conteo4 { get; set; }
    }
}