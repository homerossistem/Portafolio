using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.DALC;
using Biblioteca.Negocio.Clases;
using System.Net.Mail;
using Biblioteca.Negocio.DTOs;

namespace Biblioteca.Negocio.DAO
{
    public class TicketDAO
    {


        public bool AgregarTicket(Ticket _objTicket, int id_equipo_trabajo, string nombre_funcionario)
        {
            try
            {
                TICKET objTicketDALC = new TICKET();
                objTicketDALC.FECHA_PROBLEMA = _objTicket.FechaProblema.Date;
                objTicketDALC.PROBLEMA = _objTicket.Problema;
                objTicketDALC.RUT_FUNCIONARIO = _objTicket.Rut_funcionario;
                objTicketDALC.MODULO_COD_MODULO = _objTicket.Codigo_modulo;
                objTicketDALC.NOMBRE_MODULO = _objTicket.Nombre_modulo;

                if (EnviarCorreo(id_equipo_trabajo, _objTicket, nombre_funcionario))
                {

                    CommonBC.HomeroSystemEntities.TICKET.Add(objTicketDALC);
                    CommonBC.HomeroSystemEntities.SaveChanges();
                    return true;
                }

                return false;

            } catch
            {
                throw new ArgumentException("Error al intentar agregar un ticket");
            }

        }

        private bool EnviarCorreo(int id_equipo_trabajo, Ticket objticket, string nombre_funcionario)
        {
            try
            {
                
                MODULO objModuloDALC = CommonBC.HomeroSystemEntities.MODULO.First
                    (
                      mo => mo.COD_MODULO == objticket.Codigo_modulo
                    );
                List<FUNCIONARIO> listfuncionario = CommonBC.HomeroSystemEntities.FUNCIONARIO.Where
                    (

                       fun => fun.ID_EQUIPO_TRABAJO == objModuloDALC.FUNCIONARIO.ID_EQUIPO_TRABAJO

                    ).ToList();
                Correo Correo = new Correo();
                MailMessage mnsj = new MailMessage();
                FUNCIONARIO objfuncionarioDALC = new FUNCIONARIO();
                mnsj.Subject = string.Format("Contingencia en el modulo {0}", objticket.Codigo_modulo);
                foreach (FUNCIONARIO listFuncionarios in listfuncionario)
                {
                    objfuncionarioDALC = (FUNCIONARIO)listFuncionarios;
                    mnsj.To.Add(objfuncionarioDALC.EMAIL);
                }
                mnsj.From = new MailAddress("homerossystem@gmail.com", string.Format("Homero System"));
                mnsj.Body = string.Format("Codigo Modulo : {0}\n\r" +
                                          "Nombre Modulo : {1}\n\r" +
                                          "Problema      : {2}\n\r" +
                                          "Fecha y Hora  : {3}\n\r" +
                                          "Enviado Por   : {4} - {5}\n\r" +
                                          "ATTE\n\r HOMERO SYSTEM", objticket.Codigo_modulo, objModuloDALC.NOMBRE, objticket.Problema, objticket.FechaProblema, nombre_funcionario, objticket.Rut_funcionario);
                mnsj.IsBodyHtml = false;
                Correo.EnviarCorreo(mnsj);
                return true;
            }
            catch
            {
                throw new ArgumentException("error al intentar enviar el correo");
            }
        }

