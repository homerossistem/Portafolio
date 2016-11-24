using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Negocio.Clases
{
   [Serializable]
   public class Ticket
    {
        private int id_ticket;
        private DateTime fechaProblema;
        private string problema;
        private string rut_funcionario;
        private string codigo_modulo;
        private string nombre_modulo;

        public int Id_ticket
        {
            get
            {
                return id_ticket;
            }

            set
            {
                id_ticket = value;
            }
        }

        public DateTime FechaProblema
        {
            get
            {
                return fechaProblema;
            }

            set
            {
                fechaProblema = value;
            }
        }

        public string Problema
        {
            get
            {
                return problema;
            }

            set
            {
                problema = value;
            }
        }

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

        public string Codigo_modulo
        {
            get
            {
                return codigo_modulo;
            }

            set
            {
                codigo_modulo = value;
            }
        }

        public string Nombre_modulo
        {
            get
            {
                return nombre_modulo;
            }

            set
            {
                nombre_modulo = value;
            }
        }

        public Ticket()
        {
            this.Init();
        }

        private void Init()
        {
            this.Id_ticket = 0;
            this.FechaProblema = DateTime.Now;
            this.Problema = string.Empty;
            this.Rut_funcionario = string.Empty;
            this.Codigo_modulo = string.Empty;
            this.Nombre_modulo = string.Empty;
        }

    }
}
