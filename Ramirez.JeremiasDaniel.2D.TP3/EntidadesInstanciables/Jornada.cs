using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesAbstractas;
using Excepciones;
using Archivos;

namespace EntidadesInstanciables
{
    /// <summary>
    /// Clase Jornada
    /// </summary>
    public class Jornada
    {
        #region Fields
        private List<Alumno> _alumnos;
        private Universidad.EClases _clase;
        private Profesor _instructor;
        #endregion

        #region Properties
        /// <summary>
        /// Propiedad lectura escritura campo _alumnos
        /// </summary>
        public List<Alumno> Alumnos
        {
            get { return this._alumnos; }
            set { this._alumnos = value; }
        }
        /// <summary>
        /// Propiedad lectura escritura campo _clases
        /// </summary>
        public Universidad.EClases Clase
        {
            get { return this._clase; }
            set { this._clase = value;}
        }
        /// <summary>
        /// Propiedad lectura escritura campo _profesor
        /// </summary>
        public Profesor Profesor
        {
            get { return this._instructor; }
            set { this._instructor = value; }
        }
        #endregion

        #region Methods
        #region Constructors
        /// <summary>
        /// Constructor por default Jornada
        /// </summary>
        private Jornada()
        {
            this._alumnos = new List<Alumno>();
        }
        /// <summary>
        /// Constructor Jornada recibe 2 parámetros
        /// </summary>
        /// <param name="clase"></param>
        /// <param name="instructor"></param>
        public Jornada(Universidad.EClases clase, Profesor instructor)
            : this()
        {
            this._clase = clase;
            this._instructor = instructor;
        }
        #endregion
        /// <summary>
        /// Una Jornada y un Alumno son iguales si la jornada contiene la clase que toma el alumno
        /// </summary>
        /// <param name="j"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static bool operator ==(Jornada j, Alumno a)
        {
            if (a == j._clase)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Una Jornada y un Alumno son iguales si la jornada no contiene la clase que toma el alumno
        /// </summary>
        /// <param name="j"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static bool operator !=(Jornada j, Alumno a)
        {
            return !(j==a);
        }
        /// <summary>
        /// Un alumno puede adherirse a una jornada si ya no está en la misma
        /// </summary>
        /// <param name="j"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Jornada operator +(Jornada j, Alumno a)
        {
            foreach (Alumno item in j._alumnos)
            {
                if (item == a)
                    return j;
            }

            j._alumnos.Add(a);

            return j;
        }

        /// <summary>
        /// Publica datos de la jornada
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.AppendFormat("CLASE DE {0} POR ",this._clase.ToString());
            s.AppendLine(this._instructor.ToString());
            foreach (Alumno item in this._alumnos)
            {
                s.AppendLine(item.ToString());
            }
            s.AppendLine("<----------------------------------------------->");
            return s.ToString();
        }

        /// <summary>
        /// Guarda una jornada en txt
        /// </summary>
        /// <param name="jornada"></param>
        /// <returns></returns>
        public static bool Guardar(Jornada jornada)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            path += @"\Jornada.txt";

            Texto t = new Texto();
            try
            {
                if (t.guardar(path, jornada.ToString()))
                    return true;
            }
            catch (Exception e)
            {
                ArchivosException a = new ArchivosException(e);
                throw a;
            }
            return false;
        }

        /// <summary>
        /// Lee archivo txt
        /// </summary>
        /// <returns></returns>
        public static string Leer()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\Jornada.txt";
            string archivo;
            Texto t = new Texto();
            try
            {
                if (t.Leer(path,out archivo))
                    return archivo;
            }
            catch (Exception e)
            {
                ArchivosException a = new ArchivosException(e);
                throw a;
            }
            return "";
        }

        #endregion

    }
}
