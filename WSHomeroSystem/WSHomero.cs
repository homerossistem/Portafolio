using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Biblioteca.Negocio.Clases;
using Biblioteca.Negocio.DTOs;
using Biblioteca.Negocio.DAO;
using System.Net.Mail;

namespace WSHomeroSystem
{
    public class WSHomero : IWSHomero
    {
        #region Clases DAO
        UsuarioDAO objUsuarioDAO = new UsuarioDAO();
        SalaServidoresDAO objSalaServidoresDao = new SalaServidoresDAO();
        RackDAO objRackDAO = new RackDAO();
        HashPassDAO objHashPassDAO = new HashPassDAO();
        FuncionarioDAO objFuncionarioDAO = new FuncionarioDAO();
        TicketDAO objTicketDAO = new TicketDAO();
        BaseDatosDAO objBaseDatosDAO = new BaseDatosDAO();
        ServicioDAO objServicioDAO = new ServicioDAO();
        ServidorDAO objServidorDAO = new ServidorDAO();
        SistemaDAO objSistemaDAO = new SistemaDAO();
        SolucionTicketDAO objSolucionTicketDAO = new SolucionTicketDAO();
        RolDAO objRolDAO = new RolDAO();
        EquipoTrabajoDAO objEquipoDAO = new EquipoTrabajoDAO();
        LenguajeDAO objLenguajeDAO = new LenguajeDAO();
        DocumentoDAO objDocumentoDAO = new DocumentoDAO();
        OrganizacionesDAO objOrganizacionDAO = new OrganizacionesDAO();
        ProveedorDAO objProveedorDAO = new ProveedorDAO();
        #endregion
        #region Metodos Web Service
        #region Metodos Rack
        public bool AgregarRack(Rack _rack)
        {            
            return objRackDAO.AgregarRack(_rack);
        }

        public List<Rack> listadoRacks()
        {
            return objRackDAO.listadoRacks();
        }

        public List<DTO> listadoRacksSalas()
        {

            return objRackDAO.listadoRacksSalas();
        }

        public bool EliminarRack(int id_rack)
        {
            return objRackDAO.EliminarRack(id_rack);
        }

        public bool ModificarRack(Rack _objRack)
        {
            return objRackDAO.ModificarRack(_objRack);
        }

        public Rack BuscarRack(int id_rack)
        {
            return objRackDAO.BuscarRack(id_rack);
        }

