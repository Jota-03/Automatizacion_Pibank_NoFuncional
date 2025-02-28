using System;
using System.IO;
using System.Diagnostics;
using System.IO.Compression;
using System.Text;
using System.Net.Mail;
using System.Net;


namespace Orquestador
{
    public class Program
    {
        string rutaProyecto; //DECLARANDO VARIABLE DE LA RUTA DEL PROYECTO
        public static string RutaEvidencia, rutaComprimido; //DECLARANDO LAS VARIABLES PUBLIC STATIC PARA QUE PUEDAN SER LLAMADAS DESDE CUALQUIER METODO
        string resultConsole;// DECLARANDO VARIABLE QUE CONTIENE TODO EL MSJ DE CONSOLA
                             // string estadoCaso;// DECLARANDO VARIABLE QUE CONTIENE EL ESTADO DEL CASO
                             //bool estado;// DECLARANDO LA VARIABLE QUE CONTIENE EL ESTADO DEL CASO EN TIPO BOOLEANO

        public static void Main(String[] args)
        {
        }

        //ESTE METODO SE ENCARGA DE EJECUTAR EL COMANDO MVN EN LA RAIZ DEL PROGRAMA EN JAVA
        public bool OpenWithStartInfo(string Test, string rutas)
        {
            rutaProyecto = @rutas;
            //NOS UBICAMOS EN LA RUTA DONDE SE ENCUENTRA EL PROYECTO JAVA Y SE LANZA COMANDO DE EJECUCION POR CONSOLA
            Process process = new Process();
            ProcessStartInfo startInfo = ObtenerInformacionProceso(rutaProyecto);
            startInfo.WindowStyle = ProcessWindowStyle.Maximized;
            process.StartInfo = startInfo;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.StandardInput.WriteLine(Test);
            process.StandardInput.Flush();
            process.StandardInput.Close();
            process.WaitForExit();

            resultConsole = process.StandardOutput.ReadToEnd();

            return true;//SE RETORNA EL ESTADO DEL CASO DE PRUEBA
        }


        //METODO PARA INICIAR LA CONSOLA
        public ProcessStartInfo ObtenerInformacionProceso(string directorioTrabajo)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                WorkingDirectory = directorioTrabajo,
                FileName = "cmd.exe"
            };
            return startInfo;
        }

        //INICIALIZA EL PROCESO DE EJECUCION
        public static bool Ejecutar(string Test, string rutas)
        {
            Program myProcess = new Program();
            bool estadoEjecucion = myProcess.OpenWithStartInfo(Test, rutas);
            return true;
        }

        public static string Comprimir(string ruta)
        {
            DateTime fechaActual = DateTime.Now;
            string fecha = fechaActual.ToString("-yyyyMMdd-HHmmss");
            string startPath = @ruta;//RUTA DE LA CARPETA DONDE ESTAN LAS EVIDENCIAS
            string rutaC = ruta + fecha + ".zip";//RUTA DONDE SE VA A GUARDAR EL ARCHIVO .ZIP CON LAS EVIDENCIAS
            string zipPath = @rutaC;//RUTA DEL .ZIP
            Console.WriteLine(zipPath);
            //File.Delete(zipPath);// ELIMINA LA CARPETA .ZIP EN CASO DE QUE EXISTA
            System.Threading.Thread.Sleep(2000);//TIEMPO DE ESPERA 
            ZipFile.CreateFromDirectory(startPath, zipPath);//CREAMOS LA CARPETA .ZIP DONDE SE ENCUENTRAN LAS EVIDENCIAS
            return zipPath; //RETORNA LA RUTA DEL .ZIP CREADO
        }
        public static string eliminarArchivo(string archivo1, string archivo2)
        {
            try
            {
                // Eliminar el primer archivo si existe
                if (File.Exists(archivo1))
                {
                    File.Delete(archivo1);
                    Console.WriteLine("Archivos 1 eliminados correctamente.");
                }

                // Eliminar la carpeta y todo su contenido
                if (Directory.Exists(archivo2))
                {
                    Directory.Delete(archivo2, true);
                }
                return "Archivos eliminados correctamente.";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Archivos NO eliminados correctamente.");
                return $"Error al eliminar archivos: {ex.Message}";
            }
        }

        public static string eliminarArchivos(string archivo)
        {
            DirectoryInfo di = new DirectoryInfo(archivo);
            FileInfo[] files = di.GetFiles();
            foreach (FileInfo file in files)
            {
                file.Delete();
            }
            System.Threading.Thread.Sleep(2000);
            return archivo;
        }

        public static string crearArchivo(string archivo)
        {
            Directory.CreateDirectory(archivo);
            System.Threading.Thread.Sleep(2000);
            return archivo;
        }

        public static string eliminarCarpeta(string carpeta)
        {
            // Verifica si la carpeta existe
            if (Directory.Exists(carpeta))
            {
                // Verifica si la carpeta está vacía
                if (Directory.GetFileSystemEntries(carpeta).Length == 0)
                {
                    // La carpeta está vacía, proceder a eliminarla
                    Directory.Delete(carpeta, true);
                }
                else
                {
                    // La carpeta no está vacía, eliminar todos los elementos
                    Directory.Delete(carpeta, true);
                }
            }
            else
            {
                return "La carpeta no existe.";
            }
            return carpeta;
        }
    }
}

