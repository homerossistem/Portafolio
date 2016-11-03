using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Negocio.Clases
{
    [Serializable]
    public class AdministradorModulos
    {
        private string rut_funcionario;
        private string cod_modulo;

        public string Rut_funcionario
        {
            get
            {
                return rut_funcionario;
            }

            set
            {
                rut_funcionario = value;
            }
        }

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

        public AdministradorModulos()
        {
            this.Init();
        }

        private void Init()
        {
            this.Rut_funcionario = string.Empty;
            this.Cod_modulo = string.Empty;
        }
    }
}
