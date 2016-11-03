using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Negocio.Clases
{
    [Serializable]
   public class TipoNivel
    {
        private int id_tipoNivel;
        private string tipo_nivel;

        public int Id_tipoNivel
        {
            get
            {
                return id_tipoNivel;
            }

            set
            {
                id_tipoNivel = value;
            }
        }

        public string Tipo_nivel
        {
            get
            {
                return tipo_nivel;
            }

            set
            {
                tipo_nivel = value;
            }
        }

        public TipoNivel()
        {
            this.Init();
        }

        private void Init()
        {
            this.Id_tipoNivel = 0;
            this.Tipo_nivel = string.Empty;
        }
    }
}
