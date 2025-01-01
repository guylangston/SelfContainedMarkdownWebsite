namespace SCM.Common;

public class PageContentModel
{
    public required string Title { get; set; }
    public required ResourceFull Resource { get; set; }

    public required ResourceDirectory Directory { get; set; } // Must always have a directory
    public ResourceFile? File { get; set; } // may not always exist (uri is directory and no index)

    public ResourceModel? Parent { get; set; }
    public required List<ResourceModel> TopLevel { get; set; }
    public required List<ResourceModel> Links { get; set; } // includes self

    // TODO: Could be stream based
    public string? Markdown { get; set; }
    public string? Html { get; set; }
}
