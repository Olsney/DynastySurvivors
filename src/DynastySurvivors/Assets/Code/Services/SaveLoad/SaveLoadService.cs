using Code.Data;
using UnityEngine;

namespace Code.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";

        public void SaveProgress()
        {
            
        }

        public PlayerProgress LoadProgress()
        {
            return PlayerPrefs.GetString(ProgressKey)?
                .ToDeserialized<PlayerProgress>();
        }
    }
}