using Tasks;
using Widgets;
using WidgetTransitions;

namespace WidgetControllers
{
    public interface IWidgetActivator
    {
        ITask ActiveAsync(
            bool active,
            object extension,
            float time,
            ITransition transition,
            IWidget widget
        );
    }
}