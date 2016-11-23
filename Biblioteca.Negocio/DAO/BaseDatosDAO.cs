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
                objModuloDALC.ID_PROVEEDOR = _objBaseDatos.Id_proveedor;
                objModuloDALC.ID_DOCUMENTO = _objBaseDatos.Id_documento;
                objModuloDALC.RUT_FUNC_ADMIN = _objBaseDatos.Rut_administrador;
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

        public BaseDeDatos BuscarBaseDeDatosPorCodigo(string codigo)
        {
            BASE_DATOS bd = CommonBC.HomeroSystemEntities.BASE_DATOS.First
                (
                basedatos => basedatos.COD_BASE_DATOS == codigo
                );
            BaseDeDatos objBaseDeDatos = new BaseDeDatos();
            objBaseDeDatos.Codigo = bd.COD_BASE_DATOS;
            objBaseDeDatos.Codigo_servidor = bd.COD_SERVIDOR;
            objBaseDeDatos.Garantia = int.Parse(bd.MODULO.GARANTIA.ToString());
            objBaseDeDatos.Id_documento = int.Parse(bd.MODULO.ID_DOCUMENTO.ToString());
            objBaseDeDatos.Id_motor = int.Parse(bd.ID_MOTOR.ToString());
            objBaseDeDatos.Id_proveedor = int.Parse(bd.MODULO.ID_PROVEEDOR.ToString());
            objBaseDeDatos.Nombre = bd.MODULO.NOMBRE;
            objBaseDeDatos.NomUSer = bd.NOM_USUARIO;
            objBaseDeDatos.Rut_administrador = bd.MODULO.RUT_FUNC_ADMIN;
            objBaseDeDatos.ObjHashPassBaseDatos.Cod_modulo = bd.COD_BASE_DATOS;
            objBaseDeDatos.ObjHashPassBaseDatos.Hash_pass = DesencriptarPasswordBaseDeDatos(bd.HASH_PASS_BASE_DATOS.HASH_PASS);

            return objBaseDeDatos;
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
                int digitoCodigo = int.Parse(Codigo) + 1;
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

        public List<BaseDeDatos> ListadoBaseDeDatos()
        {
            List<BaseDeDatos> listadoBasesDeDatos = new List<BaseDeDatos>();
            List<BASE_DATOS> listadoBASEDeDatosDALC = CommonBC.HomeroSystemEntities.BASE_DATOS.ToList();

            foreach (BASE_DATOS bd in listadoBASEDeDatosDALC)
            {
                BaseDeDatos objBaseDeDatos = new BaseDeDatos();
                objBaseDeDatos.Codigo = bd.COD_BASE_DATOS;
                objBaseDeDatos.Codigo_servidor = bd.COD_SERVIDOR;
                objBaseDeDatos.Garantia = int.Parse(bd.MODULO.GARANTIA.ToString());
                objBaseDeDatos.Id_documento = int.Parse(bd.MODULO.ID_DOCUMENTO.ToString());
                objBaseDeDatos.Id_motor = int.Parse(bd.ID_MOTOR.ToString());
                objBaseDeDatos.Id_proveedor = int.Parse(bd.MODULO.ID_PROVEEDOR.ToString());
                objBaseDeDatos.Nombre = bd.MODULO.NOMBRE;
                objBaseDeDatos.NomUSer = bd.NOM_USUARIO;
                objBaseDeDatos.Rut_administrador = bd.MODULO.RUT_FUNC_ADMIN;

                listadoBasesDeDatos.Add(objBaseDeDatos);
            }

            return listadoBasesDeDatos;
        }


        public List<DTO> ListBaseDeDatos()
        {
            List<DTO> listadoBasesDeDatos = new List<DTO>();
            List<BASE_DATOS> listadoBASEDeDatosDALC = CommonBC.HomeroSystemEntities.BASE_DATOS.ToList();

            foreach (BASE_DATOS bd in listadoBASEDeDatosDALC)
            {
                DTO objBaseDeDatosDTO = new DTO();
                objBaseDeDatosDTO.BaseDeDatos.Codigo = bd.COD_BASE_DATOS;
                objBaseDeDatosDTO.BaseDeDatos.Codigo_servidor = bd.COD_SERVIDOR;
                objBaseDeDatosDTO.BaseDeDatos.Garantia = int.Parse(bd.MODULO.GARANTIA.ToString());
                objBaseDeDatosDTO.BaseDeDatos.Id_documento = int.Parse(bd.MODULO.ID_DOCUMENTO.ToString());
                objBaseDeDatosDTO.BaseDeDatos.Id_motor = int.Parse(bd.ID_MOTOR.ToString());
                objBaseDeDatosDTO.BaseDeDatos.Id_proveedor = int.Parse(bd.MODULO.ID_PROVEEDOR.ToString());
                objBaseDeDatosDTO.BaseDeDatos.Nombre = bd.MODULO.NOMBRE;
                objBaseDeDatosDTO.BaseDeDatos.NomUSer = bd.NOM_USUARIO;
                objBaseDeDatosDTO.BaseDeDatos.Rut_administrador = bd.MODULO.RUT_FUNC_ADMIN;
                objBaseDeDatosDTO.Documento.Url_documento = bd.MODULO.DOCUMENTO.URL_DOCUMENTO;
                objBaseDeDatosDTO.HashPassModulo.Hash_pass = DesencriptarPasswordBaseDeDatos(bd.HASH_PASS_BASE_DATOS.HASH_PASS);
                objBaseDeDatosDTO.Funcionario.Nombre = bd.MODULO.FUNCIONARIO.NOMBRE;
                objBaseDeDatosDTO.Funcionario.Apellido = bd.MODULO.FUNCIONARIO.APELLIDO;
                objBaseDeDatosDTO.MotorBD.Motor = bd.MOTOR_BASE_DATOS.NOMBRE_MOTOR;
                objBaseDeDatosDTO.Servidor.Nombre = bd.SERVIDOR.MODULO.NOMBRE;
                objBaseDeDatosDTO.Proveedor.Nombre_empresa = bd.MODULO.PROVEEDOR1.NOMBRE_EMPRESA;


                listadoBasesDeDatos.Add(objBaseDeDatosDTO);
            }

            return listadoBasesDeDatos;
        }
        public bool EliminarBaseDeDatosPorCodigo(string codigobd)
        {
            int resultado = 0;
            BASE_DATOS objBaseDatosDALC = CommonBC.HomeroSystemEntities.BASE_DATOS.First
                    (bd => bd.COD_BASE_DATOS == codigobd); ;
            MODULO objModuloDALC = null;
            HASH_PASS_BASE_DATOS objHashPassDALC = null;
            objModuloDALC = CommonBC.HomeroSystemEntities.MODULO.First
                (
                bd => bd.COD_MODULO == codigobd
                );
            objHashPassDALC = CommonBC.HomeroSystemEntities.HASH_PASS_BASE_DATOS.First(hash => hash.COD_MODULO == codigobd);
            CommonBC.HomeroSystemEntities.HASH_PASS_BASE_DATOS.Remove(objHashPassDALC);
            CommonBC.HomeroSystemEntities.BASE_DATOS.Remove(objBaseDatosDALC);
            CommonBC.HomeroSystemEntities.MODULO.Remove(objModuloDALC);
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
                    CommonBC.HomeroSystemEntities.Entry(objModuloDALC).Reload();
                    CommonBC.HomeroSystemEntities.Entry(objBaseDatosDALC).Reload();
                    CommonBC.HomeroSystemEntities.Entry(objHashPassDALC).Reload();
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
        //metodos para agrear,modificar,listar y eliminar para el mantenedor de MotorBD

        public bool AgregarMotorBaseDeDatos(MotorBD objMotorBD)
        {
            try {
                MOTOR_BASE_DATOS objMotorBDDALC = new MOTOR_BASE_DATOS();
                objMotorBDDALC.NOMBRE_MOTOR = objMotorBD.Motor;
                CommonBC.HomeroSystemEntities.MOTOR_BASE_DATOS.Add(objMotorBDDALC);
                CommonBC.HomeroSystemEntities.SaveChanges();
                return true;
            }catch
            {
                return false;
            }

        }

        public bool EliminarMotorBaseDatos(int id)
        {
            try
            {
                MOTOR_BASE_DATOS objMotorBDDALC = CommonBC.HomeroSystemEntities.MOTOR_BASE_DATOS.First
                    (
                      mbd=>mbd.ID_MOTOR == id
                    );

                if(objMotorBDDALC.BASE_DATOS.Count == 0)
                {
                    CommonBC.HomeroSystemEntities.MOTOR_BASE_DATOS.Remove(objMotorBDDALC);
                    CommonBC.HomeroSystemEntities.SaveChanges();
                    return true;
                }else
                {
                    return false;
                }

            }
            catch
            {
                return false;
            }
        }

        public bool ModificarMotorBD(MotorBD objMotorBD)
        {
            try
            {
                MOTOR_BASE_DATOS objMotorBDDALC = CommonBC.HomeroSystemEntities.MOTOR_BASE_DATOS.First
                   (
                     mbd => mbd.ID_MOTOR == objMotorBD.Id_motor
                   );
                objMotorBDDALC.NOMBRE_MOTOR = objMotorBD.Motor;
                CommonBC.HomeroSystemEntities.SaveChanges();
                return true;

            }
            catch
            {
                return false;
            }
        }
        
        
        public List<MotorBD> ListadoMotorBaseDeDatos()
        {
            List<MotorBD> listadoMotorBD = new List<MotorBD>();
            List<MOTOR_BASE_DATOS> listadoMotorBDDACL = CommonBC.HomeroSystemEntities.MOTOR_BASE_DATOS.ToList();
            foreach(MOTOR_BASE_DATOS mbd in listadoMotorBDDACL)
            {
                MotorBD objMotorBD = new MotorBD();
                objMotorBD.Id_motor = int.Parse(mbd.ID_MOTOR.ToString());
                objMotorBD.Motor = mbd.NOMBRE_MOTOR;

                listadoMotorBD.Add(objMotorBD);
            }

            return listadoMotorBD;
        }

        public MotorBD buscarMotorBDPorId(int id)
        {
            try
            {
                MotorBD objMotor = new MotorBD();
                MOTOR_BASE_DATOS objMotorDALC = CommonBC.HomeroSystemEntities.MOTOR_BASE_DATOS.First
                    (
                    mbd=>mbd.ID_MOTOR == id
                    );
                objMotor.Id_motor = int.Parse(objMotorDALC.ID_MOTOR.ToString());
                objMotor.Motor = objMotorDALC.NOMBRE_MOTOR;
                return objMotor;

            }catch
            {
                return null;
            }
        }

       
    }
}
