using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.DALC;
using Biblioteca.Negocio.Clases;
using Biblioteca.Negocio.DTOs;

namespace Biblioteca.Negocio.DAO
{
    public class ServicioDAO
    {
        public bool agregarServicio(Servicio _servicio,List<string> codBaseDatos)
        {
            List<BASE_DATOS> listadoBaseDatos = new List<BASE_DATOS>();
            try
            {
                string codigoServicio = GeneradorCodigoServicio();
                MODULO objModuloDALC = new MODULO();
                objModuloDALC.COD_MODULO = codigoServicio;
                objModuloDALC.NOMBRE = _servicio.Nombre;
                objModuloDALC.GARANTIA = _servicio.Garantia;
                objModuloDALC.ID_DOCUMENTO = _servicio.Id_documento;
                SERVICIOS objServicioDALC = new SERVICIOS();
                objServicioDALC.COD_SERVICIO = codigoServicio;
                objServicioDALC.COD_SERVIDOR = _servicio.Codigo_servidor;
                objServicioDALC.DESCRIPCION = _servicio.Descripcion;
                objServicioDALC.ID_TIPO = _servicio.Id_tipo;
                objServicioDALC.ID_LENGUAJE = _servicio.Id_lenguaje;
                objServicioDALC.MODULO = objModuloDALC;
                if(codBaseDatos.Count>=1)
                {
                    foreach (string cod in codBaseDatos)
                    {
                        BASE_DATOS objBaseDatos = CommonBC.HomeroSystemEntities.BASE_DATOS.First
                            (
                              bd => bd.COD_BASE_DATOS == cod
                            );
                        listadoBaseDatos.Add(objBaseDatos);
                    }
                    objServicioDALC.BASE_DATOS = listadoBaseDatos;
                }
                    
                CommonBC.HomeroSystemEntities.SERVICIOS.Add(objServicioDALC);
                CommonBC.HomeroSystemEntities.SaveChanges();
                return true;
            }catch
            {
                throw new ArgumentException("Error Al Agregar un Servicio");
            }
        }

        public bool AgregarTipoServicio(TipoServicio _tipoServicio)
        {
            try
            {
                TIPO_SERVICIO objTipoServicioDALC = new TIPO_SERVICIO();
                objTipoServicioDALC.TIPO = _tipoServicio.Tipo_servicio;
                CommonBC.HomeroSystemEntities.TIPO_SERVICIO.Add(objTipoServicioDALC);
                CommonBC.HomeroSystemEntities.SaveChanges();
                return true;
            }catch
            {
                throw new ArgumentException("Error al agregar un tipo Servicio");
            }
        }

        private string GeneradorCodigoServicio()
        {
            string Codigo = string.Empty;
            string ultimoCodigo = CommonBC.HomeroSystemEntities.SERVICIOS.Max(ser => ser.COD_SERVICIO);
            if (ultimoCodigo == null)
            {
                Codigo = string.Format("SERVI-{0:000000}", 1);
            }
            else
            {
                Codigo = ultimoCodigo.Substring(6);
                int digitoCodigo = int.Parse(Codigo) + 1;
                Codigo = string.Format("SERVI-{0:000000}", digitoCodigo);
            }
            return Codigo;
        }

