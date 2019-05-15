using RSG;
using strange.extensions.context.api;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

class PictureLoaderToFs : IPictureLoader
{
    [Inject(ContextKeys.CONTEXT_VIEW)]
    public GameObject contextView { get; set; }
    public IPromise<string> LoadPicture(string url)
    {
        var filename = url.Substring(url.LastIndexOf('/') + 1);
        filename = filename.Contains("?") ? filename.Substring(0, filename.IndexOf('?')) : filename;

        return Download(url, filename);
    }

    private IEnumerator ImportObject(string url, string filename, Promise<string> promise = null)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            string write_path = Settings.picturesDirectory + "\\" + filename;
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
        contextView.GetComponent<MonoBehaviour>().StartCoroutine(ImportObject(url, filename, promise));

        return promise;
    }
}