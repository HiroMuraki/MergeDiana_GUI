using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MergeDiana;

namespace MergeDiana_GUI.View {
    public class StrawberryRound : Control {
        private Transform _transform;
        private DoubleAnimation _animation;

        public DianaStrawberry Strawberry {
            get {
                return (DianaStrawberry)GetValue(StrawberryProperty);
            }
            set {
                SetValue(StrawberryProperty, value);
            }
        }
        public static readonly DependencyProperty StrawberryProperty =
            DependencyProperty.Register(nameof(Strawberry), typeof(DianaStrawberry), typeof(StrawberryRound), new PropertyMetadata(null));

        static StrawberryRound() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StrawberryRound), new FrameworkPropertyMetadata(typeof(StrawberryRound)));
        }
        private StrawberryRound() {
            _transform = new ScaleTransform() {
                CenterX = 50,
                CenterY = 50
            };
            _animation = new DoubleAnimation() {
                From = 0.5,
                To = 1,
                AccelerationRatio = 0.2,
                DecelerationRatio = 0.8,
                EasingFunction = new SineEase(),
                Duration = TimeSpan.FromMilliseconds(200)
            };
            RenderTransform = _transform;
        }
        public StrawberryRound(DianaStrawberry strawberry) : this() {
            Strawberry = strawberry;
            Strawberry.TypeChanged += Strawberry_StrawberryTypeChanged;
        }

        private void Strawberry_StrawberryTypeChanged(object sender, DianaStrawberryType e) {
            if (e == DianaStrawberryType.None) {
                return;
            }
            _transform.BeginAnimation(ScaleTransform.ScaleXProperty, _animation);
            _transform.BeginAnimation(ScaleTransform.ScaleYProperty, _animation);
        }
    }
}
