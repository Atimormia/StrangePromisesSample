using strange.extensions.context.impl;

class MainContextView : ContextView
{

    void Awake()
    {
        context = new MainContext(this);
    }
}