using RSG;
using strange.extensions.context.api;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

class PictureLoaderToFs : IPictureLoader
{
    [Inject(ContextKeys.CONTEXT_VIEW)]
    public GameObject contextView { get; set; }
    [Inject] public ITexturesImporter TexturesImporter { get; set; }

    public IPromise<Texture2D> LoadPicture(string url)
    {
        var filename = url.Substring(url.LastIndexOf('/') + 1);
        filename = filename.Contains("?") ? filename.Substring(0, filename.IndexOf('?')) : filename;

        return Download(url, filename);
    }


    private IEnumerator ImportObject(string url, string filename, Promise<Texture2D> promise = null)
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
                promise.Resolve(TexturesImporter.LoadTexture(write_path));
                Debug.Log("Wrote to path");
            }
        }
    }

    private IPromise<Texture2D> Download(string url, string filename)
    {
        var promise = new Promise<Texture2D>();
        contextView.GetComponent<MonoBehaviour>().StartCoroutine(ImportObject(url, filename, promise));

        return promise;
    }
}