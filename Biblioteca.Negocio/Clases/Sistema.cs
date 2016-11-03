using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Negocio.Clases
{
    [Serializable]
    public class Sistema:Modulo
    {
        private int id_sensibilidad;
        private int id_seguridad;
        private string codigo_servidor;
        private string descripcion;
        private int id_lenguaje;

        public int Id_sensibilidad
        {
            get
            {
                return id_sensibilidad;
            }

            set
            {
                id_sensibilidad = value;
            }
        }

        public int Id_seguridad
        {
            get
            {
                return id_seguridad;
            }

            set
            {
                id_seguridad = value;
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

        public string Descripcion
        {
            get
            {
                return descripcion;
            }

            set
            {
                descripcion = value;
            }
        }

        public int Id_lenguaje
        {
            get
            {
                return id_lenguaje;
            }

            set
            {
                id_lenguaje = value;
            }
        }

        public Sistema()
        {
            this.Init();
        }

        private void Init()
        {
            this.Id_sensibilidad = 0;
            this.Id_seguridad = 0;
            this.Codigo_servidor = string.Empty;
            this.Descripcion = string.Empty;
            this.Id_lenguaje = 0;
        }
    }
}
