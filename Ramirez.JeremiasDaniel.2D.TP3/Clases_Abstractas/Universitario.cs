using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excepciones;

namespace EntidadesAbstractas
{
    /// <summary>
    /// Clase abstracta Universitario
    /// </summary>
    public abstract class Universitario : Persona
    {
        #region FIELDS
        private int legajo;
        #endregion

        #region METHODS

        #region Constructors
        /// <summary>
        /// Constructor clase Universitario, recibe 5 parámetros
        /// </summary>
        /// <param name="legajo"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="dni"></param>
        /// <param name="nacionalidad"></param>
        public Universitario(int legajo, string nombre, string apellido, string dni, ENacionalidad nacionalidad) : base (nombre,apellido,dni,nacionalidad)
        {
            this.legajo = legajo;
        }

        /// <summary>
        /// Constructor default clase Universitario
        /// </summary>
        public Universitario()
            : base()
        {
            this.legajo = 0;
        }
        #endregion

        /// <summary>
        /// Metodo abstracto retorna string
        /// </summary>
        /// <returns></returns>
        protected abstract string ParticiparEnClase();

        /// <summary>
        /// Retorna string datos de la clase Universitario
        /// </summary>
        /// <returns></returns>
        protected virtual string MostrarDatos()
        {
            return base.ToString() + "\nLEGAJO NUMERO: " + this.legajo.ToString();
        }

        /// <summary>
        /// Invalidación Equals, compara si dos objetos son del tipo Universitario
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is Universitario && this is Universitario)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Dos universitarios son iguales si comparten mismo legajo o DNI
        /// </summary>
        /// <param name="pg1"></param>
        /// <param name="pg2"></param>
        /// <returns></returns>
        public static bool operator ==(Universitario pg1, Universitario pg2)
        {
            if (pg1.Equals(pg2) && (pg1.legajo == pg2.legajo || pg1.DNI == pg2.DNI))
                return true;

            else
                return false;
        }

        /// <summary>
        /// Dos universitarios son diferentes si no comparten legajo ni DNI
        /// </summary>
        /// <param name="pg1"></param>
        /// <param name="pg2"></param>
        /// <returns></returns>
        public static bool operator !=(Universitario pg1, Universitario pg2)
        {
            return !(pg2 == pg1);
        }

        #endregion
    }
}
