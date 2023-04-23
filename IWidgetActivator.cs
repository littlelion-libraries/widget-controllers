using Tasks;

namespace WidgetControllers
{
    public interface IWidgetActivator
    {
        ITask ActiveAsync(bool active, IWidgetExtension extension, float time);
    }
}