using System;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using DemoApp.Common.Common;
using Microsoft.Xaml.Interactivity;

namespace DemoApp.Windows.Common
{
    public class VisualStateBehavior : DependencyObject, IBehavior
    {
        public string VisualState
        {
            get { return (string)GetValue(VisualStateProperty); }
            set { SetValue(VisualStateProperty, value); }
        }

        public static readonly DependencyProperty VisualStateProperty =
            DependencyProperty.Register("VisualState", typeof(string), typeof(VisualStateBehavior), new PropertyMetadata(null, OnVisualStateChanged));

        private static void OnVisualStateChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var behavior = (VisualStateBehavior)dependencyObject;
            behavior.ApplyVisualState();
        }

        private bool _waitingToApplyState;

        private void ApplyVisualState()
        {
            if (!string.IsNullOrEmpty(VisualState))
            {
                //Associated object is null, lets wait until it's not null
                if (AssociatedObject == null)
                {
                    _waitingToApplyState = true;
                    return;
                }

                var page = VisualHelper.GetParentOfT<Control>(AssociatedObject);
                if (page == null)
                {
                    // Page may not be loaded yet
                    VisualHelper.ExecuteOnLoaded((FrameworkElement)AssociatedObject, r =>
                    {
                        page = VisualHelper.GetParentOfT<Control>(AssociatedObject);
                        if (page != null)
                        {
                            VisualStateManager.GoToState(page, VisualState, true);
                        }
                    });
                }
                else
                {
                    VisualStateManager.GoToState(page, VisualState, true);
                }
            }
        }

        public void Attach(DependencyObject associatedObject)
        {
            if ((associatedObject != AssociatedObject) && !DesignMode.DesignModeEnabled)
            {
                if (AssociatedObject != null)
                {
                    throw new InvalidOperationException("Cannot attach behavior multiple times.");
                }

                AssociatedObject = associatedObject;

                if (_waitingToApplyState)
                {
                    _waitingToApplyState = false;
                    ApplyVisualState();
                }
            }
        }

        public void Detach()
        {
        }

        public DependencyObject AssociatedObject { get; private set; }
    }


}