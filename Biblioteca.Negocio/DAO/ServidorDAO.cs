using Biblioteca.DALC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Negocio.Clases;
using System.Security.Cryptography;
using Biblioteca.Negocio.DTOs;

namespace Biblioteca.Negocio.DAO
{
    public class ServidorDAO
    {
        string key = "homerosystem";
        public bool AgregarServidor(Servidor _servidor, HashPassModulo _hashServidor)
        {
            try
            {
                string codigoGenerado = GeneradorCodigServidor();
                MODULO objModuloDALC = new MODULO();
                objModuloDALC.COD_MODULO = codigoGenerado;
                objModuloDALC.NOMBRE = _servidor.Nombre;
                objModuloDALC.ID_PROVEEDOR = _servidor.Id_proveedor;
                objModuloDALC.GARANTIA = _servidor.Garantia;
                objModuloDALC.ID_DOCUMENTO = _servidor.Id_documento;
                objModuloDALC.RUT_FUNC_ADMIN = _servidor.Rut_administrador;
                HASH_PASS_SERVIDOR objHashPassDALC = new HASH_PASS_SERVIDOR();
                objHashPassDALC.COD_MODULO = codigoGenerado;
                objHashPassDALC.HAS_PASS = EncriptarPasswordBaseDeDatos(_hashServidor.Hash_pass);
                SERVIDOR objServidorDALC = new SERVIDOR();
                objServidorDALC.COD_SERVIDOR = objModuloDALC.COD_MODULO;
                objServidorDALC.IP = _servidor.Ip;
                objServidorDALC.DISCO_DURO = _servidor.DiscoDuro;
                objServidorDALC.RAM = _servidor.Ram;
                objServidorDALC.ID_SO = _servidor.Id_sistemaOperativo;
                objServidorDALC.ID_RACK = _servidor.Id_rack;
                objServidorDALC.ID_TIPO_NIVEL = _servidor.Id_tipo_nivel;
                objServidorDALC.ID_TIPO = _servidor.Id_tipo;
                objServidorDALC.MODULO = objModuloDALC;
                objServidorDALC.HASH_PASS_SERVIDOR = objHashPassDALC;
               
                CommonBC.HomeroSystemEntities.SERVIDOR.Add(objServidorDALC);
                CommonBC.HomeroSystemEntities.SaveChanges();

            }
            catch
            {
                throw new ArgumentException("Error al intentar agregar un Servidor");
            }

            return true;


        }

        private string GeneradorCodigServidor()
        {
            string Codigo = string.Empty;
            string ultimoCodigo = CommonBC.HomeroSystemEntities.SERVIDOR.Max(ser => ser.COD_SERVIDOR);
            if (ultimoCodigo == null)
            {
                Codigo = string.Format("SERVER-{0:000000}", 1);
            }
            else
            {
                Codigo = ultimoCodigo.Substring(7);
                int digitoCodigo = int.Parse(Codigo) + 1;
                Codigo = string.Format("SERVER-{0:000000}", digitoCodigo);
            }
            return Codigo;
        }

