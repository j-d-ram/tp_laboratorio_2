using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Archivos;
using ConsoleApplication1;
using EntidadesInstanciables;
using EntidadesAbstractas;
using Excepciones;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Testea que los valores erróneos de DNI extranjero o argentino sean 0 o 1 respectivamente en caso de ser erróneos
        /// </summary>
        [TestMethod]
        public void ValidarDNI()
        {
            Alumno alumnonacionalidadinvalida = new Alumno(2, "Juana", "Martinez", "12234458",
               EntidadesAbstractas.Persona.ENacionalidad.Extranjero, Universidad.EClases.Laboratorio,
               Alumno.EEstadoCuenta.Deudor);

            if (alumnonacionalidadinvalida.DNI != 1)
                Assert.Fail("DNI extranjero mal validado",alumnonacionalidadinvalida.DNI);

            Alumno alumnodninvalido = new Alumno(2, "Juan", "Fernandez", "112234458",
               EntidadesAbstractas.Persona.ENacionalidad.Argentino, Universidad.EClases.Laboratorio,
               Alumno.EEstadoCuenta.Deudor);

            if (alumnodninvalido.DNI != 0)
                Assert.Fail("DNI argentino mal validado",alumnodninvalido.DNI);

        }
        
        /// <summary>
        /// Testea que la lista de alumno de la clase jornada no esté en null
        /// </summary>
        [TestMethod]
        public void ValidarlistaAlumnos()
        {
            Jornada listalumnosjornada = new Jornada(Universidad.EClases.Laboratorio,new Profesor());

            Assert.IsNotNull(listalumnosjornada.Alumnos);
        }

        /// <summary>
        /// Prueba si se lanza la excepción al intentar adherir a la universidad el mismo alumno dos veces
        /// </summary>
        [TestMethod]
        public void TestExcepciones1()
        {
            Alumno a8 = new Alumno(8, "Rodrigo", "Smith", "22236456",
           EntidadesAbstractas.Persona.ENacionalidad.Argentino, Universidad.EClases.Legislacion,
           Alumno.EEstadoCuenta.AlDia);
            Alumno a7 = new Alumno(8, "Rodrigo", "Smith", "22236456",
           EntidadesAbstractas.Persona.ENacionalidad.Argentino, Universidad.EClases.Legislacion,
           Alumno.EEstadoCuenta.AlDia);
            Universidad g = new Universidad();
            g += a8;
            
            try
            {
                g+=a7;
                Assert.Fail("Debe lanzar una excepción de alumno repetido");
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(AlumnoRepetidoException));
            }
        }

        /// <summary>
        /// El tipo de excepción debe ser NacionalidadInvalidadException
        /// </summary>
        [TestMethod]
        public void TestExcepciones2()
        {
            try
            {
                Alumno AlumExtr = new Alumno(1, "Juan", "Lopez", "9125468",
                EntidadesAbstractas.Persona.ENacionalidad.Extranjero, Universidad.EClases.Laboratorio,
                Alumno.EEstadoCuenta.AlDia);
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(NacionalidadInvalidaException));
            }
        }

        /// <summary>
        /// Verifica caracteres válidos en el campo nombre de la clase Persona
        /// </summary>
        [TestMethod]
        public void CaracteresValidos()
        {
            Alumno a8 = new Alumno(8, "375668", "79S8", "22236456",
           EntidadesAbstractas.Persona.ENacionalidad.Argentino, Universidad.EClases.Legislacion,
           Alumno.EEstadoCuenta.AlDia);

            if (a8.Nombre != "vacio")
                Assert.Fail("Nombre inválido", a8.Nombre);
        }


    }
}
