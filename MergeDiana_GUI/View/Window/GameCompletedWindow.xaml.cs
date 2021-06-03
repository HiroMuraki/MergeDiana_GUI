using MergeDiana;
using System.Windows;
using System.Windows.Input;

namespace MergeDiana_GUI.View {
    /// <summary>
    /// GameCompletedWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GameCompletedWindow : Window {
        private DianaStrawberryType _gameTarget;
        private double _gameUsingTime;
        private int _movedTimes;
        private int _skillActivedTimes;
        private int _totalScores;

        public DianaStrawberryType GameTarget {
            get {
                return _gameTarget;
            }
        }
        public double GameUsingTime {
            get {
                return _gameUsingTime;
            }
        }
        public int MovedTimes {
            get {
                return _movedTimes;
            }
        }
        public int SkillActivedTimes {
            get {
                return _skillActivedTimes;
            }
        }
        public int TotalScores {
            get {
                return _totalScores;
            }
        }

        public GameCompletedWindow(DianaStrawberryType gameTarget, double gameUsingTime, int skillActivedTimes, int movedTimes, int totalScore) {
            _gameTarget = gameTarget;
            _gameUsingTime = gameUsingTime;
            _skillActivedTimes = skillActivedTimes;
            _movedTimes = movedTimes;
            _totalScores = totalScore;
            InitializeComponent();
        }

        private void Window_Close(object sender, RoutedEventArgs e) {
            Close();
        }

        private void Window_Move(object sender, MouseButtonEventArgs e) {
            DragMove();
        }
    }
}
