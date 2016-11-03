using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Negocio.Clases;
using Biblioteca.DALC;
namespace Biblioteca.Negocio.DAO
{
   public class EquipoTrabajoDAO
   {
        public List<EquipoTrabajo> listadoEquiposDeTrabajo()
        {
            List<EquipoTrabajo> listaEquipos = new List<EquipoTrabajo>();
            try
            {
                List<EQUIPO_TRABAJO> listadoEquipoDACL = CommonBC.HomeroSystemEntities.EQUIPO_TRABAJO.ToList();
                foreach (EQUIPO_TRABAJO equipo in listadoEquipoDACL)
                {
                    EquipoTrabajo objEquipoTrabajo = new EquipoTrabajo();
                    objEquipoTrabajo.Id_equipo = int.Parse(equipo.ID_EQUIPO_TRABAJO.ToString());
                    objEquipoTrabajo.Nombre_equipo = equipo.NOMBRE_EQUIPO;
                    listaEquipos.Add(objEquipoTrabajo);
                }
            }
            catch
            {

            }

            return listaEquipos;
        }
   }

}
