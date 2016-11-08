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
    public class ProveedorDAO
    {
        private static int idTemporal = 0;
        public bool AgregarProveedor(Proveedor _prov)
        {
            try
            {
                idTemporal++;
                PROVEEDOR provDALC = new PROVEEDOR();
                provDALC.ID_PROVEEDOR = idTemporal;
                provDALC.NOMBRE_EMPRESA = _prov.Nombre_empresa;
                provDALC.NOMBRE_ENCARGADO = _prov.Nombre_encargado;
                provDALC.TELEFONO = _prov.Telefono;
                provDALC.CELULAR = _prov.Celular;
                provDALC.DIRECCION = _prov.Direcccion;
                provDALC.EMAIL = _prov.Email;

                CommonBC.HomeroSystemEntities.PROVEEDOR.Add(provDALC);
                CommonBC.HomeroSystemEntities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Proveedor> listadoProveedores()
        {
            List<Proveedor> listProv = new List<Proveedor>();
            List<PROVEEDOR> listadoProvDALC = CommonBC.HomeroSystemEntities.PROVEEDOR.ToList();

            foreach (PROVEEDOR prov in listadoProvDALC)
            {
                Proveedor objProv = new Proveedor();
                objProv.Id_proveedor = int.Parse(prov.ID_PROVEEDOR.ToString());
                objProv.Nombre_empresa = prov.NOMBRE_EMPRESA;
                objProv.Nombre_encargado = prov.NOMBRE_ENCARGADO;
                objProv.Telefono = int.Parse(prov.TELEFONO.ToString());
                objProv.Celular = int.Parse(prov.CELULAR.ToString());
                objProv.Direcccion = prov.DIRECCION;
                objProv.Email = prov.EMAIL;


                listProv.Add(objProv);
            }

            return listProv;
        }

        public bool EliminarProveedor(int id_prov)
        {
            PROVEEDOR objProv = CommonBC.HomeroSystemEntities.PROVEEDOR.First(prov => prov.ID_PROVEEDOR == id_prov);
            if (objProv != null)
            {
                CommonBC.HomeroSystemEntities.PROVEEDOR.Remove(objProv);
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

        public bool ModificarProveedor(Proveedor _objProv)
        {
            try
            {
                PROVEEDOR objProv = CommonBC.HomeroSystemEntities.PROVEEDOR.First(prov => prov.ID_PROVEEDOR == _objProv.Id_proveedor);
                objProv.NOMBRE_EMPRESA = _objProv.Nombre_empresa;
                objProv.NOMBRE_ENCARGADO = _objProv.Nombre_encargado;
                objProv.TELEFONO = _objProv.Telefono;
                objProv.CELULAR = _objProv.Celular;
                objProv.DIRECCION = _objProv.Direcccion;
                objProv.EMAIL = _objProv.Email;

                CommonBC.HomeroSystemEntities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public Proveedor BuscarProveedor(int id_prov)
        {
            Proveedor _objProv = null;
            PROVEEDOR objProv = CommonBC.HomeroSystemEntities.PROVEEDOR.First(prov => prov.ID_PROVEEDOR == id_prov);
            if (objProv != null)
            {
                _objProv = new Proveedor();
                _objProv.Id_proveedor = int.Parse(objProv.ID_PROVEEDOR.ToString());
                _objProv.Nombre_empresa = objProv.NOMBRE_EMPRESA;
                _objProv.Nombre_encargado = objProv.NOMBRE_ENCARGADO;
                _objProv.Telefono = int.Parse(objProv.TELEFONO.ToString());
                _objProv.Celular = int.Parse(objProv.CELULAR.ToString());
                _objProv.Direcccion = objProv.DIRECCION;
                _objProv.Email = objProv.EMAIL;

            }
            return _objProv;
        }
    }
}
