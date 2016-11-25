using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Negocio.Clases;
using Biblioteca.DALC;

namespace Biblioteca.Negocio.DAO
{
    public class DocumentoDAO
    {
        public bool AgregarDocumento(Documento _documento)
        {
            try
            {
                DOCUMENTO objDocumentoDALC = new DOCUMENTO();
                objDocumentoDALC.URL_DOCUMENTO = _documento.Url_documento;


                CommonBC.HomeroSystemEntities.DOCUMENTO.Add(objDocumentoDALC);
                CommonBC.HomeroSystemEntities.SaveChanges();
            }
            catch
            {
                return false;
            }


            return true;
        }

        public bool ExisteDocumento(String url)
        {
          int count =  CommonBC.HomeroSystemEntities.DOCUMENTO.Count
                (
                  doc => doc.URL_DOCUMENTO == url
                );
         if(count>=1)
            {
                return true;
            }
            return false;

        }

        public Documento bsucarDocumento(String url)
        {
            try
            {
                Documento objDocumento = new Documento();
                DOCUMENTO objDocumentoDALC = CommonBC.HomeroSystemEntities.DOCUMENTO.First
                    (
                        doc => doc.URL_DOCUMENTO == url
                    );
                objDocumento.Id_documento = int.Parse(objDocumentoDALC.ID_DOCUMENTO.ToString());
                objDocumento.Url_documento = objDocumentoDALC.URL_DOCUMENTO;
                return objDocumento;
            }
            catch
            {
                return null;
            }
        }
        public Documento buscarDocumentoPorId(int id)
        {
            try
            {
                Documento objDocumento = new Documento();
                DOCUMENTO objDocumentoDALC = CommonBC.HomeroSystemEntities.DOCUMENTO.First
                    (
                        doc => doc.ID_DOCUMENTO == id
                    );
                objDocumento.Id_documento = int.Parse(objDocumentoDALC.ID_DOCUMENTO.ToString());
                objDocumento.Url_documento = objDocumentoDALC.URL_DOCUMENTO;
                return objDocumento;
            }
            catch
            {
                return null;
            }
        }
    }
}
