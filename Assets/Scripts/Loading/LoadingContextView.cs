using strange.extensions.context.impl;

public class LoadingContextView : ContextView
{

    void Awake()
    {
        context = new LoadingContext(this);
    }
}