        public List<DTO> listarTicketConSolucionPorEquipoTrabajo(int id_equipo_Trabajo)
        {
            try
            {
                List<DTO> listadoTicketSolucionados = new List<DTO>();
                var query = CommonBC.HomeroSystemEntities.TICKET.Join(
                     CommonBC.HomeroSystemEntities.SOLUCION, tic => tic.ID_TICKET, ticso => ticso.ID_TICKET, (tic, ticso) => new
                     {
                         tic,
                         ticso
                     }).Join(
                        CommonBC.HomeroSystemEntities.FUNCIONARIO, result => result.tic.RUT_FUNCIONARIO, fun => fun.RUT_FUNCIONARIO, (result, fun) => new
                        {
                            ID_TICKET = result.tic.ID_TICKET,
                            COD_MODULO = result.tic.MODULO_COD_MODULO,
                            MODULO = result.tic.NOMBRE_MODULO,
                            FECHA_PROBLEMA = result.tic.FECHA_PROBLEMA,
                            FECHA_SOLUCION = result.ticso.FECHA_SOLUCION,
                            PROBLEMA = result.tic.PROBLEMA,
                            SOLUCION = result.ticso.DESCRIPCION_SOLUCION,
                            ID_EQUIPO = fun.ID_EQUIPO_TRABAJO,
                            EQUIPO = fun.EQUIPO_TRABAJO.NOMBRE_EQUIPO
                        }
                    ).Where(result => result.ID_EQUIPO == id_equipo_Trabajo);
                foreach (var result in query)
                {
                    DTO objDTO = new DTO();
                    objDTO.Ticket.Id_ticket = int.Parse(result.ID_TICKET.ToString());
                    objDTO.Ticket.Codigo_modulo = result.COD_MODULO;
                    objDTO.Modulo.Nombre = result.MODULO;
                    objDTO.Ticket.FechaProblema = DateTime.Parse(result.FECHA_PROBLEMA.ToString());
                    objDTO.SolucionTicket.Fecha_solucion = DateTime.Parse(result.FECHA_SOLUCION.ToString());
                    objDTO.Ticket.Problema = result.PROBLEMA;
                    objDTO.SolucionTicket.Descripcion_solucion = result.SOLUCION;
                    objDTO.EquipoTrabajo.Id_equipo = int.Parse(result.ID_EQUIPO.ToString());
                    objDTO.EquipoTrabajo.Nombre_equipo = result.EQUIPO;
                    listadoTicketSolucionados.Add(objDTO);

                }

                return listadoTicketSolucionados;

            } catch
            {
                throw new ArgumentException("Error al Cargar los ticket");
            }
        }

        public List<DTO> ListarTodosLosTicketConSolucion()
        {
            try
            {
                List<DTO> listadoTicketSolucionados = new List<DTO>();
                var query = CommonBC.HomeroSystemEntities.TICKET.Join(
                    CommonBC.HomeroSystemEntities.SOLUCION, tic => tic.ID_TICKET, ticso => ticso.ID_TICKET, (tic, ticso) => new
                    {
                        ID_TICKET = tic.ID_TICKET,
                        COD_MODULO = tic.MODULO_COD_MODULO,
                        MODULO = tic.NOMBRE_MODULO,
                        FECHA_PROBLEMA = tic.FECHA_PROBLEMA,
                        FECHA_SOLUCION = ticso.FECHA_SOLUCION,
                        PROBLEMA = tic.PROBLEMA,
                        SOLUCION = ticso.DESCRIPCION_SOLUCION,
                        ID_EQUIPO = tic.FUNCIONARIO.ID_EQUIPO_TRABAJO,
                        EQUIPO = tic.FUNCIONARIO.EQUIPO_TRABAJO.NOMBRE_EQUIPO,
                        NOMBREFUNCIONARIO = ticso.NOMBRE_FUNCIONARIO,
                        EQUIPOTRABAJO = ticso.EQUIPO_DE_TRABAJO,
                        ID_SOLUCION = ticso.ID_SOLUCION
                    });
                foreach (var result in query)
                {
                    DTO objDTO = new DTO();
                    objDTO.Ticket.Id_ticket = int.Parse(result.ID_TICKET.ToString());
                    objDTO.Ticket.Codigo_modulo = result.COD_MODULO;
                    objDTO.Ticket.FechaProblema = DateTime.Parse(result.FECHA_PROBLEMA.ToString());
                    objDTO.Ticket.Nombre_modulo = result.MODULO;
                    objDTO.Ticket.Problema = result.PROBLEMA;
                    objDTO.SolucionTicket.Fecha_solucion = DateTime.Parse(result.FECHA_SOLUCION.ToString());
                    objDTO.SolucionTicket.Equipo_trabajo = result.EQUIPOTRABAJO;
                    objDTO.SolucionTicket.Nombre_funcionario = result.NOMBREFUNCIONARIO;
                    objDTO.SolucionTicket.Descripcion_solucion = result.SOLUCION;
                    objDTO.EquipoTrabajo.Id_equipo = int.Parse(result.ID_EQUIPO.ToString());
                    objDTO.EquipoTrabajo.Nombre_equipo = result.EQUIPO;
                    objDTO.SolucionTicket.Id_solucion = int.Parse(result.ID_SOLUCION.ToString());
                    listadoTicketSolucionados.Add(objDTO);

                }

                return listadoTicketSolucionados;

            }
            catch
            {
                throw new ArgumentException("Error al Cargar los ticket");
            }
        }

        public string BuscarNombreModuloPorCodigo(string cod)
        {
            MODULO objModuloDALC = CommonBC.HomeroSystemEntities.MODULO.First
                (mod => mod.COD_MODULO == cod);

            return objModuloDALC.NOMBRE;
        }

