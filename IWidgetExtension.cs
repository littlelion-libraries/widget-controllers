using WidgetTransitions;

namespace WidgetControllers
{
    public interface IWidgetExtension
    {
        ITransition Transition { get; }
    }
}