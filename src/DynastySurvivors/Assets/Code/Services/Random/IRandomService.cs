using Code.Infrastructure.Services;

namespace Code.Services.Random
{
    public interface IRandomService : IService
    {
        int Next(int min, int max);
        float Next(float min, float max);
    }
}