using Code.Data;
using Code.Infrastructure.Services;

namespace Code.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}