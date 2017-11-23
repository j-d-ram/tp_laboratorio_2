using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Archivos
{
    public class Texto : IArchivo<string>
    {
        private string path;
        public Texto(string archivo)
        {
            this.path = archivo;
        }

        public bool guardar(string datos)
        {
            bool existencia=false;
            bool TodoOk=true;
            if (File.Exists(this.path))
                existencia = true;
            else
                existencia = false;
            try
            {

                StreamWriter EscritorStream = new StreamWriter(this.path, existencia, Encoding.UTF8);
                EscritorStream.WriteLine(datos);
                return TodoOk;
            }
            catch (Exception)
            {
                TodoOk = false;
            }
            return TodoOk;
        }

        public bool leer(out List<string> datos)
        {
            datos = new List<string>();
            bool TodoOk = true;

            if (File.Exists(this.path))
                try
                {
                    StreamReader LectorStream = new StreamReader(this.path);
                    while (!LectorStream.EndOfStream)
                    {
                        datos.Add(LectorStream.ReadLine());
                    }
                    return TodoOk;
                }
                catch (Exception)
                {
                    TodoOk = false;
                }
            else
                TodoOk = false;
            
            return TodoOk;
        }

    }
}
