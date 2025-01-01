namespace SCM.Common;

public class ModelBuilder
{
    readonly IContentFileSystemReadOnly<ContentDescriptor> fs;
    readonly IContentTypeComponent compContentType;

    public ModelBuilder(IContentFileSystemReadOnly<ContentDescriptor> fs, IContentTypeComponent compContentType)
    {
        this.fs = fs;
        this.compContentType = compContentType;
    }

    public async Task<Model> Load(string path)
    {
        var resolve = fs.Resolve(path);
        if (!resolve.IsValid) throw new Exception("invalid path");
        if (!resolve.Exists)
        {
            throw new Exception($"Path must exist: {path}");
        }
        if (resolve.IsFile)
        {
            var ct = fs.DescribeFile(path);
            var ext = Path.GetExtension(path);
            var resFile = new ResourceFile
            {
                FullPath = path,
                Name = Path.GetFileName(path),
                Title = Path.GetFileNameWithoutExtension(path),
                ParentUri = Path.GetDirectoryName(path),
                MimeType = compContentType.GetContentTypeFromExt(ext)
            };
            var resDir = new ResourceDirectory
            {
            };
            
            var model = new Model
            {
                Title = ct.Title ?? ct.Name,
                Resource = resFile,
                Directory = resDir,
                File = resFile,
                Parent = resDir,

            };
            return model
        }
        else if (resolve.IsDirectory)
        {
            throw new NotImplementedException();
        }

        throw new Exception("invaid path");
    }


    // public async Task<Model> Load(string uri)
    // {
    //     var path = mapper.GetRelative(uri);
    //     if (File.Exists(path))
    //     {
    //         var name = Path.GetFileNameWithoutExtension(path);
    //         var resource = new ResourceFile
    //         {
    //             ParentUri = mapper.GetParent(uri),
    //             Uri = uri,
    //             Name = Path.GetFileName(path),
    //             Title = name,
    //
    //             Ext = Path.GetExtension(path),
    //             MimeType = mime.GetMimeTypeFromPath(path) ?? mime.MimeTypeBinary,
    //             NameWithoutExt = name,
    //             Type = FileResourceType.Binary,
    //         };
    //         var model = new Model()
    //         {
    //             Title = resource.Title,
    //             Resource = resource,
    //             Parent = null,
    //         };
    //         return model;
    //     }
    //     else if (Directory.Exists(path))
    //     {
    //         throw new NotImplementedException();
    //     }
    //     else
    //     {
    //         throw new FileNotFoundException(uri);
    //     }
    // }
}

