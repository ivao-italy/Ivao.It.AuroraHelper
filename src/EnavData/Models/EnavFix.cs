using Ivao.It.AuroraHelper.EnavData.Exceptions;
using System.Text.RegularExpressions;

namespace Ivao.It.AuroraHelper.EnavData.Models;

public class EnavFix : IEnavModel
{
    public string Id { get; private set; }
    public string Lat { get; private set; }
    public string Lon { get; private set; }
    public bool IsTerminal { get; private set; }
    public bool IsEnroute { get; private set; }
    public bool IsFraBoundary { get; private set; }
    public bool IsFra { get; private set; }

    public EnavFix(string fixRow)
    {
        Id = TryMatch(fixRow, Consts.FixNameRegex, "FixName");
        Lat = TryMatch(fixRow, Consts.FixLatRegex, "Lat");
        Lon = TryMatch(fixRow, Consts.FixLonRegex, "Lon");

        IsTerminal = Consts.FixTerminalRegex.IsMatch(fixRow);
        IsEnroute = Consts.FixAwyRegex.IsMatch(fixRow) || Consts.FixFraAwyRegex.IsMatch(fixRow);
        IsFraBoundary = Consts.FixFraBoundaryRegex.IsMatch(fixRow);
        IsFra = Consts.FixFraRegex.IsMatch(fixRow);
    }

    private string TryMatch(string fixRow, Regex pattern, string name)
    {
        var match = pattern.Match(fixRow);
        if (!match.Success) throw EnavDataException.RegexMissMatch(name);
        return match.Value;
    }

    public override string ToString()
        => $"{Id} {Lat} {Lon} | Terminal: {IsTerminal} | Enroute: {IsEnroute} | Boundary: {IsFraBoundary}";
}
