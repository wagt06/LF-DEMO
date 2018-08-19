using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LIP.Services
{
    class EstantesServices
    {
        public Entidades.Respuesta SeleccionarEstantes(Entidades.Auth Usuario) {
            string Respuesta;
            Services.ServicesApi api = new ServicesApi();
            Entidades.Respuesta Resp = new Entidades.Respuesta();
            Entidades.Respuesta Resp2 = new Entidades.Respuesta();
            DataAccess bd = new DataAccess();
            try
            {
                Respuesta = api.PeticionPost("/lip/api/Ubicaciones/Ubicacion", JsonConvert.SerializeObject(Usuario));
                Resp = JsonConvert.DeserializeObject<Entidades.Respuesta>(Respuesta);


                if (Resp.Code == 1)
                {
                    if (bd.EjecutarQueryScalar(String.Format("UPDATE Auth SET Conteo = {0}, Codigo_Ubicacion = {1},isCerrado = 0 WHERE  Codigo_Usuario = {2}", Usuario.Conteo, Usuario.Codigo_Ubicacion, Usuario.Codigo_Usuario)) == 1) {
                      
                    }
                }
                return Resp;
            }
            catch (Exception)
            {
                return Resp;
                //throw;
            }

        }

        public Entidades.Respuesta TraerUbicaciones(Entidades.Auth Usuario)
        {
            string Respuesta;
            Services.ServicesApi api = new ServicesApi();
            Entidades.Respuesta Resp = new Entidades.Respuesta();
            Entidades.Respuesta Resp2 = new Entidades.Respuesta();
            DataAccess bd = new DataAccess();
            try
            {
                Respuesta = api.PeticionPost("/lip/api/Ubicaciones/ObtenerUbicacion", JsonConvert.SerializeObject(Usuario));
                Resp = JsonConvert.DeserializeObject<Entidades.Respuesta>(Respuesta);

                return Resp;
            }
            catch (Exception)
            {
                return Resp;
                throw;
            }

        }

        //CerrarUbicacion
        public Entidades.Respuesta CerrarUbicacion(Entidades.Auth Usuario)
        {
            string Respuesta;
            Services.ServicesApi api = new ServicesApi();
            Entidades.Respuesta Resp = new Entidades.Respuesta();
            Entidades.Respuesta Resp2 = new Entidades.Respuesta();
            DataAccess bd = new DataAccess();
            try
            {
                Respuesta = api.PeticionPost("/lip/api/Ubicaciones/CerrarUbicacion", JsonConvert.SerializeObject(Usuario));
                Resp = JsonConvert.DeserializeObject<Entidades.Respuesta>(Respuesta);

                return Resp;
            }
            catch (Exception)
            {
                return Resp;
                throw;
            }

        }

        public string Conteo(Entidades.Auth Usuario)
        {
            string Respuesta;
            Services.ServicesApi api = new ServicesApi();

            Entidades.Respuesta Resp2 = new Entidades.Respuesta();
            DataAccess bd = new DataAccess();
            try
            {
                Respuesta = api.PeticionPost("/lip/api/login/Conteo", JsonConvert.SerializeObject(Usuario));

                return JsonConvert.DeserializeObject<string>(Respuesta);


            }
            catch (Exception)
            {
                return "";
                throw;
            }

        }
    }
}
