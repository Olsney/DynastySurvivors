using Code.Infrastructure.Services;
using Code.Services.StaticData.Hero;
using Code.StaticData;

namespace Code.Services.StaticData.Enemy
{
    public interface IStaticDataService : IService
    {
        void LoadAllEnemies();
        EnemyStaticData GetEnemy(EnemyTypeId typeId);
        void LoadAllHeroes();
        HeroStaticData GetHero(HeroTypeId typeId);
        void LoadAllLevels();
        LevelStaticData GetLevel(string sceneKey);
    }
}