﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Biblioteca.Negocio.Clases;
using Biblioteca.Negocio.DTOs;

namespace WSHomeroSystem
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IWSHomero
    {
        [OperationContract]
        string AgregarUsuario(Usuario objusuario,HashPass objHashPass,Funcionario objFuncionario);
        [OperationContract]
        DTO IniciarSession(string usuario, string pass);
        [OperationContract]
        string AgregarTicket(Ticket objFuncionario,int id_equipoTrabajo,string nombre_funcionario);
        [OperationContract]
        string AgregarBaseDatos(BaseDeDatos _objBaseDatos);
        [OperationContract]
        string AgregarServicio(Servicio objServicio, List<string> listadoCodBaseDatos);
        [OperationContract]
        string AgregarTipoServicio(TipoServicio objTipoServicio);
        [OperationContract]
        string AgregarServidor(Servidor objServidor);
        [OperationContract]
        string AgregarSistema(Sistema objSistema,List<string> listadoBaseDatos, List<string> listadoServicios);
        [OperationContract]
        List<DTO> ListarUsuarios();
        [OperationContract]
        string AgregarSolucionTicket(SolucionTicket _solucionTicket);
        [OperationContract]
        List<DTO> listadoTicketConSolcuion();
        [OperationContract]
        bool EliminarUsuario(int id_usuario);
        [OperationContract]
        DTO BuscarUsuarioPorId(int id_usuario);
        [OperationContract]
        List<Rol> listadoRol();
        [OperationContract]
        List<EquipoTrabajo> listadoEquiposTrabajo();
        [OperationContract]
        bool ModificarUsuario(DTO _objDTO);
        [OperationContract]
        List<DTO>listadoServicios();
        [OperationContract]
        List<Lenguaje> listadoLenguajes();
        [OperationContract]
        bool AgregarDocumento(Documento objDocumento);
        [OperationContract]
        bool ExisteDocumento(string url);
        [OperationContract]
        Documento BuscarDocumento(string url);
        [OperationContract]
        List<TipoServicio> listadoTipoServicio();
        [OperationContract]
        List<DTO> listadoServidores();
        [OperationContract]
        List<Servidor> listadoServidorAplicaciones();
        [OperationContract]
        bool AgregarRack(Rack _rack);
        [OperationContract]
        List<Rack> listadoRacks();
        [OperationContract]
        List<DTO> listadoRacksSalas();
        [OperationContract]
        bool EliminarRack(int id_rack);
        [OperationContract]
        bool ModificarRack(Rack _objRack);
        [OperationContract]
        Rack BuscarRack(int id_rack);
        [OperationContract]
        bool AgregarProveedor(Proveedor _prov);
        [OperationContract]
        List<Proveedor> listadoProveedores();
        [OperationContract]
        bool EliminarProveedor(int id_prov);
        [OperationContract]
        bool ModificarProveedor(Proveedor _objProv);
        [OperationContract]
        Proveedor BuscarProveedor(int id_prov);
        [OperationContract]
        bool AgregarOrganizacion(Organizacion _org,List<string>codSistema);
        [OperationContract]
        List<Organizacion> listadoOrganizaciones();
        [OperationContract]
        bool EliminarOrganizacion(int id_organizacion);
        [OperationContract]
        bool ModificarOrganizacion(Organizacion _objOrg, List<string> codSistema);
        [OperationContract]
        Organizacion BuscarOrganizacion(int id_Org);
        [OperationContract]
        bool AgregarEquipoTrabajo(EquipoTrabajo _equipo);
        [OperationContract]
        bool ExisteEquipo(String nombre);
        [OperationContract]
        EquipoTrabajo BuscarEquipo(int id);
        [OperationContract]
        bool eliminarEquipo(EquipoTrabajo _equipoTrabajo);
        [OperationContract]
        List<SalaServidores> ListadoSalaServidores();
        [OperationContract]
        List<DTO> listadoUsuariosPorEquipoDeTrabajo(int id_equipo);
        [OperationContract]
        List<Funcionario> listadoFuncionariosResponsables();
        [OperationContract]
        bool AgregarLenguaje(Lenguaje _lenguaje);
        [OperationContract]
        bool AgregarSalaServidor(SalaServidores _salaServidores);
        [OperationContract]
        bool ModificarSalaServidor(SalaServidores _salaServidores);
        [OperationContract]
        List<Tipo> ListadoTipoServidor();
        [OperationContract]
        List<TipoNivel> ListadoTipoNivelServidor();
        [OperationContract]
        List<SistemaOperativo> ListadoSistemaOperativo();
        [OperationContract]
        bool EliminarServicio(string CodServicio);
        [OperationContract]
        SalaServidores BuscarSalaServidorPorId(int id);
        [OperationContract]
        bool ValidarSalaServidoxrNombre(string nombre_salaServidor);
        [OperationContract]
        List<BaseDeDatos> ListadoBaseDeDatos();

        [OperationContract]
        bool EliminarServidorPorCodigo(string codigoServidor);
        [OperationContract]
        List<Sistema> listSistemas();
        [OperationContract]
        bool ModificarServicio(Servicio objservicio, List<string> codBaseDatos);
        [OperationContract]
       bool EliminarLenguaje(int idLenguaje);
        [OperationContract]
        bool EliminarSalaServidor(int idSalaServidor);
        [OperationContract]
        List<DTO> ListBaseDeDatos();
        [OperationContract]
        List<MotorBD> ListadoMotorBaseDeDatos();
        [OperationContract]
        List<Servidor> listadoServidorBaseDeDatos();
        [OperationContract]
        bool EliminarBaseDeDatosPorCodigo(string codigobd);
        [OperationContract]
        bool ModificarEquipoTrabajo(EquipoTrabajo eqt);
        [OperationContract]
        bool AgregarMotorBaseDeDatos(MotorBD objMotorBD);
        [OperationContract]
        bool EliminarMotorBaseDatos(int id);
        [OperationContract]
        bool ModificarMotorBD(MotorBD objMotorBD);
        [OperationContract]
        MotorBD buscarMotorBDPorId(int id);
        [OperationContract]
        List<Sensibilidad> listadoSensibilidadSistema();
        [OperationContract]
        List<Seguridad> listadoSeguridadSistema();
        [OperationContract]
        List<Servicio> listadoDeServicios();
        [OperationContract]
        List<Servidor> ListDeServidor();
        [OperationContract]
        BaseDeDatos BuscarBaseDeDatosPorCodigo(string codigo);
        [OperationContract]
        Servidor BuscarServidorPorCod(string codigo_servidor);
        [OperationContract]
        Sistema BuscarSistema(string CodigoSistema);
        [OperationContract]
        Funcionario buscarFuncionarioPorRut(string rut);
        [OperationContract]
        Tipo BuscarTipoServidor(int id);
        [OperationContract]
        SistemaOperativo BuscarSistemaOperativoPorId(int id);
        [OperationContract]
        Servicio BuscarServicio(string codigoServicio);
        [OperationContract]
        bool ModificarBaseDeDatos(BaseDeDatos _objBaseDatos);
        [OperationContract]
        bool ModificarServidor(Servidor _servidor);
        [OperationContract]
        Lenguaje BuscarLenguaje(int idLenguaje);
        [OperationContract]
        MotorBD buscarMotorBDPorNombreMBD(string nombre);
        [OperationContract]
        Sensibilidad buscarSensibilidad(int idsensibilidad);
        [OperationContract]
        Seguridad buscarSeguridad(int idSeguridad);
        [OperationContract]
        List<DTO> listadoSistemas();
        [OperationContract]
        bool EliminarSistema(string codigoSistema);
        [OperationContract]
        bool ModificarSistema(Sistema _objSistema, List<string> listadoBaseDatos, List<string> listadoServicios);
        [OperationContract]
        string BuscarNombreModuloPorCodigo(string cod);
        [OperationContract]
        Documento buscarDocumentoPorId(int id);
        [OperationContract]
        Usuario ObtenerUsuarioPorNombreUsuario(string nombreUsuario);
        [OperationContract]
        List<Ticket> listadoTicketPendientesPorEquipoTrabajo(int idequipo);
        [OperationContract]
        Ticket buscarTicketPorid(int id);
        [OperationContract]
        SolucionTicket buscarSolucionTicketPoridTicket(int id);
        [OperationContract]
        List<DTO> buscarTicketBaseDeDatosSolucionados(DateTime fechaInicio, DateTime fehcaFinal);
        [OperationContract]
        List<DTO> buscarTicketSistemasSolucionados(DateTime fechaInicio, DateTime fehcaFinal);
        [OperationContract]
        bool AGREGARAUDITORIA(string nombreUsuario, string rol, string nombreFuncionario, string ip, string host, string accion);
        [OperationContract]
        List<DTO> buscarTicketServidorSolucionados(DateTime fechaInicio, DateTime fehcaFinal);
        [OperationContract]
        List<DTO> buscarTicketServiciosSolucionados(DateTime fechaInicio, DateTime fehcaFinal);


        [OperationContract]
        Sistema ObtenerSistemaPorDescripcion(string descripcion);

        [OperationContract]
        Servidor ObtenerServidorPorIp(string ip);

        [OperationContract]
        Servicio ObtenerServicioPorDescripcion(string descripcion);

        [OperationContract]
        Rack ObtenerRackPorUnidad(int unidad);

        [OperationContract]
        Proveedor ObtenerProveedorPorEmail(string email);

        [OperationContract]
        Organizacion ObtenerOrganizacionPorNombre(string nombre);

        [OperationContract]
        EquipoTrabajo ObtenerEquipoPorNombre(string nombre);

        [OperationContract]
        BaseDeDatos ObtenerBaseDeDatosPorNomuser(string nomuser);

        [OperationContract]
        SalaServidores ObtenerSalaServidorPorNombre(string nombre);

        [OperationContract]
        Lenguaje ObtenerLenguajePorNombre(string nombre);
    }
}