        public string EncriptarPasswordBaseDeDatos(string passwords)
        {

            byte[] keyArray;
            byte[] Arreglo_a_Cifrar =
            UTF8Encoding.UTF8.GetBytes(passwords);

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
            tdes.CreateEncryptor();


            byte[] ArrayResultado =
            cTransform.TransformFinalBlock(Arreglo_a_Cifrar, 0
            , Arreglo_a_Cifrar.Length);

            tdes.Clear();


            return Convert.ToBase64String(ArrayResultado,
            0, ArrayResultado.Length);
        }

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
        public List<DTO> listadoServidores()
        {
            List<DTO> listadoServidores = new List<DTO>();
            var listadoServidorDALC = CommonBC.HomeroSystemEntities.SERVIDOR.Join(CommonBC.HomeroSystemEntities.SISTEMA_OPERATIVO, ser => ser.ID_SO, so => so.ID_SO, (ser, so) => new
                   {ser,so}).Join(CommonBC.HomeroSystemEntities.RACK, ser => ser.ser.ID_RACK, ra => ra.ID_RACK, (ser, ra) => new
                   {ser,ra}).Join(CommonBC.HomeroSystemEntities.TIPO_NIVEL, ser => ser.ser.ser.ID_TIPO_NIVEL, tn => tn.ID_TIPO_NIVEL, (ser, tn) => new
                 {ser,tn}).Join(CommonBC.HomeroSystemEntities.TIPO, ser => ser.ser.ser.ser.ID_TIPO, tp => tp.ID_TIPO, (ser, tp) => new
                { ser,tp}).Join(CommonBC.HomeroSystemEntities.SALA_SERVIDORES, ser => ser.ser.ser.ra.ID_SALA_SERVIDOR, sala => sala.ID_SALA_SERVIDOR, (ser, sala) => new
                   { ser,sala}).Join(CommonBC.HomeroSystemEntities.DOCUMENTO, ser => ser.ser.ser.ser.ser.ser.MODULO.ID_DOCUMENTO, doc => doc.ID_DOCUMENTO, (ser, doc) => new
                  { CODSERVIDOR = ser.ser.ser.ser.ser.ser.COD_SERVIDOR,NOMBRESERVIDOR = ser.ser.ser.ser.ser.ser.MODULO.NOMBRE,GARANTIA = ser.ser.ser.ser.ser.ser.MODULO.GARANTIA,
                    IP = ser.ser.ser.ser.ser.ser.IP,RAM = ser.ser.ser.ser.ser.ser.RAM,DISCODURO = ser.ser.ser.ser.ser.ser.DISCO_DURO,IdSO = ser.ser.ser.ser.ser.so.ID_SO,
                    SO = ser.ser.ser.ser.ser.so.NOMBRE_SO,idTipo = ser.ser.tp.ID_TIPO,tipo = ser.ser.tp.TIPO1,idTipoNivel = ser.ser.ser.tn.ID_TIPO_NIVEL,
                    TipoNivel = ser.ser.ser.tn.TIPO_NIVEL1,idDocumento = doc.ID_DOCUMENTO,URL =doc.URL_DOCUMENTO,
                    ID_RACK=ser.ser.ser.ser.ra.ID_RACK,SALA_SERVIDOR = ser.sala.NOMBRE_SALA,PISO_SALA=ser.sala.PISO,RUT_RESPONSABLE = ser.ser.ser.ser.ser.ser.MODULO.RUT_FUNC_ADMIN,CONTRASENA = ser.ser.ser.ser.ser.ser.HASH_PASS_SERVIDOR.HAS_PASS});

            foreach(var result in listadoServidorDALC)
            {
                DTO objDTO = new DTO();
                objDTO.Servidor.Codigo = result.CODSERVIDOR; objDTO.Servidor.Nombre = result.NOMBRESERVIDOR; objDTO.Servidor.Garantia = int.Parse(result.GARANTIA.ToString()); objDTO.Servidor.Ip = result.IP;
                objDTO.Servidor.Ram =int.Parse(result.RAM.ToString());objDTO.Servidor.DiscoDuro = int.Parse(result.DISCODURO.ToString()); objDTO.Servidor.Id_sistemaOperativo = int.Parse(result.IdSO.ToString());
                objDTO.Servidor.Id_tipo = int.Parse(result.idTipo.ToString()); objDTO.Servidor.Id_tipo_nivel = int.Parse(result.idTipoNivel.ToString()); objDTO.SistemaOperativo.Id_sistemaOperativo = int.Parse(result.IdSO.ToString());
                objDTO.SistemaOperativo.Nombre_sistema = result.SO; objDTO.Tipo.Id_tipo = int.Parse(result.idTipo.ToString()); objDTO.Tipo._Tipo = result.tipo; objDTO.TipoNivel.Id_tipoNivel = int.Parse(result.idTipoNivel.ToString());
                objDTO.TipoNivel.Tipo_nivel = result.TipoNivel;objDTO.Documento.Id_documento = int.Parse(result.idDocumento.ToString());objDTO.Documento.Url_documento = result.URL;objDTO.Rack.Id_rack = int.Parse(result.ID_RACK.ToString());
                objDTO.SalaServidores.Nombre_sala = result.SALA_SERVIDOR;objDTO.SalaServidores.Piso = int.Parse(result.PISO_SALA.ToString());objDTO.Servidor.Rut_administrador = result.RUT_RESPONSABLE;
                objDTO.HashPassModulo.Hash_pass = DesencriptarPasswordBaseDeDatos(result.CONTRASENA);
                listadoServidores.Add(objDTO);
            }


            return listadoServidores;
        }

