using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Xml;
using Excepciones;

namespace Archivos
{
    /// <summary>
    /// Implementa interface IArchivo
    /// </summary>
    public class Texto : IArchivo<string>
    {
        #region Methods
        /// <summary>
        /// Guarda un string en un .txt
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="datos"></param>
        /// <returns></returns>
        public bool guardar(string archivo, string datos)
        {
            try
            {
                StreamWriter s1 = new StreamWriter(archivo);
                s1.WriteLine(datos);
                s1.Close();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }  
        }

        /// <summary>
        /// Lee contenido de un .txt y lo retorna en string
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="datos"></param>
        /// <returns></returns>
        public bool Leer(string archivo, out string datos)
        {
            try
            {
                if (File.Exists(archivo))
                {
                    StringBuilder s = new StringBuilder();
                    StreamReader s4 = new StreamReader(archivo);
                    string line = " ";
                    while ((line = s4.ReadLine()) != null)
                    {
                        s.AppendLine(line);
                    }

                    s4.Close();
                    datos = s.ToString();
                    return true;
                }
                else
                {
                    datos = "";
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}
