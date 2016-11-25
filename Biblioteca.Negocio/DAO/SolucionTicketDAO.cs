using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.DALC;
using Biblioteca.Negocio.Clases;

namespace Biblioteca.Negocio.DAO
{
    public class SolucionTicketDAO
    {
        public bool AgregarSolucionTicket(SolucionTicket _solucionTicket)
        {
            try
            {
                SOLUCION objSolucionTicketDALC = new SOLUCION();
                objSolucionTicketDALC.DESCRIPCION_SOLUCION = _solucionTicket.Descripcion_solucion;
                objSolucionTicketDALC.FECHA_SOLUCION = _solucionTicket.Fecha_solucion;
                objSolucionTicketDALC.ID_TICKET = _solucionTicket.Id_ticket;
                objSolucionTicketDALC.NOMBRE_FUNCIONARIO = _solucionTicket.Nombre_funcionario;
                objSolucionTicketDALC.EQUIPO_DE_TRABAJO = _solucionTicket.Equipo_trabajo;
                CommonBC.HomeroSystemEntities.SOLUCION.Add(objSolucionTicketDALC);
                CommonBC.HomeroSystemEntities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
