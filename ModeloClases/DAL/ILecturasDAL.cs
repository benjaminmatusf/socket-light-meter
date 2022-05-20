using ModeloClases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloClases.DAL
{
    public interface ILecturasDAL
    {
        void AgregarLectura(Lectura lectura);

        List<Lectura> ObtenerLecturas();
    }
}
