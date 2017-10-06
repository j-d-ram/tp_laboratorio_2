using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora
{
    class Numero
    {
        #region Atributos
        private double _numero;
        #endregion

        #region Constrcutores
        /// <summary>
        /// Propiedad solo lectura campo _numero
        /// </summary>
        /// <returns></returns>
        public double getNumero()
        {
            return this._numero;
        }

        /// <summary>
        /// Constructor Numero por default
        /// </summary>
        public Numero()
        {
            this._numero = 0;
        }

        /// <summary>
        /// Constructor Numero, recibe un parámetro string
        /// </summary>
        /// <param name="numero"></param>
        public Numero(string numero)
        {
            this.setNumero(numero);
        }
         /// <summary>
         /// Constructor Número, recibe un parámetro double
         /// </summary>
         /// <param name="numero"></param>
        public Numero(double numero)
        {
            this._numero = numero;
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Intenta convertir string a double
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        private static double validarNumero(string numero)
        {
            double d;

            if (double.TryParse(numero, out d))
                return d;
            else
                return 0;
        }

        /// <summary>
        /// Setea string recibido en el campo _numero previa validación
        /// </summary>
        /// <param name="numeroString"></param>
        private void setNumero(string numeroString)
        {
            this._numero = Numero.validarNumero(numeroString);
        }
        #endregion
    }
}
