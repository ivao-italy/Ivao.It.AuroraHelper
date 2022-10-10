using System.Text.RegularExpressions;

namespace Ivao.It.AuroraHelper.EnavData;

internal class Consts
{
    // Regex Segments
    public const string FixName = @"([A-Z]){5}";
    public const string LatRgx = @"([3-4]\d°\d{2}'\d{2}''N)";
    public const string LonRgx = @"(0[0-5]\d°\d{2}'\d{2}''E)";
    public const string NilRgx = @"NIL";
    public const string AwyIdRgx = @"[K,U,S]{0,1}[A-Z]{1}[0-9]{1,3}";

    public const string FraEnroute = @"(FRA\(I\))";


    public static readonly Regex FixNameRegex = new(FixName, RegexOptions.Compiled);
    public static readonly Regex FixLatRegex = new(LatRgx, RegexOptions.Compiled);
    public static readonly Regex FixLonRegex = new(LonRgx, RegexOptions.Compiled);

    public static readonly Regex FixFileRowCleanupRegex = new ($@"^{FixName}(?= )(?! effective)", RegexOptions.Compiled);

    public static readonly Regex FixAwyRegex = new ($@"{FixName} {LatRgx} {LonRgx} {AwyIdRgx}", RegexOptions.Compiled);
    public static readonly Regex FixTerminalRegex = new ("([A-Z]){5} ([3-4]\\d°\\d{2}'\\d{2}''N) (0[0-5]\\d°\\d{2}'\\d{2}''E) ([K,U,S]{0,1}[A-Z]{1}[0-9]{1,3}|NIL) (?!NIL)", RegexOptions.Compiled); //
    public static readonly Regex FixFraBoundaryRegex = new ("(FRA\\()([EXAD]{1,2})(\\)$)", RegexOptions.Compiled);
    public static readonly Regex FixFraRegex = new ("(FRA\\()([EXADI]{1,2})(\\)$)", RegexOptions.Compiled);
    public static readonly Regex FixFraAwyRegex = new ($@"{FixName} {LatRgx} {LonRgx} ({NilRgx}) ({NilRgx}) {FraEnroute}", RegexOptions.Compiled);

    public static readonly Regex CoordsCardPoint = new("[NSWE]{1}", RegexOptions.Compiled);
    public static readonly Regex CoordsDeegres = new("\\d{2,3}(?=°)", RegexOptions.Compiled);
    public static readonly Regex CoordsMinutes = new("\\d{2,3}(?=')", RegexOptions.Compiled);
    public static readonly Regex CoordsSeconds = new("\\d{2,3}(?='')", RegexOptions.Compiled);

}
