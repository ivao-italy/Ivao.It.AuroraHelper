using Ivao.It.AuroraHelper.EnavData;
using Ivao.It.AuroraHelper.EnavData.Encoding;

namespace Ivao.It.AuroraHelper.Enav.Test;

public class EnavTests
{
    [Fact]
    public void Extract_441_Read()
    {
        var doc = new EnrouteDoc441(@"C:\Users\E.Innocenti\Downloads\ENR4-4-1.pdf")
            .Read();
    }

    [Fact]
    public void Extract_441_Read_ConvertToPlainText()
    {
        var doc = new EnrouteDoc441(@"C:\Users\E.Innocenti\Downloads\ENR4-4-1.pdf")
            .Read()
            .GetContents(new PlainTextEncoder());
        Assert.NotNull(doc);
    }

    [Fact]
    public void Extract_441_Read_ConvertToAurora()
    {
        var doc = new EnrouteDoc441(@"C:\Users\E.Innocenti\Downloads\ENR4-4-1.pdf")
            .Read()
            .GetContents(new AuroraFixEncoder());
        Assert.NotNull(doc);
    }
}