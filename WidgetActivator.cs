﻿using System;
using Tasks;
using Widgets;
using WidgetTransitions;

namespace WidgetControllers
{
    public class WidgetActivator : IWidgetActivator
    {
        public class Props
        {
            public Action<IWidget> AddChild;
            public Action<Func<float, bool>> AddStep;
            public Action<IWidget> DestroyWidget;
            public Action<bool> Interactable;
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
            IWidget widget
        )
        {
            _props.Interactable(false);
            if (active)
            {
                widget.Visible = true;
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
                widget.Visible = false;
                _props.DestroyWidget(widget);
            }

            _props.Interactable(true);
        }

        public ITask ActiveAsync(bool active, IWidgetExtension extension, float time)
        {
            return ActiveAsync(active, extension, time, extension.Transition, extension.Widget);
        }
    }
}