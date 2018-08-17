using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LIP.Services
{
   public  class ProductosServices
    {
        public Entidades.Respuesta GuardarProducto(Entidades.DetalleLevantadoTemp Producto)
        {
            string Respuesta;
            Services.ServicesApi api = new ServicesApi();
            Entidades.Respuesta Resp = new Entidades.Respuesta();
            Entidades.Respuesta Resp2 = new Entidades.Respuesta();
            DataAccess bd = new DataAccess();
            try
            {
              
                Respuesta = api.PeticionPost("http://192.168.1.9/Lip/api/Productos/Guardar", JsonConvert.SerializeObject(Producto));
                Resp = JsonConvert.DeserializeObject<Entidades.Respuesta>(Respuesta);


                if (Resp.Code == 1)
                {
                    //Actualizar el conteo del Producto;
                    //var Usu = new Entidades.Auth();
                    //Usu = JsonConvert.DeserializeObject<Entidades.Auth>(Resp.Objeto.ToString());
                    //if (bd.EjecutarQueryScalar(String.Format("UPDATE Productos SET Conteo = {0}, Codigo_Ubicacion = {1} WHERE  Codigo_Usuario = {2}", Usu.Conteo, Usu.Codigo_Ubicacion, Usu.Codigo_Usuario)) == 1)
                    //{

                    //}
                }
                return Resp;
            }
            catch (Exception)
            {
                return Resp;
                throw;
            }

        }

        public Entidades.Respuesta ActualizarProducto(Entidades.DetalleLevantadoTemp Producto)
        {
            string Respuesta;
            Services.ServicesApi api = new ServicesApi();
            Entidades.Respuesta Resp = new Entidades.Respuesta();
            Entidades.Respuesta Resp2 = new Entidades.Respuesta();
            DataAccess bd = new DataAccess();
            try
            {

                Respuesta = api.PeticionPost("http://192.168.1.9/Lip/api/Productos/Actualizar", JsonConvert.SerializeObject(Producto));
                Resp = JsonConvert.DeserializeObject<Entidades.Respuesta>(Respuesta);


                if (Resp.Code == 1)
                {
                    //Actualizar el conteo del Producto;
                    //var Usu = new Entidades.Auth();
                    //Usu = JsonConvert.DeserializeObject<Entidades.Auth>(Resp.Objeto.ToString());
                    //if (bd.EjecutarQueryScalar(String.Format("UPDATE Productos SET Conteo = {0}, Codigo_Ubicacion = {1} WHERE  Codigo_Usuario = {2}", Usu.Conteo, Usu.Codigo_Ubicacion, Usu.Codigo_Usuario)) == 1)
                    //{

                    //}
                }
                return Resp;
            }
            catch (Exception)
            {
                return Resp;
                throw;
            }


        }

        public Entidades.Respuesta TraerDetalleEstantes(Entidades.DetalleLevantadoTemp Producto)
        {
            string Respuesta;
            Services.ServicesApi api = new ServicesApi();
            Entidades.Respuesta Resp = new Entidades.Respuesta();
            DataAccess bd = new DataAccess();
            try
            {

                Respuesta = api.PeticionPost("http://192.168.1.9/Lip/api/Productos/DetalleProducto", JsonConvert.SerializeObject(Producto));
                Resp = JsonConvert.DeserializeObject<Entidades.Respuesta>(Respuesta);   
                return Resp;
            }
            catch (Exception)
            {
                return Resp;
                throw;
            }

        }
    }
}
