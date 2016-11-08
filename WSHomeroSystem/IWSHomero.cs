using System;
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
        string AgregarBaseDatos(BaseDeDatos _objBaseDatos,HashPassModulo _hashpass);
        [OperationContract]
        string AgregarServicio(Servicio objServicio, List<string> listadoCodBaseDatos);
        [OperationContract]
        string AgregarTipoServicio(TipoServicio objTipoServicio);
        [OperationContract]
        string AgregarServidor(Servidor objServidor, HashPassModulo hashServidor);
        [OperationContract]
        string AgregarSistema(Sistema objSistema);
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
        bool agregarBaseDatosServicio(string codServicio, List<string> codBaseDatos);

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
        List<SalaServidores> ListadoSalaServidores();


    }
}
