using strange.extensions.mediation.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

class DisplayingView: View
{
    public Button refreshButton;
    public Button homeButton;
    public Button downloadButton;
    public GameObject noPicturesGroup;
    public GameObject scrollView;
    public GameObject imagePrefab;

    public event Action RefreshButtonPressed;

    private RectTransform contentWrapper;

    public void Init()
    {
        contentWrapper = FindChildWithName(FindChildWithName(scrollView.transform, "Viewport"), "Content").GetComponent<RectTransform>();
        refreshButton.onClick.AddListener(OnClick_RefreshButton);
        homeButton.onClick.AddListener(OnClick_HomeButton);
    }

    public void RenderPictures(IEnumerable<Texture2D> textures)
    {
        var texturesList = textures.ToList();
        if (!ShowPics(texturesList.Count > 0)) return;

        DestroyChildren(contentWrapper);
        AdjustWrapper(texturesList.Count);

        for (int i = 0; i < texturesList.Count; i++)
        {
            AddPicture(texturesList[i], GetImagePosition(i));
        }
    }

    public void RenderPicture(Texture2D texture)
    {
        if (texture == null) throw new ArgumentNullException("texture");
        var havenTexturesCount = contentWrapper.GetComponentsInChildren<Image>().Length;
        AdjustWrapper(havenTexturesCount + 1);
        AddPicture(texture, GetImagePosition(havenTexturesCount));
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
    public void OpenPanel()
    {
        gameObject.SetActive(true);
    }

    private void AddPicture(Texture2D texture, Vector2 position)
    {
        if (texture == null) throw new ArgumentNullException("texture");
        var image = Instantiate(imagePrefab, contentWrapper);
        image.GetComponent<RectTransform>().anchoredPosition = position;
        image.GetComponent<Image>().overrideSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
    }

    private Vector2 GetImagePosition(int imageNumber)
    {
        if(imageNumber<0) throw new ArgumentException("Number can't be negative");
        float y = -(250f + 450f * (imageNumber / 2));
        float x = imageNumber % 2 == 0 ? -250f : 250f;
        return new Vector2(x, y);
    }

    private void AdjustWrapper(int imagesCount)
    {
        var wrapperHeight = 500f + (450f * (imagesCount % 2 == 0 ? imagesCount / 2 - 1 : imagesCount / 2));
        contentWrapper.sizeDelta = new Vector2(contentWrapper.sizeDelta.x, wrapperHeight);
    }

    private bool ShowPics(bool isShow)
    {
        scrollView.SetActive(isShow);
        noPicturesGroup.SetActive(!isShow);
        return isShow;
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
    private void OnClick_HomeButton()
    {
        gameObject.SetActive(false);
    }

    private void OnClick_RefreshButton()
    {
        RefreshButtonPressed.Invoke();
    }

}
