namespace SCM.Common;

public class MimeTypeComponent
{
    public string? GetMimeTypeFromPath(string path)
    {
        return null;
    }

    public string MimeTypeBinary = "";
    public string MimeTypeTextPlain = "";

    public bool IsBinary(string mime) => throw new NotImplementedException();
    public bool IsText(string mime) => throw new NotImplementedException();
    public bool IsImage(string mime) => throw new NotImplementedException();
    public bool IsVideo(string mime) => throw new NotImplementedException();
    public bool IsAudio(string mime) => throw new NotImplementedException();
}

