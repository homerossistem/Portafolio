using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Negocio.Clases;
using Biblioteca.DALC;
using System.Security.Cryptography;

namespace Biblioteca.Negocio.DAO
{
    public class HashPassDAO
    {
        public bool AgregarHashPass(HashPass _hashPass)
        {
            try
            {
                HASH_PASS objHashPassDALC = new HASH_PASS();
                objHashPassDALC.ID_USUARIO = _hashPass.Id_usuario;
                objHashPassDALC.HASH_PASS1 = GeneradorPasswordsMD5(_hashPass.Hash_pass);
                CommonBC.HomeroSystemEntities.HASH_PASS.Add(objHashPassDALC);
                CommonBC.HomeroSystemEntities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }


        }

        public HashPass BuscarHashPassPorId(int id_usuario)
        {

            try
            {
                HashPass objHashPass = new HashPass();
                HASH_PASS objHashPassDALC = CommonBC.HomeroSystemEntities.HASH_PASS.First
                    (
                        hash => hash.ID_USUARIO == id_usuario
                    );
                objHashPass.Id_usuario = int.Parse(objHashPassDALC.ID_USUARIO.ToString());
                objHashPass.Hash_pass = objHashPassDALC.HASH_PASS1;

                return objHashPass;

            }
            catch
            {
                return null;
            }
        }

        public string GeneradorPasswordsMD5(string contrasena)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = md5.ComputeHash(encoding.GetBytes(contrasena));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }
    }
