using strange.extensions.context.api;
using UnityEngine;

class DisplayingContext: BaseContext
{
    public DisplayingContext(MonoBehaviour view) : base(view)
    {
    }

    public DisplayingContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
    {
    }

    protected override void mapBindings()
    {
        //Bind some abstract and/or concrete Views
        //Injection binding.
        //Map a mock model and a mock service, both as Singletons
        //injectionBinder.Bind<IExampleModel>().To<ExampleModel>().ToSingleton();
        //injectionBinder.Bind<ITexturesImporter>().To<TexturesImporterFromFs>().ToSingleton();

        //injectionBinder.Bind<TexturesImportedSignal>().ToSingleton();
        //View/Mediator binding
        //This Binding instantiates a new ExampleMediator whenever as ExampleView
        //Fires its Awake method. The Mediator communicates to/from the View
        //and to/from the App. This keeps dependencies between the view and the app
        //separated.
        mediationBinder.Bind<DisplayingView>().To<DisplayingMediator>();
        commandBinder.Bind<DisplayPicturesSignal>().To<DisplayPicturesCommand>();

        //Event/Command binding
        //commandBinder.Bind<OpenDisplayingPanelSignal>().
        //commandBinder.Bind<DisplayPicturesSignal>().To<DisplayPicturesCommand>();
        //injectionBinder.Bind<CloseDisplayingPanelSignal>().ToSingleton().CrossContext();
    }
}
