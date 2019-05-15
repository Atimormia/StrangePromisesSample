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

    public void Init()
    {
        refreshButton.onClick.AddListener(OnClick_RefreshButton);
        homeButton.onClick.AddListener(OnClick_HomeButton);
    }

    private void OnClick_HomeButton()
    {
        gameObject.SetActive(false);
    }

    private void OnClick_RefreshButton()
    {
        RefreshButtonPressed.Invoke();
    }

    public void RenderPictures(IEnumerable<Texture2D> textures)
    {
        var texturesList = textures.ToList();
        if (!ShowPics(texturesList.Count > 0)) return;

        RectTransform contentWrapper = FindChildWithName(FindChildWithName(scrollView.transform, "Viewport"), "Content").GetComponent<RectTransform>();
        DestroyChildren(contentWrapper);

        var wrapperHeight = 500f + (450f * (texturesList.Count % 2 == 0 ? texturesList.Count / 2 - 1 : texturesList.Count / 2));
        contentWrapper.sizeDelta = new Vector2(contentWrapper.sizeDelta.x, wrapperHeight);

        for (int i = 0; i < texturesList.Count; i++)
        {
            float y = -(250f + 450f * (i / 2));
            float x = i % 2 == 0 ? -250f : 250f;
            var pos = new Vector2(x, y);

            var image = Instantiate(imagePrefab, contentWrapper);
            image.GetComponent<RectTransform>().anchoredPosition = pos;
            image.GetComponent<Image>().overrideSprite = Sprite.Create(texturesList[i], new Rect(0, 0, texturesList[i].width, texturesList[i].height), new Vector2(0, 0));
        }
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
    public void OpenPanel()
    {
        gameObject.SetActive(true);
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
}
