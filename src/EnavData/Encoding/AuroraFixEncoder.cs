using System.Diagnostics;

using Ivao.It.AuroraHelper.EnavData.Extensions;
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

    private AuroraFixEncoder(FixType type = FixType.All)
    {
        _type = type;
    }
    public static readonly AuroraFixEncoder All = new AuroraFixEncoder(FixType.All);
    public static readonly AuroraFixEncoder Enroute = new AuroraFixEncoder(FixType.Enr);
    public static readonly AuroraFixEncoder Terminal = new AuroraFixEncoder(FixType.Term);
    public static readonly AuroraFixEncoder Boundary = new AuroraFixEncoder(FixType.Boundary);

    string? IEncodingStrategy.Encode<T>(T item)
    {
        var fix = item as EnavFix;
        if (fix is null) throw new InvalidOperationException($"Only type {nameof(EnavFix)} allowed for {nameof(AuroraFixEncoder)}.");

        /*
        1) se il fix compare solo nella colonna 3 va categorizzato come 0;0;
        2) se il fix compare solo nella colonna 4 va categorizzato come 1;0;
        3) se il fix compare in entrambe le colonne va categorizzato come 2;0;
        4) se il fix compare nella colonna 3 e 5 va categorizato come 0;1;
        5) se il fix compare nella colonna 4 e 5 va categorizzato come 1;1;
        6) se il fix compare nelle colonne 3, 4 e 5 va categorizzato come 2;1;
        7) se il fix compare nella solo colonna 5 (ce n'è qualcuno) lo possiamo categorizzare come 0;1; (come il punto 4)
         */

        int fixType = 0;
        int boundary = 0;
        //Caso 1: ridondante ma aggiunto per leggibilità
        if (fix is { IsEnroute: true, IsTerminal: false, IsFra: false })
        {
            fixType = 0;
            boundary = 0;
        }
        //Caso 2
        else if (fix is { IsEnroute: false, IsTerminal: true, IsFra: false })
        {
            fixType = 1;
            boundary = 0;
        }
        //Caso 3
        else if (fix is { IsEnroute: true, IsTerminal: true, IsFra: false })
        {
            fixType = 2;
            boundary = 0;
        }
        //Caso 4
        else if (fix is { IsEnroute: true, IsTerminal: false, IsFra: true })
        {
            fixType = 0;
            boundary = 1;
        }
        //Caso 5
        else if (fix is { IsEnroute: false, IsTerminal: true, IsFra: true })
        {
            fixType = 1;
            boundary = 1;
        }
        //Caso 6
        else if (fix is { IsEnroute: true, IsTerminal: true, IsFra: true })
        {
            fixType = 2;
            boundary = 1;
        }
        //Caso 7: ridondante ma aggiunto per leggibilità
        else if (fix is { IsEnroute: false, IsTerminal: false, IsFra: true })
        {
            fixType = 0;
            boundary = 1;
        }


        //var fixType = 0; //Enroute
        //if (fix.IsTerminal) fixType = 1; //Colonna 4
        //if (fix.IsTerminal && fix.IsEnroute) fixType = 2; // Colonna 5

        //var boundary = fix.IsFra && !fix.IsTerminal ? "1" : "0";


        switch (_type)
        {
            case FixType.All:
            case FixType.Enr when fix.IsEnroute:
            case FixType.Term when fix.IsTerminal:
            case FixType.Boundary when (fix.IsFra && !fix.IsTerminal):
                return $"{fix.Id};{fix.Lat.EnavCoordsToAurora()};{fix.Lon.EnavCoordsToAurora()};{fixType};{boundary};";
            default:
                return null;
        }
    }

}