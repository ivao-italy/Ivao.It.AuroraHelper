using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivao.It.AuroraHelper.EnavData.Extensions;

internal static class MemoryStreamExtensions
{
    /// <summary>
    /// Stores a Memory Stream in a File
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="destinationPath"></param>
    public static void SaveToFile(this MemoryStream stream, string destinationPath)
    {
        using (var fs = new FileStream(destinationPath, FileMode.OpenOrCreate, FileAccess.Write))
        {
            //Copio nel file stream
            stream.CopyTo(fs);

            //Salvo il FS
            fs.SaveToFile(destinationPath);
        }
    }
}

internal static class FileStreamExtension
{
    /// <summary>
    /// Stores a  Fileream in a file
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="destinationPath"></param>
    public static void SaveToFile(this FileStream stream, string destinationPath)
    {
        //Salvo il filestream nel file
        byte[] bytes = new byte[stream.Length];
        stream.Write(bytes, 0, bytes.Length);
        stream.Close();
    }
}
