using RSG;
using UnityEngine;

interface IPictureLoader
{
    IPromise<Texture2D> LoadPicture(string url);
}
