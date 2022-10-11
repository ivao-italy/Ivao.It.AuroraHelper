using System.IO;
using System.Threading.Tasks;

using Caliburn.Micro;

using Ivao.It.AuroraHelper.EnavData;
using Ivao.It.AuroraHelper.EnavData.Encoding;

using Ookii.Dialogs.Wpf;

namespace Ivao.It.AuroraHelper.Application.ViewModels;
public class EnavFixViewModel : PropertyChangedBase, IViewModel
{
    private string _filePath;
    private bool _isLoaded;
    private readonly IEnavDocHandler _enavDoc;

    public bool IsLoaded
    {
        get => _isLoaded;
        set
        {
            _isLoaded = value;
            NotifyOfPropertyChange();
        }
    }

    public string FilePath
    {
        get => _filePath;
        set
        {
            _filePath = value;
            NotifyOfPropertyChange();
            NotifyOfPropertyChange(() => CanExportEnroute);
            NotifyOfPropertyChange(() => CanExportTerminal);
            NotifyOfPropertyChange(() => CanExportBoundary);
        }
    }

    public Task Loaded() => Task.CompletedTask;


    public EnavFixViewModel(IEnavDocHandler enavDoc)
    {
        _enavDoc = enavDoc;
    }

    public void BrowseForFile()
    {
        VistaOpenFileDialog dialog = new();
        dialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
        dialog.Multiselect = false;
        dialog.Title = "Select ENAV ENR 4.4.1 file";
        if (dialog.ShowDialog() ?? false)
        {
            if(File.Exists(dialog.FileName)) 
                this.FilePath = dialog.FileName;
        }
    }


    public bool CanExport => !string.IsNullOrWhiteSpace(FilePath);

    public bool CanExportEnroute => CanExport;
    public async Task ExportEnroute()
    {
        if (FilePath is null) return;
        if (!_enavDoc.IsRead) _enavDoc.Read(FilePath);

        var data = _enavDoc.GetContents(AuroraFixEncoder.Enroute);
        await this.SaveToFile("itfix_enr_fra");
    }

    public bool CanExportTerminal => CanExport;
    public async Task ExportTerminal()
    {
        if (FilePath is null) return;
        if (!_enavDoc.IsRead) _enavDoc.Read(FilePath);

        var data = _enavDoc.GetContents(AuroraFixEncoder.Terminal);
        await this.SaveToFile("itfix_term_apt");
    }

    public bool CanExportBoundary => CanExport;
    public async Task ExportBoundary()
    {
        if (FilePath is null) return;
        if (!_enavDoc.IsRead) _enavDoc.Read(FilePath);

        var data = _enavDoc.GetContents(AuroraFixEncoder.Boundary);
        await this.SaveToFile("itfix_fra_bdry");
    }


    private async Task SaveToFile(string fileName)
    {
        VistaSaveFileDialog dialog = new();
        dialog.Filter = "Aurora FIX file (*.fix)|*.fix|Text file (*.txt)|*.txt";
        dialog.DefaultExt = "fix";
        dialog.Title = "Save Aurora FIX file";
        dialog.FileName = fileName;

        if (dialog.ShowDialog() ?? false)
        {
            await _enavDoc.WriteToFileAsync(dialog.FileName);
        }
    }
}
