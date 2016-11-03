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
                CommonBC.HomeroSystemEntities.FUNCIONARIO.Add(funcionarioDALC);

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
    }
}
