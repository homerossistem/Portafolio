using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Negocio.Clases
{
    [Serializable]
    public class EquipoTrabajo
    {
        private int id_equipo;
        private string nombre_equipo;

        public int Id_equipo
        {
            get
            {
                return id_equipo;
            }

            set
            {
                id_equipo = value;
            }
        }

        public string Nombre_equipo
        {
            get
            {
                return nombre_equipo;
            }

            set
            {
                nombre_equipo = value;
            }
        }

        public EquipoTrabajo()
        {
            this.Init();
        }

        private void Init()
        {
            this.Id_equipo = 0;
            this.Nombre_equipo = string.Empty;
        }
    }
}
