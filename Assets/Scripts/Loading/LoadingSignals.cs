using strange.extensions.signal.impl;

public class OpenLoadingPanelSignal : Signal { }
public class CloseLoadingPanelSignal : Signal { }
public class StartLoadingSignal : Signal<string> {}

public class FinishLoadingSignal : Signal<string, bool> { }
