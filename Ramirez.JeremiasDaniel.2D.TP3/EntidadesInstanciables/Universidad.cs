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
    /// Clase Serializable Universidad
    /// </summary>
    [Serializable]
    public class Universidad
    {
        #region Fields
        private List<Alumno> alumnos;
        private List<Jornada> jornada;
        private List<Profesor> profesores;
        #endregion

        #region Properties
        /// <summary>
        /// Propiedad lectura y escritura campo alumnos
        /// </summary>
        public List<Alumno> Alumnos
        {
            get { return this.alumnos; }
            set { this.alumnos = value; }
        }
        /// <summary>
        /// propiedad lectura escritura campo jornada
        /// </summary>
        public List<Jornada> Jornada
        {
            get { return this.jornada; }
            set { this.jornada = value; }
        }
        /// <summary>
        /// Propiedad lectura escritura campo instructores
        /// </summary>
        public List<Profesor> Instructores
        {
            get { return this.profesores; }
            set { this.profesores = value; }
        }
        /// <summary>
        /// Indexador lectura escritura tipo Jornada
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Jornada this [int i]
        {
            get { return this.jornada[i]; }
            set { this.jornada[i] = value; }
        }
        #endregion

        #region Methods
        #region Constructors
        /// <summary>
        /// Constructor por default Universidad
        /// </summary>
        public Universidad()
        {
            this.alumnos = new List<Alumno>();
            this.jornada = new List<Jornada>();
            this.profesores = new List<Profesor>();
        }
        #endregion
        /// <summary>
        /// Muestra todas las jornadas de la universidad
        /// </summary>
        /// <param name="gim"></param>
        /// <returns></returns>
        private static string MostrarDatos(Universidad gim)
        {
            StringBuilder s = new StringBuilder();
            s.AppendLine("---------------------------------");
            s.AppendLine("JORNADAS: \n---------------------- ");
            foreach (Jornada item in gim.jornada)
            {
                s.AppendLine(item.ToString());
            }
            return s.ToString();
        }
        /// <summary>
        /// Publica los datos de la universidad
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Universidad.MostrarDatos(this);
        }
        /// <summary>
        /// Una Universidad es igual a una clase si existen profesores que puedan darla
        /// </summary>
        /// <param name="g"></param>
        /// <param name="clase"></param>
        /// <returns></returns>
        public static Profesor operator ==(Universidad g, EClases clase)
        {
            try
            {
                foreach (Profesor item in g.profesores)
                {
                    if (item == clase)
                        return item;    
                }
                throw new SinProfesorException();
            }
            catch (SinProfesorException e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Una Universidad es diferente a una clase si no existen profesores que puedan darla
        /// </summary>
        /// <param name="g"></param>
        /// <param name="clase"></param>
        /// <returns></returns>
        public static Profesor operator !=(Universidad g, EClases clase)
        {
            foreach (Profesor item in g.profesores)
            {
                if (item != clase)
                    return item;
            }
            throw new SinProfesorException();
        }
        /// <summary>
        /// Una Universidad es igual a un Alumnos si el mismo está inscripto
        /// </summary>
        /// <param name="g"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static bool operator ==(Universidad g, Alumno a)
        {
            foreach (Alumno item in g.alumnos)
            {
                if (item == a)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Una Universidad es diferente a un Alumnos si el mismo no está inscripto
        /// </summary>
        /// <param name="g"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static bool operator !=(Universidad g, Alumno a)
        {
            return !(g == a);
        }
        /// <summary>
        /// Una universidad es igual a un Profesor si el mismo está inscripto
        /// </summary>
        /// <param name="g"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static bool operator == (Universidad g, Profesor i)
        {
            foreach (Profesor item in g.profesores)
            {
                if (item == i)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Una universidad es profesor a un Profesor si el mismo no está inscripto
        /// </summary>
        /// <param name="g"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static bool operator !=(Universidad g, Profesor i)
        {
            return !(g == i);
        }
        /// <summary>
        /// Se puede adherir un Alumno a una Universidad si el mismo no está inscripto y su DNI es válido
        /// </summary>
        /// <param name="g"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Universidad operator +(Universidad g, Alumno a)
        {
            foreach (Alumno item in g.alumnos)
            {
                try
                {
                    if (item == a )
                        throw new AlumnoRepetidoException();
                }
                catch(AlumnoRepetidoException arex)
                {
                    throw arex;
                }
                try
                {
                    if (a.DNI == 1)
                        throw new NacionalidadInvalidaException();
                }
                catch(NacionalidadInvalidaException nac)
                {
                    throw nac;
                }
                try
                {
                    if (a.DNI == 0)
                        throw new DniInvalidoException();
                }
                catch (DniInvalidoException dni)
                {
                    throw dni;
                }

            }
            g.alumnos.Add(a);
            return g;
        }
        /// <summary>
        /// Se puede adherir un Profesor a una Universidad si el mismo no está inscripto
        /// </summary>
        /// <param name="g"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static Universidad operator+ (Universidad g, Profesor i)
        {
            foreach (Profesor item in g.profesores)
            {
                if (item == i)
                    return g;
            }
            g.profesores.Add(i);
            return g;
        }
        /// <summary>
        /// Se puede ahderir una clase a una universidad si existe un profesor que pueda darla
        /// </summary>
        /// <param name="g"></param>
        /// <param name="clase"></param>
        /// <returns></returns>
        public static Universidad operator +(Universidad g, EClases clase)
        {
            Profesor p = new Profesor();
            try
            {
                p = (g == clase);
            }
            catch(SinProfesorException e)
            {
                throw e;
            }

                Jornada j = new Jornada(clase, p);
                foreach (Alumno item in g.alumnos)
                {
                    if (item == clase)
                        j.Alumnos.Add(item);
                }
                g.jornada.Add(j);
                return g;
           
        }
        /// <summary>
        /// Serializa en XML
        /// </summary>
        /// <param name="gim"></param>
        /// <returns></returns>
        public static bool Guardar(Universidad gim)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            path += @"\Universidad.xml";

            Xml<Universidad> isxml = new Xml<Universidad>();
            try
            {
                isxml.guardar(path, gim);
                return true;
            }
            catch (Exception e)
            {
                throw new ArchivosException(e);
            }
            
        }
        /// <summary>
        /// Deserializa de un documento XML y retorna datos Universidad
        /// </summary>
        /// <returns></returns>
        public static Universidad Leer()
        {
            Xml<Universidad> idsxml = new Xml<Universidad>();
            string path = AppDomain.CurrentDomain.BaseDirectory;
            path += @"\Universidad.xml";

            Universidad gim = new Universidad();
            try
            {
                idsxml.Leer(path, out gim);
                return gim;
            }
            catch (Exception e)
            {
                throw new ArchivosException(e);
            }
        }
        #endregion

        #region Nested Types
        /// <summary>
        /// Enumerado EClases
        /// </summary>
            public enum EClases
            {
                Programacion,
                Legislacion,
                Laboratorio,
                SPD
            }
        #endregion
    }
}
