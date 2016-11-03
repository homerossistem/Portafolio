using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Negocio.Clases
{
    [Serializable]
   public class Tipo
    {
        private int id_tipo;
        private string _tipo;

        public int Id_tipo
        {
            get
            {
                return id_tipo;
            }

            set
            {
                id_tipo = value;
            }
        }

        public string _Tipo
        {
            get
            {
                return _tipo;
            }

            set
            {
                _tipo = value;
            }
        }

        public Tipo()
        {
            this.Init();
        }

        private void Init()
        {
            this.Id_tipo = 0;
            this._Tipo = string.Empty;
        }
    }
}
