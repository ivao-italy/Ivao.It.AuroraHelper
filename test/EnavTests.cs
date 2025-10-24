using Ivao.It.AuroraHelper.EnavData;
using Ivao.It.AuroraHelper.EnavData.Encoding;

namespace Ivao.It.AuroraHelper.Enav.Test;

public class EnavTests
{
    [Fact]
    public void Extract_441_Read()
    {
        var doc = new EnrouteDoc441()
            .Read(@"C:\Users\E.Innocenti\Downloads\ENR4-4-1.pdf");
    }

    [Fact]
    public void Extract_441_Read_ConvertToPlainText()
    {
        var doc = new EnrouteDoc441()
            .Read(@"C:\Users\E.Innocenti\Downloads\ENR4-4-1.pdf")
            .GetContents(new PlainTextEncoder());
        Assert.NotNull(doc);
    }

    [Theory]
    [InlineData("ODINA", true)]
    [InlineData("ROKUD", true)]
    [InlineData("RONIV", true)]

    [InlineData( "ODINA", true)]
    [InlineData( "PEVAL", false)]
    [InlineData( "ROKUD", false)]
    [InlineData( "RONIV", true)]

    [InlineData("OSMAR", true)]
    [InlineData("PEVAL", false)]
    [InlineData("ROKUD", true)]
    [InlineData("RONIV", false)]

    [InlineData( "PEVAL", true)]
    [InlineData( "OSMAR", true)]
    [InlineData( "ODINA", false)]
    [InlineData( "ROKUD", false)]
    [InlineData( "RONIV", false)]

    [InlineData("ABAKO", true)]
    [InlineData("ABAKO", false)]
    [InlineData( "ABAKO", true)]
    public void Extract_441_Read_ConvertToAurora(string fixToFind, bool result)
    {
        var doc = new EnrouteDoc441();
        var encoder = AuroraFixEncoder.Instance;

        var contents = doc.Read(@"C:\Users\E.Innocenti\Downloads\ENR4-4-1.pdf").GetContents(encoder);

        Assert.NotNull(doc);
        Assert.Equal(result, contents.Contains(fixToFind));
    }
}