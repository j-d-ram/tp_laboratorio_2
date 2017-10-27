using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excepciones;

namespace EntidadesAbstractas
{
    /// <summary>
    /// Clase abstracta Persona
    /// </summary>
    public abstract class Persona
    {

        #region FIELDS
        private string _apellido;
        private int _dni;
        private ENacionalidad _nacionalidad;
        private string _nombre;
        #endregion

        #region PROPERTIES
        /// <summary>
        /// Propiedad lectura y escritura, campo string Apellido
        /// </summary>
        public string Apellido
        {
            get { return this._apellido; }
            set { this._apellido = this.ValidarNombreApellido(value); }
        }

        /// <summary>
        /// Propiedad lectura y escritura, campo int _dni
        /// </summary>
        public int DNI
        {
            get { return this._dni; }
            set { try 
                    { 
                        this._dni = this.ValidarDni(this._nacionalidad, value); 
                    } 
                    catch(NacionalidadInvalidaException e)
                    {
                        this._dni = 1;
                        Console.WriteLine(e.Message);
                    }
                    catch (DniInvalidoException e) 
                    {
                        this._dni = 0;
                        Console.WriteLine(e.Message);
                    } 
                }
        }
        
        /// <summary>
        /// Propiedad lectura escritura, campo ENacionalidad _nacionalidad
        /// </summary>
        public ENacionalidad Nacionalidad
        {
            get { return this._nacionalidad; }
            set {this._nacionalidad = value;}
        }

        /// <summary>
        /// Propiedad lectura escritura, campo string _nombre
        /// </summary>
        public string Nombre
        {
            get { return this._nombre; }
            set { this._nombre = this.ValidarNombreApellido(value); }
        }

        /// <summary>
        /// Propiedad solo escritura, valida string recibido e inserta en el campo int _dni
        /// </summary>
        public string StringToDNI
        {
            set {
                try 
                {
                    this._dni = this.ValidarDni(this._nacionalidad, value); 
                }
                catch (NacionalidadInvalidaException e)
                {
                    this._dni = 1;
                    Console.WriteLine(e.Message);
                }
                catch (DniInvalidoException e)
                {
                    this._dni = 0;
                    Console.WriteLine(e.Message);
                } 
                
            }
        }

        #endregion

        #region METHODS

        #region Constructors
        /// <summary>
        /// Constructor por default de la clase Persona
        /// </summary>
        public Persona()
        {
            this._apellido = "";
            this._dni = 0;
            this._nacionalidad = ENacionalidad.Argentino;
            this._nombre = "";
        }

        /// <summary>
        /// Constructor clase Persona, recibe 4 parámetros
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="dni"></param>
        /// <param name="nacionalidad"></param>
        public Persona(string nombre, string apellido, int dni, ENacionalidad nacionalidad) : this (nombre, apellido, nacionalidad)
        {
            this.DNI = dni;
        }

        /// <summary>
        /// Constructor clase Persona, recibe 4 parámetros
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="dni"></param>
        /// <param name="nacionalidad"></param>
        public Persona(string nombre, string apellido, string dni, ENacionalidad nacionalidad) : this (nombre,apellido,nacionalidad)
        {
            this.StringToDNI = dni;
        }

        /// <summary>
        /// Constructor clase Persona, recibe 3 parámetros
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="nacionalidad"></param>
        public Persona(string nombre, string apellido, ENacionalidad nacionalidad) : this ()
        {
            this.Apellido = apellido;
            this.Nacionalidad = nacionalidad;
            this.Nombre = nombre;
        }
        #endregion

        #region Validation
        /// <summary>
        /// Valida dni segun ENacionalidad y valor entero recibido, retorna entero
        /// </summary>
        /// <param name="nacionalidad"></param>
        /// <param name="dato"></param>
        /// <returns></returns>
        private int ValidarDni(ENacionalidad nacionalidad, int dato)
        {
            if (nacionalidad == ENacionalidad.Argentino && (dato > 0 && dato < 89999999))
                return dato;
            else if (nacionalidad == ENacionalidad.Argentino && (dato > 89999999))
                throw new DniInvalidoException();
            else if (nacionalidad == ENacionalidad.Extranjero && (dato > 89999999))
                return dato;
            else if (nacionalidad == ENacionalidad.Extranjero && (dato < 89999999))
                throw new NacionalidadInvalidaException();
            else
                return dato;
        }

        /// <summary>
        /// Valida dni segun ENacionalidad y valor string recibido, retorna entero
        /// </summary>
        /// <param name="nacionalidad"></param>
        /// <param name="dato"></param>
        /// <returns></returns>
        private int ValidarDni(ENacionalidad nacionalidad, string dato)
        {
            int dni = 0;
            try
            {
                dni = int.Parse(dato);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            if (nacionalidad == ENacionalidad.Argentino && (dni > 0 && dni< 89999999))
                return dni;
            else if (nacionalidad == ENacionalidad.Argentino && (dni > 89999999))
                throw new DniInvalidoException();
            else if (nacionalidad == ENacionalidad.Extranjero && (dni > 89999999))
                return dni;
            else if (nacionalidad == ENacionalidad.Extranjero && (dni < 89999999))
                throw new NacionalidadInvalidaException();
            else
                return dni;
        }

        /// <summary>
        /// Verifica que un string tenga caracteres válidos para nombre y apellido
        /// </summary>
        /// <param name="dato"></param>
        /// <returns></returns>
        private string ValidarNombreApellido(string dato)
        {

            char j=(char)0;
            for (int i = 0; i < dato.Length; i++)
            {
                
                j = char.Parse(dato.Substring(i,1));
                
                if ( (int)j < 65 || ((int)j > 90 &&  (int)j < 97) || (int)j >122 )
                    return "vacio";
            }

            return dato;
            
        }
        #endregion

        /// <summary>
        /// Publica información de Persona
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Nombre completo: "+ this.Apellido +", "+this.Nombre+"\nNACIONALIDAD: "+ this.Nacionalidad.ToString()+"\nDNI: "+this.DNI.ToString();
        }

        #endregion

        #region NESTED TYPES
        /// <summary>
        /// Enumerado: Argentino, Extranjero, SinNacionalidad
        /// </summary>
        public enum ENacionalidad
        {
            Argentino,
            Extranjero,
        }
        
        #endregion
    }
}
