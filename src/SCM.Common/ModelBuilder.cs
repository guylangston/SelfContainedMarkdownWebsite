namespace SCM.Common;

public class ModelBuilder
{

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

