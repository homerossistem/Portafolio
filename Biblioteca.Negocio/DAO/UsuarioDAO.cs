using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.DALC;
using Biblioteca.Negocio.Clases;
using Biblioteca.Negocio.DTOs;
using System.Data.Entity.Core;
using System.Security.Cryptography;
using System.Data.Entity.Infrastructure;

namespace Biblioteca.Negocio.DAO
{
   public class UsuarioDAO
   {
        public bool AgregarUsuario(Usuario _usuario)
        {
            try
            {
                USUARIO usuarioDALC = new USUARIO();
                usuarioDALC.NOMBRE_USUARIO = _usuario.Nombre_usuario.ToUpper();
                usuarioDALC.ESTADO = 1;
                usuarioDALC.FECHA_CREACION = _usuario.Fecha_creacion;
                usuarioDALC.ID_ROL = _usuario.Id_rol;
                CommonBC.HomeroSystemEntities.USUARIO.Add(usuarioDALC);
                CommonBC.HomeroSystemEntities.SaveChanges();
            }
            catch
            {
                return false;
            }


                return true;
        }
        public Usuario ObtenerUsuarioPorNombreUsuario(string nombreUsuario)
        {
            try
            {
                Usuario objusuario = new Usuario();
                USUARIO objUsuarioDALC = CommonBC.HomeroSystemEntities.USUARIO.First
                    (
                        us => us.NOMBRE_USUARIO == nombreUsuario.ToUpper()
                    );
                objusuario.Id_usuario = int.Parse(objUsuarioDALC.ID_USUARIO.ToString());
                objusuario.Nombre_usuario = objUsuarioDALC.NOMBRE_USUARIO;
                objusuario.Estado = int.Parse(objUsuarioDALC.ESTADO.ToString());
                objusuario.Fecha_creacion = DateTime.Parse(objUsuarioDALC.FECHA_CREACION.ToString());
                objusuario.Id_rol = int.Parse(objUsuarioDALC.ID_ROL.ToString());

                return objusuario;

            }
            catch
            {
                return null;
            }



        }
        public bool ExisteNombreUsuario(string nombreUsuario)
        {
            int cantidad = CommonBC.HomeroSystemEntities.USUARIO.Count
                   (
                       us => us.NOMBRE_USUARIO == nombreUsuario.ToUpper()
                   );
            if (cantidad != 0)
            {
                return true;
            }

            return false;

        }
        public DTO BuscarUsuarioLogin(string nombre_usuario, string hash_pass)
        {
            try
            {
                DTO objdto = null;
                var query = CommonBC.HomeroSystemEntities.USUARIO.Join
                         (
                         CommonBC.HomeroSystemEntities.FUNCIONARIO, us => us.ID_USUARIO, fun => fun.ID_USUARIO, (us, fun) => new
                         {
                             us,
                             fun
                         }
                         ).Join(
                         CommonBC.HomeroSystemEntities.HASH_PASS, resultado => resultado.us.ID_USUARIO, hash => hash.ID_USUARIO, (resultado, hash) => new
                         {
                             Id_usuario = resultado.us.ID_USUARIO,
                             NOMBRE_USUARIO = resultado.us.NOMBRE_USUARIO,
                             ESTADO = resultado.us.ESTADO,
                             FECHA_CREACION = resultado.us.FECHA_CREACION,
                             ID_ROL = resultado.us.ID_ROL,
                             PASS = hash.HASH_PASS1,
                             Rut = resultado.fun.RUT_FUNCIONARIO,
                             Nombre = resultado.fun.NOMBRE,
                             Apellido = resultado.fun.APELLIDO,
                             Email = resultado.fun.EMAIL,
                             Celular = resultado.fun.CELULAR,
                             Direccion = resultado.fun.DIRECION,
                             ID_EQUIPO = resultado.fun.ID_EQUIPO_TRABAJO
                         }
                         ).Where(us => us.NOMBRE_USUARIO == nombre_usuario.ToUpper() && us.PASS == hash_pass);

               
                foreach (var result in query)
                {
                    objdto = new DTO();
                    objdto.Usuario.Id_usuario = int.Parse(result.Id_usuario.ToString());
                    objdto.Usuario.Nombre_usuario = result.NOMBRE_USUARIO;
                    objdto.Usuario.Estado = int.Parse(result.ESTADO.ToString());
                    objdto.Usuario.Fecha_creacion = DateTime.Parse(result.FECHA_CREACION.ToString());
                    objdto.Usuario.Id_rol = int.Parse(result.ID_ROL.ToString());
                    objdto.HashPass.Id_usuario = int.Parse(result.Id_usuario.ToString());
                    objdto.HashPass.Hash_pass = result.PASS;
                    objdto.Funcionario.Rut_funcionario = result.Rut;
                    objdto.Funcionario.Nombre = result.Nombre;
                    objdto.Funcionario.Apellido = result.Apellido;
                    objdto.Funcionario.Email = result.Email;
                    objdto.Funcionario.Celular = int.Parse(result.Celular.ToString());
                    objdto.Funcionario.Direccion = result.Direccion;
                    objdto.Funcionario.Id_equipo_trabajo = int.Parse(result.ID_EQUIPO.ToString());
                }
                return objdto;
            }catch
            {
                throw new ArgumentException("Error al Consultar en la base de datos");
            }
        }
        public List<DTO> listadoUsuarios()
        {
            try
                {
                    List<DTO> listadoUsuarios = new List<DTO>();
                    var query = CommonBC.HomeroSystemEntities.USUARIO.Join
                        (
                        CommonBC.HomeroSystemEntities.FUNCIONARIO, us => us.ID_USUARIO, fun => fun.ID_USUARIO, (us, fun) => new
                        {
                            us,
                            fun
                        }
                        ).Join(
                        CommonBC.HomeroSystemEntities.HASH_PASS, resultado => resultado.us.ID_USUARIO, hash => hash.ID_USUARIO, (resultado, hash) => new
                        {
                            Id_usuario = resultado.us.ID_USUARIO,
                            NOMBRE_USUARIO = resultado.us.NOMBRE_USUARIO,
                            ESTADO = resultado.us.ESTADO,
                            FECHA_CREACION = resultado.us.FECHA_CREACION,
                            ID_ROL = resultado.us.ID_ROL,
                            ROL = resultado.us.ROL.NOMBRE_ROL,
                            PASS = hash.HASH_PASS1,
                            Rut = resultado.fun.RUT_FUNCIONARIO,
                            Nombre = resultado.fun.NOMBRE,
                            Apellido = resultado.fun.APELLIDO,
                            Email = resultado.fun.EMAIL,
                            Celular = resultado.fun.CELULAR,
                            Direccion = resultado.fun.DIRECION,
                            ID_EQUIPO = resultado.fun.ID_EQUIPO_TRABAJO
                        }
                        ).ToList();

                foreach (var result in query)
                {
                    DTO objdto = new DTO();
                    objdto.Usuario.Id_usuario = int.Parse(result.Id_usuario.ToString());
                    objdto.Usuario.Nombre_usuario = result.NOMBRE_USUARIO;
                    objdto.Usuario.Estado = int.Parse(result.ESTADO.ToString());
                    objdto.Usuario.Fecha_creacion = DateTime.Parse(result.FECHA_CREACION.ToString());
                    objdto.Usuario.Id_rol = int.Parse(result.ID_ROL.ToString());
                    objdto.HashPass.Id_usuario = int.Parse(result.Id_usuario.ToString());
                    objdto.Rol.Nombre_rol = result.ROL;
                    objdto.HashPass.Hash_pass = result.PASS;
                    objdto.Funcionario.Rut_funcionario = result.Rut;
                    objdto.Funcionario.Nombre = result.Nombre;
                    objdto.Funcionario.Apellido = result.Apellido;
                    objdto.Funcionario.Email = result.Email;
                    objdto.Funcionario.Celular = int.Parse(result.Celular.ToString());
                    objdto.Funcionario.Direccion = result.Direccion;
                    objdto.Funcionario.Id_equipo_trabajo = int.Parse(result.ID_EQUIPO.ToString());
                    listadoUsuarios.Add(objdto);
                }

                return listadoUsuarios;

            }
            catch
            {
                throw new ArgumentException("Error al Consultar en la base de datos");
            }
        }
        public List<DTO> listadoUsuariosPorEquipoDeTrabajo(int id_equipo)
        {
            try
            {
                List<DTO> listadoUsuarios = new List<DTO>();
                var query = CommonBC.HomeroSystemEntities.USUARIO.Join
                    (
                    CommonBC.HomeroSystemEntities.FUNCIONARIO, us => us.ID_USUARIO, fun => fun.ID_USUARIO, (us, fun) => new
                    {
                        us,
                        fun
                    }
                    ).Join(
                    CommonBC.HomeroSystemEntities.HASH_PASS, resultado => resultado.us.ID_USUARIO, hash => hash.ID_USUARIO, (resultado, hash) => new
                    {
                        Id_usuario = resultado.us.ID_USUARIO,
                        NOMBRE_USUARIO = resultado.us.NOMBRE_USUARIO,
                        ESTADO = resultado.us.ESTADO,
                        FECHA_CREACION = resultado.us.FECHA_CREACION,
                        ID_ROL = resultado.us.ID_ROL,
                        ROL = resultado.us.ROL.NOMBRE_ROL,
                        PASS = hash.HASH_PASS1,
                        Rut = resultado.fun.RUT_FUNCIONARIO,
                        Nombre = resultado.fun.NOMBRE,
                        Apellido = resultado.fun.APELLIDO,
                        Email = resultado.fun.EMAIL,
                        Celular = resultado.fun.CELULAR,
                        Direccion = resultado.fun.DIRECION,
                        ID_EQUIPO = resultado.fun.ID_EQUIPO_TRABAJO
                    }
                    ).ToList().Where(fun=>fun.ID_EQUIPO == id_equipo);

                foreach (var result in query)
                {
                    DTO objdto = new DTO();
                    objdto.Usuario.Id_usuario = int.Parse(result.Id_usuario.ToString());
                    objdto.Usuario.Nombre_usuario = result.NOMBRE_USUARIO;
                    objdto.Usuario.Estado = int.Parse(result.ESTADO.ToString());
                    objdto.Usuario.Fecha_creacion = DateTime.Parse(result.FECHA_CREACION.ToString());
                    objdto.Usuario.Id_rol = int.Parse(result.ID_ROL.ToString());
                    objdto.HashPass.Id_usuario = int.Parse(result.Id_usuario.ToString());
                    objdto.Rol.Nombre_rol = result.ROL;
                    objdto.HashPass.Hash_pass = result.PASS;
                    objdto.Funcionario.Rut_funcionario = result.Rut;
                    objdto.Funcionario.Nombre = result.Nombre;
                    objdto.Funcionario.Apellido = result.Apellido;
                    objdto.Funcionario.Email = result.Email;
                    objdto.Funcionario.Celular = int.Parse(result.Celular.ToString());
                    objdto.Funcionario.Direccion = result.Direccion;
                    objdto.Funcionario.Id_equipo_trabajo = int.Parse(result.ID_EQUIPO.ToString());
                    listadoUsuarios.Add(objdto);
                }

                return listadoUsuarios;

            }
            catch
            {
                throw new ArgumentException("Error al Consultar en la base de datos");
            }
        }
        public bool EliminarUsuario(int id_usuario)
        {
            USUARIO objusuario = CommonBC.HomeroSystemEntities.USUARIO.First(us => us.ID_USUARIO == id_usuario);
            FUNCIONARIO objFuncionario = null;
            HASH_PASS objHashPass = null;
            int resultado = 0;
            if (objusuario != null)
            {
                objFuncionario = CommonBC.HomeroSystemEntities.FUNCIONARIO.First(fun => fun.ID_USUARIO == id_usuario);
                 objHashPass = CommonBC.HomeroSystemEntities.HASH_PASS.First(hash => hash.ID_USUARIO == id_usuario);
                CommonBC.HomeroSystemEntities.HASH_PASS.Remove(objHashPass);
                CommonBC.HomeroSystemEntities.FUNCIONARIO.Remove(objFuncionario);
                CommonBC.HomeroSystemEntities.USUARIO.Remove(objusuario);
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
                        CommonBC.HomeroSystemEntities.Entry(objHashPass).Reload();
                        exx.Entries.Single().Reload();
                        resultado = 1;
                    }

                } while (saveFailed);
               
            }
            if(resultado== 0)
            {
                return true;
            }else
            {
                return false;
            }
         
        }
        public DTO BuscarUsuarioPorId(int id_usuario)
        {
            try
            {
                DTO objdto = null;
                var query = CommonBC.HomeroSystemEntities.USUARIO.Join
                         (
                         CommonBC.HomeroSystemEntities.FUNCIONARIO, us => us.ID_USUARIO, fun => fun.ID_USUARIO, (us, fun) => new
                         {
                             us,
                             fun
                         }
                         ).Join(
                         CommonBC.HomeroSystemEntities.HASH_PASS, resultado => resultado.us.ID_USUARIO, hash => hash.ID_USUARIO, (resultado, hash) => new
                         {
                             Id_usuario = resultado.us.ID_USUARIO,
                             NOMBRE_USUARIO = resultado.us.NOMBRE_USUARIO,
                             ESTADO = resultado.us.ESTADO,
                             FECHA_CREACION = resultado.us.FECHA_CREACION,
                             ID_ROL = resultado.us.ID_ROL,
                             PASS = hash.HASH_PASS1,
                             Rut = resultado.fun.RUT_FUNCIONARIO,
                             Nombre = resultado.fun.NOMBRE,
                             Apellido = resultado.fun.APELLIDO,
                             Email = resultado.fun.EMAIL,
                             Celular = resultado.fun.CELULAR,
                             Direccion = resultado.fun.DIRECION,
                             ID_EQUIPO = resultado.fun.ID_EQUIPO_TRABAJO
                         }
                         ).Where(us =>us.Id_usuario == id_usuario);


                foreach (var result in query)
                {
                    objdto = new DTO();
                    objdto.Usuario.Id_usuario = int.Parse(result.Id_usuario.ToString());
                    objdto.Usuario.Nombre_usuario = result.NOMBRE_USUARIO;
                    objdto.Usuario.Estado = int.Parse(result.ESTADO.ToString());
                    objdto.Usuario.Fecha_creacion = DateTime.Parse(result.FECHA_CREACION.ToString());
                    objdto.Usuario.Id_rol = int.Parse(result.ID_ROL.ToString());
                    objdto.HashPass.Id_usuario = int.Parse(result.Id_usuario.ToString());
                    objdto.HashPass.Hash_pass = result.PASS;
                    objdto.Funcionario.Rut_funcionario = result.Rut;
                    objdto.Funcionario.Nombre = result.Nombre;
                    objdto.Funcionario.Apellido = result.Apellido;
                    objdto.Funcionario.Email = result.Email;
                    objdto.Funcionario.Celular = int.Parse(result.Celular.ToString());
                    objdto.Funcionario.Direccion = result.Direccion;
                    objdto.Funcionario.Id_equipo_trabajo = int.Parse(result.ID_EQUIPO.ToString());
                }
                return objdto;
            }
            catch
            {
                throw new ArgumentException("Error al Consultar en la base de datos");
            }
        }
        public bool ModificarUsuario(Usuario _objUsuario,Funcionario _objFuncionario,HashPass _objHashPass)
        {
            try {
                USUARIO objUsuario = CommonBC.HomeroSystemEntities.USUARIO.First(us => us.ID_USUARIO == _objUsuario.Id_usuario);
                objUsuario.ESTADO = _objUsuario.Estado;
                objUsuario.ID_ROL = _objUsuario.Id_rol;
                FUNCIONARIO objFuncionario = CommonBC.HomeroSystemEntities.FUNCIONARIO.First(fun => fun.RUT_FUNCIONARIO == _objFuncionario.Rut_funcionario);
                objFuncionario.EMAIL = _objFuncionario.Email;
                objFuncionario.CELULAR = _objFuncionario.Celular;
                objFuncionario.DIRECION = _objFuncionario.Direccion;
                objFuncionario.ID_EQUIPO_TRABAJO = _objFuncionario.Id_equipo_trabajo;
                HASH_PASS objHashPass = CommonBC.HomeroSystemEntities.HASH_PASS.First(hash => hash.ID_USUARIO == _objHashPass.Id_usuario);
                if (!objHashPass.HASH_PASS1.Equals(_objHashPass.Hash_pass))
                    objHashPass.HASH_PASS1 = GeneradorPasswordsMD5(_objHashPass.Hash_pass);

                CommonBC.HomeroSystemEntities.SaveChanges();
                return true;
            }catch
            {
                return false;
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
