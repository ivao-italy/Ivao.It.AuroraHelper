using System.Threading;
using System.Threading.Tasks;

using Caliburn.Micro;

namespace Ivao.It.AuroraHelper.Application.ViewModels;

public class ShellViewModel : Conductor<object>, IViewModel
{
    public bool IsLoaded { get; set; }

    public Task Loaded() => Task.CompletedTask;

    protected override async void OnViewLoaded(object view)
    {
        base.OnViewLoaded(view);
        await ShowEnavFix();
    }


    public async Task ShowEnavFix()
    {
        var viewmodel = IoC.Get<EnavFixViewModel>();
        await ActivateItemAsync(viewmodel, new CancellationToken());
    }
}
