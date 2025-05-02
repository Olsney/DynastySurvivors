using System.Collections.Generic;
using System.Linq;
using Code.Services.StaticData.Hero;
using UnityEngine;

namespace Code.Services.StaticData.Enemy
{
    public class StaticDataService : IStaticDataService
    {
        private const string EnemiesStaticDataPath = "StaticData/Enemies";
        private const string HeroesStaticDataPath = "StaticData/Heroes";
        
        private Dictionary<HeroTypeId, HeroStaticData> _heroes;
        
        private Dictionary<EnemyTypeId, EnemyStaticData> _enemies;

        public void LoadAllEnemies()
        {
            _enemies = Resources
                .LoadAll<EnemyStaticData>(EnemiesStaticDataPath)
                .ToDictionary(x => x.EnemyType, x => x);
        }
        
        public void LoadAllHeroes()
        {
            _heroes = Resources
                .LoadAll<HeroStaticData>(HeroesStaticDataPath)
                .ToDictionary(x => x.HeroType, x => x);
        }

        public EnemyStaticData GetEnemy(EnemyTypeId typeId) => 
            _enemies.TryGetValue(typeId, out EnemyStaticData staticData) 
                ? staticData 
                : throw new KeyNotFoundException($"Enemy with type: {typeId} is not found");
        
        public HeroStaticData GetHero(HeroTypeId typeId) => 
            _heroes.TryGetValue(typeId, out HeroStaticData staticData) 
                ? staticData 
                : throw new KeyNotFoundException($"Hero with type: {typeId} is not found");
    }
}