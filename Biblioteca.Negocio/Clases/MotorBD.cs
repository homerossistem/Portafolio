using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Negocio.Clases
{
    [Serializable]
   public class MotorBD
    {
        private int id_motor;
        private string _motor;

        public int Id_motor
        {
            get
            {
                return id_motor;
            }

            set
            {
                id_motor = value;
            }
        }

        public string Motor
        {
            get
            {
                return _motor;
            }

            set
            {
                _motor = value;
            }
        }

        public MotorBD()
        {
            this.Init();
        }

        private void Init()
        {
            this.Id_motor = 0;
            this.Motor = string.Empty;
        }
    }
}
