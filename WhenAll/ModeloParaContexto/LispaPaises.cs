using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhenAll
{

    public static class ClaseListaPaises
    {
        public static List<IServicioPais> ListaPaises { get; set; } = new List<IServicioPais>();

        public static void AdicionarPais(IServicioPais pais)
        {
            try
            {
                ListaPaises.Add(pais);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
