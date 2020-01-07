using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace WhenAll
{
    class Program
    {
        public static HttpClient clientePais = new HttpClient
        {
            BaseAddress = new Uri("https://restcountries.eu/rest/v2/all"),
        };

        static void Main(string[] args)
        {
            Inicio().Wait();
        }

        public static async Task  Inicio()
        {

            var WaitAnyTasks = new  Task[2];
            WaitAnyTasks[0]=(Task.Run(() =>
            {
                ObtenerPaisAsync();
            }));

            WaitAnyTasks[1]=( Task.Run(() =>
            {
                ObtenerNombrePaisAsync();
            }));

            //---------------
            var WhenAnyTasks = new Task[2];
            WhenAnyTasks[0] = (Task.Run(() =>
            {
                ObtenerPaisAsync();
            }));

            WhenAnyTasks[1] = (Task.Run(() =>
            {
                ObtenerNombrePaisAsync();
            }));


            //------------

            var WaitAllTasks = new Task[2];
            WaitAllTasks[0] = (Task.Run(() =>
            {
                ObtenerPaisAsync();
            }));

            WaitAllTasks[1] = (Task.Run(() =>
            {
                ObtenerNombrePaisAsync();
            }));


            var WhenAllTasks = new Task[2];
            WhenAllTasks[0] = (Task.Run(() =>
            {
                ObtenerPaisAsync();
            }));

            WhenAllTasks[1] = (Task.Run(() =>
            {
                ObtenerNombrePaisAsync();
            }));

            WaitAnyMethod(WaitAnyTasks);

            WhenAnyMethod(WhenAnyTasks);

            WaitAllMethod(WaitAllTasks);

            WhenAllMethod(WhenAllTasks);

            Console.WriteLine("oj is ready");
          
            Console.ReadLine();
        }


        public static void WaitAllMethod(Task[] tasks)
        {
            Task.WaitAll(tasks);
            Console.WriteLine("WaitAllMethod---------Todas las tareas terminaron");
        }

        public static void WaitAnyMethod(Task[] tasks)
        {
            var index= Task.WaitAny(tasks);
            foreach (var t in tasks)
                Console.WriteLine("WaitAnyMethod-----------Tarea {0}: {1}", t.Id, t.Status);
        }

        public static void WhenAllMethod(Task[] tasks)
        {
            Task t = Task.WhenAll(tasks);
            try
            {
                t.Wait();
            }
            catch { }

            if (t.Status == TaskStatus.RanToCompletion)
                Console.WriteLine("WhenAllMethod ----  todas las tareas terminaron.");
            else if (t.Status == TaskStatus.Faulted)
                Console.WriteLine("{0} WhenAllMethod---------algunas de las tareas pudo fallar", 0);
        }

        public static void WhenAnyMethod(Task[] tasks)
        {
            var index = Task.WaitAny(tasks);
            foreach (var t in tasks)
                Console.WriteLine("WhenAnyMethod----------Tarea {0}: {1}", t.Id, t.Status);
        }
        
        
        public static async Task<string> ObtenerPaisAsync()
        {
            for (int i = 0; i < 2000000000; i++)
            {
                var d = i;
            }
           var httpResponse = await clientePais.GetAsync("rest/v2/all");
            if (httpResponse.IsSuccessStatusCode)
            {
                return await httpResponse.Content.ReadAsStringAsync();
            }
            return string.Empty;
        }

        public static async Task<string> ObtenerNombrePaisAsync()
        {

            var httpResponse = await clientePais.GetAsync("rest/v2/name/united");
            if (httpResponse.IsSuccessStatusCode)
            {
                return await httpResponse.Content.ReadAsStringAsync();
            }
            return string.Empty;
        }
    }
}