        public List<Ticket> listadoTicketPendientesPorEquipoTrabajo(int idequipo)
        {
            try {
                List<TICKET> listadoTicketDALC = CommonBC.HomeroSystemEntities.TICKET.ToList();
                List<Ticket> listadoTicket = new List<Ticket>();
                foreach (TICKET objTicketDALC in listadoTicketDALC)
                {
                    if (objTicketDALC.SOLUCION.Count == 0 && objTicketDALC.FUNCIONARIO.ID_EQUIPO_TRABAJO == idequipo)
                    {
                        Ticket objticket = buscarTicketPorid(int.Parse(objTicketDALC.ID_TICKET.ToString()));
                        listadoTicket.Add(objticket);
                    }
                }
                return listadoTicket;
            }catch
            {
                return null;
            }
        }

        public Ticket buscarTicketPorid(int id)
        {
            try
            {
                TICKET objTICKETDALC = CommonBC.HomeroSystemEntities.TICKET.First
                    (
                     tick => tick.ID_TICKET == id
                    );
                Ticket objTicket = new Ticket();
                objTicket.Id_ticket = int.Parse(objTICKETDALC.ID_TICKET.ToString());
                objTicket.Codigo_modulo = objTICKETDALC.MODULO_COD_MODULO;
                objTicket.FechaProblema = objTICKETDALC.FECHA_PROBLEMA;
                objTicket.Nombre_modulo = objTICKETDALC.NOMBRE_MODULO;
                objTicket.Problema = objTICKETDALC.PROBLEMA;
                objTicket.Rut_funcionario = objTICKETDALC.RUT_FUNCIONARIO;
                return objTicket;
            }catch
            {
                return null;
            }
        }

        public SolucionTicket buscarSolucionTicketPoridTicket(int id)
        {
            try
            {
                SOLUCION objSolucion = CommonBC.HomeroSystemEntities.SOLUCION.First
                    (so=>so.ID_TICKET == id);

                SolucionTicket objSolucionTicket = new SolucionTicket();
                objSolucionTicket.Id_solucion = int.Parse(objSolucion.ID_TICKET.ToString());
                objSolucionTicket.Descripcion_solucion = objSolucion.DESCRIPCION_SOLUCION;
                objSolucionTicket.Equipo_trabajo = objSolucion.EQUIPO_DE_TRABAJO;
                objSolucionTicket.Fecha_solucion = DateTime.Parse(objSolucion.FECHA_SOLUCION.ToString());
                objSolucionTicket.Id_ticket = int.Parse(objSolucion.ID_TICKET.ToString());
                objSolucionTicket.Nombre_funcionario = objSolucion.NOMBRE_FUNCIONARIO;

                return objSolucionTicket;
            }catch
            {
                return null;
            }
        }

        public List<DTO> buscarTicketBaseDeDatosSolucionados(DateTime fechaInicio,DateTime fehcaFinal)
        {
            BaseDatosDAO objBaseDatosDAO = new BaseDatosDAO();
            FuncionarioDAO objfuncionarioDA =  new FuncionarioDAO();
            ServidorDAO objServidorDAO = new ServidorDAO();
            List<DTO> listadoTicketContingeciaBD = new List<DTO>();
            try
            {
                List<SOLUCION> listado = CommonBC.HomeroSystemEntities.SOLUCION.Where
                     (so => so.TICKET.FECHA_PROBLEMA >= fechaInicio.Date & so.TICKET.FECHA_PROBLEMA <= fehcaFinal.Date & so.TICKET.MODULO_COD_MODULO.Contains("BD")).ToList();

                foreach (SOLUCION objSolucion in listado)
                {
                    TimeSpan ts = DateTime.Parse(objSolucion.FECHA_SOLUCION.ToString()) - DateTime.Parse(objSolucion.TICKET.FECHA_PROBLEMA.ToString());
                    DTO objSolucionTicket = new DTO();
                    objSolucionTicket.SolucionTicket.Id_solucion = int.Parse(objSolucion.ID_TICKET.ToString());
                    objSolucionTicket.SolucionTicket.Descripcion_solucion = objSolucion.DESCRIPCION_SOLUCION;
                    objSolucionTicket.SolucionTicket.Equipo_trabajo = objSolucion.EQUIPO_DE_TRABAJO;
                    objSolucionTicket.SolucionTicket.Fecha_solucion = DateTime.Parse(objSolucion.FECHA_SOLUCION.ToString());
                    objSolucionTicket.SolucionTicket.Id_ticket = int.Parse(objSolucion.ID_TICKET.ToString());
                    objSolucionTicket.SolucionTicket.Nombre_funcionario = objSolucion.NOMBRE_FUNCIONARIO;
                    objSolucionTicket.Ticket.Codigo_modulo = objSolucion.TICKET.MODULO_COD_MODULO;
                    objSolucionTicket.Ticket.Nombre_modulo = objSolucion.TICKET.NOMBRE_MODULO;
                    objSolucionTicket.Ticket.FechaProblema = objSolucion.TICKET.FECHA_PROBLEMA;
                    objSolucionTicket.BaseDeDatos = objBaseDatosDAO.BuscarBaseDeDatosPorCodigo(objSolucion.TICKET.MODULO_COD_MODULO);
                    objSolucionTicket.Servidor = objServidorDAO.BuscarServidorPorCod(objSolucionTicket.BaseDeDatos.Codigo_servidor);
                    objSolucionTicket.Funcionario = objfuncionarioDA.buscarFuncionarioPorRut(objSolucionTicket.BaseDeDatos.Rut_administrador);
                    objSolucionTicket.SolucionTicket.Tinactividad = ts.Days;

                    listadoTicketContingeciaBD.Add(objSolucionTicket);
                }

                return listadoTicketContingeciaBD;
            }
            catch
            {
                return null;
            }
        }

