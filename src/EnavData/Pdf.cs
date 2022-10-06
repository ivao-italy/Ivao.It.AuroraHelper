using PdfSharpCore.Pdf.IO;
using PdfSharpCore.Pdf;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using System.Text;

namespace Ivao.It.AuroraHelper.EnavData;

internal static class Pdf
{
    /// <summary>
    /// Unlocks a PDF by copying its content in a new in memory PDF
    /// </summary>
    /// <param name="filePath">Path to the PDF to unlock</param>
    /// <returns>The memorystream of the unlocked PDF</returns>
    public static MemoryStream Unlock(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"File in path \"{filePath}\" not found.");
        }

        //Importo il documento
        PdfDocument document;
        var unlockedDoc = new PdfDocument();

        using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            document = PdfReader.Open(fs, PdfDocumentOpenMode.Import);
        }

        //Copio ogni pagina del doc in quello sbloccato
        foreach (PdfPage page in document.Pages)
        {
            unlockedDoc.AddPage(page);
        }

        //Salvo il doc in uno stream
        var outputStream = new MemoryStream();
        unlockedDoc.Save(outputStream, false);

        document.Dispose();
        unlockedDoc.Dispose();

        return outputStream;
    }

    /// <summary>
    /// Reads the text content of a PDF File
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static string ReadText(string filePath)
    {
        var pdfDocument = new iText.Kernel.Pdf.PdfDocument(new iText.Kernel.Pdf.PdfReader(filePath));
        var strategy = new SimpleTextExtractionStrategy();
        var processed = new StringBuilder();
        for (int i = 1; i <= pdfDocument.GetNumberOfPages(); ++i)
        {
            var page = pdfDocument.GetPage(i);
            string text = PdfTextExtractor.GetTextFromPage(page);
            processed.Append(text);
        }
        pdfDocument.Close();

        return processed.ToString();
    }
}
