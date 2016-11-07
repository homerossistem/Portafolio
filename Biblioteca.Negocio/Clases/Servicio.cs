using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Negocio.Clases
{
    [Serializable]
   public class Servicio:Modulo
    {
        private string codigo_servidor;
        private int id_tipo;
        private string descripcion;
        private int id_lenguaje;
        private List<BaseDeDatos> listadoBaseDatos;

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

        public int Id_tipo
        {
            get
            {
                return id_tipo;
            }

            set
            {
                id_tipo = value;
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

        public List<BaseDeDatos> ListadoBaseDatos
        {
            get
            {
                return listadoBaseDatos;
            }

            set
            {
                listadoBaseDatos = value;
            }
        }

        public Servicio()
        {
            this.Codigo_servidor = string.Empty;
            this.Id_tipo = 0;
            this.Descripcion = string.Empty;
            this.id_lenguaje = 0;
            this.ListadoBaseDatos = new List<BaseDeDatos>();
        }
    }
}
