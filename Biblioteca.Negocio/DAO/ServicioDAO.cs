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
        public bool agregarServicio(Servicio _servicio)
        {
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
                CommonBC.HomeroSystemEntities.MODULO.Add(objModuloDALC);
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
    }
}
