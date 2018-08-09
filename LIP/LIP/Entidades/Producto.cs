using System;
using System.Collections.Generic;
using System.Text;

namespace LIP.Entidades
{
    class Producto
    {
            public int CodigoSucursal { get; set; }
            public int Codigo_Factura { get; set; }
            public int Bodega { get; set; }
         
            public string Codigo_Producto { get; set; }
            public int Numero { get; set; }
            public Single Cantidad { get; set; }
            public int Precio_Unitario { get; set; }
            public Boolean Cargado { get; set; }

            public int Codigo_Ubicacion { get; set; }
            public int Codigo_Usuario { get; set; }


            public Single Conteo1 { get; set; }
            public int UC1 { get; set; }
            public Single Conteo2 { get; set; }
            public int UC2 { get; set; }
            public Single Conteo3 { get; set; }
            public int UC3 { get; set; }

            public Single Resultado { get; set; }
            public int Tipo_Origen { get; set; }

    }
}
