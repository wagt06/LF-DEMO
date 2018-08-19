using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace LIP.Entidades
{
    public class DetalleLevantadoTemp
    {
    
        public int CodigoSucursal { get; set; }
        public string Codigo_Factura { get; set; }
        public int Bodega { get; set; }

        public string Codigo_Producto { get; set; }
        public int Numero { get; set; }
        public double Cantidad { get; set; }
        public decimal Precio_Unitario { get; set; }
        public Boolean Cargado { get; set; }

        public int Codigo_Ubicacion { get; set; }
        public int Codigo_Usuario { get; set; }
        public double Conteo1 { get; set; }
        public int UC1 { get; set; }
        public double Conteo2 { get; set; }
        public int UC2 { get; set; }
        public double Conteo3 { get; set; }
        public int UC3 { get; set; }
        public double Resultado { get; set; }
        public int Tipo_Origen { get; set; }
        public string Codigo_Barra { get; set; }
        public int Orden { get; set; }
    }
}

