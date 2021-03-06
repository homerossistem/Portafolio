﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Negocio.Clases
{
    [Serializable]
    public class Modulo
    {
        private string codigo;
        private string nombre;
        private int garantia;
        private int id_documento;
        private string rut_administrador;
        private int id_proveedor;

        public string Codigo
        {
            get
            {
                return codigo;
            }

            set
            {
                codigo = value;
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

        public int Garantia
        {
            get
            {
                return garantia;
            }

            set
            {
                garantia = value;
            }
        }

        public int Id_documento
        {
            get
            {
                return id_documento;
            }

            set
            {
                id_documento = value;
            }
        }

        public string Rut_administrador
        {
            get
            {
                return rut_administrador;
            }

            set
            {
                rut_administrador = value;
            }
        }

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

        public Modulo()
        {
            this.Init();
        }

        private void Init()
        {
            this.Codigo = string.Empty;
            this.Nombre = string.Empty;
            this.Garantia = 1;
            this.Id_documento = 1;
            this.Rut_administrador = string.Empty;
            this.Id_proveedor = 0;
        }
    }
}