        public List<Servidor>listadoServidorAplicaciones()
        {
            List<Servidor> listServidorApp = new List<Servidor>();
            List<SERVIDOR> listadoServidorAplicacionesDALC = CommonBC.HomeroSystemEntities.SERVIDOR.Where
                (
                  ser => ser.ID_TIPO == 1
                ).ToList();

            foreach(SERVIDOR servidor in listadoServidorAplicacionesDALC)
            {
                Servidor objservidor = new Servidor();
                objservidor.Codigo = servidor.COD_SERVIDOR;
                objservidor.Nombre = servidor.MODULO.NOMBRE;
                listServidorApp.Add(objservidor);
            }

            return listServidorApp;
        }

        public Servidor BuscarServidorPorCod(string codigo_servidor)
        {
            SERVIDOR objServidorDALC = CommonBC.HomeroSystemEntities.SERVIDOR.First(serv => serv.COD_SERVIDOR == codigo_servidor);
            Servidor objServidor = new Servidor();
            objServidor.Codigo = objServidorDALC.COD_SERVIDOR;
            objServidor.Ip = objServidorDALC.IP;
            objServidor.DiscoDuro = int.Parse(objServidorDALC.DISCO_DURO.ToString());
            objServidor.Ram = int.Parse(objServidorDALC.RAM.ToString());
            objServidor.Id_sistemaOperativo = int.Parse(objServidorDALC.ID_SO.ToString());
            objServidor.Id_rack = int.Parse(objServidorDALC.ID_RACK.ToString());
            objServidor.Id_tipo_nivel = int.Parse(objServidorDALC.ID_TIPO_NIVEL.ToString());
            objServidor.Id_tipo = int.Parse(objServidorDALC.ID_TIPO.ToString());
            objServidor.Nombre = objServidorDALC.MODULO.NOMBRE;
            objServidor.Id_documento = int.Parse(objServidorDALC.MODULO.ID_DOCUMENTO.ToString());
            objServidor.Garantia = int.Parse(objServidorDALC.MODULO.ID_PROVEEDOR.ToString());
            objServidor.ObjHashPass.Cod_modulo = objServidorDALC.HASH_PASS_SERVIDOR.COD_MODULO;
            objServidor.ObjHashPass.Hash_pass = objServidorDALC.HASH_PASS_SERVIDOR.HAS_PASS;

            return objServidor;
        }

        public List<TipoNivel> ListadoTipoNivelServidor()
        {
            List<TipoNivel> listadoTipoNivel = new List<TipoNivel>();

            List<TIPO_NIVEL> listadoTipoNivelDALC = CommonBC.HomeroSystemEntities.TIPO_NIVEL.ToList();

            foreach(TIPO_NIVEL tipoNivel in listadoTipoNivelDALC)
            {
                TipoNivel objTipoNivel = new TipoNivel();
                objTipoNivel.Id_tipoNivel = int.Parse(tipoNivel.ID_TIPO_NIVEL.ToString());
                objTipoNivel.Tipo_nivel = tipoNivel.TIPO_NIVEL1;

                listadoTipoNivel.Add(objTipoNivel);
            }

            return listadoTipoNivel;
        }

        public List<Tipo> ListadoTipoServidor()
        {
            List<Tipo> listadoTipo = new List<Tipo>();

            List<TIPO> listadoTipoDALC = CommonBC.HomeroSystemEntities.TIPO.ToList();

            foreach(TIPO tipo in listadoTipoDALC)
            {
                Tipo objtipo = new Tipo();
                objtipo.Id_tipo = int.Parse(tipo.ID_TIPO.ToString());
                objtipo._Tipo = tipo.TIPO1;
                listadoTipo.Add(objtipo);
            }

            return listadoTipo;
        }

        public List<SistemaOperativo> ListadoSistemaOperativo()
        {
            List<SistemaOperativo> listadoSistemaOperativo = new List<SistemaOperativo>();
            List<SISTEMA_OPERATIVO> listadoSistemaOperativoDALC = CommonBC.HomeroSystemEntities.SISTEMA_OPERATIVO.ToList();
            
            foreach(SISTEMA_OPERATIVO so in listadoSistemaOperativoDALC)
            {
                SistemaOperativo objsistemaoperativo = new SistemaOperativo();
                objsistemaoperativo.Id_sistemaOperativo = int.Parse(so.ID_SO.ToString());
                objsistemaoperativo.Nombre_sistema = so.NOMBRE_SO;

                listadoSistemaOperativo.Add(objsistemaoperativo);
            }

            return listadoSistemaOperativo;
        }
    }
}