        public List<DTO> listadoServicios()
        {
            List<DTO> listadoServicios = new List<DTO>();
            try
            {
                var listadoServiciosDALC = CommonBC.HomeroSystemEntities.SERVICIOS.Join
                    (
                        CommonBC.HomeroSystemEntities.SERVIDOR, ser => ser.COD_SERVIDOR, servi => servi.COD_SERVIDOR, (ser, servi) => new
                        {
                            ser,
                            servi
                        }

                    ).Join
                    (
                        CommonBC.HomeroSystemEntities.DOCUMENTO, ser => ser.ser.MODULO.ID_DOCUMENTO, doc => doc.ID_DOCUMENTO, (ser, doc) => new
                        {
                            ser,
                            doc
                        }
                    ).Join
                    (
                        CommonBC.HomeroSystemEntities.TIPO_SERVICIO, ser => ser.ser.ser.ID_TIPO, tser => tser.ID_TIPO, (ser, tser) => new
                        {
                            ser,
                            tser
                        }
                    ).Join
                    (
                        CommonBC.HomeroSystemEntities.LENGUAJE,ser=>ser.ser.ser.ser.ID_LENGUAJE,leng=>leng.ID_LENGUAJE,(ser,leng)=>new
                        {
                            CodigoServicio = ser.ser.ser.ser.COD_SERVICIO,
                            NombreServicio = ser.ser.ser.ser.MODULO.NOMBRE,
                            IdTipoServicio = ser.tser.ID_TIPO,
                            TipoServicio = ser.tser.TIPO,
                            DescripcionServicio = ser.ser.ser.ser.DESCRIPCION,
                            CodigoServidor = ser.ser.ser.servi.COD_SERVIDOR,
                            NombreServidor = ser.ser.ser.servi.MODULO.NOMBRE,
                            IdDocumento = ser.ser.doc.ID_DOCUMENTO,
                            IDLENGUAJE = leng.ID_LENGUAJE,
                            LENGUAJE = leng.NOMBRE_LENGUAJE,
                            UrlDocumento = ser.ser.doc.URL_DOCUMENTO
                        }
                    );

            foreach(var result in listadoServiciosDALC)
            {
                    DTO objDTO = new DTO();
                    objDTO.Servicio.Codigo = result.CodigoServicio;
                    objDTO.Servicio.Nombre = result.NombreServicio;
                    objDTO.TipoServicio.Id_tipoServicio = int.Parse(result.IdTipoServicio.ToString());
                    objDTO.TipoServicio.Tipo_servicio = result.TipoServicio;
                    objDTO.Servicio.Descripcion = result.DescripcionServicio;
                    objDTO.Servidor.Codigo = result.CodigoServidor;
                    objDTO.Servidor.Nombre = result.NombreServidor;
                    objDTO.Documento.Id_documento = int.Parse(result.IdDocumento.ToString());
                    objDTO.Documento.Url_documento = result.UrlDocumento;
                    objDTO.Lenguaje.Id_lenguaje = int.Parse(result.IDLENGUAJE.ToString());
                    objDTO.Lenguaje.Nombre_lenguaje = result.LENGUAJE;
                    listadoServicios.Add(objDTO);
            }
             
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return listadoServicios;
        }

        public List<TipoServicio> listadoTipoServicio()
        {
            List<TipoServicio> listadoTipoServicio = new List<TipoServicio>();
            var listTipoSercioDALC = CommonBC.HomeroSystemEntities.TIPO_SERVICIO.ToList();
            foreach(var result in listTipoSercioDALC)
            {
                TipoServicio objTipoServicio= new TipoServicio();
                objTipoServicio.Id_tipoServicio = int.Parse(result.ID_TIPO.ToString());
                objTipoServicio.Tipo_servicio = result.TIPO;
                listadoTipoServicio.Add(objTipoServicio);
            }
            return listadoTipoServicio;
        }

        public bool agregarBaseDatosServicio(string codServicio,List<string>codBaseDatos)
        {
            List<BASE_DATOS> listadoBaseDatos = new List<BASE_DATOS>();
            SERVICIOS objServicio = CommonBC.HomeroSystemEntities.SERVICIOS.First
                (
                   servi=>servi.COD_SERVICIO == codServicio
                );
            foreach(string cod in codBaseDatos)
            {
                BASE_DATOS objBaseDatos = CommonBC.HomeroSystemEntities.BASE_DATOS.First
                    (
                      bd=>bd.COD_BASE_DATOS == cod
                    );
                listadoBaseDatos.Add(objBaseDatos);
            }

            objServicio.BASE_DATOS = listadoBaseDatos;


            CommonBC.HomeroSystemEntities.SaveChanges();
            return true;
        }

        public Servicio BuscarServicio(string codigoServicio)
        {
            List<BaseDeDatos> listadoBaseDatos = new List<BaseDeDatos>();
            SERVICIOS objServicioDALC = CommonBC.HomeroSystemEntities.SERVICIOS.First(servi=>servi.COD_SERVICIO == codigoServicio);
            Servicio objServicio = new Servicio();
            objServicio.Codigo = objServicioDALC.COD_SERVICIO;
            objServicio.Nombre = objServicioDALC.MODULO.NOMBRE;
            objServicio.Garantia =int.Parse(objServicioDALC.MODULO.GARANTIA.ToString());
            objServicio.Id_documento = int.Parse(objServicioDALC.MODULO.ID_DOCUMENTO.ToString());
            objServicio.Descripcion = objServicioDALC.DESCRIPCION;
            objServicio.Codigo_servidor = objServicioDALC.COD_SERVIDOR;
            objServicio.Id_lenguaje = int.Parse(objServicioDALC.ID_LENGUAJE.ToString());
            objServicio.Id_tipo = int.Parse(objServicioDALC.ID_TIPO.ToString());
            if (objServicioDALC.BASE_DATOS.Count >= 0)
            {
                foreach (BASE_DATOS baseDatos in objServicioDALC.BASE_DATOS)
                {
                    BaseDeDatos objBaseDatos = new BaseDeDatos();
                    objBaseDatos.Codigo = baseDatos.COD_BASE_DATOS;
                    objBaseDatos.Nombre = baseDatos.MODULO.NOMBRE;
                    objBaseDatos.Garantia = int.Parse(baseDatos.MODULO.GARANTIA.ToString());
                    objBaseDatos.Id_documento = int.Parse(baseDatos.MODULO.ID_DOCUMENTO.ToString());
                    objBaseDatos.Id_motor = int.Parse(baseDatos.ID_MOTOR.ToString());
                    objBaseDatos.Codigo_servidor = baseDatos.COD_SERVIDOR;
                    objBaseDatos.NomUSer = baseDatos.NOM_USUARIO;
                    listadoBaseDatos.Add(objBaseDatos);
                }

                objServicio.ListadoBaseDatos = listadoBaseDatos;
            }
            return objServicio;
        }

        public List<Servicio> listadoServiciosByEquipoTrabajo(int id_equipoTrabajo)
        {
            List<Servicio> listadoServiciosAsociados = new List<Servicio>();
            List<MODULO> listadoModuloDALC = new List<MODULO>();
            try
            {
                List<USUARIO> listadoUsuariosDALC = CommonBC.HomeroSystemEntities.USUARIO.Where
                     (
                        us=> us.FUNCIONARIO.ID_EQUIPO_TRABAJO == id_equipoTrabajo && us.ID_ROL == 1
                    ).ToList();

                foreach(USUARIO u in listadoUsuariosDALC)
                {
                    List<MODULO> listadoModuloDALCTemp= u.FUNCIONARIO.MODULO.Where(mo => mo.COD_MODULO.Contains("SERVI")).ToList();
                    foreach(MODULO mo in listadoModuloDALCTemp)
                    {
                        listadoModuloDALC.Add(mo);
                    }
                }

                foreach(MODULO ob in listadoModuloDALC)
                {
                    SERVICIOS obj = CommonBC.HomeroSystemEntities.SERVICIOS.First(ser => ser.COD_SERVICIO == ob.COD_MODULO);
                    Servicio objServicio = new Servicio();
                    objServicio.Codigo = obj.COD_SERVICIO;
                    objServicio.Codigo_servidor = obj.COD_SERVIDOR;
                    objServicio.Descripcion = obj.DESCRIPCION;
                    objServicio.Garantia = int.Parse(obj.MODULO.GARANTIA.ToString());
                    objServicio.Id_documento = int.Parse(obj.MODULO.ID_DOCUMENTO.ToString());
                    objServicio.Id_lenguaje = int.Parse(obj.ID_LENGUAJE.ToString());
                    objServicio.Id_tipo = int.Parse(obj.ID_TIPO.ToString());
                    objServicio.Nombre = obj.MODULO.NOMBRE;

                    listadoServiciosAsociados.Add(objServicio);
                }

                return listadoServiciosAsociados;

            }catch
            {
                return null;
            }
        }
    }
}
