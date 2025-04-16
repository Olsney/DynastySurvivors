using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace Code.Data
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel;
        [FormerlySerializedAs("VisitedTriggerIDs")] [FormerlySerializedAs("TriggerIDs")] public List<int> VisitedTriggerIds;

        public WorldData(string initialLevel)
        {
            PositionOnLevel = new PositionOnLevel(initialLevel);
            VisitedTriggerIds = new List<int>();
        }
    }
}