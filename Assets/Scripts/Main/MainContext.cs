using strange.extensions.context.api;
using UnityEngine;

class MainContext: BaseContext
{
    public MainContext(MonoBehaviour view) : base(view)
    {
    }

    public MainContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
    {
    }

    protected override void mapBindings()
    {
        injectionBinder.Bind<OpenLoadingPanelSignal>().ToSingleton().CrossContext();
        injectionBinder.Bind<OpenDisplayingPanelSignal>().ToSingleton().CrossContext();
        injectionBinder.Bind<ITexturesImporter>().To<TexturesImporterFromFs>().ToSingleton().CrossContext();
        injectionBinder.Bind<TexturesImportedSignal>().ToSingleton().CrossContext();
        injectionBinder.Bind<FinishLoadingSignal>().ToSingleton().CrossContext();
        mediationBinder.Bind<MainView>().To<MainMediator>();
        commandBinder.Bind<StartSignal>().To<StartCommand>();
        commandBinder.Bind<DisplayPicturesSignal>().To<DisplayPicturesCommand>();
    }
    public override void Launch()
    {
        base.Launch();
        StartSignal startSignal = injectionBinder.GetInstance<StartSignal>();
        startSignal.Dispatch();
    }
}
