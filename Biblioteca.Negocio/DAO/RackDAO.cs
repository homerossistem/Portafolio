using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.DALC;
using Biblioteca.Negocio.Clases;
using Biblioteca.Negocio.DTOs;
using System.Data.Entity.Infrastructure;

namespace Biblioteca.Negocio.DAO
{
    public class RackDAO
    {
        private static int idTemporal = 0;
        public bool AgregarRack(Rack _rack)
        {
            try
            {
                idTemporal++;
                RACK rackDALC = new RACK();
                rackDALC.ID_RACK = idTemporal;
                rackDALC.UNIDAD_RACK = _rack.Unidad_rack;
                rackDALC.ID_SALA_SERVIDOR = _rack.Id_sala;                

                CommonBC.HomeroSystemEntities.RACK.Add(rackDALC);
                CommonBC.HomeroSystemEntities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Rack> listadoRacks()
        {
            List<Rack> listRack = new List<Rack>();
            List<RACK> listadoRackDALC = CommonBC.HomeroSystemEntities.RACK.ToList();               

            foreach (RACK rack in listadoRackDALC)
            {
                Rack objRack = new Rack();
                objRack.Id_rack = int.Parse(rack.ID_RACK.ToString());
                objRack.Id_sala = int.Parse(rack.ID_SALA_SERVIDOR.ToString());
                objRack.Unidad_rack = int.Parse(rack.UNIDAD_RACK.ToString());
                listRack.Add(objRack);
            }

            return listRack;
        }

        public List<DTO> listadoRacksSalas()
        {
            List<DTO> listRack = new List<DTO>();

            var resultset = 
                CommonBC.HomeroSystemEntities.RACK.Join(
                    CommonBC.HomeroSystemEntities.SALA_SERVIDORES, rack => rack.ID_SALA_SERVIDOR, sala => sala.ID_SALA_SERVIDOR,
                    (
                        rack, sala
                    ) => new { id_rack = rack.ID_RACK, id_sala = rack.ID_SALA_SERVIDOR, unidad_rack = rack.UNIDAD_RACK, nombre_sala = sala.NOMBRE_SALA, piso = sala.PISO }
                );

            foreach(var lista in resultset)
            {
                DTO objDTO = new DTO();
                objDTO.Rack.Id_rack = int.Parse(lista.id_rack.ToString());
                objDTO.Rack.Id_sala = int.Parse(lista.id_sala.ToString());
                objDTO.Rack.Unidad_rack = int.Parse(lista.unidad_rack.ToString());
                objDTO.SalaServidores.Nombre_sala = lista.nombre_sala;
                objDTO.SalaServidores.Piso = int.Parse(lista.piso.ToString());
                listRack.Add(objDTO);
            }

            return listRack;
        }

        public bool EliminarRack(int id_rack)
        {
            RACK objrack = CommonBC.HomeroSystemEntities.RACK.First(rack => rack.ID_RACK == id_rack);
            if (objrack != null)
            {
                CommonBC.HomeroSystemEntities.RACK.Remove(objrack);
                bool saveFailed;
                do
                {
                    saveFailed = false;
                    try
                    {
                        CommonBC.HomeroSystemEntities.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        saveFailed = true;
                        ex.Entries.Single().Reload();
                    }
                    catch (DbUpdateException exx)
                    {
                        saveFailed = true;
                        exx.Entries.Single().Reload();
                    }

                } while (saveFailed);
                return true;
            }
            return false;
        }

        public bool ModificarRack(Rack _objRack)
        {
            try
            {
                RACK objRack = CommonBC.HomeroSystemEntities.RACK.First(us => us.ID_RACK == _objRack.Id_rack);
                objRack.UNIDAD_RACK = _objRack.Unidad_rack;
                objRack.ID_SALA_SERVIDOR = _objRack.Id_sala;                

                CommonBC.HomeroSystemEntities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public Rack BuscarRack(int id_rack)
        {
            Rack _objRack = null;            
            RACK objrack = CommonBC.HomeroSystemEntities.RACK.First(rack => rack.ID_RACK == id_rack);
            if (objrack != null)
            {
                _objRack = new Rack();
                _objRack.Id_rack = int.Parse(objrack.ID_RACK.ToString());
                _objRack.Id_sala = int.Parse(objrack.ID_SALA_SERVIDOR.ToString());
                _objRack.Unidad_rack = int.Parse(objrack.UNIDAD_RACK.ToString());
            }
            return _objRack;
        }

    }
}
