using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Negocio.Clases;

namespace Biblioteca.Negocio.DTOs
{
    [Serializable]
    public class DTO
    {
        private AdministradorModulos _administrarModulo;
        private BaseDeDatos _baseDeDatos;
        private DetalleOrganizacion _detalleOrganizacion;
        private DetalleProveedor _detalleProveedor;
        private Documento _documento;
        private EquipoTrabajo _equipoTrabajo;
        private Funcionario _funcionario;
        private HashPass _hashPass;
        private HashPassModulo _HashPassModulo;
        private Lenguaje _lenguaje;
        private Organizacion _organizacion;
        private Proveedor _proveedor;
        private Rack _rack;
        private Rol _rol;
        private SalaServidores _salaServidores;
        private Seguridad _seguridad;
        private Sensibilidad _sencibilidad;
        private Servicio _servicio;
        private Servidor _servidor;
        private Sistema _sistema;
        private SistemaOperativo _sistemaOperativo;
        private SolucionTicket _solucionTicket;
        private Ticket _ticket;
        private Tipo _tipo;
        private TipoNivel _tipoNivel;
        private TipoServicio _tipoServicio;
        private Usuario _usuario;
        private Modulo modulo;

        public AdministradorModulos AdministrarModulo
        {
            get
            {
                return _administrarModulo;
            }

            set
            {
                _administrarModulo = value;
            }
        }

        public BaseDeDatos BaseDeDatos
        {
            get
            {
                return _baseDeDatos;
            }

            set
            {
                _baseDeDatos = value;
            }
        }

        public DetalleOrganizacion DetalleOrganizacion
        {
            get
            {
                return _detalleOrganizacion;
            }

            set
            {
                _detalleOrganizacion = value;
            }
        }

        public DetalleProveedor DetalleProveedor
        {
            get
            {
                return _detalleProveedor;
            }

            set
            {
                _detalleProveedor = value;
            }
        }

        public Documento Documento
        {
            get
            {
                return _documento;
            }

            set
            {
                _documento = value;
            }
        }

        public EquipoTrabajo EquipoTrabajo
        {
            get
            {
                return _equipoTrabajo;
            }

            set
            {
                _equipoTrabajo = value;
            }
        }

        public Funcionario Funcionario
        {
            get
            {
                return _funcionario;
            }

            set
            {
                _funcionario = value;
            }
        }

        public HashPass HashPass
        {
            get
            {
                return _hashPass;
            }

            set
            {
                _hashPass = value;
            }
        }

        public HashPassModulo HashPassModulo
        {
            get
            {
                return _HashPassModulo;
            }

            set
            {
                _HashPassModulo = value;
            }
        }

        public Lenguaje Lenguaje
        {
            get
            {
                return _lenguaje;
            }

            set
            {
                _lenguaje = value;
            }
        }

        public Organizacion Organizacion
        {
            get
            {
                return _organizacion;
            }

            set
            {
                _organizacion = value;
            }
        }

        public Proveedor Proveedor
        {
            get
            {
                return _proveedor;
            }

            set
            {
                _proveedor = value;
            }
        }

        public Rack Rack
        {
            get
            {
                return _rack;
            }

            set
            {
                _rack = value;
            }
        }

        public Rol Rol
        {
            get
            {
                return _rol;
            }

            set
            {
                _rol = value;
            }
        }

        public SalaServidores SalaServidores
        {
            get
            {
                return _salaServidores;
            }

            set
            {
                _salaServidores = value;
            }
        }

        public Seguridad Seguridad
        {
            get
            {
                return _seguridad;
            }

            set
            {
                _seguridad = value;
            }
        }

        public Sensibilidad Sencibilidad
        {
            get
            {
                return _sencibilidad;
            }

            set
            {
                _sencibilidad = value;
            }
        }

        public Servicio Servicio
        {
            get
            {
                return _servicio;
            }

            set
            {
                _servicio = value;
            }
        }

        public Sistema Sistema
        {
            get
            {
                return _sistema;
            }

            set
            {
                _sistema = value;
            }
        }

        public SistemaOperativo SistemaOperativo
        {
            get
            {
                return _sistemaOperativo;
            }

            set
            {
                _sistemaOperativo = value;
            }
        }

        public SolucionTicket SolucionTicket
        {
            get
            {
                return _solucionTicket;
            }

            set
            {
                _solucionTicket = value;
            }
        }

        public Ticket Ticket
        {
            get
            {
                return _ticket;
            }

            set
            {
                _ticket = value;
            }
        }

        public Tipo Tipo
        {
            get
            {
                return _tipo;
            }

            set
            {
                _tipo = value;
            }
        }

        public TipoNivel TipoNivel
        {
            get
            {
                return _tipoNivel;
            }

            set
            {
                _tipoNivel = value;
            }
        }

        public TipoServicio TipoServicio
        {
            get
            {
                return _tipoServicio;
            }

            set
            {
                _tipoServicio = value;
            }
        }

        public Usuario Usuario
        {
            get
            {
                return _usuario;
            }

            set
            {
                _usuario = value;
            }
        }
        public Servidor Servidor
        {
            get
            {
                return _servidor;
            }

            set
            {
                _servidor = value;
            }
        }

        public Modulo Modulo
        {
            get
            {
                return modulo;
            }

            set
            {
                modulo = value;
            }
        }

        public DTO()
        {
            this.Init();
        }
        private void Init()
        {
            this.AdministrarModulo = new AdministradorModulos();
            this.BaseDeDatos = new BaseDeDatos();
            this.DetalleOrganizacion = new DetalleOrganizacion();
            this.DetalleProveedor = new DetalleProveedor();
            this.Documento = new Documento();
            this.EquipoTrabajo = new EquipoTrabajo();
            this.Funcionario = new Funcionario();
            this.HashPass = new HashPass();
            this.HashPassModulo = new HashPassModulo();
            this.Lenguaje = new Lenguaje();
            this.Organizacion = new Organizacion();
            this.Proveedor = new Proveedor();
            this.Rack = new Rack();
            this.Rol = new Rol();
            this.SalaServidores = new SalaServidores();
            this.Seguridad = new Seguridad();
            this.Sencibilidad = new Sensibilidad();
            this.Servicio = new Servicio();
            this.Servidor = new Servidor();
            this.Sistema = new Sistema();
            this.SistemaOperativo = new SistemaOperativo();
            this.SolucionTicket = new SolucionTicket();
            this.Ticket = new Ticket();
            this.Tipo = new Tipo();
            this.TipoNivel = new TipoNivel();
            this.TipoServicio = new TipoServicio();
            this.Usuario = new Usuario();
            this.Modulo = new Modulo();
            this.Usuario = new Usuario();
    }
        
    }
}
