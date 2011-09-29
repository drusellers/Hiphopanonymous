using Automatonymous;

namespace Hiphopanonymous
{
    public interface StateMachineInstanceRepository
    {
        StateMachineInstance Find(int id);
    }
}