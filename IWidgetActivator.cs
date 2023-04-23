using Tasks;
using WidgetTransitions;

namespace WidgetControllers
{
    public interface IWidgetActivator<T>
    {
        ITask ActiveAsync(
            bool active,
            object extension,
            float time,
            ITransition transition,
            T widget
        );
        
        ITask ActiveAsync(bool active, IWidgetExtension<T> extension, float time);
    }
}