using System.Collections.Generic;
using UnityEngine;

interface ITexturesImporter
{
    IEnumerable<Texture2D> ImportTextures(string path);
}