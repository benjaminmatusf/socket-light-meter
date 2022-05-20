using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloClases.DTO
{
    public class Medidor
    {
        private string codigo;


        public string Codigo { get => codigo; set => codigo = value; }


        public override string ToString()
        {
            return codigo;
        }
    }
}
