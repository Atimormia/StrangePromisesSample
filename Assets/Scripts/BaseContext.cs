using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;

class BaseContext: MVCSContext
{
    public BaseContext(MonoBehaviour view) : base(view)
    {
    }

    public BaseContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
    {
    }
    protected override void addCoreComponents()
    {
        base.addCoreComponents();
        injectionBinder.Unbind<ICommandBinder>();
        injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
    }
}
