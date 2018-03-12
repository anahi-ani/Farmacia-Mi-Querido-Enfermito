using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.COMMON.Tools
{
   public class ManejdorDeArchivos
    {
        public string Archivo { get; set; }
        public ManejdorDeArchivos(string archivo)
        {
            Archivo = archivo;
        }
        public bool Guardar(string datos)
        {
            try
            {
                StreamWriter file = new StreamWriter(Archivo);
                file.Write(datos);
                file.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public string Leer()
        {
            try
            {
                StreamReader file = new StreamReader(Archivo);
                string datos = file.ReadToEnd();
                file.Close();
                return datos;
            }
            catch (Exception)
            {
                return null;
            }
        }
        //public string Archivo { get; set; }
        //public ManejdorDeArchivos(string archivo)
        //{
        //    Archivo = archivo;
        //}
        //public bool Guardar(string datos)
        //{
        //    try
        //    {
        //        File.WriteAllText(Archivo, datos);
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        //public string Leer()
        //{
        //    string datos;
        //    try
        //    {
        //        if (File.Exists(Archivo))
        //        {
        //            File.OpenRead(Archivo);
        //            datos = File.ReadAllText(Archivo);
        //            return datos;
        //        }
        //        else
        //        {
        //            File.Create(Archivo);
        //            datos = "";
        //            return datos;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}
    }
}
