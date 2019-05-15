using strange.extensions.command.impl;

class DisplayPicturesCommand: Command
{
    [Inject]
    public ITexturesImporter importer { get; set; }
    [Inject]
    public TexturesImportedSignal TexturesImportedSignal { get; set; }
    public override void Execute()
    {
        Retain();
        TexturesImportedSignal.Dispatch(importer.ImportTextures(Settings.picturesDirectory));
        Release();        
    }
}
