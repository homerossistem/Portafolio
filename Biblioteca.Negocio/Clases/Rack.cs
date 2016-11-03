using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Negocio.Clases
{
    [Serializable]
   public class Rack
    {
        private int id_rack;
        private int unidad_rack;
        private int id_sala;

        public int Id_rack
        {
            get
            {
                return id_rack;
            }

            set
            {
                id_rack = value;
            }
        }

        public int Unidad_rack
        {
            get
            {
                return unidad_rack;
            }

            set
            {
                unidad_rack = value;
            }
        }

        public int Id_sala
        {
            get
            {
                return id_sala;
            }

            set
            {
                id_sala = value;
            }
        }

        public Rack()
        {
            this.Init();
        }

        private void Init()
        {
            this.Id_rack = 0;
            this.Unidad_rack = 0;
            this.Id_sala = 0;
        }
    }
}
