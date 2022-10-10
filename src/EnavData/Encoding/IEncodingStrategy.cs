using Ivao.It.AuroraHelper.EnavData.Models;

namespace Ivao.It.AuroraHelper.EnavData.Encoding;


public interface IEncodingStrategy
{
    internal string? Encode<T>(T item) where T : IEnavModel;
}