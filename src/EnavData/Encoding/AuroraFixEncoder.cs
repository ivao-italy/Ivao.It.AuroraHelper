﻿using Ivao.It.AuroraHelper.EnavData.Extensions;
using Ivao.It.AuroraHelper.EnavData.Models;

namespace Ivao.It.AuroraHelper.EnavData.Encoding;

public class AuroraFixEncoder : IEncodingStrategy
{
    private readonly FixType _type;

    public enum FixType
    {
        All,
        Enr,
        Term,
        Boundary
    }

    public AuroraFixEncoder(FixType type = FixType.All)
    {
        _type = type;
    }

    public string? Encode<T>(T item) where T : IEnavModel
    {
        var fix = item as EnavFix;
        if (fix is null) throw new InvalidOperationException($"Only type {nameof(EnavFix)} allowed for {nameof(AuroraFixEncoder)}.");

        var fixType = 0; //Enroute
        if (fix.IsTerminal) fixType = 1;
        if (fix.IsTerminal && fix.IsEnroute) fixType = 2;

        var boundary = fix.IsFraBoundary ? "1" : "0";


        switch (_type)
        {
            case FixType.All:
            case FixType.Enr when fix.IsEnroute:
            case FixType.Term when fix.IsTerminal:
            case FixType.Boundary when fix.IsFraBoundary:
                return $"{fix.Id};{fix.Lat.EnavCoordsToAurora()};{fix.Lon.EnavCoordsToAurora()};{fixType};{boundary};";
            default:
                return null;
        }
    }
}