using Ivao.It.AuroraHelper.EnavData.Extensions;
using Ivao.It.AuroraHelper.EnavData.Models;

namespace Ivao.It.AuroraHelper.EnavData.Encoding;

public class AuroraFixEncoder : IEncodingStrategy
{
    public static readonly AuroraFixEncoder Instance = new();

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

        return $"{fix.Id};{fix.Lat.EnavCoordsToAurora()};{fix.Lon.EnavCoordsToAurora()};{fixType};{boundary};";
    }
}