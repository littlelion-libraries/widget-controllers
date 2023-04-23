using System;
using Tasks;
using WidgetTransitions;

namespace WidgetControllers
{
    public class WidgetActivator<T> : IWidgetActivator<T>
    {
        public class Props
        {
            public Action<T> AddChild;
            public Action<Func<float, bool>> AddStep;
            public Action<T> DestroyWidget;
        }

        private Props _props;

        public Props Properties
        {
            set => _props = value;
        }

        public async ITask ActiveAsync(
            bool active,
            object extension,
            float time,
            ITransition transition,
            T widget
        )
        {
            if (active)
            {
                _props.AddChild(widget);
            }

            if (extension is IWidgetFocusHandler focusHandler)
            {
                focusHandler.OnWidgetFocus(active);
            }

            transition.BeginStep(!active, time);
            await TaskUtils.Wait(transition.Step, _props.AddStep);
            if (!active)
            {
                _props.DestroyWidget(widget);
            }
        }

        public ITask ActiveAsync(bool active, IWidgetExtension<T> extension, float time)
        {
            return ActiveAsync(active, extension, time, extension.Transition, extension.Widget);
        }
    }
}