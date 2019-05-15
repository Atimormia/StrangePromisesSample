using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

class TexturesImporterFromFs : ITexturesImporter
{
    public IEnumerable<Texture2D> ImportTextures(string path)
    {
        var dir = Directory.CreateDirectory(path);
        var pics = dir.GetFiles().Where(x => !x.Extension.Contains("meta")).ToArray();
        var picsCount = pics.Length;

        foreach (var pic in pics)
        {
            yield return LoadTexture(pic.FullName);
        }
    }

    private Texture2D LoadTexture(string FilePath)
    {
        var FileData = File.ReadAllBytes(FilePath);
        var Tex2D = new Texture2D(2, 2);
        return Tex2D.LoadImage(FileData) ? Tex2D : null;
    }
}
