using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Negocio.Clases;
using Biblioteca.DALC;
using System.Data.Entity.Infrastructure;

namespace Biblioteca.Negocio.DAO
{
    public class SalaServidoresDAO
    {
        public bool AgregarSalaServidor(SalaServidores _salaServidores)
        {
            try
            {
                SALA_SERVIDORES sala = new Biblioteca.DALC.SALA_SERVIDORES();
                sala.NOMBRE_SALA = _salaServidores.Nombre_sala;
                sala.PISO = _salaServidores.Piso;
                CommonBC.HomeroSystemEntities.SALA_SERVIDORES.Add(sala);
                CommonBC.HomeroSystemEntities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ModificarSalaServidor(SalaServidores _salaServidores)
        {
            try
            {
                SALA_SERVIDORES sala = CommonBC.HomeroSystemEntities.SALA_SERVIDORES.First
                    (
                        sa => sa.ID_SALA_SERVIDOR == _salaServidores.Id_salaServidor

                    );

                sala.NOMBRE_SALA = _salaServidores.Nombre_sala;
                sala.PISO = _salaServidores.Piso;
                CommonBC.HomeroSystemEntities.SaveChanges();

                return true;

            }
            catch
            {
                return false;
            }
        }

        public bool EliminarSalaServidor(int idSalaServidor)
        {
            int resultado = 0;
            SALA_SERVIDORES sala = CommonBC.HomeroSystemEntities.SALA_SERVIDORES.First
                   (
                       sa => sa.ID_SALA_SERVIDOR == idSalaServidor
                   );

            CommonBC.HomeroSystemEntities.SALA_SERVIDORES.Remove(sala);
            bool saveFailed;
            if (sala.RACK.Count==0) { 
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
                    exx.Entries.Single().Reload();
                    resultado = 1;
                }

            } while (saveFailed);
        }else
            {
                resultado = 1;
            }
            if(resultado == 0)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public SalaServidores BuscarSalaServidorPorId(int id_salaServidor)
        {

            SalaServidores objSalaServidores = new SalaServidores();
            try
            {
                SALA_SERVIDORES sala = CommonBC.HomeroSystemEntities.SALA_SERVIDORES.First
                    (
                        sa => sa.ID_SALA_SERVIDOR == id_salaServidor
                    );

                objSalaServidores.Id_salaServidor = int.Parse(sala.ID_SALA_SERVIDOR.ToString());
                objSalaServidores.Nombre_sala = sala.NOMBRE_SALA;
                objSalaServidores.Piso = int.Parse(sala.PISO.ToString());

                return objSalaServidores;

            }
            catch
            {
                return null;

            }
        }

        public bool ValidarSalaServidoxrNombre(string nombre_salaServidor)
        {
            try
            {
                SALA_SERVIDORES sala = CommonBC.HomeroSystemEntities.SALA_SERVIDORES.First
                    (
                        sa => sa.NOMBRE_SALA == nombre_salaServidor
                    );

                return true;

            }
            catch
            {
                return false;
            }

        }


        private List<SalaServidores> GenerarListado(List<SALA_SERVIDORES> listSalaServidoresDALC)
        {
            List<SalaServidores> listadoSalaServidores = new List<SalaServidores>();
            foreach (SALA_SERVIDORES sal in listSalaServidoresDALC)
            {
                SalaServidores salaServidores = new SalaServidores();
                salaServidores.Id_salaServidor = int.Parse(sal.ID_SALA_SERVIDOR.ToString());
                salaServidores.Nombre_sala = sal.NOMBRE_SALA;
                salaServidores.Piso = int.Parse(sal.PISO.ToString());
                listadoSalaServidores.Add(salaServidores);
            }

            return listadoSalaServidores;
        }
        public List<SalaServidores> ListadoSalaServidores()
        {
            var salaServidores = CommonBC.HomeroSystemEntities.SALA_SERVIDORES.ToList();
            return GenerarListado(salaServidores);
        }


    }
}
