using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Negocio.Clases
{
    [Serializable]
    public class DetalleProveedor
    {
        private string cod_modulo;
        private int id_proveedor;

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

        public int Id_proveedor
        {
            get
            {
                return id_proveedor;
            }

            set
            {
                id_proveedor = value;
            }
        }

        public DetalleProveedor()
        {
            this.Init();
        }

        private void Init()
        {
            this.Cod_modulo = string.Empty;
            this.Id_proveedor = 0;
        }
    }
}
