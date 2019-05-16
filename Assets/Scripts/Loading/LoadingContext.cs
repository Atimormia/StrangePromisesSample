using strange.extensions.context.api;
using UnityEngine;

class LoadingContext: BaseContext
{
    public LoadingContext(MonoBehaviour view) : base(view)
    {
    }

    public LoadingContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
    {
    }

    protected override void mapBindings()
    {
        //Bind some abstract and/or concrete Views
        //Injection binding.
        //Map a mock model and a mock service, both as Singletons
        //injectionBinder.Bind<IExampleModel>().To<ExampleModel>().ToSingleton();
        injectionBinder.Bind<IPictureLoader>().To<PictureLoaderToFs>().ToSingleton();


        //View/Mediator binding
        //This Binding instantiates a new ExampleMediator whenever as ExampleView
        //Fires its Awake method. The Mediator communicates to/from the View
        //and to/from the App. This keeps dependencies between the view and the app
        //separated.
        mediationBinder.Bind<LoadingView>().To<LoadingMediator>();


        //Event/Command binding
        commandBinder.Bind<StartLoadingSignal>().To<LoadPictureCommand>();

    }
}
