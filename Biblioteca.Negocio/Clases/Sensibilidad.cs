using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Negocio.Clases
{
    [Serializable]
    public class Sensibilidad
    {
        private int id_sensibilidad;
        private string tipo_sensibilidad;

        public int Id_sensibilidad
        {
            get
            {
                return id_sensibilidad;
            }

            set
            {
                id_sensibilidad = value;
            }
        }

        public string Tipo_sensibilidad
        {
            get
            {
                return tipo_sensibilidad;
            }

            set
            {
                tipo_sensibilidad = value;
            }
        }

        public Sensibilidad()
        {
            this.Init();
        }

        private void Init()
        {
            this.Id_sensibilidad = 0;
            this.Tipo_sensibilidad = string.Empty;
        }
    }
}
