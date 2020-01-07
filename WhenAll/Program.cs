using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace WhenAll
{
    class Program
    {

        static void Main(string[] args)
        {

            Inicializar.Inicio().Wait();

            Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Reset();
            //stopwatch.Start();
            //Tareas.WaitAnyMethod(Inicializar.WaitAnyTasks);
            //stopwatch.Stop();
            //Console.WriteLine(stopwatch.ElapsedMilliseconds);


            //stopwatch.Reset();
            //stopwatch.Start();
            //Tareas.WhenAnyMethod(Inicializar.WhenAnyTasks);
            //stopwatch.Stop();
            //Console.WriteLine(stopwatch.ElapsedMilliseconds);

            //stopwatch.Reset();
            //stopwatch.Start();
            //Tareas.WaitAllMethod(Inicializar.WaitAllTasks);
            //stopwatch.Stop();
            //Console.WriteLine(stopwatch.ElapsedMilliseconds);

            //stopwatch.Reset();
            //stopwatch.Start();
            //Tareas.WhenAllMethod(Inicializar.WhenAllTasks);
            //stopwatch.Stop();
            //Console.WriteLine(stopwatch.ElapsedMilliseconds);


            stopwatch.Reset();
            stopwatch.Start();
            var d=Tareas.ContextoSincronizacion(ClaseListaPaises.ListaPaises);
            Console.WriteLine("ContextoSincronizacion----------Tital de  paises: {0}", d.Count());
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);


            Console.ReadLine();
        }
    }
}
