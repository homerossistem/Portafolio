using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Negocio.Clases;
using Biblioteca.DALC;
using System.Data.Entity.Infrastructure;

namespace Biblioteca.Negocio.DAO
{
    public class LenguajeDAO
    {
        public bool AgregarLenguaje(Lenguaje _lenguaje)
        {
            try {
                LENGUAJE objLenguajeDALC = new LENGUAJE();
                objLenguajeDALC.NOMBRE_LENGUAJE = _lenguaje.Nombre_lenguaje;
                CommonBC.HomeroSystemEntities.LENGUAJE.Add(objLenguajeDALC);
                CommonBC.HomeroSystemEntities.SaveChanges();

                return true;
            } catch
            {
                return false;
            }


        }
        public List<Lenguaje> listadoLenguajes()
        {
            List<Lenguaje> listadoLenguaje = new List<Lenguaje>();
            try
            {
                List<LENGUAJE> listadoLenguajeDALC = CommonBC.HomeroSystemEntities.LENGUAJE.ToList();
                foreach (LENGUAJE objlenguajeDALC in listadoLenguajeDALC)
                {
                    Lenguaje objLenguaje = new Lenguaje();
                    objLenguaje.Id_lenguaje = int.Parse(objlenguajeDALC.ID_LENGUAJE.ToString());
                    objLenguaje.Nombre_lenguaje = objlenguajeDALC.NOMBRE_LENGUAJE;
                    listadoLenguaje.Add(objLenguaje);
                }
            }
            catch
            {

            }

            return listadoLenguaje;
        }

        public bool EliminarLenguaje(int idLenguaje)
        {
            int resultado = 0;
            LENGUAJE objLenguajeDALC = CommonBC.HomeroSystemEntities.LENGUAJE.First
                (
                   lengu => lengu.ID_LENGUAJE == idLenguaje
                );
            if (objLenguajeDALC.SERVICIOS.Count == 0 && objLenguajeDALC.SISTEMA.Count == 0)
            {
                CommonBC.HomeroSystemEntities.LENGUAJE.Remove(objLenguajeDALC);
                bool saveFailed;
                do
                {
                    saveFailed = false;
                    try
                    {
                        CommonBC.HomeroSystemEntities.SaveChanges();
                    }
                    catch (DbUpdateException exx)
                    {
                        saveFailed = true;
                        exx.Entries.Single().Reload();
                        resultado = 1;
                    }

                } while (saveFailed);
            }else
            {
                resultado = 1;
            }
            if(resultado == 0)
            {
                return true;
            } else
            {
                return false;
            }

        }

        public Lenguaje BuscarLenguaje(int idLenguaje)
        {
            try
            {
                Lenguaje objLenguaje = new Lenguaje();
                LENGUAJE objLengujaeDALC = CommonBC.HomeroSystemEntities.LENGUAJE.First
                    (
                      leng => leng.ID_LENGUAJE == idLenguaje
                    );
                objLenguaje.Id_lenguaje = int.Parse(objLengujaeDALC.ID_LENGUAJE.ToString());
                objLenguaje.Nombre_lenguaje = objLengujaeDALC.NOMBRE_LENGUAJE;
                return objLenguaje;
            }
            catch
            {
                return null;
            }
        }


        public Lenguaje ObtenerLeguajePorNombre(string nombre)
        {
            try
            {
                Lenguaje objLenguaje = new Lenguaje();
                LENGUAJE objLengujaeDALC = CommonBC.HomeroSystemEntities.LENGUAJE.First
                    (
                      leng => leng.NOMBRE_LENGUAJE== nombre
                    );
                objLenguaje.Id_lenguaje = int.Parse(objLengujaeDALC.ID_LENGUAJE.ToString());
                objLenguaje.Nombre_lenguaje = objLengujaeDALC.NOMBRE_LENGUAJE;
                return objLenguaje;
            }
            catch
            {
                return null;
            }
        }
    }
}
