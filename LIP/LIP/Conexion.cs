using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace LIP
{
    public class Conexion
    {
        //private string ConnectionString = @"Data Source = 192.168.1.256; Initial Catalog = Prueba ; Persist Security Info = True; User ID = wagt";
        private SqlConnection Con = new SqlConnection();
        public Boolean Conectar(string ConnectionString)  {
            var respuesta = new Boolean();
            respuesta = false;
            try
            {
                Con.ConnectionString = ConnectionString;
                if (Con.State == ConnectionState.Closed)
                {
                    Con.Open();
                    respuesta = true;
                }
                return respuesta;
            }
            catch (Exception)
            {
                respuesta = false;
                return respuesta;
                throw;
            }
        }
        public List<string> ListaElementos()
        {
            var list = new List<string>();
            list.Add("Paisas S.A");
            list.Add("Sucursal Satellite");
            list.Add("Sucursal Huembes");
            list.Add("Sucursal Encajes");
            return list;
        }

        //public List<Productos> ListaProductos()
        //{
        //    var list = new List<Productos>();
        //    list.Add(new Productos { Codigo = 1, Nombre = "Amoxicilina", Cantidad = 22 });
        //    list.Add(new Productos { Codigo = 2, Nombre = "Hiboprofeno", Cantidad = 22 });
        //    list.Add(new Productos { Codigo = 3, Nombre = "Panadol", Cantidad = 22 });
        //    list.Add(new Productos { Codigo = 4, Nombre = "Curitas", Cantidad = 22 });
        //    list.Add(new Productos { Codigo = 5, Nombre = "Guarito", Cantidad = 22 });
        //    list.Add(new Productos { Codigo = 5, Nombre = "Victorias", Cantidad = 22 });
        //    list.Add(new Productos { Codigo = 5, Nombre = "Guarito", Cantidad = 22 });
        //    list.Add(new Productos { Codigo = 5, Nombre = "Solucion Salina", Cantidad = 22 });
        //    list.Add(new Productos { Codigo = 5, Nombre = "Piroxidil", Cantidad = 22 });
        //    list.Add(new Productos { Codigo = 5, Nombre = "Ranitidina", Cantidad = 22 });
        //    list.Add(new Productos { Codigo = 5, Nombre = "Amoxicilina", Cantidad = 22 });
        //    list.Add(new Productos { Codigo = 5, Nombre = "Mariadinato", Cantidad = 22 });
        //    list.Add(new Productos { Codigo = 5, Nombre = "Redosa", Cantidad = 22 });
        //    list.Add(new Productos { Codigo = 5, Nombre = "Samsumg", Cantidad = 22 });
        //    return list;
        //}
    }

    //public class Productos {
    //    public int Codigo { get; set; }
    //    public string Nombre { get; set; }
    //    public Decimal Cantidad { get; set; }
    //}


}
