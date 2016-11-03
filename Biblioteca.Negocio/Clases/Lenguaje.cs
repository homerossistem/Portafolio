using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Negocio.Clases
{
    [Serializable]
    public class Lenguaje
    {
        private int id_lenguaje;
        private string nombre_lenguaje;

        public int Id_lenguaje
        {
            get
            {
                return id_lenguaje;
            }

            set
            {
                id_lenguaje = value;
            }
        }

        public string Nombre_lenguaje
        {
            get
            {
                return nombre_lenguaje;
            }

            set
            {
                nombre_lenguaje = value;
            }
        }

        public Lenguaje()
        {
            this.Init();
        }

        private void Init()
        {
            this.Id_lenguaje = 0;
            this.Nombre_lenguaje = string.Empty;
        }
    }
}
