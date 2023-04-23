using Widgets;
using WidgetTransitions;

namespace WidgetControllers
{
    public interface IWidgetExtension
    {
        IWidget Widget { get; }
        ITransition Transition { get; }
    }
}