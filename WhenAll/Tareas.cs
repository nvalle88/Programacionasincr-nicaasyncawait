using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WhenAll
{
    public static class  Tareas
    {
        public static void WaitAllMethod(Task[] tasks)
        {
            Task.WaitAll(tasks);
            Console.WriteLine("WaitAllMethod---------Todas las tareas terminaron");
        }

        public static void WaitAnyMethod(Task[] tasks)
        {
            var index = Task.WaitAny(tasks);
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


        public static List<Pais> ContextoSincronizacion(IList<IServicioPais> ListaPaises)
        {
            var contextoSincronizacion = new ContextoSincronizacion<Pais>
            {
                ColeccionConcurrenteSegura = new ConcurrentBag<Pais>(),
                TiempoEsperaAgotado = false,
                BloqueoLecturaEscritura = new ReaderWriterLock()
            };
            Task.WaitAll(
               ListaPaises.Select(ta => Task.Factory.StartNew(
                       () => TareaObtenerPaises(ref contextoSincronizacion,ta),
                       CancellationToken.None,
                       TaskCreationOptions.DenyChildAttach,
                       TaskScheduler.Default))
                   .ToArray(),
               Timeout.Infinite);

            contextoSincronizacion.BloqueoLecturaEscritura.AcquireWriterLock(Timeout.Infinite);

            contextoSincronizacion.TiempoEsperaAgotado = true;

            contextoSincronizacion.BloqueoLecturaEscritura.ReleaseWriterLock();
            
            return contextoSincronizacion.ColeccionConcurrenteSegura.Distinct().ToList();

        }

        private static void TareaObtenerPaises(ref ContextoSincronizacion<Pais> contextoSincronizacion,IServicioPais servicioPais)
        {
            Console.WriteLine("ContextoSincronizacion----------Consultando: {0}", servicioPais.Nombre);
            var paises=  servicioPais.ObtenerPais();
            Console.WriteLine("ContextoSincronizacion----------Resultado de paises: {0}", paises.Count());

            contextoSincronizacion.BloqueoLecturaEscritura.AcquireReaderLock(Timeout.Infinite);

            var tiempoEsperaAgotado = contextoSincronizacion.TiempoEsperaAgotado;

            contextoSincronizacion.BloqueoLecturaEscritura.ReleaseReaderLock();

            if (!tiempoEsperaAgotado)
                contextoSincronizacion.ColeccionConcurrenteSegura =
                    contextoSincronizacion.ColeccionConcurrenteSegura.Concat(paises);
        }
    }
}
