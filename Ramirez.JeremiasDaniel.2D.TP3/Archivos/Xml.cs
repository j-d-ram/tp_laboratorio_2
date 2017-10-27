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
    /// Clase genérica implementa interface IArchivo
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Xml<T> : IArchivo<T>
    {
        #region Methods
        /// <summary>
        /// Serializa datos T en XML
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="datos"></param>
        /// <returns></returns>
        public bool guardar(string archivo, T datos)
        {
            try
            {
                    XmlSerializer serializador = new XmlSerializer(typeof(T));
                    XmlTextWriter escritortextoxml = new XmlTextWriter(archivo, Encoding.UTF8);
                    serializador.Serialize(escritortextoxml, datos);
                    escritortextoxml.Close();
                    return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Deserializa XML y devuelve los datos deserializados en el parámetro de salida de tipo T
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="datos"></param>
        /// <returns></returns>
        public bool Leer(string archivo, out T datos)
        {
            try
            {
                if (File.Exists(archivo))
                {
                    XmlTextReader xmlreader = new XmlTextReader(archivo);
                    XmlSerializer serializadorxml = new XmlSerializer(typeof(T));
                    datos = (T)serializadorxml.Deserialize(xmlreader);
                    xmlreader.Close();
                    return true;
                }
                else
                    throw new Exception();
            }
            catch(Exception e)
            {
                datos = default(T);
                throw e;
            }

        }
        #endregion
    }
}
