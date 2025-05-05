using Code.Infrastructure.Services;

namespace Code.UI.Services.Windows
{
    public interface IWindowService : IService
    {
        void Open(WindowId windowId);
    }
}