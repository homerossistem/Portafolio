using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.DALC;
using Biblioteca.Negocio.Clases;
using System.Security.Cryptography;
using Biblioteca.Negocio.DTOs;
using System.Data.Entity.Infrastructure;

namespace Biblioteca.Negocio.DAO
{
    public class SistemaDAO
    {
        public bool AgregarSistema(Sistema _objSistema,List<string> listadoBaseDatos, List<string> listadoServicios)
        {
            List<BASE_DATOS> listadoBaseDatosDALC = new List<BASE_DATOS>();
            List<SERVICIOS> listadoServiciosDALC = new List<SERVICIOS>();
            try
            {
                foreach (string cod in listadoBaseDatos)
                {
                    BASE_DATOS objBaseDatos = CommonBC.HomeroSystemEntities.BASE_DATOS.First
                        (
                          bd => bd.COD_BASE_DATOS == cod
                        );
                    listadoBaseDatosDALC.Add(objBaseDatos);
                }
                foreach (string cod in listadoServicios)
                {
                    SERVICIOS objServicio = CommonBC.HomeroSystemEntities.SERVICIOS.First
                        (
                          servi => servi.COD_SERVICIO == cod
                        );
                    listadoServiciosDALC.Add(objServicio);
                }
                string codigoGenerado = GeneradorCodigSistema();
                SEGURIDAD objSeguridad = CommonBC.HomeroSystemEntities.SEGURIDAD.First
                    (seg=>seg.ID_SEGURIDAD == _objSistema.Id_seguridad);
                SENSIBILIDAD objSensibilidad = CommonBC.HomeroSystemEntities.SENSIBILIDAD.First
                    (sen=>sen.ID_SENSIBILIDAD== _objSistema.Id_sensibilidad);
                DOCUMENTO objDocumento = CommonBC.HomeroSystemEntities.DOCUMENTO.First
                    (doc=>doc.ID_DOCUMENTO == _objSistema.Id_documento);
                MODULO objModuloDALC = new MODULO();
                objModuloDALC.COD_MODULO = codigoGenerado;
                objModuloDALC.RUT_FUNC_ADMIN = _objSistema.Rut_administrador;
                objModuloDALC.NOMBRE = _objSistema.Nombre;
                objModuloDALC.GARANTIA = _objSistema.Garantia;
                objModuloDALC.ID_PROVEEDOR = int.Parse(_objSistema.Id_proveedor.ToString());
                objModuloDALC.ID_DOCUMENTO = _objSistema.Id_documento;
                objModuloDALC.DOCUMENTO = objDocumento;
                SISTEMA objSistemaDALC = new SISTEMA();
                objSistemaDALC.CODIGO_SISTEMA = codigoGenerado;
                objSistemaDALC.COD_SERVIDOR = _objSistema.Codigo_servidor;
                objSistemaDALC.DESCRIPCION = _objSistema.Descripcion;
                objSistemaDALC.ID_LENGUAJE = _objSistema.Id_lenguaje;
                objSistemaDALC.ID_SEGURIDAD = _objSistema.Id_seguridad;
                objSistemaDALC.ID_SENSIBILIDAD = _objSistema.Id_sensibilidad;
                objSistemaDALC.SENSIBILIDAD = objSensibilidad;
                objSistemaDALC.SEGURIDAD = objSeguridad;
                objSistemaDALC.MODULO = objModuloDALC;
                objSistemaDALC.BASE_DATOS = listadoBaseDatosDALC;
                objSistemaDALC.SERVICIOS = listadoServiciosDALC;
                CommonBC.HomeroSystemEntities.SISTEMA.Add(objSistemaDALC);
                CommonBC.HomeroSystemEntities.SaveChanges();

            }
            catch
            {
                throw new ArgumentException("Error al intentar agregar un Sistema");
            }

            return true;
        }
        public bool ModificarSistema(Sistema _objSistema,List<string>listadoBaseDatos,List<string>listadoServicios)
        {
            List<BASE_DATOS> listadoBaseDatosDALC = new List<BASE_DATOS>();
            List<SERVICIOS> listadoServiciosDALC = new List<SERVICIOS>();
            try
            {
                foreach (string cod in listadoBaseDatos)
                {
                    BASE_DATOS objBaseDatos = CommonBC.HomeroSystemEntities.BASE_DATOS.First
                        (
                          bd => bd.COD_BASE_DATOS == cod
                        );
                    listadoBaseDatosDALC.Add(objBaseDatos);
                }
                foreach (string cod in listadoServicios)
                {
                    SERVICIOS objServicio = CommonBC.HomeroSystemEntities.SERVICIOS.First
                        (
                          servi => servi.COD_SERVICIO == cod
                        );
                    listadoServiciosDALC.Add(objServicio);
                }
                SISTEMA objSistemaDACL = CommonBC.HomeroSystemEntities.SISTEMA.First
                    (sis => sis.CODIGO_SISTEMA == _objSistema.Codigo);
                objSistemaDACL.MODULO.NOMBRE = _objSistema.Nombre;
                objSistemaDACL.MODULO.GARANTIA = _objSistema.Garantia;
                objSistemaDACL.MODULO.ID_DOCUMENTO = _objSistema.Id_documento;
                objSistemaDACL.MODULO.RUT_FUNC_ADMIN = _objSistema.Rut_administrador;
                objSistemaDACL.MODULO.ID_PROVEEDOR = _objSistema.Id_proveedor;
                objSistemaDACL.CODIGO_SISTEMA = _objSistema.Codigo;
                objSistemaDACL.COD_SERVIDOR = _objSistema.Codigo_servidor;
                objSistemaDACL.DESCRIPCION = _objSistema.Descripcion;
                objSistemaDACL.ID_SEGURIDAD = _objSistema.Id_seguridad;
                objSistemaDACL.ID_SENSIBILIDAD = _objSistema.Id_sensibilidad;
                objSistemaDACL.BASE_DATOS = listadoBaseDatosDALC;
                objSistemaDACL.SERVICIOS = listadoServiciosDALC;
                objSistemaDACL.ID_LENGUAJE = _objSistema.Id_lenguaje;
                CommonBC.HomeroSystemEntities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool EliminarSistema(string codigoSistema)
        {
                int resultado = 0;
                SISTEMA objSistemaDALC = CommonBC.HomeroSystemEntities.SISTEMA.First
                    (sis => sis.CODIGO_SISTEMA == codigoSistema);
                MODULO objModuloDALLC = CommonBC.HomeroSystemEntities.MODULO.First
                    (mod => mod.COD_MODULO == codigoSistema);
                
                CommonBC.HomeroSystemEntities.SISTEMA.Remove(objSistemaDALC);
                CommonBC.HomeroSystemEntities.MODULO.Remove(objModuloDALLC);
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
                        CommonBC.HomeroSystemEntities.Entry(objModuloDALLC).Reload();
                        CommonBC.HomeroSystemEntities.Entry(objSistemaDALC).Reload();
                        resultado = 1;
                        exx.Entries.Single().Reload();
                    }

                } while (saveFailed);

                if (resultado == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

        }

        private string GeneradorCodigSistema()
        {
            string Codigo = string.Empty;
            string ultimoCodigo = CommonBC.HomeroSystemEntities.SISTEMA.Max(sis => sis.CODIGO_SISTEMA);
            if (ultimoCodigo == null)
            {
                Codigo = string.Format("SISTEM-{0:000000}", 1);
            }
            else
            {
                Codigo = ultimoCodigo.Substring(7);
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

        public List<Sistema> listSistemas()
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
            BaseDatosDAO objBDDAO = new BaseDatosDAO();
            ServicioDAO objServicioDAO = new ServicioDAO();
            OrganizacionesDAO objOrganizacionesDAO = new OrganizacionesDAO();
            List<BaseDeDatos> listadoBaseDeDatos = new List<BaseDeDatos>();
            List<Servicio> listadoServicio = new List<Servicio>();
            List<Organizacion> listadoOrganizacion = new List<Organizacion>();
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
                foreach (BASE_DATOS bdDALC in objSistemaDALC.BASE_DATOS)
                {
                    BaseDeDatos objBD = objBDDAO.BuscarBaseDeDatosPorCodigo(bdDALC.COD_BASE_DATOS);
                    listadoBaseDeDatos.Add(objBD);
                }
                foreach(SERVICIOS serviDALC in objSistemaDALC.SERVICIOS)
                {
                    Servicio objServicio = objServicioDAO.BuscarServicio(serviDALC.COD_SERVICIO);
                    listadoServicio.Add(objServicio);
                }
                foreach(ORGANIZACION orgaDALC in objSistemaDALC.ORGANIZACION)
                {
                    Organizacion objOrganizacion = objOrganizacionesDAO.BuscarOrganizacionPorid(int.Parse(orgaDALC.ID_ORGANIZACION.ToString()));
                    listadoOrganizacion.Add(objOrganizacion);
                }
                objSistema.ListadoOrganizacion = listadoOrganizacion;
                objSistema.ListadoBaseDatos = listadoBaseDeDatos;
                objSistema.ListadoServicios = listadoServicio;

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

        public Seguridad buscarSeguridad(int idSeguridad)
        {
            try
            {
                Seguridad objSeguridad = new Seguridad();
                SEGURIDAD objSeguridadDALC = CommonBC.HomeroSystemEntities.SEGURIDAD.First
                    (
                       seg =>seg.ID_SEGURIDAD == idSeguridad
                    );
                objSeguridad.Id_seguridad = int.Parse(objSeguridadDALC.ID_SEGURIDAD.ToString());
                objSeguridad.Tipo_seguridad = objSeguridadDALC.TIPO_SEGURIDAD;
                return objSeguridad;
            }
            catch
            {
                return null;
            }
        }

        public Sensibilidad buscarSensibilidad(int idsensibilidad)
        {
            try
            {
                Sensibilidad objSensibilidad = new Sensibilidad();
                SENSIBILIDAD objsensibilidadDALC = CommonBC.HomeroSystemEntities.SENSIBILIDAD.First
                    (
                      sen=>sen.ID_SENSIBILIDAD == idsensibilidad
                    );
                objSensibilidad.Id_sensibilidad = int.Parse(objsensibilidadDALC.ID_SENSIBILIDAD.ToString());
                objSensibilidad.Tipo_sensibilidad = objsensibilidadDALC.TIPO_SENSIBILIDAD;
                return objSensibilidad;
            }
            catch
            {
                return null;
            }
        }

        public List<DTO> listadoSistemas()
        {
            try
            {
                List<DTO> listadoSistemas = new List<DTO>();
                List<SISTEMA> listadoSistemaDALC = CommonBC.HomeroSystemEntities.SISTEMA.ToList();

                foreach(SISTEMA objSistemaDALC in listadoSistemaDALC )
                {
                    DTO objDTO = new DTO();
                    objDTO.Sistema.Codigo = objSistemaDALC.CODIGO_SISTEMA;
                    objDTO.Sistema.Codigo_servidor = objSistemaDALC.COD_SERVIDOR;
                    objDTO.Sistema.Descripcion = objSistemaDALC.DESCRIPCION;
                    objDTO.Sistema.Nombre = objSistemaDALC.MODULO.NOMBRE;
                    objDTO.Sistema.Garantia = int.Parse(objSistemaDALC.MODULO.GARANTIA.ToString());
                    objDTO.Sistema.Id_documento = int.Parse(objSistemaDALC.MODULO.ID_DOCUMENTO.ToString());
                    objDTO.Sistema.Rut_administrador = objSistemaDALC.MODULO.RUT_FUNC_ADMIN;
                    objDTO.Sistema.Id_lenguaje = int.Parse(objSistemaDALC.ID_LENGUAJE.ToString());
                    objDTO.Sistema.Id_proveedor = int.Parse(objSistemaDALC.MODULO.ID_PROVEEDOR.ToString());
                    objDTO.Sistema.Id_seguridad = int.Parse(objSistemaDALC.ID_SEGURIDAD.ToString());
                    objDTO.Sistema.Id_sensibilidad = int.Parse(objSistemaDALC.SENSIBILIDAD.ID_SENSIBILIDAD.ToString());
                    objDTO.Documento.Id_documento = int.Parse(objSistemaDALC.MODULO.DOCUMENTO.ID_DOCUMENTO.ToString());
                    objDTO.Documento.Url_documento = objSistemaDALC.MODULO.DOCUMENTO.URL_DOCUMENTO;
                    objDTO.Lenguaje.Id_lenguaje = int.Parse(objSistemaDALC.LENGUAJE.ID_LENGUAJE.ToString());
                    objDTO.Lenguaje.Nombre_lenguaje = objSistemaDALC.LENGUAJE.NOMBRE_LENGUAJE;
                    objDTO.Proveedor.Id_proveedor = int.Parse(objSistemaDALC.MODULO.PROVEEDOR1.ID_PROVEEDOR.ToString());
                    objDTO.Proveedor.Nombre_empresa = objSistemaDALC.MODULO.PROVEEDOR1.NOMBRE_EMPRESA;
                    objDTO.Seguridad.Id_seguridad = int.Parse(objSistemaDALC.SEGURIDAD.ID_SEGURIDAD.ToString());
                    objDTO.Seguridad.Tipo_seguridad = objSistemaDALC.SEGURIDAD.TIPO_SEGURIDAD;
                    objDTO.Sencibilidad.Id_sensibilidad = int.Parse(objSistemaDALC.SENSIBILIDAD.ID_SENSIBILIDAD.ToString());
                    objDTO.Sencibilidad.Tipo_sensibilidad = objSistemaDALC.SENSIBILIDAD.TIPO_SENSIBILIDAD;
                    objDTO.Funcionario.Nombre = objSistemaDALC.MODULO.FUNCIONARIO.NOMBRE;
                    objDTO.Funcionario.Apellido = objSistemaDALC.MODULO.FUNCIONARIO.APELLIDO;
                    objDTO.Funcionario.Id_equipo_trabajo = int.Parse(objSistemaDALC.MODULO.FUNCIONARIO.ID_EQUIPO_TRABAJO.ToString());
                    objDTO.Servidor.Nombre = objSistemaDALC.SERVIDOR.MODULO.NOMBRE;

                    listadoSistemas.Add(objDTO);
                }
                return listadoSistemas;
            }
            catch
            {
                return null;
            }
        }
    }
}
