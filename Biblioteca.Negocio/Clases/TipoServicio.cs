using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Negocio.Clases
{
    [Serializable]
    public class TipoServicio
    {
        private int id_tipoServicio;
        private string tipo_servicio;

        public int Id_tipoServicio
        {
            get
            {
                return id_tipoServicio;
            }

            set
            {
                id_tipoServicio = value;
            }
        }

        public string Tipo_servicio
        {
            get
            {
                return tipo_servicio;
            }

            set
            {
                tipo_servicio = value;
            }
        }

        public TipoServicio()
        {
            this.Id_tipoServicio = 0;
            this.Tipo_servicio = string.Empty;
        }
    }
}
