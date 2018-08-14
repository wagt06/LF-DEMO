using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LIP.Services
{
    class LoginServices
    {
        public async System.Threading.Tasks.Task<Entidades.Respuesta> LoginAsync(string NoCedula)
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

                    Respuesta = api.PeticionPost("http://192.168.1.9/lip/api/login/login", JsonConvert.SerializeObject(NoCedula));
                    Resp = JsonConvert.DeserializeObject<Entidades.Respuesta>(Respuesta);
                    var user = new Entidades.Auth();
                    user = JsonConvert.DeserializeObject<Entidades.Auth>(Resp.Objeto.ToString());
                    user.Cedula = NoCedula;

                    if (Resp.Lista != null)
                    {

                        bd.SaveLevantado(user);
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
    }
}
