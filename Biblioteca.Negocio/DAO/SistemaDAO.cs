using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.DALC;
using Biblioteca.Negocio.Clases;

namespace Biblioteca.Negocio.DAO
{
    public class SistemaDAO
    {
        public bool AgregarSistema(Sistema _objSistema)
        {
            try
            {
                string codigoGenerado = GeneradorCodigSistema();
                MODULO objModuloDALC = new MODULO();
                objModuloDALC.COD_MODULO = codigoGenerado;
                objModuloDALC.NOMBRE = _objSistema.Nombre;
                objModuloDALC.GARANTIA = _objSistema.Garantia;
                objModuloDALC.ID_DOCUMENTO = _objSistema.Id_documento;
                SISTEMA objSistemaDALC = new SISTEMA();
                objSistemaDALC.CODIGO_SISTEMA = codigoGenerado;
                objSistemaDALC.COD_SERVIDOR = _objSistema.Codigo_servidor;
                objSistemaDALC.DESCRIPCION = _objSistema.Descripcion;
                objSistemaDALC.ID_LENGUAJE = _objSistema.Id_lenguaje;
                objSistemaDALC.ID_SEGURIDAD = _objSistema.Id_seguridad;
                objSistemaDALC.ID_SENSIBILIDAD = _objSistema.Id_sensibilidad;
                CommonBC.HomeroSystemEntities.MODULO.Add(objModuloDALC);
                CommonBC.HomeroSystemEntities.SISTEMA.Add(objSistemaDALC);
                CommonBC.HomeroSystemEntities.SaveChanges();

            }
            catch
            {
                throw new ArgumentException("Error al intentar agregar un Sistema");
            }

            return true;
        }

        private string GeneradorCodigSistema()
        {
            string Codigo = string.Empty;
            string ultimoCodigo = CommonBC.HomeroSystemEntities.BASE_DATOS.Max(bd => bd.COD_BASE_DATOS);
            if (ultimoCodigo == null)
            {
                Codigo = string.Format("SISTEM-{0:000000}", 1);
            }
            else
            {
                Codigo = ultimoCodigo.Substring(3);
                int digitoCodigo = int.Parse(Codigo) + 1;
                Codigo = string.Format("SISTEM-{0:000000}", digitoCodigo);
            }
            return Codigo;
        }
    }
}
