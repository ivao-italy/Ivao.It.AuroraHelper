using System;
using System.Threading.Tasks;

namespace Ivao.It.AuroraHelper.Application.ViewModels;

public class DummyViewModel : IViewModel
{
    public bool IsLoaded { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public Task Loaded()
    {
        throw new NotImplementedException();
    }
}
