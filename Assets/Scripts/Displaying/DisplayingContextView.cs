using strange.extensions.context.impl;
public class DisplayingContextView : ContextView
{

    void Awake()
    {
        context = new DisplayingContext(this);
    }
}
