using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ivao.It.AuroraHelper.Application.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class ShellView : MetroWindow
{
    public ShellView()
    {
        InitializeComponent();
    }



    public void LaunchIvaoIt(object sender, RoutedEventArgs e)
        => OpenBrowser("https://www.ivao.it");

    public void LaunchIvaoAuroraWiki(object sender, RoutedEventArgs e)
        => OpenBrowser("https://wiki.ivao.aero/en/home/devops/manuals/SectorFile_Definition");

    public void LaunchGitHub(object sender, RoutedEventArgs e)
        => OpenBrowser("https://github.com/ivao-italy/Ivao.It.AuroraHelper");


    public static void OpenBrowser(string url)
    {
        try
        {
            Process.Start(url);
        }
        catch
        {
            // hack because of this: https://github.com/dotnet/corefx/issues/10361
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                url = url.Replace("&", "^&");
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", url);
            }
            else
            {
                throw;
            }
        }
    }
}
