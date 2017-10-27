using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesAbstractas;
using Excepciones;

namespace EntidadesInstanciables
{
    /// <summary>
    /// Clase sellada Profesor, hereda de Universitario
    /// </summary>
    public sealed class Profesor : Universitario
    {
        #region Fields
        private Queue<Universidad.EClases> _clasesDelDia;
        private static Random _random;
        #endregion

        #region Methods
        #region Constructors
        /// <summary>
        /// Constructor Profesor de clase inicializar el campo _random
        /// </summary>
        static Profesor()
        {
            Profesor._random = new Random();
        }

        /// <summary>
        /// constructor por default Profesor
        /// </summary>
        public Profesor() : base ()
        {
            this._clasesDelDia = new Queue<Universidad.EClases>();
        }

        /// <summary>
        /// Constructor Profesor recibe 5 parámetros
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="dni"></param>
        /// <param name="nacionalidad"></param>
        public Profesor(int id, string nombre, string apellido, string dni, ENacionalidad nacionalidad)
           : base (id, nombre, apellido, dni, nacionalidad)
        {
            this._clasesDelDia = new Queue<Universidad.EClases>();
            this._randomClase();
        }
        #endregion

        /// <summary>
        /// Apila dos clases al azar seleccionados por el atributo de tipo Random
        /// </summary>
        private void _randomClase()
        {
            this._clasesDelDia.Enqueue((Universidad.EClases)Profesor._random.Next(0,3));
            this._clasesDelDia.Enqueue((Universidad.EClases)Profesor._random.Next(0,3));
        }
        /// <summary>
        /// Un profesor es igual a una clase si dicta la misma
        /// </summary>
        /// <param name="i"></param>
        /// <param name="clase"></param>
        /// <returns></returns>
        public static bool operator ==(Profesor i, Universidad.EClases clase)
        {
            foreach (Universidad.EClases item in i._clasesDelDia)
            {
                if (item == clase)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Un profesor es diferente a una clase si no dicta la misma
        /// </summary>
        /// <param name="i"></param>
        /// <param name="clase"></param>
        /// <returns></returns>
        public static bool operator !=(Profesor i, Universidad.EClases clase)
        {
            return !(i==clase);
        }
        /// <summary>
        /// Muestra las clases del día que dicta el profesor
        /// </summary>
        /// <returns></returns>
        protected override string ParticiparEnClase()
        {
            StringBuilder s = new StringBuilder();
            s.AppendLine("\nCLASES DEL DIA: \n");
            foreach (Universidad.EClases item in this._clasesDelDia)
            {
                s.AppendLine(item.ToString());
            }
            return s.ToString();
        }
        /// <summary>
        /// Muestra datos del profesor
        /// </summary>
        /// <returns></returns>
        protected override string MostrarDatos()
        {
            return base.MostrarDatos()+this.ParticiparEnClase();
        }

        /// <summary>
        /// Publica datos del profesor
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.MostrarDatos();
        }

        #endregion
    }
}
