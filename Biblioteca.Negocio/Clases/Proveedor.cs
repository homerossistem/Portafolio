using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Negocio.Clases
{
    [Serializable]
    public class Proveedor
    {
        private int id_proveedor;
        private string nombre_empresa;
        private string nombre_encargado;
        private int telefono;
        private int celular;
        private string direcccion;
        private string email;

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

        public string Nombre_empresa
        {
            get
            {
                return nombre_empresa;
            }

            set
            {
                nombre_empresa = value;
            }
        }

        public string Nombre_encargado
        {
            get
            {
                return nombre_encargado;
            }

            set
            {
                nombre_encargado = value;
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

        public int Celular
        {
            get
            {
                return celular;
            }

            set
            {
                celular = value;
            }
        }

        public string Direcccion
        {
            get
            {
                return direcccion;
            }

            set
            {
                direcccion = value;
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

        public Proveedor()
        {
            this.Init();
        }

        private void Init()
        {
            this.Id_proveedor = 0;
            this.Nombre_empresa = string.Empty;
            this.Nombre_encargado = string.Empty;
            this.Telefono = 0;
            this.Celular = 0;
            this.Direcccion = string.Empty;
            this.Email = string.Empty;
        }
    }
}
