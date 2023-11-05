using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Tp2.clases
{
    interface IExportable
    {
        void Importar(string ruta, int opc);

        void Exportar(string ruta, int opc);
    }
}
