using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PicturesViewer : MonoBehaviour
{
    public PictureLoader loader;
    public GameObject scrollView;
    public GameObject imagePrefab;
    public GameObject noPictureGroup;

    public void DisplayPictures()
    {
        var dir = Directory.CreateDirectory(loader.picturesDirectory);
        var pics = dir.GetFiles().Where(x => !x.Extension.Contains("meta")).ToArray();
        var picsCount = pics.Length;

        if (!ShowPics(picsCount > 0)) return;

        RectTransform contentWrapper = FindChildWithName(FindChildWithName(scrollView.transform,"Viewport"),"Content").GetComponent<RectTransform>();
        DestroyChildren(contentWrapper);
        
        var wrapperHeight = 500 + (450 * (picsCount % 2 == 0 ? picsCount / 2 - 1 : picsCount / 2));
        contentWrapper.sizeDelta = new Vector2(contentWrapper.sizeDelta.x, wrapperHeight);

        for (int i = 0; i < picsCount; i++)
        {
            float y = -(250 + 450 * (i / 2));
            float x = i % 2 == 0 ? -250 : 250;
            var pos = new Vector2(x, y);

            var image = Instantiate(imagePrefab, contentWrapper);
            image.GetComponent<RectTransform>().anchoredPosition = pos;

            Texture2D SpriteTexture = LoadTexture(pics[i].FullName);
            var sprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0, 0));
            image.GetComponent<Image>().overrideSprite = sprite;
        }
    }

    private bool ShowPics(bool isShow)
    {
        scrollView.SetActive(isShow);
        noPictureGroup.SetActive(!isShow);
        return isShow;
    }
    private Texture2D LoadTexture(string FilePath)
    {        
        var FileData = File.ReadAllBytes(FilePath);
        var Tex2D = new Texture2D(2, 2);
        return Tex2D.LoadImage(FileData) ? Tex2D : null;                 
    }

    private Transform FindChildWithName(Transform parent, string childName)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            var child = parent.transform.GetChild(i);
            if (child.name.Equals(childName))
                return child;
        }
        return null;
    }

    private void DestroyChildren(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }
}
