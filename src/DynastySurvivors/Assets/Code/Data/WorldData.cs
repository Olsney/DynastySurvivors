using System;

namespace Code.Data
{
    [Serializable]
    public class WorldData
    {
        
        public PositionOnLevel PositionOnLevel;

        public WorldData(string initialLevel)
        {
            PositionOnLevel = new PositionOnLevel(initialLevel);
        }
    }
}