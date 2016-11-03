using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Negocio.Clases
{
    [Serializable]
    public class HashPassModulo
    {
        private string cod_modulo;
        private string hash_pass;

        public string Cod_modulo
        {
            get
            {
                return cod_modulo;
            }

            set
            {
                cod_modulo = value;
            }
        }

        public string Hash_pass
        {
            get
            {
                return hash_pass;
            }

            set
            {
                hash_pass = value;
            }
        }

        public HashPassModulo()
        {
            this.Init();
        }

        private void Init()
        {
            this.Cod_modulo = string.Empty;
            this.Hash_pass = string.Empty;
        }
    }
}
