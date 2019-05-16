using strange.extensions.mediation.impl;
using System.Collections.Generic;
using UnityEngine;

class DisplayingMediator: Mediator
{
    [Inject]
    public DisplayingView view { get; set; }
    [Inject]
    public DisplayPicturesSignal DisplayPicturesSignal { get; set; }
    [Inject]
    public TexturesImportedSignal TexturesImportedSignal { get; set; }
    [Inject]
    public OpenDisplayingPanelSignal OpenDisplayingPanelSignal { get; set; }
    [Inject]
    public OpenLoadingPanelSignal OpenLoadingPanelSignal { get; set; }
    [Inject] public FinishLoadingSignal FinishLoadingSignal { get; set; }

    public override void OnRegister()
    {
        view.Init();
        view.RefreshButtonPressed += OnRefreshButtonPressed;
        view.downloadButton.onClick.AddListener(OnDownloadButtonPressed);
        TexturesImportedSignal.AddListener(view.RenderPictures);
        OpenDisplayingPanelSignal.AddListener(view.OpenPanel);
        FinishLoadingSignal.AddListener(view.RenderPicture);
    }
    public override void OnRemove()
    {
        view.RefreshButtonPressed -= OnRefreshButtonPressed;
        view.downloadButton.onClick.RemoveListener(OnDownloadButtonPressed);
        TexturesImportedSignal.RemoveListener(view.RenderPictures);
        OpenDisplayingPanelSignal.RemoveListener(view.OpenPanel);
        FinishLoadingSignal.RemoveListener(view.RenderPicture);
    }
    
    private void OnRefreshButtonPressed()
    {
        DisplayPicturesSignal.Dispatch();// initiates DisplayPicturesCommand though context
        Debug.Log("DisplayPicturesSignal");
    } 
    private void OnDownloadButtonPressed()
    {
        view.ClosePanel();
        OpenLoadingPanelSignal.Dispatch();
        Debug.Log("OpenLoadingPanelSignal");
    }

}
