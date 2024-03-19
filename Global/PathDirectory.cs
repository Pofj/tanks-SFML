using System;


namespace game.Global
{
    static class PathDirectory
    {
        static public string Path;
        static PathDirectory()
        {
            Path = Directory.GetCurrentDirectory();
            Path = Directory.GetParent(Path)?.Parent?.FullName;
            Path = Path = Directory.GetParent(Path)?.FullName;
        }
    }
}
