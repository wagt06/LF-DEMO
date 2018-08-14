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

        public string PeticionGet(string URL)
        {
            try
            {
            string sRespuesta ;
            sRespuesta = "Esta es la respuesta";
            var rxcui = "198440";
            var request = HttpWebRequest.Create(string.Format(@"http://192.168.1.9:45455/api/Productos/Guardar"));
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

        public string PeticionPost(string URL, string Values)
        {
            try
            {

                string content;
                HttpWebResponse Respuesta;


                var rxcui = "198440";
                var request = HttpWebRequest.Create(string.Format(URL, rxcui));
                request.ContentType = "application/json";
                request.Method = "POST";
                request.Timeout = 10000;
                //request.ContentType = "Application/x-www-form-urlencoded";
                using (System.IO.Stream s = request.GetRequestStream())
                {
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(s))
                        sw.Write(Values);
                }
                //using (HttpWebResponse response = (request.AsyncState as HttpWebRequest).EndGetResponse(request) as HttpWebResponse)
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
