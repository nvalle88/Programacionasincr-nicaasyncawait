using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhenAll;

namespace WhenAll
{
    public interface IServicioPais
    {
        string Nombre { get;}
        List<Pais> ObtenerPais();
    }
}
