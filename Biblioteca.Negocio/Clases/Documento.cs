using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Negocio.Clases
{
    [Serializable]
   public class Documento
    {
        private int id_documento;
        private string url_documento;

        public int Id_documento
        {
            get
            {
                return id_documento;
            }

            set
            {
                id_documento = value;
            }
        }

        public string Url_documento
        {
            get
            {
                return url_documento;
            }

            set
            {
                url_documento = value;
            }
        }

        public Documento()
        {
            this.Init();
        }
        private void Init()
        {
            this.Id_documento = 0;
            this.Url_documento = string.Empty;
        }
    }
}
