using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ec.gob.mimg.tms.api.Utils
{
 
    public static class MessageUtils
    {
        public enum CodigosHttp
        {
            OK = 200,
            Created=201,
            Aceptada=202,
            Error = 400,
            ErrorServidor=500
        }

       

    }
}
