using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace DeskProImport
{
    public partial class UploadForm
    {
        private void OnLoad(object o, EventArgs eventArgs)
        {
            if (!File.Exists(StrHistoryFile)) return;
            using (var reader = new StreamReader(StrHistoryFile))
            {
                if (!reader.EndOfStream)
                {
                    txtServerAddress.Text = reader.ReadLine();
                }
                if (!reader.EndOfStream)
                {
                    txtAPIKey.Text = reader.ReadLine();
                }
                if (!reader.EndOfStream)
                {
                    txtCSVDataFile.Text = reader.ReadLine();
                }
            }
        }
        
        private void OnClosing(object o, CancelEventArgs cancelEventArgs)
        {
            using (var writer = new StreamWriter(StrHistoryFile,false))
            {
                writer.WriteLine(txtServerAddress.Text);
                writer.WriteLine(txtAPIKey.Text);
                writer.WriteLine(txtCSVDataFile.Text);
                writer.Flush();
            }
        }
        
        private void BtnCsvDataFileDialogOnClick(object sender, EventArgs eventArgs)
        {
            txtCSVDataFile.Text = dataFileDialog.ShowDialog() == DialogResult.OK ? dataFileDialog.FileName : string.Empty;
        }
        
        
        private void BtnUploadOnClick(object sender, EventArgs eventArgs)
        {
            string strFileData;
            using (var fileStream = new StreamReader(txtCSVDataFile.Text))
            {
                strFileData = fileStream.ReadToEnd();
            }

            if (strFileData.Length <= 0) return;

            try
            {

                var request = (HttpWebRequest) WebRequest.Create(txtServerAddress.Text + ArticlesUrlSuffix);

                request.Method = "POST";
                request.ContentType = "application/json";
                request.Accept = "application/json";
                request.Headers.Add("X-Agent-Request", "true");
                request.Headers.Add("Authorization", $"key {txtAPIKey.Text}");

                using (var writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(strFileData);
                }

                try
                {
                    var response = request.GetResponse();
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        var strResponse = reader.ReadToEnd();
                        Console.WriteLine(strResponse);
                        try
                        {
                            dynamic json = JsonConvert.DeserializeObject(strResponse);
                            MessageBox.Show($"Article id: {json.data.id} created for title: {json.data.title}",
                                "Upload Successful");

                        }
                        catch (Exception e)
                        {
                            LogException(e);
                            Console.WriteLine("Original Response: ");
                            Console.WriteLine(strResponse);
                        }
                    }
                }
                catch (WebException e)
                {
                    Console.WriteLine("Error: Status: {0}", e.Status);
                    var response = e.Response;
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        var strResponse = reader.ReadToEnd();
                        try
                        {
                            dynamic json = JsonConvert.DeserializeObject(strResponse);
                            MessageBox.Show($"Error Status: {json.status}\nCode: {json.code}\nMessage: {json.message}",
                                "Upload Failed");
                            Console.WriteLine(strResponse);
                        }
                        catch (Exception e1)
                        {
                            LogException(e1);
                            Console.WriteLine("Original Response: ");
                            Console.WriteLine(strResponse);
                        }

                    }
                }
            }
            catch (Exception e)
            {
                LogException(e);
            }
        }

        private static void LogException(Exception e)
        {
            MessageBox.Show($"Error encountered: {e.Message}","Error Encountered");
            Console.WriteLine($"Error encountered: {e.Message}");
            Console.WriteLine($"Error encountered: {e.StackTrace}");
        }

        private void TxtCsvDataFileOnTextChanged(object sender, EventArgs e)
        {
            SetUploadEnabled();
        }
        
        private void TxtApiKeyOnTextChanged(object o, EventArgs eventArgs)
        {
            SetUploadEnabled();
        }

        private void TxtServerAddressOnTextChanged(object o, EventArgs eventArgs)
        {
            SetUploadEnabled();
        }


        private void SetUploadEnabled()
        {
            btnUpload.Enabled = txtCSVDataFile.Text.Length > 0 && txtAPIKey.Text.Length > 0 && txtServerAddress.Text.Length > 0;
        }

        private const string ArticlesUrlSuffix = "/api/v2/articles";
        private const string StrHistoryFile = "history.txt";
    }
}
