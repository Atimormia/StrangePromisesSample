using System;
using System.Net;
using UnityEngine;
using RSG;
using Assets.Scripts;
using System.Collections;

public class PictureLoader : MonoBehaviour
{
    public string picturesDirectory;

    public bool LoadPicture(string url)
    {
        var filename = url.Substring(url.LastIndexOf('/') + 1);
        filename = filename.Contains("?") ? filename.Substring(0, filename.IndexOf('?')) : filename;

        StartCoroutine(ImportObject(url, filename));
        
        return true;
    }

    IEnumerator ImportObject(string url, string filename)
    {

        WWW www = new WWW(url);
        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log("Download Error");
        }
        else
        {
            string write_path = picturesDirectory + "\\" + filename;

            System.IO.File.WriteAllBytes(write_path, www.bytes);

            Debug.Log("Wrote to path");
        }

    }

    //private IPromise<string> Download(string url, string filename)
    //{
    //    var promise = new Promise<string>();    // Create promise.
    //    using (var client = new WebClient())
    //    {
    //        client.DownloadStringCompleted +=   // Monitor event for download completed.
    //            (s, ev) =>
    //            {
    //                if (ev.Error != null)
    //                {
    //                    promise.Reject(ev.Error);   // Error during download, reject the promise.
    //            }
    //                else
    //                {
    //                    promise.Resolve(ev.Result); // Downloaded completed successfully, resolve the promise.
    //                }
    //            };

    //        client.DownloadStringAsync(new Uri(url), null); // Initiate async op.
    //    }

    //    return promise; // Return the promise so the caller can await resolution (or error).
    //}
}
