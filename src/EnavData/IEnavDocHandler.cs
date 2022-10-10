using Ivao.It.AuroraHelper.EnavData.Encoding;

namespace Ivao.It.AuroraHelper.EnavData;

public interface IEnavDocHandler
{
    bool IsRead { get; }

    IEnavDocHandler Read(string filePath);
    string GetContents(IEncodingStrategy encodingStrategy);
    Task WriteToFileAsync(string filePath, CancellationToken cancellationToken = default);

}
