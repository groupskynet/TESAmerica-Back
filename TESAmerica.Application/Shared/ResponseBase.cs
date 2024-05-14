using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TESAmerica.Application.Shared
{
    public class ResponseBase<T>
    {
        public ICollection<string>? Errores { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public T? Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }

    }
}
