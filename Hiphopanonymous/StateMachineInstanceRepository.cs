using Automatonymous;

namespace Hiphopanonymous
{
    //implement this in your app
    public interface StateMachineInstanceRepository
    {
        StateMachineInstance Find(int id);
    }
}