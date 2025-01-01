using System.Diagnostics.CodeAnalysis;

namespace SCM.Common;

//  _   _  ___ _____ _____
// | \ | |/ _ \_   _| ____|
// |  \| | | | || | |  _|
// | |\  | |_| || | | |___
// |_| \_|\___/ |_| |_____|
// I use the pattern in loads of projects.
// While I don't really want a full nuget package
// So this is a one-file copy/paste implementation

// See rustlings:FileSystemZ:IFileSystem
public interface IVirtualFileSystemReadOnly
{
    string? GetParent(string path);
    bool Check(string path, [NotNullWhen(false)] out string? error);
    Task<IReadOnlyList<string>> GetDirectories(string path);
    Task<IReadOnlyList<string>> GetFiles(string path);
    Task<Stream> OpenRead(string file);
}

public static class VirtualFileSystemReadOnlyExt
{
    public static void AssertValid(this IVirtualFileSystemReadOnly fs, string path)
    {
        if (!fs.Check(path, out var error))
        {
            throw new InvalidDataException(error);
        }
    }
}

// here a path/resouce is mapped to TContentIdent{Title,MimeType, ... } for example
public interface IContentFileSystemReadOnly<TContentDescriptor> : IVirtualFileSystemReadOnly
{
    TContentDescriptor DescribeFile(string file);
    Task<IReadOnlyList<TContentDescriptor>> DescribeAllFiles(string path);
}

public interface IContentTypeComponent
{
    ContentType? GetContentTypeFromExt(string ext);
    ContentType? GetContentTypeFromMime(string mimeType);

}
public record ContentType
{
    public required string Name { get; init; }
    public required string MimeType { get; init; }
    public required string Ext { get; init; }

    public bool IsBinary { get; init; }
    public bool IsText { get; init; }
    public bool IsImage { get; init; }
    public bool IsVideo { get; init; }
    public bool IsAudio { get; init; }
}

public record ContentDescriptor
{
    public required string Uri         { get; init; }
    public required string Name        { get; init; }
    public required string? ParentUri  { get; init; }
    public required ContentType ContentType { get; init; }
    public string? Title               { get; init; }
}

public class VirtualFileSystemReadOnly : IVirtualFileSystemReadOnly
{
    public VirtualFileSystemReadOnly(string rootPath)
    {
        var dirInfo = new DirectoryInfo(rootPath);
        if (!dirInfo.Exists) throw new DirectoryNotFoundException(rootPath);
        RootPath = dirInfo.FullName;
    }

    public string RootPath { get;  }

    public string MakeRelative(string abs)
    {
        if (!abs.StartsWith(RootPath)) throw new InvalidDataException($"Must start with RootPath: {abs}");

        var rel = abs.Remove(0, RootPath.Length);
        this.AssertValid(rel);

        return rel;
    }

    public string MakeAbs(string rel)
    {
        if (rel.Length == 0 || rel == "." || rel == "./") return RootPath;
        return Path.Combine(RootPath, rel);
    }

    public string? GetParent(string path)
    {
        return Path.GetDirectoryName(path);
    }

    public bool Check(string path, [NotNullWhen(false)] out string? error)
    {
        // cannot have relative paths
        if (path.Contains(".."))
        {
            error = "Cannot use relative .. paths ";
            return false;
        }
        error = null;
        return true;
    }

    public Task<IReadOnlyList<string>> GetDirectories(string path) => Task.Run<IReadOnlyList<string>>(
                () => Directory.GetDirectories(MakeAbs(path)).Select(MakeRelative).ToArray());

    public Task<IReadOnlyList<string>> GetFiles(string path) => Task.Run<IReadOnlyList<string>>(
                () => Directory.GetFiles(MakeAbs(path)).Select(MakeRelative).ToArray());

    public Task<Stream> OpenRead(string file) => Task.Run<Stream>(
                () => File.OpenRead(MakeAbs(file)));
}

public class ContentFileSystemReadOnly : VirtualFileSystemReadOnly, IContentFileSystemReadOnly<ContentDescriptor>
{
    readonly IContentTypeComponent compContentType;

    public ContentFileSystemReadOnly(IContentTypeComponent compContentType, string root) : base(root)
    {
        this.compContentType = compContentType;
    }

    public ContentDescriptor DescribeFile(string file)
    {
        return new ContentDescriptor
        {
            Uri = file,
            Name  = Path.GetFileName(file),
            ParentUri = Path.GetDirectoryName(file),
            Title = Path.GetFileNameWithoutExtension(file),
            ContentType = compContentType.GetContentTypeFromExt(Path.GetExtension(file)) ?? ContentTypes.Binary
        };
    }
    public async Task<IReadOnlyList<ContentDescriptor>> DescribeAllFiles(string path)
    {
        var files = await GetFiles(path);
        return files.Select(x=>DescribeFile(x)).ToArray();
    }

}
