﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhenAll
{
    public class Pais_1 : IServicioPais
    {
        public string Nombre => "Servicio País 1";
        public List<Pais> ObtenerPais()
        {
            return Metodos.ObtenerNombrePaisAsync().Result;
        }
    }
}
