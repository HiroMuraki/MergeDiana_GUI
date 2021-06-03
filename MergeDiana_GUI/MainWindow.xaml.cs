using MergeDiana.GameLib;
using MergeDiana_GUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MergeDiana_GUI {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private static readonly Dictionary<Direction, SolidColorBrush> _directionColors = new Dictionary<Direction, SolidColorBrush>() {
            [Direction.None] = new SolidColorBrush(Colors.White),
            [Direction.Up] = new SolidColorBrush(Color.FromRgb(0x63, 0x61, 0x8b)),
            [Direction.Down] = new SolidColorBrush(Color.FromRgb(0xf2, 0xe3, 0xe1)),
            [Direction.Left] = new SolidColorBrush(Color.FromRgb(0xba, 0xbc, 0xed)),
            [Direction.Right] = new SolidColorBrush(Color.FromRgb(0x72, 0x6e, 0x8d))
        };

        private MergeDianaGame _game;
        private GameSetter _gameSetter;
        private Point _hitPos;
        private Line _directionLine;
        private DispatcherTimer _gameTimer;
        private long _gameUsingTime;
        private int _movedTimes;
        private int _skillActivedTimes;

        public GameSetter GameSetter {
            get {
                return _gameSetter;
            }
        }
        public MergeDianaGame Game {
            get {
                return _game;
            }
        }

        public MainWindow() {
            // 设置计时器
            _gameTimer = new DispatcherTimer() {
                Interval = TimeSpan.FromMilliseconds(50)
            };
            _gameTimer.Tick += GameTimer_Tick;
            // 初始化后台数据
            _game = new MergeDianaGame();
            _game.GameCompleted += Game_GameCompleted;
            _game.SkillActived += Game_SkillActived;
            _game.Moved += Game_Moved;
            _gameSetter = GameSetter.GetInstance();
            _gameSetter.RowSize = 4;
            _gameSetter.ColumnSize = 4;
            _gameSetter.GameTarget = DianaStrawberryType.K;
            // 初始化方向线
            _directionLine = new Line {
                StrokeThickness = 5,
                StrokeEndLineCap = PenLineCap.Triangle,
                StrokeDashCap = PenLineCap.Round,
                StrokeStartLineCap = PenLineCap.Round,
                Visibility = Visibility.Hidden
            };
            InitializeComponent();
            GamePlayAreaCanvas.Children.Add(_directionLine);
            ExpandGameSetterPanel();
        }

        private void ExpandGameSetter_Click(object sender, RoutedEventArgs e) {
            if (GameSetterPanel.Height != 0) {
                FoldGameSetterPanel();
            }
            else {
                ExpandGameSetterPanel();
            }
        }
        private void StartGame_Click(object sender, RoutedEventArgs e) {
            StartGame();
            FoldGameSetterPanel();
        }
        private void Move_DClick(object sender, DClickEventArgs e) {
            _game.Move(e.Direction);
        }
        private void ActiveSkill_Click(object sender, SClickEventArgs e) {
            switch (e.Skill) {
                case MergeDianaGameSkill.Randomize:
                    _game.ActiveSkill(MergeDianaGameSkill.Randomize, null);
                    break;
                case MergeDianaGameSkill.UpgradeBase:
                    _game.ActiveSkill(MergeDianaGameSkill.UpgradeBase, null);
                    break;
                case MergeDianaGameSkill.DegradeAll:
                    _game.ActiveSkill(MergeDianaGameSkill.DegradeAll, null);
                    break;
                default:
                    break;
            }
        }
        private void Game_GameCompleted(object sender, GameCompletedEventArgs e) {
            // 停止计时
            _gameTimer.Stop();
            // 模糊背景
            BlurEffect effect = new BlurEffect();
            DoubleAnimation effectAnimation = new DoubleAnimation() {
                To = 25,
                AccelerationRatio = 0.2,
                DecelerationRatio = 0.8,
                Duration = TimeSpan.FromMilliseconds(150)
            };
            GameArea.Effect = effect;
            effect.BeginAnimation(BlurEffect.RadiusProperty, effectAnimation);
            // 弹出统计窗口
            var gameTarget = _game.GameTarget;
            var gameUsingTime = _gameUsingTime / 1000.0;
            var movedTimes = _movedTimes;
            var skillActivedTimes = _skillActivedTimes;
            var totalScores = e.TotalScores;
            View.GameCompletedWindow gcw = new View.GameCompletedWindow(gameTarget, gameUsingTime, skillActivedTimes, movedTimes, totalScores);
            gcw.Owner = this;
            gcw.ShowDialog();
            // 取消模糊背景
            GameArea.Effect = null;
            ExpandGameSetterPanel();
        }
        private void GameTimer_Tick(object sender, EventArgs e) {
            _gameUsingTime += 50;
        }
        private void Game_SkillActived(object sender, SkillActiveEventArgs e) {
            if (e.ActiveResult == true) {
                _skillActivedTimes++;
            }
        }
        private void Game_Moved(object sender, MovedEventArgs e) {
            if (e.Direction == Direction.None) {
                return;
            }
            _movedTimes++;
        }

        private async void GameKey_KeyDown(object sender, KeyEventArgs e) {
            Direction direction = Direction.None;
            switch (e.Key) {
                case Key.Up:
                case Key.W:
                    direction = Direction.Up;
                    break;
                case Key.Down:
                case Key.S:
                    direction = Direction.Down;
                    break;
                case Key.Left:
                case Key.A:
                    direction = Direction.Left;
                    break;
                case Key.Right:
                case Key.D:
                    direction = Direction.Right;
                    break;
                default:
                    break;
            }
            e.Handled = true;
            HighlightDirectionButton(direction);
            _game.Move(direction);
            await Task.Delay(TimeSpan.FromMilliseconds(50));
            ResetDirectionButtonStatus();
        }
        private void GameGesture_MouseDown(object sender, MouseButtonEventArgs e) {
            _hitPos = e.GetPosition(GamePlayAreaCanvas);
        }
        private void GameGesture_MouseUp(object sender, MouseButtonEventArgs e) {
            Point releasePos = e.GetPosition(GamePlayAreaCanvas);
            Direction direction = GetDirectionFromPosChange(_hitPos, releasePos);
            _game.Move(direction);
            ClearDirectionLine();
            ResetDirectionButtonStatus();
        }
        private void GameGesture_MouseMove(object sender, MouseEventArgs e) {
            if (e.LeftButton != MouseButtonState.Pressed && e.RightButton != MouseButtonState.Pressed) {
                return;
            }
            Point currentPos = e.GetPosition(GamePlayAreaCanvas);
            // 绘制方向线
            DrawDirectionLine(_hitPos, currentPos);
            // 高亮方向键
            ResetDirectionButtonStatus();
            Direction direction = GetDirectionFromPosChange(_hitPos, currentPos);
            HighlightDirectionButton(direction);
        }

        private void Window_Move(object sender, MouseButtonEventArgs e) {
            DragMove();
        }
        private void Window_Minimum(object sender, RoutedEventArgs e) {
            WindowState = WindowState.Minimized;
        }
        private void Window_Close(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// 开始游戏
        /// </summary>
        private void StartGame() {
            // 重置统计信息
            _gameUsingTime = 0;
            _movedTimes = 0;
            _skillActivedTimes = 0;
            // 开始游戏
            _game.StartGame(_gameSetter.RowSize, _gameSetter.ColumnSize, _gameSetter.GameTarget, _gameSetter.SkillPoint);
            StrawberryDisplayer.Children.Clear();
            foreach (var strawberry in _game.DianaStrawberryArray) {
                StrawberryDisplayer.Children.Add(new View.StrawberryRound(strawberry));
            }
            // 开始计时
            _gameTimer.Start();
        }
        /// <summary>
        /// 通过坐标计算方向
        /// </summary>
        /// <param name="startPos">始坐标</param>
        /// <param name="endPos">尾坐标</param>
        /// <returns>方向信息</returns>
        private static Direction GetDirectionFromPosChange(Point startPos, Point endPos) {
            Direction resultDirection = Direction.None;

            // 分别计算出点与入点的x，y坐标距离
            double xDistance = endPos.X - startPos.X;
            double yDistance = endPos.Y - startPos.Y;
            double absXDistance = Math.Abs(xDistance);
            double absYDistance = Math.Abs(yDistance);
            // 通过距离绝对值判断此次是x方向还是y方向位置
            // 进行相应的位移操作
            // 当x移动距离绝对值为x移动距离的绝对值的2倍时，且x移动距离绝对值大于50时，判定为x方向
            if (absXDistance > absYDistance &&
                absXDistance / absYDistance >= 2 &&
                absXDistance >= 50) {
                if (xDistance > 0) {
                    resultDirection = Direction.Right;
                }
                else if (xDistance < 0) {
                    resultDirection = Direction.Left;
                }
            }
            // 当y移动距离绝对值为x移动距离的绝对值的2倍时，且y移动距离绝对值大于50时，判定为y方向
            else if (absXDistance < absYDistance &&
                     absYDistance / absXDistance >= 2 &&
                     absYDistance >= 50) {
                if (yDistance > 0) {
                    resultDirection = Direction.Down;
                }
                else if (yDistance < 0) {

                    resultDirection = Direction.Up;
                }
            }
            else {
                resultDirection = Direction.None;
            }

            return resultDirection;
        }
        /// <summary>
        /// 计算两点距离
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        /// <returns></returns>
        private static double CalculateLength(Point startPos, Point endPos) {
            return Math.Sqrt(Math.Pow(endPos.X - startPos.X, 2) + Math.Pow(endPos.Y - startPos.Y, 2));
        }
        /// <summary>
        /// 绘制方向线
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        private void DrawDirectionLine(Point startPos, Point endPos) {
            _directionLine.Visibility = Visibility.Visible;
            // 绘制线条虚线信息
            double lineLength = CalculateLength(startPos, endPos);
            double dashLength = lineLength / 50 <= 5 ? lineLength / 50 : 5;
            double dashInterval = lineLength / 50 <= 3 ? lineLength / 50 : 3;
            _directionLine.StrokeDashArray = new DoubleCollection(new double[2] { dashLength, dashInterval });
            // 绘制线条颜色
            //Direction direction = GetDirectionFromPosChange(startPos, endPos);
            _directionLine.Stroke = _directionColors[Direction.None];
            // 绘制线条位置
            _directionLine.X1 = startPos.X;
            _directionLine.Y1 = startPos.Y;
            _directionLine.X2 = endPos.X;
            _directionLine.Y2 = endPos.Y;
        }
        /// <summary>
        /// 清除方向线
        /// </summary>
        private void ClearDirectionLine() {
            _directionLine.Visibility = Visibility.Hidden;
            _directionLine.X1 = 0;
            _directionLine.Y1 = 0;
            _directionLine.X2 = 0;
            _directionLine.Y2 = 0;
        }
        /// <summary>
        /// 高亮方向键
        /// </summary>
        /// <param name="direction"></param>
        private void HighlightDirectionButton(Direction direction) {
            switch (direction) {
                case Direction.None:
                    break;
                case Direction.Up:
                    UpButton.IsHovered = true;
                    break;
                case Direction.Down:
                    DownButton.IsHovered = true;
                    break;
                case Direction.Left:
                    LeftButton.IsHovered = true;
                    break;
                case Direction.Right:
                    RightButton.IsHovered = true;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 重置方向键状态
        /// </summary>
        private void ResetDirectionButtonStatus() {
            UpButton.IsHovered = false;
            DownButton.IsHovered = false;
            LeftButton.IsHovered = false;
            RightButton.IsHovered = false;
        }
        /// <summary>
        /// 展开游戏设置面板
        /// </summary>
        private void ExpandGameSetterPanel() {
            DoubleAnimation heightAnimation = new DoubleAnimation() {
                To = Height - 40,
                AccelerationRatio = 0.2,
                DecelerationRatio = 0.8,
                Duration = TimeSpan.FromMilliseconds(200)
            };
            DoubleAnimation opacityAnimation = new DoubleAnimation() {
                To = 1,
                AccelerationRatio = 0.2,
                DecelerationRatio = 0.8,
                Duration = TimeSpan.FromMilliseconds(200)
            };
            GameSetterPanel.BeginAnimation(Grid.HeightProperty, heightAnimation);
            GameSetterPanel.BeginAnimation(Grid.OpacityProperty, opacityAnimation);
        }
        /// <summary>
        /// 收起游戏设置面板
        /// </summary>
        private void FoldGameSetterPanel() {
            DoubleAnimation heightAnimation = new DoubleAnimation() {
                To = 0,
                AccelerationRatio = 0.2,
                DecelerationRatio = 0.8,
                Duration = TimeSpan.FromMilliseconds(200)
            };
            DoubleAnimation opacityAnimation = new DoubleAnimation() {
                To = 0,
                AccelerationRatio = 0.2,
                DecelerationRatio = 0.8,
                Duration = TimeSpan.FromMilliseconds(200)
            };
            GameSetterPanel.BeginAnimation(HeightProperty, heightAnimation);
            GameSetterPanel.BeginAnimation(OpacityProperty, opacityAnimation);
        }
    }
}