        #endregion
        #region Agregar,Listra,Modificar,Eliminar,IniciarSession Usuarios
        public string AgregarUsuario(Usuario objusuario, HashPass objHashPass,Funcionario objFuncionario)
        {
            
            string msj = "";
            try
            {
                if (objFuncionario.Rut_funcionario.Length >= 8 && objFuncionario.Rut_funcionario.Length <= 10)
                {
                    if (!objFuncionarioDAO.ExisteFuncionarioPorRut(objFuncionario.Rut_funcionario))
                    {
                        if (!objUsuarioDAO.ExisteNombreUsuario(objusuario.Nombre_usuario))
                        {
                            if (objFuncionarioDAO.AgregarFuncionario(objFuncionario))
                            {
                                if (objUsuarioDAO.AgregarUsuario(objusuario))
                                {
                                    objusuario = objUsuarioDAO.ObtenerUsuarioPorNombreUsuario(objusuario.Nombre_usuario);
                                    objHashPass.Id_usuario = objusuario.Id_usuario;
                                    if (objHashPassDAO.AgregarHashPass(objHashPass))
                                    {
                                        msj = string.Format("Usuario Ingresado al Sistema");
                                    }
                                }
                            }
                        }
                        else
                        {
                            msj = string.Format("Ya existe un Usuario con el Nick Name : {0}", objusuario.Nombre_usuario);
                        }
                    }
                    else
                    {
                        msj = string.Format("Ya existe un Funcionario con el rut : {0}", objFuncionario.Rut_funcionario);
                    }
                }
                else
                {
                    msj = string.Format("El Rut {0} No es valido", objFuncionario.Rut_funcionario);
                }
            }catch(ArgumentException arge)
            {
                msj = arge.Message;
            }

            return msj;
            
        }
        public bool EliminarUsuario(int id_usuario)
        {
            return objUsuarioDAO.EliminarUsuario(id_usuario);
        }
        public DTO IniciarSession(string usuario, string pass)
        {
            string hash_pass = objHashPassDAO.GeneradorPasswordsMD5(pass);
            DTO objDto = objUsuarioDAO.BuscarUsuarioLogin(usuario,hash_pass);
           
            if (objDto != null)
            {
                return objDto;
            }
            return null;
        }
        public List<DTO> ListarUsuarios()
        {
            return objUsuarioDAO.listadoUsuarios();
        }
        public DTO BuscarUsuarioPorId(int id_usuario)
        {
            DTO objDto = objUsuarioDAO.BuscarUsuarioPorId(id_usuario);
            if(objDto != null)
            {
                return objDto;
            }else
            {
                return null;
            }
        }
        public bool ModificarUsuario(DTO _objDTO)
        {
            return objUsuarioDAO.ModificarUsuario(_objDTO.Usuario, _objDTO.Funcionario, _objDTO.HashPass);
        }
        #endregion
        #region Agregar,Listra,Ticket
        public string AgregarTicket(Ticket objTicket, int id_equipoTrabajo,string nombre_funcionario)
        {
            string mensaje = "";
            try {
                if(objTicketDAO.AgregarTicket(objTicket,id_equipoTrabajo,nombre_funcionario))
                {
                    mensaje = "Ticket ingresado exitosamente";
                }
                else
                {
                    mensaje = "Error al ingresar el ticket";
                }
            }catch(ArgumentException argn)
            {
                mensaje = argn.Message;
            }
            
            return mensaje;
        }
        public string AgregarSolucionTicket(SolucionTicket _solucionTicket)
        {
            string mensaje = "";
            try
            {
                if (objSolucionTicketDAO.AgregarSolucionTicket(_solucionTicket))
                {
                    mensaje = "Solucion Agregada Exitosamente";
                }
                else
                {
                    mensaje = "Error Al Agregar una Solucion";
                }
            }
            catch (ArgumentException ae)
            {
                mensaje = ae.Message;
            }

            return mensaje;
        }

        public List<DTO> listadoTicketConSolcuion()
        {
            return objTicketDAO.ListarTodosLosTicketConSolucion();
        }
        #endregion
        #region Agregar,Modificar,listado Base Datos
        public string AgregarBaseDatos(BaseDeDatos _objBaseDatos,HashPassModulo _hashPass)
        {
            string mensaje = "";
            try
            {
                if (objBaseDatosDAO.AgregarBaseDeDatos(_objBaseDatos, _hashPass))
                {
                    mensaje = "Base De Datos Ingresada exitosamente";
                }else
                {
                    mensaje = "Error al agregar una base de datos";
                }
            }catch(ArgumentException ae)
            {
                mensaje = ae.Message;
            }
            return mensaje;
        }
        #endregion
        #region Agregar,Modificar,listado Servicio
        public string AgregarServicio(Servicio objServicio,List<string>listadoCodBaseDatos)
        {
            string mensaje = "";
            try {
                if (objServicioDAO.agregarServicio(objServicio,listadoCodBaseDatos))
                {
                    mensaje = "Servicio Agregado Con Exito";
                } else
                {
                    mensaje = "Error Al Agregar Un Servicio";
                }
            }catch(ArgumentException ae)
            {
                mensaje = ae.Message;
            }

            return mensaje;
        }

