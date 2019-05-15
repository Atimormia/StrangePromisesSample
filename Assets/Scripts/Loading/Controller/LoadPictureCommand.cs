using strange.extensions.command.impl;
using UnityEngine;

class LoadPictureCommand: Command
{
    //[Inject]
    //public GameObject contextView { get; set; }
    [Inject]
    public IPictureLoader loader { get; set; }
    [Inject]
    public string url { get; set; }
    [Inject]
    public FinishLoadingSignal finishLoadingSignal { get; set; }

    public override void Execute()
    {
        Retain();
        loader.LoadPicture(url).Then(x => OutputResult(x, true)).Catch(x => OutputResult(x.Message, false));
    }

    private void OutputResult(string result, bool successed)
    {
        finishLoadingSignal.Dispatch(result, successed);
        Release();
    }
}
