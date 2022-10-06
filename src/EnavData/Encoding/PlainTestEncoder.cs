using Ivao.It.AuroraHelper.EnavData.Models;

namespace Ivao.It.AuroraHelper.EnavData.Encoding;
public class PlainTextEncoder : IEncodingStrategy
{
    public string? Encode<T>(T item) where T : IEnavModel
        => item?.ToString();
}
