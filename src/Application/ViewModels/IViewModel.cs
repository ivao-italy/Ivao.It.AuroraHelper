using System.Threading.Tasks;

namespace Ivao.It.AuroraHelper.Application.ViewModels;

public interface IViewModel
{
    public bool IsLoaded { get; set; }
    public Task Loaded();
}
