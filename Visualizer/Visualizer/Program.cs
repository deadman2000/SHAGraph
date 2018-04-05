using System;
using System.Windows.Forms;
using System.Security.Cryptography;
using Visualizer.Sha256Net;

namespace Visualizer
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            /*Random r = new Random();
            byte[] b = new byte[4];
            for (int i = 0; i < 10; i++)
            {
                r.NextBytes(b);
                Console.WriteLine(BitConverter.ToInt32(b, 0));
            }*/

            byte[] data = new byte[821];
            Random r = new Random();
            r.NextBytes(data);

            SHA256 sha = SHA256.Create();
            byte[] hash = sha.ComputeHash(data);

            SHA mySHA = new SHA(data);
            string myHash = mySHA.Result();
            Console.WriteLine(myHash);

            Application.Run(new Form1());
        }
    }
}
