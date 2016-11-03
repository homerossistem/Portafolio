using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Negocio.Clases
{
    [Serializable]
    public class HashPass
    {
        private int id_usuario;
        private string hash_pass;

        public int Id_usuario
        {
            get
            {
                return id_usuario;
            }

            set
            {
                id_usuario = value;
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

        public HashPass()
        {
            this.Init();
        }

        private void Init()
        {
            this.Id_usuario = 0;
            this.Hash_pass = string.Empty;
        }
    }
}
