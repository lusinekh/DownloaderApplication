using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows;


namespace DownloaderApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void DownloadUri_Click(object sender, RoutedEventArgs e)
        {
            try
            {              

                Uri address = new Uri(TextUri.Text);
                string[] ar = address.Segments;
                string EnterFolderName = EnterName.Text;
                string folderPath = System.IO.Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.Desktop), EnterFolderName);

                Directory.CreateDirectory(folderPath);
                string newFile = folderPath + @"\" + ar[ar.Length - 1];
                using (WebClient client = new WebClient())
                {
                    client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgresBarr_ValueChanged);

                    client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                   await client.DownloadFileTaskAsync(address, newFile);

                }

            }
            catch (UriFormatException ex)
            {
                MessageBox.Show(ex.Message);              
            }

            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }

            catch (Exception)
            {
                throw;
            }
        }
        
        private void ProgresBarr_ValueChanged(object sender, DownloadProgressChangedEventArgs e )
        { 
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;
            ProgresBarr.Value = int.Parse(Math.Truncate(percentage).ToString());
        }


        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Download Completed");

        }

    }
}
