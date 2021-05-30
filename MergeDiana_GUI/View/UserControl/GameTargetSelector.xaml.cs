using MergeDiana.GameLib;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MergeDiana_GUI.View {
    /// <summary>
    /// GameTargetSelector.xaml 的交互逻辑
    /// </summary>
    public partial class GameTargetSelector : UserControl {
        private int _currentIndex;

        public static readonly DependencyProperty GameTargetsProperty =
            DependencyProperty.Register(nameof(GameTargets), typeof(List<DianaStrawberryType>), typeof(GameTargetSelector), new PropertyMetadata(null));
        public static readonly DependencyProperty GameTargetProperty =
            DependencyProperty.Register(nameof(GameTarget), typeof(DianaStrawberryType), typeof(GameTargetSelector), new PropertyMetadata(DianaStrawberryType.K));


        public List<DianaStrawberryType> GameTargets {
            get {
                return (List<DianaStrawberryType>)GetValue(GameTargetsProperty);
            }
            set {
                SetValue(GameTargetsProperty, value);
            }
        }
        public DianaStrawberryType GameTarget {
            get {
                return (DianaStrawberryType)GetValue(GameTargetProperty);
            }
            set {
                SetValue(GameTargetProperty, value);
            }
        }

        public GameTargetSelector() {
            _currentIndex = 0;
            InitializeComponent();
        }

        private void MovePrevious_Click(object sender, RoutedEventArgs e) {
            if (_currentIndex <= 0) {
                return;
            }
            _currentIndex -= 1;
            GameTarget = GameTargets[_currentIndex];
        }

        private void MoveNext_Click(object sender, RoutedEventArgs e) {
            if (_currentIndex + 1 >= GameTargets.Count) {
                return;
            }
            _currentIndex += 1;
            GameTarget = GameTargets[_currentIndex];
        }
    }
}
