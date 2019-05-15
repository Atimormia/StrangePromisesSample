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

        Download(url, filename).Then(res =>messageOutput.text = res);

        return true;
    }

    IEnumerator ImportObject(string url, string filename, Promise<string> promise = null)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            string write_path = picturesDirectory + "\\" + filename;
            webRequest.downloadHandler = new DownloadHandlerFile(write_path);
            yield return webRequest.SendWebRequest();

            if (!string.IsNullOrEmpty(webRequest.error))
            {
                promise.Reject(new System.Exception(webRequest.error));
                Debug.Log("Download Error");
            }
            else
            {
                promise.Resolve("Wrote to path");
                Debug.Log("Wrote to path");
            }
        }
    }

    private IPromise<string> Download(string url, string filename)
    {
        var promise = new Promise<string>(); 
        StartCoroutine(ImportObject(url, filename, promise));

        return promise;
    }
}
