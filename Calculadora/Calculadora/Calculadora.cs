using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora
{
    class Calculadora
    {
        #region Métodos
        /// <summary>
        /// Realiza operación aritmética entre dos tipos Numero mediante el operador recibido por el parámetro string y devuelve el resultado en un double
        /// </summary>
        /// <param name="numero1"></param>
        /// <param name="numero2"></param>
        /// <param name="operador"></param>
        /// <returns></returns>
        public static double Operar(Numero numero1, Numero numero2, string operador)
        {
            double resultado;

            switch (Calculadora.validarOperador(operador))
            {
                case "/":
                    if (numero1.getNumero() == 0)
                        resultado = 0;
                    else if (numero2.getNumero() == 0)
                        resultado = numero1.getNumero();
                    else
                        resultado = numero1.getNumero() / numero2.getNumero();
                    break;
                case "*":
                    resultado = numero1.getNumero() * numero2.getNumero();
                    break;
                case "+":
                    resultado = numero1.getNumero() + numero2.getNumero();
                    break;
                case "-":
                    resultado = numero1.getNumero() - numero2.getNumero();
                    break;
                default:
                    resultado = 0;
                    break;
            }


            return resultado;
        }



        /// <summary>
        /// Valida el operador
        /// </summary>
        /// <param name="operador"></param>
        /// <returns></returns>
        private static string validarOperador(string operador)
        {
            if (operador == "+" || operador == "-" || operador == "/" || operador == "*")
                return operador;
            else
                return "+";
        }
        #endregion
    }
}
