using Ivao.It.AuroraHelper.EnavData.Extensions;
using Ivao.It.AuroraHelper.EnavData.Models;

namespace Ivao.It.AuroraHelper.EnavData.Encoding;

public class AuroraFixToStringEncoder : IEncodingStrategy
{
    public static readonly AuroraFixToStringEncoder Instance = new();

    string? IEncodingStrategy.Encode<T>(T item)
    {
        var fix = item as EnavFix;
        if (fix is null) throw new InvalidOperationException($"Only type {nameof(EnavFix)} allowed for {nameof(AuroraFixToStringEncoder)}.");

        return fix.IsFraBoundary 
            ? $"{fix.Id};{fix.Lat.EnavCoordsToAurora()};{fix.Lon.EnavCoordsToAurora()};" 
            : string.Empty;
    }
}