        public List<DTO> buscarTicketSistemasSolucionados(DateTime fechaInicio, DateTime fehcaFinal)
        {
            FuncionarioDAO objfuncionarioDA = new FuncionarioDAO();
            ServidorDAO objServidorDAO = new ServidorDAO();
            SistemaDAO objSistemaDAO = new SistemaDAO();
            List<DTO> listadoTicketContingeciaSistema = new List<DTO>();
            try
            {
                List<SOLUCION> listado = CommonBC.HomeroSystemEntities.SOLUCION.Where
                     (so => so.TICKET.FECHA_PROBLEMA >= fechaInicio.Date & so.TICKET.FECHA_PROBLEMA <= fehcaFinal.Date & so.TICKET.MODULO_COD_MODULO.Contains("SISTEM")).ToList();

                foreach (SOLUCION objSolucion in listado)
                {
                    TimeSpan ts = DateTime.Parse(objSolucion.FECHA_SOLUCION.ToString()) - DateTime.Parse(objSolucion.TICKET.FECHA_PROBLEMA.ToString());
                    DTO objSolucionTicket = new DTO();
                    objSolucionTicket.SolucionTicket.Id_solucion = int.Parse(objSolucion.ID_TICKET.ToString());
                    objSolucionTicket.SolucionTicket.Descripcion_solucion = objSolucion.DESCRIPCION_SOLUCION;
                    objSolucionTicket.SolucionTicket.Equipo_trabajo = objSolucion.EQUIPO_DE_TRABAJO;
                    objSolucionTicket.SolucionTicket.Fecha_solucion = DateTime.Parse(objSolucion.FECHA_SOLUCION.ToString());
                    objSolucionTicket.SolucionTicket.Id_ticket = int.Parse(objSolucion.ID_TICKET.ToString());
                    objSolucionTicket.SolucionTicket.Nombre_funcionario = objSolucion.NOMBRE_FUNCIONARIO;
                    objSolucionTicket.Ticket.Codigo_modulo = objSolucion.TICKET.MODULO_COD_MODULO;
                    objSolucionTicket.Ticket.Nombre_modulo = objSolucion.TICKET.NOMBRE_MODULO;
                    objSolucionTicket.Ticket.FechaProblema = objSolucion.TICKET.FECHA_PROBLEMA;
                    objSolucionTicket.Sistema = objSistemaDAO.BuscarSistema(objSolucion.TICKET.MODULO_COD_MODULO);
                    objSolucionTicket.Servidor = objServidorDAO.BuscarServidorPorCod(objSolucionTicket.Sistema.Codigo_servidor);
                    objSolucionTicket.Funcionario = objfuncionarioDA.buscarFuncionarioPorRut(objSolucionTicket.Sistema.Rut_administrador);
                    objSolucionTicket.SolucionTicket.Tinactividad = ts.Days;

                    listadoTicketContingeciaSistema.Add(objSolucionTicket);
                }

                return listadoTicketContingeciaSistema;
            }
            catch
            {
                return null;
            }
        }

    }
}
