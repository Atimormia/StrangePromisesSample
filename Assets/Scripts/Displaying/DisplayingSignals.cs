using strange.extensions.signal.impl;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPicturesSignal : Signal { }

public class TexturesImportedSignal : Signal<IEnumerable<Texture2D>> { }