using Tasks;

namespace WidgetControllers
{
    public interface IWidgetActivator<in T>
    {
        ITask ActiveAsync(bool active, IWidgetExtension<T> extension, float time);
    }
}