using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Negocio.Clases
{
    [Serializable]
   public class Funcionario
    {
        private string rut_funcionario;
        private string nombre;
        private string apellido;
        private string email;
        private int celular;
        private string direccion;
        private int id_equipo_trabajo;

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

        public string Nombre
        {
            get
            {
                return nombre;
            }

            set
            {
                nombre = value;
            }
        }

        public string Apellido
        {
            get
            {
                return apellido;
            }

            set
            {
                apellido = value;
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

        public int Id_equipo_trabajo
        {
            get
            {
                return id_equipo_trabajo;
            }

            set
            {
                id_equipo_trabajo = value;
            }
        }

        public Funcionario()
        {
            this.Init();
        }

        private void Init()
        {
            this.Rut_funcionario = string.Empty;
            this.Nombre = string.Empty;
            this.Apellido = string.Empty;
            this.Email = string.Empty;
            this.Celular = 0;
            this.Direccion = string.Empty;
            this.Id_equipo_trabajo = 0;
        }
    }
}
