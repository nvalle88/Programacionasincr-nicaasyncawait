using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhenAll
{
    public static class Inicializar
    {
        public static Task[] WaitAnyTasks=  new Task[2];

        public static Task[] WhenAnyTasks = new Task[2];

        public static Task[] WaitAllTasks = new Task[2];

        public static Task[] WhenAllTasks = new Task[2];

        public static async Task Inicio()
        {
            WaitAnyTasks[0] = (Task.Run( () =>
            {
                 Metodos.ObtenerPaisAsyncContentString();
            }));

            WaitAnyTasks[1] = (Task.Run( () =>
            {
                Metodos.ObtenerNombrePaisAsyncContentString();
            }));

            WhenAnyTasks[0] = (Task.Run( () =>
            {
                Metodos.ObtenerPaisAsyncContentString();
            }));

            WhenAnyTasks[1] = (Task.Run( () =>
            {
                Metodos.ObtenerNombrePaisAsyncContentString();
            }));

            WaitAllTasks[0] = (Task.Run( () =>
            {
                 Metodos.ObtenerPaisAsyncContentString();
            }));

            WaitAllTasks[1] = (Task.Run( () =>
            {
                 Metodos.ObtenerNombrePaisAsyncContentString();
            }));

            WhenAllTasks[0] = (Task.Run( () =>
            {
                 Metodos.ObtenerPaisAsyncContentString();
            }));

            WhenAllTasks[1] = (Task.Run( () =>
            {
                 Metodos.ObtenerNombrePaisAsyncContentString();
            }));


            ClaseListaPaises.AdicionarPais(new Pais_0());

            ClaseListaPaises.AdicionarPais(new Pais_1());

           

            Console.WriteLine("Tareas cargadas...");

            Console.WriteLine("Ejecutando proceso...");
        }

    }
}
