using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.DALC;
using Biblioteca.Negocio.Clases;
using System.Security.Cryptography;


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
                objSistemaDALC.MODULO = objModuloDALC;
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

        //listado de sistemas segun el rol del usuario y de la seguridad que tenga el sistema que pueden ser altos,medio y bajo
       
        public List<Sistema> ListadoSistemasbyEquipoTrabajo(int id_equipoTrabajo, int id_rol)
        {
            List<Sistema> listadoSistema = new List<Sistema>();
            List<MODULO> listadoModuloDALC = CommonBC.HomeroSystemEntities.MODULO.Where(mod => mod.COD_MODULO.Contains("SISTEM")).ToList();

            foreach (MODULO modulo in listadoModuloDALC)
            {
                List<SISTEMA> objSistemaDALC2 = CommonBC.HomeroSystemEntities.SISTEMA.ToList();
                SISTEMA objSistemaDALC = CommonBC.HomeroSystemEntities.SISTEMA.First(sis => sis.CODIGO_SISTEMA == modulo.COD_MODULO);
                Sistema objSistema = null;
                switch (int.Parse(objSistemaDALC.ID_SEGURIDAD.ToString()))
                    {
                        case 1:
                            if (objSistemaDALC.MODULO.FUNCIONARIO.ID_EQUIPO_TRABAJO == id_equipoTrabajo)
                            {
                                objSistema = TransformarSISTEMADALCToSistema(objSistemaDALC, id_rol);
                                listadoSistema.Add(objSistema);

                            }
                            break;
                        case 2:
                        objSistema = TransformarSISTEMADALCToSistema(objSistemaDALC, id_rol);
                        foreach (SISTEMA sis in objSistemaDALC2)
                        {
                            foreach(SERVICIOS ser in objSistemaDALC.SERVICIOS)
                            {
                                if (objSistemaDALC.SERVICIOS.Count((servi => servi.COD_SERVICIO == ser.COD_SERVICIO ))>0)
                                {
                                    objSistema = TransformarSISTEMADALCToSistema(objSistemaDALC,id_rol);
                                    listadoSistema.Add(objSistema);
                                }
                            }
                        }
                        
                            break;
                        case 3:
                            objSistema = TransformarSISTEMADALCToSistema(objSistemaDALC,id_rol);
                            listadoSistema.Add(objSistema);
                            break;
                    }
                
            }
            return listadoSistema;
        }

        public List<Sistema> listadoSistemas()
        {
            List<Sistema> listadoSistemas = new List<Sistema>();
            try
            {
                
                List<SISTEMA> listadoSistemasDALC = CommonBC.HomeroSystemEntities.SISTEMA.ToList();
                foreach(SISTEMA sistema in listadoSistemasDALC)
                {
                    Sistema objSistema = new Sistema();
                    objSistema.Codigo = sistema.CODIGO_SISTEMA;
                    objSistema.Codigo_servidor = sistema.COD_SERVIDOR;
                    objSistema.Descripcion = sistema.DESCRIPCION;
                    objSistema.Nombre = sistema.MODULO.NOMBRE;
                    objSistema.Garantia = int.Parse(sistema.MODULO.GARANTIA.ToString());
                    objSistema.Id_documento = int.Parse(sistema.MODULO.ID_DOCUMENTO.ToString());
                    objSistema.Id_lenguaje = int.Parse(sistema.ID_LENGUAJE.ToString());
                    objSistema.Id_proveedor = int.Parse(sistema.MODULO.ID_PROVEEDOR.ToString());
                    objSistema.Id_seguridad = int.Parse(sistema.ID_SEGURIDAD.ToString());
                    objSistema.Id_sensibilidad = int.Parse(sistema.ID_SENSIBILIDAD.ToString());
                    objSistema.Rut_administrador = sistema.MODULO.RUT_FUNC_ADMIN;
                    listadoSistemas.Add(objSistema);
                }
            }catch
            {
                return null;
            }

            return listadoSistemas;
        }



        string key = "homerosystem";
        public string DesencriptarPasswordBaseDeDatos(string passwordsEncriptada)
        {
            byte[] keyArray;
            //convierte el texto en una secuencia de bytes
            byte[] Array_a_Descifrar =
            Convert.FromBase64String(passwordsEncriptada);

            //se llama a las clases que tienen los algoritmos
            //de encriptación se le aplica hashing
            //algoritmo MD5
            MD5CryptoServiceProvider hashmd5 =
            new MD5CryptoServiceProvider();

            keyArray = hashmd5.ComputeHash(
            UTF8Encoding.UTF8.GetBytes(key));

            hashmd5.Clear();

            TripleDESCryptoServiceProvider tdes =
            new TripleDESCryptoServiceProvider();

            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform =
            tdes.CreateDecryptor();

            byte[] resultArray =
            cTransform.TransformFinalBlock(Array_a_Descifrar,
            0, Array_a_Descifrar.Length);

            tdes.Clear();
            //se regresa en forma de cadena
            return UTF8Encoding.UTF8.GetString(resultArray);
        }


        private Sistema TransformarSISTEMADALCToSistema(SISTEMA obj,int id_rol)
        {
            ServidorDAO servidorDAO = new ServidorDAO();
            Sistema objSistema = new Sistema();
            objSistema.Codigo = obj.CODIGO_SISTEMA;
            objSistema.Codigo_servidor = obj.COD_SERVIDOR;
            objSistema.Descripcion = obj.DESCRIPCION;
            objSistema.Garantia = int.Parse(obj.MODULO.GARANTIA.ToString());
            objSistema.Id_documento = int.Parse(obj.MODULO.ID_DOCUMENTO.ToString());
            objSistema.Id_lenguaje = int.Parse(obj.ID_LENGUAJE.ToString());
            objSistema.Id_seguridad = int.Parse(obj.ID_SEGURIDAD.ToString());
            objSistema.Id_sensibilidad = int.Parse(obj.ID_SENSIBILIDAD.ToString());
            objSistema.Nombre = obj.MODULO.NOMBRE;
            objSistema.ObjServidor = servidorDAO.BuscarServidorPorCod(objSistema.Codigo_servidor);
            if(id_rol == 1 || id_rol ==2)
            {
                string decodificarContraseña = DesencriptarPasswordBaseDeDatos(objSistema.ObjServidor.ObjHashPass.Hash_pass);
                objSistema.ObjServidor.ObjHashPass.Hash_pass = decodificarContraseña;
            }
            
            foreach (SERVICIOS servi in obj.SERVICIOS)
            {
                Servicio objServicio = new Servicio();
                objServicio.Codigo = servi.COD_SERVICIO;
                objServicio.Codigo_servidor = servi.COD_SERVIDOR;
                objServicio.Descripcion = servi.DESCRIPCION;
                objServicio.Garantia = int.Parse(servi.MODULO.GARANTIA.ToString());
                objServicio.Id_documento = int.Parse(servi.MODULO.ID_DOCUMENTO.ToString());
                objServicio.Id_lenguaje = int.Parse(obj.ID_LENGUAJE.ToString());
                objServicio.Id_tipo = int.Parse(servi.ID_TIPO.ToString());
                objServicio.Nombre = obj.MODULO.NOMBRE;

                objSistema.ListadoServicios.Add(objServicio);
            }
            foreach (BASE_DATOS bd in obj.BASE_DATOS)
            {
                BaseDeDatos objbd = new BaseDeDatos();
                objbd.Codigo = bd.COD_BASE_DATOS;
                objbd.Codigo_servidor = bd.COD_SERVIDOR;
                objbd.NomUSer = bd.NOM_USUARIO;
                objbd.Garantia = int.Parse(bd.MODULO.GARANTIA.ToString());
                objbd.Id_documento = int.Parse(bd.MODULO.ID_DOCUMENTO.ToString());
                objbd.Nombre = bd.MODULO.NOMBRE;
                objbd.Id_motor = int.Parse(bd.ID_MOTOR.ToString());
                objbd.ObjHashPassBaseDatos.Cod_modulo = bd.HASH_PASS_BASE_DATOS.COD_MODULO;
                if (id_rol==1 || id_rol == 2)
                {
                    objbd.ObjHashPassBaseDatos.Hash_pass = DesencriptarPasswordBaseDeDatos(bd.HASH_PASS_BASE_DATOS.HASH_PASS);
                }
                else
                {
                    objbd.ObjHashPassBaseDatos.Hash_pass = bd.HASH_PASS_BASE_DATOS.HASH_PASS;
                }

            }

            return objSistema;
        }

        public Sistema BuscarSistema(string CodigoSistema)
        {
            Sistema objSistema = new Sistema();
            try
            {
                SISTEMA objSistemaDALC = CommonBC.HomeroSystemEntities.SISTEMA.First
                    (
                       sis => sis.CODIGO_SISTEMA == CodigoSistema
                    );
                objSistema.Codigo = objSistemaDALC.CODIGO_SISTEMA;
                objSistema.Codigo_servidor = objSistemaDALC.COD_SERVIDOR;
                objSistema.Descripcion = objSistemaDALC.DESCRIPCION;
                objSistema.Garantia = int.Parse(objSistemaDALC.MODULO.GARANTIA.ToString());
                objSistema.Id_documento = int.Parse(objSistemaDALC.MODULO.ID_DOCUMENTO.ToString());
                objSistema.Id_lenguaje = int.Parse(objSistemaDALC.ID_LENGUAJE.ToString());
                objSistema.Id_proveedor = int.Parse(objSistemaDALC.MODULO.ID_PROVEEDOR.ToString());
                objSistema.Id_seguridad = int.Parse(objSistemaDALC.ID_SEGURIDAD.ToString());
                objSistema.Id_sensibilidad = int.Parse(objSistemaDALC.ID_SENSIBILIDAD.ToString());
                objSistema.Nombre = objSistemaDALC.MODULO.NOMBRE;
                objSistema.Rut_administrador = objSistemaDALC.MODULO.RUT_FUNC_ADMIN;

                return objSistema;
            }catch
            {
                return null;
            }
        }

        public List<Sensibilidad> listadoSensibilidadSistema()
        {
            List<Sensibilidad> listadoSensibilidad = new List<Sensibilidad>();
            try
            {
                List<SENSIBILIDAD> listadoSensibilidadDALC = CommonBC.HomeroSystemEntities.SENSIBILIDAD.ToList();
                foreach(SENSIBILIDAD sen in listadoSensibilidadDALC)
                {
                    Sensibilidad objSensibilidad = new Sensibilidad();
                    objSensibilidad.Id_sensibilidad = int.Parse(sen.ID_SENSIBILIDAD.ToString());
                    objSensibilidad.Tipo_sensibilidad = sen.TIPO_SENSIBILIDAD;
                    listadoSensibilidad.Add(objSensibilidad);
                }
            }catch
            {
                return null;
            }
            return listadoSensibilidad;
        }

        public List<Seguridad> listadoSeguridadSistema()
        {
            List<Seguridad> listadoSeguridad = new List<Seguridad>();
            try
            {
                List<SEGURIDAD> listadoSeguridadDALC = CommonBC.HomeroSystemEntities.SEGURIDAD.ToList();
                foreach (SEGURIDAD seg in listadoSeguridadDALC)
                {
                    Seguridad objSeguridad = new Seguridad();
                    objSeguridad.Id_seguridad = int.Parse(seg.ID_SEGURIDAD.ToString());
                    objSeguridad.Tipo_seguridad = seg.TIPO_SEGURIDAD;
                    listadoSeguridad.Add(objSeguridad);
                }
            }
            catch
            {
                return null;
            }

            return listadoSeguridad;
        }
    }
}
