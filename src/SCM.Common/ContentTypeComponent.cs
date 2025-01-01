namespace SCM.Common;

public class ContentTypeComponent : IContentTypeComponent
{
    public ContentType? GetContentTypeFromExt(string ext)
    {
        return ContentTypes.All.FirstOrDefault(x=>string.Equals(ext, x.Ext, StringComparison.CurrentCultureIgnoreCase));
    }

    public ContentType? GetContentTypeFromMime(string mimeType)
    {
        return ContentTypes.All.FirstOrDefault(x=>string.Equals(mimeType, x.MimeType, StringComparison.CurrentCultureIgnoreCase));
    }

    public IEnumerable<ContentType> GetAll() => ContentTypes.All;
}

public static class ContentTypes
{
    public static readonly ContentType Binary = new ContentType
    {
        Name = "Binary",
        MimeType = "application/octet-stream",
        Ext = ".bin",
        IsBinary = true
    };

    public static readonly ContentType TextPlain = new ContentType
    {
        Name = "Plain Text",
        MimeType = "text/plain",
        Ext = ".txt",
        IsText = true
    };

    public static readonly ContentType TextMarkDown = new ContentType
    {
        Name = "Markdowwn",
        MimeType = "text/markdown",
        Ext = ".md",
        IsText = true
    };


    public static readonly ContentType ImagePng = new ContentType
    {
        Name = "Portable Image?",
        MimeType = "image/png",
        Ext = ".png",
        IsBinary = true,
        IsImage = true
    };

    public static readonly IReadOnlyCollection<ContentType> All = [
        Binary, TextPlain, TextMarkDown, ImagePng
    ];
    // public static readonly ContentType ImagePng = null;
    // public static readonly ContentType ImageJpg = null;
    // public static readonly ContentType ImageSvg = null;
}

