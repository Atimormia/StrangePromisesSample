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
        mediationBinder.Bind<MainView>().To<MainMediator>();
        injectionBinder.Bind<OpenLoadingPanelSignal>().ToSingleton().CrossContext();
        injectionBinder.Bind<OpenDisplayingPanelSignal>().ToSingleton().CrossContext();
    }
}
