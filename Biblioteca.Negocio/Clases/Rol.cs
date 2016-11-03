using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Negocio.Clases
{
    [Serializable]
    public class Rol
    {
        private int id_rol;
        private string nombre_rol;

        public int Id_rol
        {
            get
            {
                return id_rol;
            }

            set
            {
                id_rol = value;
            }
        }

        public string Nombre_rol
        {
            get
            {
                return nombre_rol;
            }

            set
            {
                nombre_rol = value;
            }
        }

        public Rol()
        {
            this.Init();
        }

        private void Init()
        {
            this.Id_rol = 0;
            this.Nombre_rol = string.Empty;
        }
    }
}
