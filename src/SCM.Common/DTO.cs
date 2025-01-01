namespace SCM.Common;

/* Defn: Resource
 *       A simple type useful for User Intergaces. Ie a file/dir path and a display title.
 *       May be enriched for related files and folders
 *       May also include file type (mimetype metadata)
 */
public abstract class ResourceModel
{
    protected ResourceModel(string fullPath, string? title)
    {
        FullPath = fullPath;
        Title = title;
    }

    public string FullPath   { get;  }
    public string? Title { get; }
    public virtual string Name => Path.GetFileName(FullPath);

    public virtual string? GetParent()
    {
        if (!FullPath.Contains('/')) return null;
        return Path.GetDirectoryName(FullPath);
    }
}

public sealed class ResourceTitled : ResourceModel
{
    public ResourceTitled(string fullPath, string? title) : base(fullPath, title)
    {
    }
}

public abstract class ResourceFull : ResourceModel
{
    protected ResourceFull(string fullPath, string? title) : base(fullPath, title)
    {
    }
}

public class ResourceDirectory: ResourceFull
{
    public ResourceDirectory(string fullPath, string? title) : base(fullPath, title)
    {
    }

    public required IReadOnlyList<ResourceTitled> Children { get; init; }
    public required IReadOnlyList<ResourceTitled> Files    { get; init; }
}

// TODO: FileInfo does not allow virtual items; but we need metadata like Created,Modified,Owner,Size
public class ResourceFile : ResourceFull
{
    public ResourceFile(string fullPath, string? title) : base(fullPath, title)
    {
    }

    public string Ext => Path.GetExtension(Name);
    public string NameWithoutExt => Path.GetFileNameWithoutExtension(Name);
    public required ContentType? MimeType       { get; init; }

}

