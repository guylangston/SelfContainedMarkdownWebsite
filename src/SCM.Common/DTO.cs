namespace SCM.Common;

public abstract class ResourceBase
{
    public required string? ParentUri { get; init; }
    public required string Uri        { get; init; }
    public required string Name       { get; init; }
    public string? Title              { get; init; }
}

public enum FileOrDir { File, Directory, Unknown }

public sealed class Resource : ResourceBase
{
    public required FileOrDir Type { get; init; }
};

public abstract class ResourceFull : ResourceBase {  }

public class ResourceDirectory: ResourceFull
{
    public required IReadOnlyList<Resource> Children { get; init; }
    public required IReadOnlyList<Resource> Files    { get; init; }
}

public class ResourceFile : ResourceFull
{
    public required string Ext            { get; init; }
    public required string NameWithoutExt { get; init; }
    public required string MimeType       { get; init; }
    public required FileResourceType Type { get; init; }

    // TODO: FileInfo does not allow virtual items; but we need metadata like Created,Modified,Owner,Size
}

[Flags]
public enum FileResourceType
{
    Binary,
    Text,
    Image,
    Video
} 
