using System.Collections.Generic;
using System.Threading;

namespace WhenAll
{
    public class ContextoSincronizacion<T>
    {
        public IEnumerable<T> ColeccionConcurrenteSegura { get; set; }

        public bool TiempoEsperaAgotado { get; set; }

        public ReaderWriterLock BloqueoLecturaEscritura { get; set; }
    }
}