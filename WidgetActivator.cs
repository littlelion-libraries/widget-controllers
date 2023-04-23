using System;
using System.Collections.Generic;
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

        private readonly Queue<IWidgetExtension<T>> _extensions = new();
        private Props _props;

        public Props Properties
        {
            set => _props = value;
        }

        public async ITask ActiveAsync(bool active, IWidgetExtension<T> extension, float time)
        {
            Focus(!active);
            if (active)
            {
                _props.AddChild(extension);
                _extensions.Enqueue(extension);
            }
            Focus(extension, active);

            extension.Transition.BeginStep(!active, time);
            await TaskUtils.Wait(extension.Transition.Step, _props.AddStep);
            if (!active)
            {
                _props.DestroyWidget(extension);
            }
        }

        public void Focus(bool focus)
        {
            if (_extensions.TryPeek(out var value))
            {
                Focus(value, focus);
            }
        }

        private static void Focus(object extension, bool focus)
        {
            if (extension is IWidgetFocusHandler focusHandler)
            {
                focusHandler.OnWidgetFocus(focus);
            }
        }
    }
}