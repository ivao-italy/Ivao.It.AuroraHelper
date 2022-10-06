using Ivao.It.AuroraHelper.EnavData.Encoding;
using Ivao.It.AuroraHelper.EnavData.Models;

namespace Ivao.It.AuroraHelper.EnavData;

public interface IEnavDocHandler
{
    IEnavDocHandler Read();
    string GetContents(IEncodingStrategy encodingStrategy);

    Task WriteAsync();

}
