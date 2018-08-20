using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LIP.Services
{
    class LoginServices
    {
        public Entidades.Respuesta LoginAsync(string NoCedula)
        {
            string Respuesta;
            Services.ServicesApi api = new ServicesApi();
            Entidades.Respuesta Resp = new Entidades.Respuesta();
            Entidades.Respuesta Resp2 = new Entidades.Respuesta();
            DataAccess bd = new DataAccess();

            try
            {
                Entidades.Auth usuario = new Entidades.Auth();
                usuario = bd.GetAllLevantado(NoCedula);

                if (usuario == null)
                {

                    Respuesta =  api.PeticionPost("/lip/api/login/login", JsonConvert.SerializeObject(NoCedula));
                    Resp = JsonConvert.DeserializeObject<Entidades.Respuesta>(Respuesta);
                    var user = new Entidades.Auth();
                    user = JsonConvert.DeserializeObject<Entidades.Auth>(Resp.Objeto.ToString());
                    user.Cedula = NoCedula;

                    if (Resp.Lista != null)
                    {
                       
                        user.Conteo =  user.Conteo > 0 ? user.Conteo - 1: user.Conteo;
                        bd.SaveLevantado(user);
                    }
                    else {
                        Resp.Code = 0;
                        Resp.Response = " Error al Conectar con el SERVER";
                    }

                }
                else
                {
                    Resp.Objeto = usuario;
                    Resp.Code = 4; //Encontrado BD Local
                }
                    return Resp;
            }
            catch (Exception)
            {
                return Resp;
                throw;
            }

        }


        public Entidades.Respuesta CerrarSession(Entidades.Auth Usuario)
        {
            string Respuesta;
            Services.ServicesApi api = new ServicesApi();
            Entidades.Respuesta Resp = new Entidades.Respuesta();
            DataAccess bd = new DataAccess();

            try
            {

                    Respuesta = api.PeticionPost("/lip/api/login/Logout", JsonConvert.SerializeObject(Usuario));
                    Resp = JsonConvert.DeserializeObject<Entidades.Respuesta>(Respuesta);
                    var user = new Entidades.Auth();
                    user = JsonConvert.DeserializeObject<Entidades.Auth>(Resp.Objeto.ToString());

                    if (Resp.Lista != null)
                    {

                        user.Conteo = user.Conteo > 0 ? user.Conteo - 1 : user.Conteo;
                        bd.SaveLevantado(user);
                    }
                    else
                    {
                        Resp.Code = 0;
                        Resp.Response = " Error al Conectar con el SERVER";
                    }
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
