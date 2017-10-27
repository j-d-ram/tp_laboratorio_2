using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excepciones
{
    public class DniInvalidoException : Exception
    {
        private string mensajeBase;
        public string mensajebase
        {
            get { return this.mensajeBase; }
        }

        public DniInvalidoException()
        {
            this.mensajeBase = "dni invalido";
        }

        public DniInvalidoException(string message, Exception e) : base (message,e)
        {
        }

        public DniInvalidoException(string message) : base (message)
        {
        }
        public DniInvalidoException(Exception e) : base ("Error Dni Inválido",e)
        {
        }

    }
}
