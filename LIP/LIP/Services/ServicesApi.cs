using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace LIP.Services
{
    class ServicesApi
    {
        public string URL_API;
        public string PeticionGet(string URL)
        {
            try
            {
            string sRespuesta ;
            sRespuesta = "Esta es la respuesta";
            var rxcui = "198440";
            var request = HttpWebRequest.Create(string.Format(@"http://"+ URL_API + "/api/Productos/Guardar"));
            request.ContentType = "application/json";
            request.Method = "GET";

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode !=   HttpStatusCode.OK)
                    Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var content = reader.ReadToEnd();
                    if (string.IsNullOrWhiteSpace(content))
                    {
                        Console.Out.WriteLine("Response contained empty body...");
                    }
                    else
                    {
                        Console.Out.WriteLine("Response Body: \r\n {0}", content);
                    }

                    //Assert.NotNull(content);
                }
            }
             return sRespuesta;
            }
            catch (Exception)
            {
                return "";
                throw;
            }
        }

        public string PeticionPost(string Controlador, string Values)
        {
            try
            {
               // URL_API = "192.168.1.9";
                URL_API = App.Current.Properties["Direccion"].ToString();
                string content;
                var rxcui = "198440";
                var request = HttpWebRequest.Create(string.Format("http://"+ URL_API + Controlador, rxcui));
                request.ContentType = "application/json";
                request.Method = "POST";
                request.Timeout = 10000;
                using (System.IO.Stream s = request.GetRequestStream())
                {
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(s))
                        sw.Write(Values);
                }
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)

                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);

                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {

                        content = reader.ReadToEnd();
                        if (string.IsNullOrWhiteSpace(content))
                        {
                            Console.Out.WriteLine("Response contained empty body...");
                        }
                        else
                        {
                            Console.Out.WriteLine("Response Body: \r\n {0}", content);
                        }
                    }
                }
                return content;

            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
        }
    }
}
