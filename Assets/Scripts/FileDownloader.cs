using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using UnityEngine;

namespace Assets.Scripts
{
    
    class FileDownloader
    {
        private readonly string _url;
        private readonly string _fullPathWhereToSave;
        private bool _result = false;

        public FileDownloader(string url, string fullPathWhereToSave)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException("url");
            if (string.IsNullOrEmpty(fullPathWhereToSave)) throw new ArgumentNullException("fullPathWhereToSave");

            this._url = url;
            this._fullPathWhereToSave = fullPathWhereToSave;
        }

        public bool StartDownload()
        {
            try
            {
                System.IO.Directory.CreateDirectory(Path.GetDirectoryName(_fullPathWhereToSave));

                if (File.Exists(_fullPathWhereToSave))
                {
                    File.Delete(_fullPathWhereToSave);
                }
                using (WebClient client = new WebClient())
                {
                    var ur = new Uri(_url);
                    client.DownloadFileCompleted += WebClientDownloadCompleted;
                    Debug.Log(@"Downloading file:");
                    client.DownloadFileAsync(ur, _fullPathWhereToSave);
                    return _result && File.Exists(_fullPathWhereToSave);
                }
            }
            catch (Exception e)
            {
                Debug.Log("Was not able to download file!");
                Debug.Log(e);
                return false;
            }
        }


        private void WebClientDownloadCompleted(object sender, AsyncCompletedEventArgs args)
        {
            _result = !args.Cancelled;
            if (!_result)
            {
                Debug.Log(args.Error.ToString());
            }
            Debug.Log(Environment.NewLine + "Download finished!");
        }

        public static bool DownloadFile(string url, string fullPathWhereToSave)
        {
            return new FileDownloader(url, fullPathWhereToSave).StartDownload();
        }
    }
}
