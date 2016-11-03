﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Negocio.Clases;
using Biblioteca.DALC;
namespace Biblioteca.Negocio.DAO
{
    public class LenguajeDAO
    {
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
    }
}
