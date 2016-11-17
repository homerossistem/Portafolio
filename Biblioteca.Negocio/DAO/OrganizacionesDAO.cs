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
    public class OrganizacionesDAO
    {
        public bool AgregarOrganizacion(Organizacion _org)
        {
            try
            {
                ORGANIZACION orgDALC = new ORGANIZACION();
                orgDALC.NOMBRE_ORGANIZACION = _org.Nombre_organizacion;
                orgDALC.DIRECCION = _org.Direccion;
                orgDALC.TELEFONO = _org.Telefono;
                orgDALC.EMAIL = _org.Email;

                CommonBC.HomeroSystemEntities.ORGANIZACION.Add(orgDALC);
                CommonBC.HomeroSystemEntities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Organizacion> listadoOrganizaciones()
        {
            List<Organizacion> listOrg = new List<Organizacion>();
            List<ORGANIZACION> listadoOrgDALC = CommonBC.HomeroSystemEntities.ORGANIZACION.ToList();

            foreach (ORGANIZACION org in listadoOrgDALC)
            {
                Organizacion objOrg = new Organizacion();
                objOrg.Id_organizacion = int.Parse(org.ID_ORGANIZACION.ToString());
                objOrg.Nombre_organizacion = org.NOMBRE_ORGANIZACION;
                objOrg.Direccion = org.DIRECCION;
                objOrg.Telefono = int.Parse(org.TELEFONO.ToString());
                objOrg.Email = org.EMAIL;


                listOrg.Add(objOrg);
            }

            return listOrg;
        }

        public bool EliminarOrganizacion(int id_organizacion)
        {
            ORGANIZACION objOrg = CommonBC.HomeroSystemEntities.ORGANIZACION.First(org => org.ID_ORGANIZACION == id_organizacion);
            if (objOrg != null)
            {
                CommonBC.HomeroSystemEntities.ORGANIZACION.Remove(objOrg);
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

        public bool ModificarOrganizacion (Organizacion _objOrg)
        {
            try
            {
                ORGANIZACION objOrg = CommonBC.HomeroSystemEntities.ORGANIZACION.First(org => org.ID_ORGANIZACION == _objOrg.Id_organizacion);
                objOrg.NOMBRE_ORGANIZACION = _objOrg.Nombre_organizacion;
                objOrg.DIRECCION = _objOrg.Direccion;
                objOrg.TELEFONO = _objOrg.Telefono;
                objOrg.EMAIL = _objOrg.Email;

                CommonBC.HomeroSystemEntities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public Organizacion BuscarOrganizacion(int id_Org)
        {
            Organizacion _objOrg = null;
            ORGANIZACION objOrg= CommonBC.HomeroSystemEntities.ORGANIZACION.First(org => org.ID_ORGANIZACION == id_Org);
            if (objOrg != null)
            {
                _objOrg = new Organizacion();
                _objOrg.Id_organizacion = int.Parse(objOrg.ID_ORGANIZACION.ToString());
                _objOrg.Nombre_organizacion = objOrg.NOMBRE_ORGANIZACION;
                _objOrg.Direccion = objOrg.DIRECCION;
                _objOrg.Telefono = int.Parse(objOrg.TELEFONO.ToString());
                _objOrg.Email = objOrg.EMAIL;

            }
            return _objOrg;
        }
    }
}
