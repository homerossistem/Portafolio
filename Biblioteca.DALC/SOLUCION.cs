//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Biblioteca.DALC
{
    using System;
    using System.Collections.Generic;
    
    public partial class SOLUCION
    {
        public decimal ID_SOLUCION { get; set; }
        public string DESCRIPCION_SOLUCION { get; set; }
        public Nullable<System.DateTime> FECHA_SOLUCION { get; set; }
        public decimal ID_TICKET { get; set; }
        public string NOMBRE_FUNCIONARIO { get; set; }
        public string EQUIPO_DE_TRABAJO { get; set; }
    
        public virtual TICKET TICKET { get; set; }
    }
}
