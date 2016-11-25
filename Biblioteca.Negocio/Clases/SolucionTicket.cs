using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Negocio.Clases
{
    [Serializable]
    public class SolucionTicket
    {
        private int id_solucion;
        private string descripcion_solucion;
        private DateTime fecha_solucion;
        private int id_ticket;
        private string nombre_funcionario;
        private string equipo_trabajo;

        public int Id_solucion
        {
            get
            {
                return id_solucion;
            }

            set
            {
                id_solucion = value;
            }
        }

        public string Descripcion_solucion
        {
            get
            {
                return descripcion_solucion;
            }

            set
            {
                descripcion_solucion = value;
            }
        }

        public DateTime Fecha_solucion
        {
            get
            {
                return fecha_solucion;
            }

            set
            {
                fecha_solucion = value;
            }
        }

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

        public string Nombre_funcionario
        {
            get
            {
                return nombre_funcionario;
            }

            set
            {
                nombre_funcionario = value;
            }
        }

        public string Equipo_trabajo
        {
            get
            {
                return equipo_trabajo;
            }

            set
            {
                equipo_trabajo = value;
            }
        }

        public SolucionTicket()
        {
            this.Id_solucion = 0;
            this.Descripcion_solucion = string.Empty;
            this.Fecha_solucion = DateTime.Now;
            this.Id_ticket = 0;
            this.Nombre_funcionario = string.Empty;
            this.Equipo_trabajo = string.Empty;
        }
    }
}
