using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Negocio.Clases
{
    [Serializable]
    public class SalaServidores
    {
        private int id_salaServidor;
        private string nombre_sala;
        private int piso;

        public string Nombre_sala
        {
            get
            {
                return nombre_sala;
            }

            set
            {
                nombre_sala = value;
            }
        }

        public int Piso
        {
            get
            {
                return piso;
            }

            set
            {
                piso = value;
            }
        }

        public int Id_salaServidor
        {
            get
            {
                return id_salaServidor;
            }

            set
            {
                id_salaServidor = value;
            }
        }

        public SalaServidores()
        {
            this.init();
        }

        private void init()
        {
            this.Id_salaServidor = 0;
            this.Nombre_sala = string.Empty;
            this.Piso = 0;
        }
    }
}