        public string AgregarTipoServicio(TipoServicio objTipoServicio)
        {
            string mensaje = "";
            try
            {
                if (objServicioDAO.AgregarTipoServicio(objTipoServicio))
                {
                    mensaje = "Tipo Servicio Agregado Con Exito";
                }
                else
                {
                    mensaje = "Error Al Agregar Un Tipo Servicio";
                }
            }
            catch (ArgumentException ae)
            {
                mensaje = ae.Message;
            }

            return mensaje;
        }
        public List<DTO> listadoServicios()
        {
            return objServicioDAO.listadoServicios();
        }
        #endregion
        #region Agregar,Modificar,listado Servidor
        public string AgregarServidor(Servidor objServidor,HashPassModulo hashServidor)
        {
            string mensaje = "";
            try
            {
                if (objServidorDAO.AgregarServidor(objServidor, hashServidor))
                {
                    mensaje = "Servidor Agregado Con Exito";
                }
                else
                {
                    mensaje = "Error Al Agregar Un Servidor";
                }
            }catch(ArgumentException ae)
            {
                mensaje = ae.Message;
            }

            return mensaje;
        }
        public List<DTO> listadoServidores()
        {
            return objServidorDAO.listadoServidores();
        }
        public List<Servidor> listadoServidorAplicaciones()
        {
            return objServidorDAO.listadoServidorAplicaciones();
        }
        #endregion
        #region Agregar,Modificar,listado Sistema
        public string AgregarSistema(Sistema objSistema)
        {
            string mensaje = "";
            try
            {
                if (objSistemaDAO.AgregarSistema(objSistema))
                {
                    mensaje = "Sistema Agregado Con Exito";
                }
                else
                {
                    mensaje = "Error Al Agregar Un Sistema";
                }
            }
            catch (ArgumentException ae)
            {
                mensaje = ae.Message;
            }

            return mensaje;
        }
        #endregion
        #region listadoRol,listado equipo,listado Lenguajes,metodos Documento
        public List<Rol> listadoRol()
        {
            return objRolDAO.listadoRol();
        }
        public List<EquipoTrabajo> listadoEquiposTrabajo()
        {
            return objEquipoDAO.listadoEquiposDeTrabajo();
        }
        public List<Lenguaje> listadoLenguajes()
        {
            return objLenguajeDAO.listadoLenguajes();
        }

        public bool AgregarDocumento(Documento objDocumento)
        {
            return objDocumentoDAO.AgregarDocumento(objDocumento);
        }

        public bool ExisteDocumento(string url)
        {
            return objDocumentoDAO.ExisteDocumento(url);
        }

        public Documento BuscarDocumento(string url)
        {
            return objDocumentoDAO.bsucarDocumento(url);
        }

        public List<TipoServicio> listadoTipoServicio()
        {
            return objServicioDAO.listadoTipoServicio();
        }
        #endregion
        #region Sala Servidores
        public List<SalaServidores> ListadoSalaServidores()
        {
            return objSalaServidoresDao.ListadoSalaServidores();
        }
        #endregion
        #region Agregar,Modificar,listado,Buscar, Eliminar Proveedor
        public bool AgregarProveedor(Proveedor _prov)
        {
            return objProveedorDAO.AgregarProveedor(_prov);
        }

        public List<Proveedor> listadoProveedores()
        {
            return objProveedorDAO.listadoProveedores();
        }

        public bool EliminarProveedor(int id_prov)
        {
            return objProveedorDAO.EliminarProveedor(id_prov);
        }

        public bool ModificarProveedor(Proveedor _objProv)
        {
            return objProveedorDAO.ModificarProveedor(_objProv);
        }

        public Proveedor BuscarProveedor(int id_prov)
        {
            return objProveedorDAO.BuscarProveedor(id_prov);
        }
        #endregion
        #region Agregar,Modificar,listado,Buscar, Eliminar Organizaciones
        public bool AgregarOrganizacion(Organizacion _org)
        {
            return objOrganizacionDAO.AgregarOrganizacion(_org);
        }

        public List<Organizacion> listadoOrganizaciones()
        {
            return objOrganizacionDAO.listadoOrganizaciones();
        }

        public bool EliminarOrganizacion(int id_organizacion)
        {
            return objOrganizacionDAO.EliminarOrganizacion(id_organizacion);
        }

        public bool ModificarOrganizacion(Organizacion _objOrg)
        {
            return objOrganizacionDAO.ModificarOrganizacion(_objOrg);
        }

        public Organizacion BuscarOrganizacion(int id_Org)
        {
            return objOrganizacionDAO.BuscarOrganizacion(id_Org);
        }
        #endregion
        #endregion

        public bool agregarBaseDatosServicio(string codServicio, List<string> codBaseDatos)
        {
           return objServicioDAO.agregarBaseDatosServicio(codServicio, codBaseDatos);
        }

    }
}
