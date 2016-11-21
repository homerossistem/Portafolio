using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Negocio.Clases
{
    [Serializable]
    public class Organizacion
    {
        private int id_organizacion;
        private string nombre_organizacion;
        private string direccion;
        private int telefono;
        private string email;
        private List<Sistema> listadoSistemas;



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

        public string Nombre_organizacion
        {
            get
            {
                return nombre_organizacion;
            }

            set
            {
                nombre_organizacion = value;
            }
        }

        public string Direccion
        {
            get
            {
                return direccion;
            }

            set
            {
                direccion = value;
            }
        }

        public int Telefono
        {
            get
            {
                return telefono;
            }

            set
            {
                telefono = value;
            }
        }

        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }

        public List<Sistema> ListadoSistemas
        {
            get
            {
                return listadoSistemas;
            }

            set
            {
                listadoSistemas = value;
            }
        }

        public Organizacion()
        {
            this.Init();
        }

        private void Init()
        {
            this.id_organizacion = 0;
            this.nombre_organizacion = string.Empty;
            this.direccion = string.Empty;
            this.telefono = 0;
            this.email = string.Empty;
            this.ListadoSistemas = new List<Sistema>();
        }
    }
}
