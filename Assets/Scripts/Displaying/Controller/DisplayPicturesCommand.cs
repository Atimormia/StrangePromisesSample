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
        var textures = importer.ImportTextures(Settings.picturesDirectory);
        TexturesImportedSignal.Dispatch(textures);
        Release();        
    }
}
