using strange.extensions.signal.impl;
using UnityEngine;

public class StartLoadingSignal : Signal<string> {}

public class FinishLoadingSignal : Signal<Texture2D> { }
