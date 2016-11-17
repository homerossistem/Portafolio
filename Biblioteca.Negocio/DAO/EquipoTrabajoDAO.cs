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



        public bool AgregarEquipoTrabajo(EquipoTrabajo _equipo )
        {
            try
            {
                EQUIPO_TRABAJO equipoDALC = new EQUIPO_TRABAJO();
                equipoDALC.NOMBRE_EQUIPO = _equipo.Nombre_equipo;

                CommonBC.HomeroSystemEntities.EQUIPO_TRABAJO.Add(equipoDALC);
                CommonBC.HomeroSystemEntities.SaveChanges();
            }
            catch
            {
                return false;
            }


            return true;
        }


        public bool ExisteEquipo(String nombre)
        {
            int count = CommonBC.HomeroSystemEntities.EQUIPO_TRABAJO.Count
                  (
                    equ => equ.NOMBRE_EQUIPO == nombre
                  );
            if (count >= 1)
            {
                return true;
            }
            return false;

        }

        public EquipoTrabajo buscarEquipo(String nombre)
        {
            EquipoTrabajo equipo = new EquipoTrabajo();
            EQUIPO_TRABAJO equipoDALC = CommonBC.HomeroSystemEntities.EQUIPO_TRABAJO.First
                (
                    equ => equ.NOMBRE_EQUIPO == nombre
                );
            equipo.Id_equipo = int.Parse(equipoDALC.ID_EQUIPO_TRABAJO.ToString());
            equipo.Nombre_equipo = equipoDALC.NOMBRE_EQUIPO;

            return equipo;
        }


        public bool eliminarEquipo(EquipoTrabajo _equipoTrabajo)
        {
            try
            {
                EQUIPO_TRABAJO equipo = CommonBC.HomeroSystemEntities.EQUIPO_TRABAJO.First
                    (
                        equ => equ.ID_EQUIPO_TRABAJO == _equipoTrabajo.Id_equipo
                    );

                CommonBC.HomeroSystemEntities.EQUIPO_TRABAJO.Remove(equipo);
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
