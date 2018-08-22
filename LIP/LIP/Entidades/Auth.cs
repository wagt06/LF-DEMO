using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace LIP.Entidades
{
   public  class Auth
    {
        [PrimaryKey]
        public int Codigo_Usuario { get; set; }

        public String Parcial { get; set; }
        public int Sucursal { get; set; }
        public int Bodega { get; set; }
        public String Nombre { get; set; }
        public int Codigo_Ubicacion { get; set; }
        public String Cedula { get; set; }
        public bool IsCerrado { get; set; }
        public int Conteo { get; set;  }
        public string NombreUbicacion { get; set; }
    }

    
}
