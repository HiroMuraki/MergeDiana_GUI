using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MergeDiana {
    using DianaStrawberryArray = IEnumerable<DianaStrawberry>;
    public sealed class MergeDianaGame : INotifyPropertyChanged {
        private int _rowSize;
        private int _columnSize;
        private DianaStrawberryType _gameTarget;
        private DianaStrawberry[,] _dianaStrawberries;
        private int _skillPoint;
        private int _holdTimes;

        #region 公开事件
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<GameCompletedEventArgs> GameCompleted;
        public event EventHandler<SkillActiveEventArgs> SkillActived;
        public event EventHandler<MovedEventArgs> Moved;
        public event EventHandler<MergedEventArgs> Merged;
        #endregion

        #region 公开属性
        public static readonly List<DianaStrawberryType> GenerateableStrawberrys;
        public int RowSize {
            get {
                return _rowSize;
            }
        }
        public int ColumnSize {
            get {
                return _columnSize;
            }
        }
        public DianaStrawberryType GameTarget {
            get {
                return _gameTarget;
            }
        }
        public bool IsGameCompleted {
            get {
                return CheckIfGameCompleted();
            }
        }
        public DianaStrawberryArray DianaStrawberryArray {
            get {
                for (int row = 0; row < _rowSize; row++) {
                    for (int col = 0; col < _columnSize; col++) {
                        yield return _dianaStrawberries[row, col];
                    }
                }
            }
        }
        public int SkillPoint {
            get {
                return _skillPoint;
            }
        }
        #endregion

        #region 构造方法
        static MergeDianaGame() {
            GenerateableStrawberrys = new List<DianaStrawberryType>() {
                DianaStrawberryType.None,
                DianaStrawberryType.A,
                DianaStrawberryType.B,
                DianaStrawberryType.C,
                DianaStrawberryType.D
            };
        }
        public MergeDianaGame() {

        }
        #endregion

        #region 公开方法
        /// <summary>
        /// 开始游戏
        /// </summary>
        /// <param name="rowSize">行数</param>
        /// <param name="columnSize">列数</param>
        /// <param name="gameTarget">游戏目标</param>
        public void StartGame(int rowSize, int columnSize, DianaStrawberryType gameTarget, int skillPoint) {
            // 若行列小于3，抛出异常
            if (rowSize < 3 || columnSize < 3) {
                throw new NotSupportedException();
            }
            _holdTimes = 0;
            _rowSize = rowSize;
            _columnSize = columnSize;
            _gameTarget = gameTarget;
            _skillPoint = skillPoint;
            OnPropertyChanged(nameof(RowSize));
            OnPropertyChanged(nameof(ColumnSize));
            OnPropertyChanged(nameof(GameTarget));
            OnPropertyChanged(nameof(SkillPoint));

            // 创建草莓数组
            _dianaStrawberries = new DianaStrawberry[_rowSize, columnSize];
            for (int row = 0; row < _rowSize; row++) {
                for (int col = 0; col < _columnSize; col++) {
                    _dianaStrawberries[row, col] = new DianaStrawberry(DianaStrawberryType.None);
                }
            }
            // 生成两个初始草莓
            GenerateRandomStrawberry();
            GenerateRandomStrawberry();
            UpdateGameStatus();
            OnPropertyChanged(nameof(DianaStrawberryArray));
        }
        /// <summary>
        /// 移动草莓
        /// </summary>
        /// <param name="direction">移动方向</param>
        public void Move(Direction direction) {
            switch (direction) {
                case Direction.None:
                    break;
                case Direction.Up:
                    MoveUp();
                    break;
                case Direction.Down:
                    MoveDown();
                    break;
                case Direction.Left:
                    MoveLeft();
                    break;
                case Direction.Right:
                    MoveRight();
                    break;
                default:
                    break;
            }
            Moved?.Invoke(this, new MovedEventArgs(direction));
            GenerateRandomStrawberry();
            UpdateGameStatus();
        }
        /// <summary>
        /// 启用技能
        /// </summary>
        /// <param name="skill">技能类型</param>
        /// <param name="skillArgs">技能参数</param>
        /// 
        public void ActiveSkill(MergeDianaGameSkill skill, object skillArgs) {
            if (_skillPoint <= 0) {
                SkillActived?.Invoke(this, new SkillActiveEventArgs(skill, false));
                return;
            }
            switch (skill) {
                case MergeDianaGameSkill.Randomize:
                    Random rnd = new Random();
                    int numStrawberry = _rowSize * _columnSize;
                    for (int currentIndex = 0; currentIndex < numStrawberry; currentIndex++) {
                        int rndIndex = rnd.Next(currentIndex, numStrawberry);
                        int aRow = currentIndex / _columnSize;
                        int aCol = currentIndex % _columnSize;
                        int bRow = rndIndex / _columnSize;
                        int bCol = rndIndex % _columnSize;
                        var t = _dianaStrawberries[aRow, aCol].StrawberryType;
                        _dianaStrawberries[aRow, aCol].StrawberryType = _dianaStrawberries[bRow, bCol].StrawberryType;
                        _dianaStrawberries[bRow, bCol].StrawberryType = t;
                    }
                    break;
                case MergeDianaGameSkill.DegradeAll:
                    for (int row = 0; row < _rowSize; row++) {
                        for (int col = 0; col < _columnSize; col++) {
                            _dianaStrawberries[row, col].Downgrade();
                        }
                    }
                    break;
                case MergeDianaGameSkill.UpgradeBase:
                    for (int row = 0; row < _rowSize; row++) {
                        for (int col = 0; col < _columnSize; col++) {
                            if ((int)(_dianaStrawberries[row, col].StrawberryType) <= (int)DianaStrawberryType.E) {
                                _dianaStrawberries[row, col].Upgrade();
                            }
                        }
                    }
                    break;
                case MergeDianaGameSkill.HoldOn:
                    _holdTimes = 3;
                    break;
                default:
                    break;
            }
            _skillPoint -= 1;
            SkillActived?.Invoke(this, new SkillActiveEventArgs(skill, true));
            UpdateGameStatus();
        }
        #endregion

        private void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// 更新游戏状态
        /// </summary>
        private void UpdateGameStatus() {
            OnPropertyChanged(nameof(SkillPoint));
            if (CheckIfGameCompleted()) {
                GameCompleted?.Invoke(this, new GameCompletedEventArgs(_gameTarget, GetTotalScores()));
            }
        }
        /// <summary>
        /// 上移
        /// </summary>
        private void MoveUp() {
            for (int col = 0; col < _columnSize; col++) {
                bool hasMove = true;
                while (hasMove) {
                    hasMove = false;
                    for (int row = 1; row < _rowSize; row++) {
                        Coordinate sourceCoordinate = new Coordinate(row, col);
                        Coordinate targetCoordinate = new Coordinate(row - 1, col);
                        bool compareResult = CompareStrawberries(sourceCoordinate, targetCoordinate);
                        if (compareResult == true) {
                            hasMove = true;
                        }
                    }
                };
            }
        }
        /// <summary>
        /// 下移
        /// </summary>
        private void MoveDown() {
            for (int col = 0; col < _columnSize; col++) {
                bool hasMove = true;
                while (hasMove) {
                    hasMove = false;
                    for (int row = _rowSize - 2; row >= 0; row--) {
                        Coordinate sourceCoordinate = new Coordinate(row, col);
                        Coordinate targetCoordinate = new Coordinate(row + 1, col);
                        bool compareResult = CompareStrawberries(sourceCoordinate, targetCoordinate);
                        if (compareResult == true) {
                            hasMove = true;
                        }
                    }
                };
            }
        }
        /// <summary>
        /// 左移
        /// </summary>
        private void MoveLeft() {
            for (int row = 0; row < _rowSize; row++) {
                bool hasMove = true;
                while (hasMove) {
                    hasMove = false;
                    for (int col = 1; col < _columnSize; col++) {
                        Coordinate sourceCoordinate = new Coordinate(row, col);
                        Coordinate targetCoordinate = new Coordinate(row, col - 1);
                        bool compareResult = CompareStrawberries(sourceCoordinate, targetCoordinate);
                        if (compareResult == true) {
                            hasMove = true;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 右移
        /// </summary>
        private void MoveRight() {
            for (int row = 0; row < _rowSize; row++) {
                bool hasMove = true;
                while (hasMove) {
                    hasMove = false;
                    for (int col = _columnSize - 2; col >= 0; col--) {
                        Coordinate sourceCoordinate = new Coordinate(row, col);
                        Coordinate targetCoordinate = new Coordinate(row, col + 1);
                        bool compareStatus = CompareStrawberries(sourceCoordinate, targetCoordinate);
                        if (compareStatus == true) {
                            hasMove = true;
                        }
                    }
                }
            }
        }
        private bool CompareStrawberries(Coordinate a, Coordinate b) {
            int aRow = a.Row;
            int aCol = a.Column;
            int bRow = b.Row;
            int bCol = b.Column;

            // 跳过空草莓
            if (_dianaStrawberries[aRow, aCol].StrawberryType == DianaStrawberryType.None) {
                return false;
            }
            // 若目标草莓为空草莓，则交换位置
            else if (_dianaStrawberries[bRow, bCol].StrawberryType == DianaStrawberryType.None) {
                var t = _dianaStrawberries[aRow, aCol].StrawberryType;
                _dianaStrawberries[aRow, aCol].StrawberryType = _dianaStrawberries[bRow, bCol].StrawberryType;
                _dianaStrawberries[bRow, bCol].StrawberryType = t;
                return true;
            }
            // 若目标草莓和当前草莓类型相同，则升级目标草莓，重置当前草莓
            else if (DianaStrawberry.IsSameType(_dianaStrawberries[aRow, aCol], _dianaStrawberries[bRow, bCol])) {
                _dianaStrawberries[bRow, bCol].Upgrade();
                _dianaStrawberries[aRow, aCol].Reset();
                Merged?.Invoke(this, new MergedEventArgs(_dianaStrawberries[bRow, bCol].StrawberryType));
                return true;
            }
            // 未知情况
            else {
                return false;
            }
        }
        private void GenerateRandomStrawberry() {
            if (_holdTimes > 0) {
                _holdTimes -= 1;
                return;
            }
            // 搜索可以填充的空白区
            List<Coordinate> nullCoordinates = new List<Coordinate>();
            for (int row = 0; row < _rowSize; row++) {
                for (int col = 0; col < _columnSize; col++) {
                    if (_dianaStrawberries[row, col].StrawberryType == DianaStrawberryType.None) {
                        nullCoordinates.Add(new Coordinate(row, col));
                    }
                }
            }
            // 若没有可随机生成的位置，则跳过
            if (nullCoordinates.Count <= 0) {
                return;
            }
            // 选择空白区生成一个随机草莓
            Random rnd = new Random();
            Coordinate selectedCoordiante = nullCoordinates[rnd.Next(0, nullCoordinates.Count)];
            DianaStrawberryType rndType = GenerateableStrawberrys[rnd.Next(0, GenerateableStrawberrys.Count)];
            _dianaStrawberries[selectedCoordiante.Row, selectedCoordiante.Column].StrawberryType = rndType;
        }
        private bool CheckIfGameCompleted() {
            // 只要能找到一个草莓等级等于目标等级，则游戏完成
            for (int row = 0; row < _rowSize; row++) {
                for (int col = 0; col < _columnSize; col++) {
                    if (_dianaStrawberries[row, col].StrawberryType == _gameTarget) {
                        return true;
                    }
                }
            }
            return false;
        }
        private int GetTotalScores() {
            int totalScores = 0;
            for (int row = 0; row < _rowSize; row++) {
                for (int col = 0; col < _columnSize; col++) {
                    totalScores += (int)(_dianaStrawberries[row, col].StrawberryType);
                }
            }
            return totalScores;
        }
    }
}
