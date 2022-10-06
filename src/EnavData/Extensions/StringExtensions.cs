namespace Ivao.It.AuroraHelper.EnavData.Extensions;

internal static class StringExtension
{
    public static string AddSuffixToFilename(this string filename, string suffix)
    {
        string dir = Path.GetDirectoryName(filename) ?? "";
        string name = Path.GetFileNameWithoutExtension(filename);
        string ext = Path.GetExtension(filename);

        return Path.Combine(dir, string.Concat(name, suffix, ext));
    }

    public static string EnavCoordsToAurora(this string coords)
    {
        var coordPoint = Consts.CoordsCardPoint.Match(coords).Value;
        var degrees = Consts.CoordsDeegres.Match(coords).Value.PadLeft(3, '0');
        var minutes = Consts.CoordsMinutes.Match(coords).Value;
        var seconds = Consts.CoordsSeconds.Match(coords).Value;

        return $"{coordPoint}{degrees}.{minutes}.{seconds}.000";
    }
}
