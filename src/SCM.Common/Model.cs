namespace SCM.Common;

public class Model
{
    public required string Title { get; set; }
    public required ResourceFull Resource { get; set; }

    public required ResourceDirectory Directory { get; set; } // Must always have a directory
    public ResourceFile? File { get; set; } // may not always exist (uri is directory and no index)

    public Resource? Parent { get; set; }
    public required IReadOnlyList<Resource> TopLevel { get; set; }
    public required IReadOnlyList<Resource> Links { get; set; } // includes self

    // TODO: Could be stream based
    public string? Markdown { get; set; }
    public string? Html { get; set; }
}

public class UrlMapper
{
    public required string RootDir { get; init; }

    public string GetRelative(string path) => Path.Combine(RootDir, path);

    public string? GetParent(string uri)
    {
        return null;
    }
}

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

public class ModelBuilder
{
    UrlMapper mapper = new UrlMapper
    {
        RootDir = "/home/guy/repo/SelfContainedMarkdownWebsite/data/www"
    };
    MimeTypeComponent mime = new();

    public async Task<Model> Load(string uri)
    {
        var path = mapper.GetRelative(uri);
        if (File.Exists(path))
        {
            var name = Path.GetFileNameWithoutExtension(path),
            var resource = new ResourceFile
            {
                ParentUri = mapper.GetParent(uri),
                Uri = uri,
                Name = Path.GetFileName(path),
                Title = name,

                Ext = Path.GetExtension(path),
                MimeType = mime.GetMimeTypeFromPath(path) ?? mime.MimeTypeBinary,
                NameWithoutExt = name,
                Type = FileResourceType.Binary,
            };
            var model = new Model()
            {
                Title = resource.Title,
                Resource = resource,
                Parent = null,
            };
        }
        else if (Directory.Exists(path))
        {
            throw new NotImplementedException();
        }
        else
        {
            throw new FileNotFoundException(uri);
        }
    }
}
