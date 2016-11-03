using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Negocio.Clases
{
    [Serializable]
    public class SistemaOperativo
    {
        private int id_sistemaOperativo;
        private string nombre_sistema;

        public int Id_sistemaOperativo
        {
            get
            {
                return id_sistemaOperativo;
            }

            set
            {
                id_sistemaOperativo = value;
            }
        }

        public string Nombre_sistema
        {
            get
            {
                return nombre_sistema;
            }

            set
            {
                nombre_sistema = value;
            }
        }

        public SistemaOperativo()
        {
            this.Init();
        }

        private void Init()
        {
            this.Id_sistemaOperativo = 0;
            this.Nombre_sistema = string.Empty;
        }
    }
}
