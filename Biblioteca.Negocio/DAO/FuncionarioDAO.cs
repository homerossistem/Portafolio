using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Negocio.Clases;
using Biblioteca.DALC;

namespace Biblioteca.Negocio.DAO
{
    public class FuncionarioDAO
    {
        public bool AgregarFuncionario(Funcionario _funcionario)
        {
            try
            {
                FUNCIONARIO funcionarioDALC = new FUNCIONARIO();
                funcionarioDALC.RUT_FUNCIONARIO = _funcionario.Rut_funcionario;
                funcionarioDALC.NOMBRE = _funcionario.Nombre.ToUpper();
                funcionarioDALC.APELLIDO = _funcionario.Apellido.ToUpper();
                funcionarioDALC.DIRECION = _funcionario.Direccion.ToUpper();
                funcionarioDALC.EMAIL = _funcionario.Email;
                funcionarioDALC.CELULAR = _funcionario.Celular;
                funcionarioDALC.ID_EQUIPO_TRABAJO = int.Parse(_funcionario.Id_equipo_trabajo.ToString());
                funcionarioDALC.ID_USUARIO = _funcionario.ObjUsuario.Id_usuario;
                CommonBC.HomeroSystemEntities.FUNCIONARIO.Add(funcionarioDALC);
                CommonBC.HomeroSystemEntities.SaveChanges();

            }
            catch
            {
                return false;
            }

            return true;


        }

        public bool ExisteFuncionarioPorRut(string rut)
        {
            int contador = CommonBC.HomeroSystemEntities.FUNCIONARIO.Count( fun => fun.RUT_FUNCIONARIO == rut);
            if(contador!=0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public List<Funcionario> listadoFuncionariosResponsables()
        {
            List<Funcionario> listadoFuncionario = new List<Funcionario>();
            List<FUNCIONARIO> listadoFuncionarioDALC = CommonBC.HomeroSystemEntities.FUNCIONARIO.Where(fun => fun.USUARIO.ID_ROL == 2).ToList();

            foreach(FUNCIONARIO objfuncionarioDALC in listadoFuncionarioDALC)
            {
                Funcionario objFuncionario = new Funcionario();
                objFuncionario.Rut_funcionario = objfuncionarioDALC.RUT_FUNCIONARIO;
                objFuncionario.Nombre = objfuncionarioDALC.NOMBRE;
                objFuncionario.Apellido = objfuncionarioDALC.APELLIDO;
                objFuncionario.Celular = int.Parse(objfuncionarioDALC.CELULAR.ToString());
                objFuncionario.Email = objfuncionarioDALC.EMAIL;
                objFuncionario.Id_equipo_trabajo = int.Parse(objfuncionarioDALC.ID_EQUIPO_TRABAJO.ToString());
                objFuncionario.Direccion = objfuncionarioDALC.DIRECION;

                listadoFuncionario.Add(objFuncionario);
            }

            return listadoFuncionario;
        }
    }
}
