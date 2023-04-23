using WidgetTransitions;

namespace WidgetControllers
{
    public interface IWidgetExtension<T>
    {
        T Widget { get; }
        ITransition Transition { get; }
    }
}