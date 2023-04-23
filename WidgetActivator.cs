using System;
using Tasks;
using WidgetTransitions;

namespace WidgetControllers
{
    public class WidgetActivator<T> : IWidgetActivator<T>
    {
        public class Props
        {
            public Action<IWidgetExtension<T>> AddChild;
            public Action<Func<float, bool>> AddStep;
            public Action<IWidgetExtension<T>> DestroyWidget;
        }

        private Props _props;

        public Props Properties
        {
            set => _props = value;
        }

        public async ITask ActiveAsync(bool active, IWidgetExtension<T> extension, float time)
        {
            if (active)
            {
                _props.AddChild(extension);
            }

            if (extension is IWidgetFocusHandler focusHandler)
            {
                focusHandler.OnWidgetFocus(active);
            }

            extension.Transition.BeginStep(!active, time);
            await TaskUtils.Wait(extension.Transition.Step, _props.AddStep);
            if (!active)
            {
                _props.DestroyWidget(extension);
            }
        }
    }
}