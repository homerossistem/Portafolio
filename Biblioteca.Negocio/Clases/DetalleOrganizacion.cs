using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Negocio.Clases
{
    public class DetalleOrganizacion
    {
        private int id_organizacion;
        private string cod_modulo;

        public int Id_organizacion
        {
            get
            {
                return id_organizacion;
            }

            set
            {
                id_organizacion = value;
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

        public DetalleOrganizacion()
        {
            this.Init();
        }

        private void Init()
        {
            this.Id_organizacion = 0;
            this.Cod_modulo = string.Empty;
        }
    }
}
