using Character;

namespace FMS
{
    public interface IState
    {
        void Enter(Bot bot);
        void Execute(Bot bot);
    }
}