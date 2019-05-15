using strange.extensions.signal.impl;
using System.Collections.Generic;
using UnityEngine;

public class OpenDisplayingPanelSignal : Signal { }

public class CloseDisplayingPanelSignal : Signal { }

public class DisplayPicturesSignal : Signal { }

public class TexturesImportedSignal : Signal<IEnumerable<Texture2D>> { }