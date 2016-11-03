using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Negocio.Clases
{
    [Serializable]
    public class Seguridad
    {
        private int id_seguridad;
        private string tipo_seguridad;

        public int Id_seguridad
        {
            get
            {
                return id_seguridad;
            }

            set
            {
                id_seguridad = value;
            }
        }

        public string Tipo_seguridad
        {
            get
            {
                return tipo_seguridad;
            }

            set
            {
                tipo_seguridad = value;
            }
        }

        public Seguridad()
        {
            this.Id_seguridad = 0;
            this.Tipo_seguridad = string.Empty;
        }
    }
}
