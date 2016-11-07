using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Negocio.Clases
{
    [Serializable]
    public class BaseDeDatos:Modulo
    {
        private int id_motor;
        private string nomUSer;
        private string codigo_servidor;

        public int Id_motor
        {
            get
            {
                return id_motor;
            }

            set
            {
                id_motor = value;
            }
        }

        public string Codigo_servidor
        {
            get
            {
                return codigo_servidor;
            }

            set
            {
                codigo_servidor = value;
            }
        }

        public string NomUSer
        {
            get
            {
                return nomUSer;
            }

            set
            {
                nomUSer = value;
            }
        }

     

        public BaseDeDatos()
        {
            this.Init();
        }

        private void Init()
        {
            this.Id_motor = 0;
            this.NomUSer = string.Empty;
            this.Codigo_servidor = string.Empty;
           
        }
    }
}
