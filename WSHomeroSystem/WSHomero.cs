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
        public string AgregarUsuario(Usuario objusuario, HashPass objHashPass, Funcionario objFuncionario)
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
                            if (objUsuarioDAO.AgregarUsuario(objusuario))
                            {
                                objusuario = objUsuarioDAO.ObtenerUsuarioPorNombreUsuario(objusuario.Nombre_usuario);
                                objFuncionario.ObjUsuario = objusuario;
                                if (objFuncionarioDAO.AgregarFuncionario(objFuncionario))
                                {
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
            } catch (ArgumentException arge)
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
            DTO objDto = objUsuarioDAO.BuscarUsuarioLogin(usuario, hash_pass);

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
            if (objDto != null)
            {
                return objDto;
            } else
            {
                return null;
            }
        }
        public bool ModificarUsuario(DTO _objDTO)
        {
            return objUsuarioDAO.ModificarUsuario(_objDTO.Usuario, _objDTO.Funcionario, _objDTO.HashPass);
        }
        public List<Funcionario> listadoFuncionariosResponsables()
        {
            return objFuncionarioDAO.listadoFuncionariosResponsables();
        }

        public List<DTO> listadoUsuariosPorEquipoDeTrabajo(int id_equipo)
        {
            return objUsuarioDAO.listadoUsuariosPorEquipoDeTrabajo(id_equipo);
        }
        public Funcionario buscarFuncionarioPorRut(string rut)
        {
            return objFuncionarioDAO.buscarFuncionarioPorRut(rut);
        }
        #endregion
        #region Agregar,Listra,Ticket
        public string AgregarTicket(Ticket objTicket, int id_equipoTrabajo, string nombre_funcionario)
        {
            string mensaje = "";
            try {
                if (objTicketDAO.AgregarTicket(objTicket, id_equipoTrabajo, nombre_funcionario))
                {
                    mensaje = "Ticket ingresado exitosamente";
                }
                else
                {
                    mensaje = "Error al ingresar el ticket";
                }
            } catch (ArgumentException argn)
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
        public string AgregarBaseDatos(BaseDeDatos _objBaseDatos)
        {
            string mensaje = "";
            try
            {
                if (objBaseDatosDAO.AgregarBaseDeDatos(_objBaseDatos))
                {
                    mensaje = "Base De Datos Ingresada exitosamente";
                } else
                {
                    mensaje = "Error al agregar una base de datos";
                }
            } catch (ArgumentException ae)
            {
                mensaje = ae.Message;
            }
            return mensaje;
        }
        public List<BaseDeDatos> ListadoBaseDeDatos()
        {
            return objBaseDatosDAO.ListadoBaseDeDatos();
        }
        public List<DTO> ListBaseDeDatos()
        {
            return objBaseDatosDAO.ListBaseDeDatos();
        }
        public List<MotorBD> ListadoMotorBaseDeDatos()
        {
            return objBaseDatosDAO.ListadoMotorBaseDeDatos();
        }

        public bool EliminarBaseDeDatosPorCodigo(string codigobd)
        {
            return objBaseDatosDAO.EliminarBaseDeDatosPorCodigo(codigobd);
        }
        public BaseDeDatos BuscarBaseDeDatosPorCodigo(string codigo)
        {
            return objBaseDatosDAO.BuscarBaseDeDatosPorCodigo(codigo);
        }
        public bool ModificarBaseDeDatos(BaseDeDatos _objBaseDatos)
        {
            return objBaseDatosDAO.ModificarBaseDeDatos(_objBaseDatos);
        }
        public MotorBD buscarMotorBDPorNombreMBD(string nombre)
        {
            return objBaseDatosDAO.buscarMotorBDPorNombreMBD(nombre);
        }


        #endregion
        #region Agregar,Modificar,listado Servicio

        public bool ModificarServicio(Servicio objservicio, List<string> codBaseDatos)
        {
            return objServicioDAO.ModificarServicio(objservicio, codBaseDatos);
        }
        public string AgregarServicio(Servicio objServicio, List<string> listadoCodBaseDatos)
        {
            string mensaje = "";
            try {
                if (objServicioDAO.agregarServicio(objServicio, listadoCodBaseDatos))
                {
                    mensaje = "Servicio Agregado Con Exito";
                } else
                {
                    mensaje = "Error Al Agregar Un Servicio";
                }
            } catch (ArgumentException ae)
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

        public bool EliminarServicio(string CodServicio)
        {
            return objServicioDAO.EliminarServicio(CodServicio);
        }

        public List<Servicio> listadoDeServicios()
        {
            return objServicioDAO.listadoDeServicios();
        }
        public Servicio BuscarServicio(string codigoServicio)
        {
            return objServicioDAO.BuscarServicio(codigoServicio);
        }
        #endregion
        #region Agregar,Modificar,listado Servidor
        public string AgregarServidor(Servidor objServidor)
        {
            string mensaje = "";
            try
            {
                if (objServidorDAO.AgregarServidor(objServidor))
                {
                    mensaje = "Servidor Agregado Con Exito";
                }
                else
                {
                    mensaje = "Error Al Agregar Un Servidor";
                }
            } catch (ArgumentException ae)
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
        public List<TipoNivel> ListadoTipoNivelServidor()
        {
            return objServidorDAO.ListadoTipoNivelServidor();
        }
        public List<Tipo> ListadoTipoServidor()
        {
            return objServidorDAO.ListadoTipoServidor();
        }

        public List<SistemaOperativo> ListadoSistemaOperativo()
        {
            return objServidorDAO.ListadoSistemaOperativo();
        }
        public bool EliminarServidorPorCodigo(string codigoServidor)
        {
            return objServidorDAO.EliminarServidorPorCodigo(codigoServidor);
        }

        public List<Servidor> listadoServidorBaseDeDatos()
        {
            return objServidorDAO.listadoServidorBaseDeDatos();
        }
        public List<Servidor> ListDeServidor()
        {
            return objServidorDAO.ListDeServidor();
        }
        public Servidor BuscarServidorPorCod(string codigo_servidor)
        {
            return objServidorDAO.BuscarServidorPorCod(codigo_servidor);
        }
        public Tipo BuscarTipoServidor(int id)
        {
            return objServidorDAO.BuscarTipoServidor(id);
        }
        public SistemaOperativo BuscarSistemaOperativoPorId(int id)
        {
            return objServidorDAO.BuscarSistemaOperativoPorId(id);
        }
        public bool ModificarServidor(Servidor _servidor)
        {
            return objServidorDAO.ModificarServidor(_servidor);
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
        public List<Sistema> listSistemas()
        {
            return objSistemaDAO.listSistemas();
        }
        public List<Sensibilidad> listadoSensibilidadSistema()
        {
            return objSistemaDAO.listadoSensibilidadSistema();
        }
        public List<Seguridad> listadoSeguridadSistema()
        {
            return objSistemaDAO.listadoSeguridadSistema();
        }
        public Sistema BuscarSistema(string CodigoSistema)
        {
            return objSistemaDAO.BuscarSistema(CodigoSistema);
        }
        public Sensibilidad buscarSensibilidad(int idsensibilidad)
        {
            return objSistemaDAO.buscarSensibilidad(idsensibilidad);
        }
        public Seguridad buscarSeguridad(int idSeguridad)
        {
            return objSistemaDAO.buscarSeguridad(idSeguridad);
        }
        public List<DTO> listadoSistemas()
        {
            return objSistemaDAO.listadoSistemas();
        }
        #endregion
        #region listadoRol,listado equipo,listado Lenguajes,metodos Documento
        public List<Rol> listadoRol()
        {
            return objRolDAO.listadoRol();
        }
        public bool AgregarLenguaje(Lenguaje _lenguaje)
        {
            return objLenguajeDAO.AgregarLenguaje(_lenguaje);
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

        public bool EliminarLenguaje(int idLenguaje)
        {
            return objLenguajeDAO.EliminarLenguaje(idLenguaje);
        }
        public Lenguaje BuscarLenguaje(int idLenguaje)
        {
            return objLenguajeDAO.BuscarLenguaje(idLenguaje);
        }
        #endregion
        #region Sala Servidores
        public List<SalaServidores> ListadoSalaServidores()
        {
            return objSalaServidoresDao.ListadoSalaServidores();
        }
        public bool AgregarSalaServidor(SalaServidores _salaServidores)
        {
            return objSalaServidoresDao.AgregarSalaServidor(_salaServidores);
        }

        public bool ModificarSalaServidor(SalaServidores _salaServidores)
        {
            return objSalaServidoresDao.ModificarSalaServidor(_salaServidores);
        }
        public SalaServidores BuscarSalaServidorPorId(int id)
        {
            return objSalaServidoresDao.BuscarSalaServidorPorId(id);
        }
        public bool ValidarSalaServidoxrNombre(string nombre_salaServidor)
        {
            return objSalaServidoresDao.ValidarSalaServidoxrNombre(nombre_salaServidor);
        }
        public bool EliminarSalaServidor(int idSalaServidor)
        {
            return objSalaServidoresDao.EliminarSalaServidor(idSalaServidor);
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
        public bool AgregarOrganizacion(Organizacion _org, List<string> codSistema)
        {
            return objOrganizacionDAO.AgregarOrganizacion(_org, codSistema);
        }

        public List<Organizacion> listadoOrganizaciones()
        {
            return objOrganizacionDAO.listadoOrganizaciones();
        }

        public bool EliminarOrganizacion(int id_organizacion)
        {
            return objOrganizacionDAO.EliminarOrganizacion(id_organizacion);
        }

        public bool ModificarOrganizacion(Organizacion _objOrg, List<string> codSistema)
        {
            return objOrganizacionDAO.ModificarOrganizacion(_objOrg, codSistema);
        }

        public Organizacion BuscarOrganizacion(int id_Org)
        {
            return objOrganizacionDAO.BuscarOrganizacion(id_Org);
        }
        #endregion
        #region EquipoTrabajo
        public bool AgregarEquipoTrabajo(EquipoTrabajo _equipo)
        {
            return objEquipoDAO.AgregarEquipoTrabajo(_equipo);
        }

        public bool ExisteEquipo(string nombre)
        {
            return objEquipoDAO.ExisteEquipo(nombre);
        }

        public EquipoTrabajo BuscarEquipo(int id)
        {
            return objEquipoDAO.BuscarEquipo(id);
        }

        public bool eliminarEquipo(EquipoTrabajo _equipoTrabajo)
        {
            return objEquipoDAO.eliminarEquipo(_equipoTrabajo);
        }
        public bool ModificarEquipoTrabajo(EquipoTrabajo eqt)
        {
            return objEquipoDAO.ModificarEquipoTrabajo(eqt);
        }
        #endregion
        #region motorBD
        public bool AgregarMotorBaseDeDatos(MotorBD objMotorBD)
        {
            return objBaseDatosDAO.AgregarMotorBaseDeDatos(objMotorBD);
        }
        public bool EliminarMotorBaseDatos(int id)
        {
            return objBaseDatosDAO.EliminarMotorBaseDatos(id);
        }
        public bool ModificarMotorBD(MotorBD objMotorBD)
        {
            return objBaseDatosDAO.ModificarMotorBD(objMotorBD);
        }
        public MotorBD buscarMotorBDPorId(int id)
        {
            return objBaseDatosDAO.buscarMotorBDPorId(id);
        }
        #endregion
        #endregion

        public List<Servicio> listadoServiciosByEquipoTrabajo(int id_equipoTrabajo)
        {
            return null;
        }
        public List<Sistema> ListadoSistemasbyEquipoTrabajo(int id_equipoTrabajo, int id_rol)
        {
            return objSistemaDAO.ListadoSistemasbyEquipoTrabajo(id_equipoTrabajo, id_rol);
        }


        }
}

















