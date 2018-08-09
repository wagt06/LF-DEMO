using System;
using System.Collections.Generic;
using System.Text;

namespace LIP.Entidades
{
  
    public class Respuesta
    {
        public string Response { get; set; }
        public object Objeto { get; set; }
        public List<object> Lista { get; set; }
        public int Code { get; set; }
    }
}
