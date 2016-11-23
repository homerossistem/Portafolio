using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Negocio.Clases
{
    [Serializable]
    public class Servidor:Modulo
    {
        private string ip;
        private int discoDuro;
        private int ram;
        private int id_sistemaOperativo;
        private int id_rack;
        private int id_tipo_nivel;
        private int id_tipo;
        private List<Sistema> listadoSistemas;
        private List<Servicio> listadoServicios;
        private List<BaseDeDatos> listadoBaseDatos;
        private HashPassModulo objHashPass;

        public string Ip
        {
            get
            {
                return ip;
            }

            set
            {
                ip = value;
            }
        }

        public int DiscoDuro
        {
            get
            {
                return discoDuro;
            }

            set
            {
                discoDuro = value;
            }
        }

        public int Ram
        {
            get
            {
                return ram;
            }

            set
            {
                ram = value;
            }
        }

        public int Id_sistemaOperativo
        {
            get
            {
                return id_sistemaOperativo;
            }

            set
            {
                id_sistemaOperativo = value;
            }
        }

        public int Id_rack
        {
            get
            {
                return id_rack;
            }

            set
            {
                id_rack = value;
            }
        }

        public int Id_tipo_nivel
        {
            get
            {
                return id_tipo_nivel;
            }

            set
            {
                id_tipo_nivel = value;
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

        public List<Servicio> ListadoServicios
        {
            get
            {
                return listadoServicios;
            }

            set
            {
                listadoServicios = value;
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

        public HashPassModulo ObjHashPass
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

        public Servidor()
        {
            this.Init();
        }

        private void Init()
        {
            this.Ip = string.Empty;
            this.DiscoDuro = 0;
            this.Ram = 0;
            this.Id_sistemaOperativo = 0;
            this.Id_rack = 0;
            this.Id_tipo_nivel = 0;
            this.Id_tipo = 0;
            this.ListadoSistemas = new List<Sistema>();
            this.ListadoServicios = new List<Servicio>();
            this.ListadoBaseDatos = new List<BaseDeDatos>();
            this.ObjHashPass = new HashPassModulo();
        }
    }
}
