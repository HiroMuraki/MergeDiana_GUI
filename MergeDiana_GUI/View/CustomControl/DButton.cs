using MergeDiana;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MergeDiana_GUI.View {
    public class DButton : Button {
        public static readonly DependencyProperty IsHoveredProperty =
            DependencyProperty.Register(nameof(IsHovered), typeof(bool), typeof(DButton), new PropertyMetadata(false));
        public static readonly DependencyProperty DirectionProperty =
            DependencyProperty.Register(nameof(Direction), typeof(Direction), typeof(DButton), new PropertyMetadata(Direction.None));

        public event EventHandler<DClickEventArgs> DClick;
        public bool IsHovered {
            get {
                return (bool)GetValue(IsHoveredProperty);
            }
            set {
                SetValue(IsHoveredProperty, value);
            }
        }
        public Direction Direction {
            get {
                return (Direction)GetValue(DirectionProperty);
            }
            set {
                SetValue(DirectionProperty, value);
            }
        }

        static DButton() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DButton), new FrameworkPropertyMetadata(typeof(DButton)));
        }

        protected override void OnMouseEnter(MouseEventArgs e) {
            base.OnMouseEnter(e);
            IsHovered = true;
        }
        protected override void OnMouseLeave(MouseEventArgs e) {
            base.OnMouseLeave(e);
            IsHovered = false;
        }
        protected override void OnClick() {
            base.OnClick();
            DClick?.Invoke(this, new DClickEventArgs(Direction));
        }
    }
}
