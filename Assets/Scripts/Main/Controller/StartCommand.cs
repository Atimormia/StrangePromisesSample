using strange.extensions.command.impl;

class StartCommand : Command
{
    [Inject] public DisplayPicturesSignal DisplayPicturesSignal { get; set; }

    public override void Execute()
    {
        Retain();
        DisplayPicturesSignal.Dispatch();
        Release();
    }
}
