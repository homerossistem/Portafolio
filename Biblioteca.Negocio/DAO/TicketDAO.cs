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


        public bool AgregarTicket(Ticket _objTicket,int id_equipo_trabajo,string nombre_funcionario)
        {
            try
            {
                TICKET objTicketDALC = new TICKET();
                objTicketDALC.FECHA_PROBLEMA = _objTicket.FechaProblema;
                objTicketDALC.PROBLEMA = _objTicket.Problema;
                objTicketDALC.RUT_FUNCIONARIO = _objTicket.Rut_funcionario;
                objTicketDALC.MODULO_COD_MODULO = _objTicket.Codigo_modulo;

                if (EnviarCorreo(id_equipo_trabajo,_objTicket,nombre_funcionario))
                {

                    CommonBC.HomeroSystemEntities.TICKET.Add(objTicketDALC);
                    CommonBC.HomeroSystemEntities.SaveChanges();
                    return true;
                }

                return false;

            }catch
            {
                throw new ArgumentException("Error al intentar agregar un ticket");
            }

        }

        private bool EnviarCorreo(int id_equipo_trabajo,Ticket objticket,string nombre_funcionario)
        {
            try
            {
                List<FUNCIONARIO> listfuncionario = CommonBC.HomeroSystemEntities.FUNCIONARIO.Where
                    (

                       fun => fun.ID_EQUIPO_TRABAJO == id_equipo_trabajo

                    ).ToList();

                Correo Correo = new Correo();
                MailMessage mnsj = new MailMessage();

                mnsj.Subject = string.Format("Contingencia en el modulo {0}",objticket.Codigo_modulo);
                foreach (FUNCIONARIO listFuncionarios in listfuncionario)
                {
                    FUNCIONARIO objfuncionarioDALC = (FUNCIONARIO)listFuncionarios;
                    mnsj.To.Add(objfuncionarioDALC.EMAIL);
                }
                mnsj.From = new MailAddress("homerossystem@gmail.com",string.Format("Homero System"));
                mnsj.Body = string.Format("{0} - {1}\n\n {2}\n\n {3}\n\n\n  ATTE. {4} - {5} ",objticket.Codigo_modulo,objticket.Problema,objticket.FechaProblema,nombre_funcionario,objticket.Rut_funcionario);
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
                            MODULO = result.tic.MODULO.NOMBRE,
                            FECHA_PROBLEMA = result.tic.FECHA_PROBLEMA,
                            FECHA_SOLUCION = result.ticso.FECHA_SOLUCION,
                            PROBLEMA = result.tic.PROBLEMA,
                            SOLUCION = result.ticso.DESCRIPCION_SOLUCION,
                            ID_EQUIPO = fun.ID_EQUIPO_TRABAJO,
                            EQUIPO = fun.EQUIPO_TRABAJO.NOMBRE_EQUIPO
                        }
                    ).Where(result=>result.ID_EQUIPO == id_equipo_Trabajo);
                foreach(var result in query)
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

            }catch
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
                        MODULO = tic.MODULO.NOMBRE,
                        FECHA_PROBLEMA = tic.FECHA_PROBLEMA,
                        FECHA_SOLUCION = ticso.FECHA_SOLUCION,
                        PROBLEMA = tic.PROBLEMA,
                        SOLUCION = ticso.DESCRIPCION_SOLUCION,
                        ID_EQUIPO = tic.FUNCIONARIO.ID_EQUIPO_TRABAJO,
                        EQUIPO = tic.FUNCIONARIO.EQUIPO_TRABAJO.NOMBRE_EQUIPO
                    });
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

            }
            catch
            {
                throw new ArgumentException("Error al Cargar los ticket");
            }
        }

    }
}
