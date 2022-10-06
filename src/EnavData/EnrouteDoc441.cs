using Ivao.It.AuroraHelper.EnavData.Encoding;
using Ivao.It.AuroraHelper.EnavData.Models;
using System.Text;

namespace Ivao.It.AuroraHelper.EnavData;

public class EnrouteDoc441 : IEnavDocHandler
{
    private readonly string _filePath;
    private readonly List<string> _rows;

    public string? RawContents { get; private set; }
    public List<EnavFix> Fixes { get; private set; }
    public bool IsRead { get; private set; }

    public EnrouteDoc441(string filePath)
    {
        _filePath = filePath;
        _rows = new();
        Fixes = new();
    }

    public IEnavDocHandler Read()
    {
        ArgumentNullException.ThrowIfNull(_filePath);
        var contents = Pdf.ReadText(_filePath);

        //PDF Cleanup & mapping
        var sb = new StringBuilder();

        foreach (var row in contents.Split("\n"))
        {
            if (Consts.FixFileRowCleanupRegex.IsMatch(row))
            {
                sb.AppendLine(row);
                _rows.Add(row);
                Fixes.Add(new EnavFix(row));
            }
        }
        this.RawContents = sb.ToString();
        this.IsRead = true;

        return this;
    }

   
    public Task WriteAsync()
    {
        throw new NotImplementedException();
    }

    public string GetContents(IEncodingStrategy encodingStrategy)
    {
        var sb = new StringBuilder();
        foreach (var fix in this.Fixes)
        {
            sb.AppendLine(encodingStrategy.Encode(fix));
        }
        return sb.ToString();
    }
}
