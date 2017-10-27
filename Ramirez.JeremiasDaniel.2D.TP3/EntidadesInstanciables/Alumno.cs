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
    /// Clase sellada Alumno, hereda de Universitario
    /// </summary>
    public sealed class Alumno : Universitario
    {
        #region Fields
        private Universidad.EClases _claseQueToma;
        private EEstadoCuenta _estadoCuenta;
        #endregion

        #region Methods
        
        #region Constructors
        /// <summary>
        /// Constructor default Alumno
        /// </summary>
        public Alumno() 
            : base ()
        {
            this._claseQueToma = Universidad.EClases.SPD;
            this._estadoCuenta = EEstadoCuenta.AlDia;
        }

        /// <summary>
        /// Constructor Alumnos recibe 6 parámetros
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="dni"></param>
        /// <param name="nacionalidad"></param>
        /// <param name="clasesQueToma"></param>
        public Alumno(int id, string nombre, string apellido, string dni, ENacionalidad nacionalidad, Universidad.EClases clasesQueToma) 
            :base (id,nombre,apellido,dni,nacionalidad)
        {
            this._claseQueToma = clasesQueToma;
            this._estadoCuenta = EEstadoCuenta.AlDia;
        }

        /// <summary>
        /// Constructor Alumno, recibe 7 parámetros
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="dni"></param>
        /// <param name="nacionalidad"></param>
        /// <param name="claseQueToma"></param>
        /// <param name="estadoCuenta"></param>
        public Alumno(int id, string nombre, string apellido, string dni, ENacionalidad nacionalidad, Universidad.EClases claseQueToma, EEstadoCuenta estadoCuenta) 
            : this (id, nombre, apellido, dni, nacionalidad,claseQueToma)
        {
            this._estadoCuenta = estadoCuenta;
        }
        #endregion

        /// <summary>
        /// Muestra todos los datos el alumno
        /// </summary>
        /// <returns></returns>
        protected override string MostrarDatos()
        {
            StringBuilder s = new StringBuilder();
            s.AppendFormat(base.MostrarDatos());
            if(this._estadoCuenta == EEstadoCuenta.Deudor)
                s.AppendLine("\nESTADO DE CUENTA: Deudor");
            else if (this._estadoCuenta == EEstadoCuenta.AlDia)
                s.AppendLine("\nESTADO DE CUENTA: Cuota al día");
            else if (this._estadoCuenta == EEstadoCuenta.Becado)
                s.AppendLine("\nESTADO DE CUENTA: Becado");
            s.AppendLine(this.ParticiparEnClase());
            return s.ToString();
        }

        /// <summary>
        /// Muestra en que clase participa el Alumno
        /// </summary>
        /// <returns></returns>
        protected override string ParticiparEnClase()
        {
            return "\nTOMA CLASES DE: "+ this._claseQueToma.ToString();
        }

        /// <summary>
        /// Publica datos del alumno
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.MostrarDatos();
        }

        /// <summary>
        /// Un alumno es igual a una clase si participa en ella y si su estado de cuenta no es deudor
        /// </summary>
        /// <param name="a"></param>
        /// <param name="clase"></param>
        /// <returns></returns>
        public static bool operator ==(Alumno a, Universidad.EClases clase)
        {
            if (a._claseQueToma == clase && a._estadoCuenta != EEstadoCuenta.Deudor)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Un alumno es diferente a una clase solo si no toma la clase
        /// </summary>
        /// <param name="a"></param>
        /// <param name="clase"></param>
        /// <returns></returns>
        public static bool operator !=(Alumno a, Universidad.EClases clase)
        {
            if (a._claseQueToma != clase)
                return true;
            else
                return false;
        }
        #endregion

        #region Nested Types
        /// <summary>
        /// Enumerado EEstadoCuenta
        /// </summary>
        public enum EEstadoCuenta
        {
            Deudor,
            Becado,
            AlDia
        }
        #endregion

    }
}
