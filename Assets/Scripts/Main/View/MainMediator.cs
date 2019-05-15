using strange.extensions.mediation.impl;
class MainMediator: Mediator
{
    [Inject]
    public MainView View { get; set; }
    [Inject]
    public OpenLoadingPanelSignal OpenLoadingPanelSignal { get; set; }
    [Inject]
    public OpenDisplayingPanelSignal OpenDisplayingPanelSignal { get; set; }
    public override void OnRegister()
    {
        View.picturesButton.onClick.AddListener(OnClick_PicturesButton);
        View.downloadButton.onClick.AddListener(OnClick_DownloadButton);
    }
    public override void OnRemove()
    {

    }
    private void OnClick_DownloadButton()
    {
        OpenLoadingPanelSignal.Dispatch();
    }

    private void OnClick_PicturesButton()
    {
        OpenDisplayingPanelSignal.Dispatch();
    }
}
