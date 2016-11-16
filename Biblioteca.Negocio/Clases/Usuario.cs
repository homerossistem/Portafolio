using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Negocio.Clases
{
    [Serializable]
   public class Usuario
    {

        private int id_usuario;
        private string nombre_usuario;
        private int estado;
        private DateTime fecha_creacion;
        private int id_rol;
        private HashPass objHashPass;

        public int Id_usuario
        {
            get
            {
                return id_usuario;
            }

            set
            {
                id_usuario = value;
            }
        }

        public string Nombre_usuario
        {
            get
            {
                return nombre_usuario;
            }

            set
            {
                nombre_usuario = value;
            }
        }

        public int Estado
        {
            get
            {
                return estado;
            }

            set
            {
                estado = value;
            }
        }

        public DateTime Fecha_creacion
        {
            get
            {
                return fecha_creacion;
            }

            set
            {
                fecha_creacion = value;
            }
        }

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
        public HashPass ObjHashPass
        {
            get
            {
                return objHashPass;
            }

            set
            {
                objHashPass = value;
            }
        }

        public Usuario()
        {
            this.Init();
        }

        private void Init()
        {
            this.Id_usuario = 0;
            this.Nombre_usuario = string.Empty;
            this.Estado = 1;
            this.Id_rol = 0;
            this.ObjHashPass = new HashPass();
        }
    }
}
