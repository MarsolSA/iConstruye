using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using iConstruye.Methods;


namespace iConstruye
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Nominas _nominas = new Nominas();
            System.Windows.Forms.Application.Run();
        }
    }
}
