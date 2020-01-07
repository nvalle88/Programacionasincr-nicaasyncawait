using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WhenAll
{
   

    public static class Metodos
    {

        private static HttpClient clientePais = new HttpClient
        {
            BaseAddress = new Uri("https://restcountries.eu/"),
        };
        
        public static async Task<string> ObtenerPaisAsyncContentString()
        {
            var httpResponse = await clientePais.GetAsync("rest/v2/all");
            if (httpResponse.IsSuccessStatusCode)
            {
                return await httpResponse.Content.ReadAsStringAsync();
            }
            return string.Empty;
        }

        public static async Task<string> ObtenerNombrePaisAsyncContentString()
        {
            var httpResponse = await clientePais.GetAsync("rest/v2/name/united");
            if (httpResponse.IsSuccessStatusCode)
            {
                return await httpResponse.Content.ReadAsStringAsync();
            }
            return string.Empty;
        }

        public static async Task<List<Pais>> ObtenerPaisAsync()
        {
            var httpResponse = await clientePais.GetAsync("rest/v2/all");
            if (httpResponse.IsSuccessStatusCode)
            {
                var data= await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Pais>>(data);
            }
            return new List<Pais>();
        }

        public static async Task<List<Pais>> ObtenerNombrePaisAsync()
        {

            var httpResponse = await clientePais.GetAsync("rest/v2/name/united");
            if (httpResponse.IsSuccessStatusCode)
            {
                var data = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Pais>>(data);
            }
            return new List<Pais>();
        }


        public static void TareaObtenerPaises(ref ContextoSincronizacion<Pais> contextoSincronizacion, IServicioPais servicioPais)
        {
            Console.WriteLine("ContextoSincronizacion----------Consultando: {0}", servicioPais.Nombre);
            var paises = servicioPais.ObtenerPais();
            Console.WriteLine("ContextoSincronizacion----------Resultado de paises: {0}", arg0: paises.Count());

            contextoSincronizacion.BloqueoLecturaEscritura.AcquireReaderLock(Timeout.Infinite);

            var tiempoEsperaAgotado = contextoSincronizacion.TiempoEsperaAgotado;

            contextoSincronizacion.BloqueoLecturaEscritura.ReleaseReaderLock();

            if (!tiempoEsperaAgotado)
                contextoSincronizacion.ColeccionConcurrenteSegura =
                    contextoSincronizacion.ColeccionConcurrenteSegura.Concat(paises);
        }
    }
}
