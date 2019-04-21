# DownloaderApplication

<h3>
we give an uri and, the program downloads file.
</h3>
<h3>
The program use ProgresBarr.
</h3>

<h3>
Technologies
C# WPF
</h3>


``` c#
            using (WebClient client = new WebClient())
            {              
                    DownloadCancel.IsEnabled = true;
                    DownloadUri.IsEnabled = false;

                    ctsForDownload = new CancellationTokenSource();
                    ctsForDownload.Token.Register(() =>
                    {
                        client.CancelAsync();
                    });

                try
                {
                    Uri address = new Uri(TextUri.Text);
                    string[] ar = address.Segments;
                    string EnterFolderName = EnterName.Text;

                    string folderPath = System.IO.Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.Desktop), EnterFolderName);

                    Directory.CreateDirectory(folderPath);

                    string newFile = folderPath + @"\" + ar[ar.Length - 1];                   

                    client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgresBarr_ValueChanged);
                    client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);

                    await client.DownloadFileTaskAsync(address, newFile);
                }

                catch (UriFormatException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (NotSupportedException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    DownloadUri.IsEnabled = true;
                    DownloadCancel.IsEnabled = false;
                    ProgresBarr.Value = 0;
                }
            }
        }



```
