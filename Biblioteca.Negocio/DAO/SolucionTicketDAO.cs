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

                CommonBC.HomeroSystemEntities.SOLUCION.Add(objSolucionTicketDALC);
                CommonBC.HomeroSystemEntities.SaveChanges();
                return true;
            }
            catch
            {
                throw new ArgumentException("Error al agregar una solucion al ticket");
            }
        }
    }
}
