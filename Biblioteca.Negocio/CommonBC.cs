using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.DALC;

namespace Biblioteca.Negocio
{
    public class CommonBC
    {
        private static HomeroSystemEntities _homeroSystemEntities;

        public static HomeroSystemEntities HomeroSystemEntities
        {
            get
            {
                if (_homeroSystemEntities == null)
                {
                    _homeroSystemEntities = new HomeroSystemEntities();
                }
                return _homeroSystemEntities;
            }
        }

        public CommonBC()
        {

        }
    }
}
