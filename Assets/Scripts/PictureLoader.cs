using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using RSG;
using UnityEngine.UI;

public class PictureLoader : MonoBehaviour
{
    public string picturesDirectory;
    public Text messageOutput;

    public bool LoadPicture(string url)
    {
        var filename = url.Substring(url.LastIndexOf('/') + 1);
        filename = filename.Contains("?") ? filename.Substring(0, filename.IndexOf('?')) : filename;

        StartCoroutine(ImportObject(url, filename));
        //Download(url, filename).Then(res =>messageOutput.text = res);

        return true;
    }

    IEnumerator ImportObject(string url, string filename)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            string write_path = picturesDirectory + "\\" + filename;
            webRequest.downloadHandler = new DownloadHandlerFile(write_path);
            yield return webRequest.SendWebRequest();

            if (!string.IsNullOrEmpty(webRequest.error))
            {
                Debug.Log("Download Error");
            }
            else
            {
                Debug.Log("Wrote to path");
            }
        }
    }

    private IPromise<string> Download(string url, string filename)
    {
        var promise = new Promise<string>();    // Create promise.
        using (var client = UnityWebRequest.Get(url))
        {
            string write_path = picturesDirectory + "\\" + filename;
            client.downloadHandler = new DownloadHandlerFile(write_path);
            var operation = client.SendWebRequest();
            operation.completed +=   // Monitor event for download completed.
                (s) =>
                {
                    if (s.isDone)
                    {
                        promise.Resolve("Saved");
                    }
                };
        }

        return promise;
    }
}
