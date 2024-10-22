using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iConstruye.Helpers
{
    internal class LibreriaLog
    {
        public static void AddLog(string Mensaje)
        {
            string RutaC = @"C:\LogTemp\AddoniConstruye";
            string NomArch = $@"AiC_{DateTime.Now:dd-MM-yyyy}.log";
            string RutaCompleta = Path.Combine(RutaC, NomArch);

            try
            {
                if (!Directory.Exists(RutaC))
                {
                    Directory.CreateDirectory(RutaC);
                }

                string logEntry = $"{DateTime.Now:dd-MM-yyyy HH:mm:ss} {Mensaje}{Environment.NewLine}";
                File.AppendAllText(RutaCompleta, logEntry);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al escribir en el archivo de log: {ex.Message}");
            }
        }
    }
}
