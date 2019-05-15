using RSG;

interface IPictureLoader
{
    IPromise<string> LoadPicture(string url);
}
