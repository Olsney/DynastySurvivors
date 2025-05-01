using Code.Infrastructure.Services;

namespace Code.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadEnemies();
        EnemyStaticData ForEnemy(EnemyTypeId typeId);
    }
}