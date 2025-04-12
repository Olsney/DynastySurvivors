namespace Code.Infrastructure
{
    public interface IState
    {
        void Enter();
        void Exit();
    }
}