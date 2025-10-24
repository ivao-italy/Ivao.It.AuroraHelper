using Ivao.It.AuroraHelper.EnavData.Encoding;
using Ivao.It.AuroraHelper.EnavData.Models;
using System.Text;

namespace Ivao.It.AuroraHelper.EnavData;

public class EnrouteDoc441 : IEnavDocHandler
{
    private string _filePath = null!;
    private readonly List<string> _rows;
    private string _outputContent;

    public string? RawContents { get; private set; }
    public List<EnavFix> Fixes { get; private set; }
    public bool IsRead { get; private set; }


    public EnrouteDoc441()
    {
        _rows = new();
        Fixes = new();
    }

    public IEnavDocHandler Read(string filePath)
    {
        ArgumentNullException.ThrowIfNull(filePath);
        _filePath = filePath;
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

    public string GetContents(IEncodingStrategy encodingStrategy)
    {
        var sb = new StringBuilder();
        foreach (var fix in this.Fixes)
        {
            var encodedLine = encodingStrategy.Encode(fix);
            if(encodedLine is not null) sb.AppendLine(encodedLine);
        }
        _outputContent = sb.ToString();
        return _outputContent;
    }


    public async Task WriteToFileAsync(string filePath, CancellationToken cancellationToken = default) 
        => await File.WriteAllTextAsync(filePath, _outputContent, cancellationToken);
}
