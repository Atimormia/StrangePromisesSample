using strange.extensions.mediation.impl;
using System.Collections.Generic;
using UnityEngine;

class DisplayingMediator: Mediator
{
    [Inject]
    public DisplayingView view { get; set; }
    [Inject]
    public DisplayPicturesSignal LoadTexturesSignal { get; set; }
    [Inject]
    public TexturesImportedSignal TexturesImportedSignal { get; set; }
    [Inject]
    public OpenDisplayingPanelSignal OpenDisplayingPanelSignal { get; set; }
    [Inject]
    public OpenLoadingPanelSignal OpenLoadingPanelSignal { get; set; }

    public override void OnRegister()
    {
        view.Init();
        view.RefreshButtonPressed += OnRefreshButtonPressed;
        view.downloadButton.onClick.AddListener(OnDownloadButtonPressed);
        TexturesImportedSignal.AddListener(view.RenderPictures);
        OpenDisplayingPanelSignal.AddListener(view.OpenPanel);
        OpenDisplayingPanelSignal.AddListener(OnRefreshButtonPressed);
    }
    public override void OnRemove()
    {
        view.RefreshButtonPressed -= OnRefreshButtonPressed;
        view.downloadButton.onClick.RemoveListener(OnDownloadButtonPressed);
        TexturesImportedSignal.RemoveListener(view.RenderPictures);
        OpenDisplayingPanelSignal.RemoveListener(view.OpenPanel);
        OpenDisplayingPanelSignal.RemoveListener(OnRefreshButtonPressed);
    }
    private void OnRefreshButtonPressed()
    {
        LoadTexturesSignal.Dispatch();// initiates DisplayPicturesCommand though context
        Debug.Log("LoadTexturesSignal");
    }

    private void OnDownloadButtonPressed()
    {
        view.ClosePanel();
        OpenLoadingPanelSignal.Dispatch();
        Debug.Log("OpenLoadingPanelSignal");
    }

}
