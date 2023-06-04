using System.Text.RegularExpressions;

namespace Wec.gob.mimg.tms.api.Utils
{
    public  class Util
    {
        public Util()
        {

        }

        public static bool EsCorreo(string correo)
        {
            bool existe;
            string expresion = @"/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/";
            System.Text.RegularExpressions.Regex automata = new Regex(expresion);
            existe= automata.IsMatch(correo);
            return existe;
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
