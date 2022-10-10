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

    [Theory]
    [InlineData(AuroraFixEncoder.FixType.All, "ODINA", true)]
    [InlineData(AuroraFixEncoder.FixType.All, "ROKUD", true)]
    [InlineData(AuroraFixEncoder.FixType.All, "RONIV", true)]

    [InlineData(AuroraFixEncoder.FixType.Term, "ODINA", true)]
    [InlineData(AuroraFixEncoder.FixType.Term, "PEVAL", false)]
    [InlineData(AuroraFixEncoder.FixType.Term, "ROKUD", false)]
    [InlineData(AuroraFixEncoder.FixType.Term, "RONIV", true)]

    [InlineData(AuroraFixEncoder.FixType.Enr, "OSMAR", true)]
    [InlineData(AuroraFixEncoder.FixType.Enr, "PEVAL", false)]
    [InlineData(AuroraFixEncoder.FixType.Enr, "ROKUD", true)]
    [InlineData(AuroraFixEncoder.FixType.Enr, "RONIV", false)]

    [InlineData(AuroraFixEncoder.FixType.Boundary, "PEVAL", true)]
    [InlineData(AuroraFixEncoder.FixType.Boundary, "OSMAR", true)]
    [InlineData(AuroraFixEncoder.FixType.Boundary, "ODINA", true)]
    [InlineData(AuroraFixEncoder.FixType.Boundary, "ROKUD", false)]
    [InlineData(AuroraFixEncoder.FixType.Boundary, "RONIV", false)]
    public void Extract_441_Read_ConvertToAurora(AuroraFixEncoder.FixType type, string fixToFind, bool result)
    {
        var doc = new EnrouteDoc441(@"C:\Users\E.Innocenti\Downloads\ENR4-4-1.pdf");
        var contents = doc.Read().GetContents(new AuroraFixEncoder(type));

        Assert.NotNull(doc);
        Assert.Equal(result, contents.Contains(fixToFind));
    }
}