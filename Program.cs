using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace DeskProImport
{
    public static class Program
    {
        public static void Main()
        {
            /* Hack for Mono for server certificate errors
             * May not be necessary for windows
             */
            ServicePointManager.ServerCertificateValidationCallback += delegate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors sslPolicyErrors) {
                Console.WriteLine(sslPolicyErrors);
                return true;
            };
            /* End hack */
            
            var form = new UploadForm();
            form.ShowDialog();
        }
    }
}