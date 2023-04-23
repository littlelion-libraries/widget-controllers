using WidgetTransitions;

namespace WidgetControllers
{
    public interface IWidgetExtension<out T>
    {
        T Widget { get; }
        ITransition Transition { get; }
    }
}