using Code.Infrastructure.Services;
using Code.StaticData;
using Code.StaticData.Enemy;
using Code.StaticData.Hero;
using Code.StaticData.Level;
using Code.StaticData.Windows;
using Code.UI.Services.Windows;

namespace Code.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadAllEnemies();
        EnemyStaticData GetEnemy(EnemyTypeId typeId);
        void LoadAllHeroes();
        HeroStaticData GetHero(HeroTypeId typeId);
        void LoadAllLevels();
        LevelStaticData GetLevel(string sceneKey);
        WindowConfig GetWindow(WindowId shop);
        void LoadAllWindows();
    }
}