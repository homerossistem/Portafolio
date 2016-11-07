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
    public class BaseDatosDAO
    {
        string key = "homerosystem";
        public bool AgregarBaseDeDatos(BaseDeDatos _objBaseDatos, HashPassModulo _hashBaseDatos)
        {
            try
            {
                string codigoGenerado = GeneradorCodigoBaseDatos();
                MODULO objModuloDALC = new MODULO();
                objModuloDALC.COD_MODULO = codigoGenerado;
                objModuloDALC.NOMBRE = _objBaseDatos.Nombre;
                objModuloDALC.GARANTIA = _objBaseDatos.Garantia;
                objModuloDALC.ID_DOCUMENTO = _objBaseDatos.Id_documento;
                HASH_PASS_BASE_DATOS objHashPassDALC = new HASH_PASS_BASE_DATOS();
                objHashPassDALC.COD_MODULO = codigoGenerado;
                objHashPassDALC.HASH_PASS = EncriptarPasswordBaseDeDatos(_hashBaseDatos.Hash_pass);
                BASE_DATOS objBaseDatosDALC = new BASE_DATOS();
                objBaseDatosDALC.COD_BASE_DATOS = codigoGenerado;
                objBaseDatosDALC.ID_MOTOR = _objBaseDatos.Id_motor;
                objBaseDatosDALC.COD_SERVIDOR = _objBaseDatos.Codigo_servidor;
                objBaseDatosDALC.NOM_USUARIO = _objBaseDatos.NomUSer;
                objBaseDatosDALC.MODULO = objModuloDALC;
                objBaseDatosDALC.HASH_PASS_BASE_DATOS = objHashPassDALC;
                CommonBC.HomeroSystemEntities.BASE_DATOS.Add(objBaseDatosDALC);
                CommonBC.HomeroSystemEntities.SaveChanges();

            }
            catch
            {
                throw new ArgumentException("Error al intentar agregar una base de datos");
            }

            return true;


        }

        private string GeneradorCodigoBaseDatos()
        {
            string Codigo = string.Empty;
            string ultimoCodigo = CommonBC.HomeroSystemEntities.BASE_DATOS.Max(bd => bd.COD_BASE_DATOS);
            if (ultimoCodigo == null)
            {
                Codigo = string.Format("BD-{0:000000}", 1);
            }
            else
            {
                Codigo = ultimoCodigo.Substring(3);
                int digitoCodigo = int.Parse(Codigo)+1;
                Codigo = string.Format("BD-{0:000000}", digitoCodigo);
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
            cTransform.TransformFinalBlock(Arreglo_a_Cifrar,0
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
    }
}
