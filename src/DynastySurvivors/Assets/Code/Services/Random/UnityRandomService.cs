namespace Code.Services.Random
{
    public class UnityRandomService : IRandomService
    {
        public int Next(int min, int max) =>
            UnityEngine.Random.Range(min, max);
    }
}