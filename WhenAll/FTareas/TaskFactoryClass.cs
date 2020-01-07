using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace WhenAll
{
    public static class TaskFactoryClass
    {

        static CancellationTokenSource cts = new CancellationTokenSource();
        static TaskFactory factory = new TaskFactory(
            cts.Token,
            TaskCreationOptions.PreferFairness,
            TaskContinuationOptions.ExecuteSynchronously,
            TaskScheduler.Current);

        public static void EjemploLeerDirectorio()
        {
            Task[] tasks = new Task[2];
            string[] files = null;
            string[] dirs = null;
            string docsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            tasks[0] = Task.Factory.StartNew(() => files = Directory.GetFiles(docsDirectory));
            tasks[1] = Task.Factory.StartNew(() => dirs = Directory.GetDirectories(docsDirectory));

            Task.Factory.ContinueWhenAll(tasks, completedTasks =>
            {
                Console.WriteLine("{0} contains: ", docsDirectory);
                Console.WriteLine("   {0} subdirectories", dirs.Length);
                Console.WriteLine("   {0} files", files.Length);
            });

        }

       public static void CancellationToken()
        {
            var t2 = factory.StartNew(() => Metodos.ObtenerPaisAsyncContentString());
            cts.Dispose();
        }


        public static List<Pais> ContinueWhenAll()
        {
            // Schedule a list of tasks that return integer
            Task<string>[] tasks = new Task<string>[]
                {
                Task<string>.Factory.StartNew(() =>
                    {
                        Thread.Sleep(500);
                        Console.WriteLine("Task={0}, Thread={1}, x=5", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
                        return Metodos.ObtenerNombrePaisAsyncContentString().Result;
                    }),

                Task<string>.Factory.StartNew(() =>
                    {
                        Thread.Sleep(10);
                        Console.WriteLine("Task={0}, Thread={1}, x=3", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
                        return  Metodos.ObtenerPaisAsyncContentString().Result;
                    }),

                Task<string>.Factory.StartNew(() =>
                    {
                        Thread.Sleep(20000);
                        Console.WriteLine("Task={0}, Thread={1}, x=2", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
                        return Metodos.ObtenerPaisAsyncContentString().Result;
                    })
                };

            // Schedule a continuation to indicate the result of the first task to complete
            //Task.Factory.ContinueWhenAny(tasks, winner =>
            //{
            //    // You would expect winning result = 3 on multi-core systems, because you expect
            //    // tasks[1] to finish first.
            //    Console.WriteLine("Task={0}, Thread={1} (ContinueWhenAny): Winning result = {2}", Task.CurrentId, Thread.CurrentThread.ManagedThreadId, winner.Result);
            //});

            var lista = new List<Pais>();
            // Schedule a continuation that sums up the results of all tasks, then wait on it.
            // The list of antecendent tasks is passed as an argument by the runtime.
            Task.Factory.ContinueWhenAll(tasks,
                (antecendents) =>
                {
                    int sum = 0;
                    foreach (Task<string> task in antecendents)
                    {
                        lista.AddRange( JsonConvert.DeserializeObject<List<Pais>>(task.Result));
                    }

                    Console.WriteLine("Task={0}, Thread={1}, (ContinueWhenAll): Total={2} (expected 10)", Task.CurrentId, Thread.CurrentThread.ManagedThreadId, sum);
                })
                .Wait();

            return lista;
        }

    }
}
