using Ivao.It.AuroraHelper.EnavData.Extensions;
using Ivao.It.AuroraHelper.EnavData.Models;

namespace Ivao.It.AuroraHelper.EnavData.Encoding;

public class AuroraFixEncoder : IEncodingStrategy
{
    public string? Encode<T>(T item) where T : IEnavModel
    {
        var fix = item as EnavFix;
        if (fix is null) throw new InvalidOperationException($"Only type {nameof(EnavFix)} allowed for {nameof(AuroraFixEncoder)}.");

        var fixType = 0; //Enroute
        if (fix.IsTerminal) fixType = 1;
        if (fix.IsTerminal && fix.IsEnroute) fixType = 2;

        var boundary = fix.IsFraBoundary ? "1" : "0";

        return $"{fix.Id};{fix.Lat.EnavCoordsToAurora()};{fix.Lon.EnavCoordsToAurora()};{fixType};{boundary};";
    }
}