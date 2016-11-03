using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.DALC;
using Biblioteca.Negocio.Clases;

namespace Biblioteca.Negocio.DAO
{
    public class RolDAO
    {
        public List<Rol> listadoRol()
        {
            List<Rol> listadoRol = new List<Rol>();
            try
            {
                List<ROL> listadoROLDACL = CommonBC.HomeroSystemEntities.ROL.ToList();
                foreach(ROL rol in listadoROLDACL)
                {
                    Rol objrol = new Rol();
                    objrol.Id_rol = int.Parse(rol.ID_ROL.ToString());
                    objrol.Nombre_rol = rol.NOMBRE_ROL;
                    listadoRol.Add(objrol);
                }

            }catch
            {
                throw new ArgumentException("Error al listar los rol");
            }

            return listadoRol;

        }
    }
}
