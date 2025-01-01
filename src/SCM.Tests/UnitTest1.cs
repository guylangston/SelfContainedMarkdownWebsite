using SCM.Common;

namespace SCM.Tests;

public class UnitTest1
{
    [Fact]
    public  async Task CanListFiles()
    {
        // foreach(var t in Directory.GetFiles("."))
        // {
        //     Console.WriteLine(t);
        // }


        IVirtualFileSystemReadOnly vfs = new SCM.Common.VirtualFileSystemReadOnly("/home/guy/repo/SelfContainedMarkdownWebsite");
        foreach(var f in await vfs.GetDirectories("."))
        {
            Console.WriteLine($"Dir: {f}");
        }

        foreach(var f in await vfs.GetFiles("src/SCM.Common"))
        {
            Console.WriteLine(f);
        }

    }
}
