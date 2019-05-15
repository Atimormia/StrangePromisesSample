using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DownloadController : MonoBehaviour {

    public InputField inputUrl;
    public PictureLoader loader;
    // Use this for initialization
    void Start ()
    {
        GetComponent<Button>().onClick.AddListener(() => { loader.LoadPicture(inputUrl.text); });
    }

}
