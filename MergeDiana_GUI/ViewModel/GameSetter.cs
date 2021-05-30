using MergeDiana.GameLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MergeDiana_GUI.ViewModel {
    public class GameSetter : INotifyPropertyChanged {
        public readonly static int MaxRange = 6;
        public readonly static int MinRagne = 3;
        public readonly static Dictionary<DianaStrawberryType, string> TargetName = new Dictionary<DianaStrawberryType, string>() {
            [DianaStrawberryType.I] = "圣诞",
            [DianaStrawberryType.J] = "新年",
            [DianaStrawberryType.K] = "头像",
            [DianaStrawberryType.L] = "圣",
            [DianaStrawberryType.M] = "好耶",
            [DianaStrawberryType.M] = "初始-素线",
            [DianaStrawberryType.O] = "初始-普通",
            [DianaStrawberryType.P] = "初始-高级",
        };

        public event PropertyChangedEventHandler PropertyChanged;
        private int _rowSize;
        private int _columnSize;
        private DianaStrawberryType _gameTarget;
        private int _skillPoint;
        private ObservableCollection<DianaStrawberryType> _targetStrawberries;

        public ObservableCollection<DianaStrawberryType> TargetStrawberries {
            get {
                return _targetStrawberries;
            }
            private set {
                _targetStrawberries = value;
                OnPropertyChanged(nameof(TargetStrawberries));
            }
        }
        public int ColumnSize {
            get {
                return _columnSize;
            }
            set {
                _columnSize = value;
                GetSkillPoint();
                GetTargetStrawberries();
                OnPropertyChanged(nameof(ColumnSize));
            }
        }
        public int RowSize {
            get {
                return _rowSize;
            }
            set {
                _rowSize = value;
                GetSkillPoint();
                GetTargetStrawberries();
                OnPropertyChanged(nameof(RowSize));
            }
        }
        public DianaStrawberryType GameTarget {
            get {
                return _gameTarget;
            }
            set {
                _gameTarget = value;
                GetSkillPoint();
                OnPropertyChanged(nameof(GameTarget));
            }
        }
        public int SkillPoint {
            get {
                return _skillPoint;
            }
        }

        public GameSetter() {
            TargetStrawberries = new ObservableCollection<DianaStrawberryType>();
        }

        private void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void GetTargetStrawberries() {
            DianaStrawberryType previousTarget = _gameTarget;
            _targetStrawberries.Clear();
            int minRange = Math.Min(_columnSize, _rowSize);
            // 获取可选草莓
            if (minRange >= 3) {
                _targetStrawberries.Add(DianaStrawberryType.H);
                _targetStrawberries.Add(DianaStrawberryType.I);
                _targetStrawberries.Add(DianaStrawberryType.J);
            }
            if (minRange >= 4) {
                _targetStrawberries.Add(DianaStrawberryType.K);
                _targetStrawberries.Add(DianaStrawberryType.L);
            }
            if (minRange >= 5) {
                _targetStrawberries.Add(DianaStrawberryType.M);
                _targetStrawberries.Add(DianaStrawberryType.N);
            }
            if (minRange >= 6) {
                _targetStrawberries.Add(DianaStrawberryType.O);
                _targetStrawberries.Add(DianaStrawberryType.P);
            }
            // 调整当前选项
            if (_targetStrawberries.Contains(previousTarget)) {
                GameTarget = previousTarget;
            }
            else if (_targetStrawberries.Count > 0) {
                GameTarget = _targetStrawberries[0];
            }
            OnPropertyChanged(nameof(TargetStrawberries));
        }
        private void GetSkillPoint() {
            _skillPoint = 0;
            // 行或列任意一边小于4则默认获取5点技能点
            if (_rowSize <= 4 || _columnSize <= 4) {
                _skillPoint += 2;
                // 行或列任意一边小于3则默认获取3点技能点
                if (_rowSize <= 3 || _columnSize <= 3) {
                    _skillPoint += 3;
                }
            }
            // 根据难度基于技能点
            switch (_gameTarget) {
                case DianaStrawberryType.None:
                case DianaStrawberryType.A:
                case DianaStrawberryType.B:
                case DianaStrawberryType.C:
                case DianaStrawberryType.D:
                case DianaStrawberryType.E:
                case DianaStrawberryType.F:
                case DianaStrawberryType.G:
                case DianaStrawberryType.H:
                case DianaStrawberryType.I:
                case DianaStrawberryType.J:
                    _skillPoint += 0;
                    break;
                case DianaStrawberryType.K:
                    _skillPoint += 1;
                    break;
                case DianaStrawberryType.L:
                    _skillPoint += 2;
                    break;
                case DianaStrawberryType.M:
                    _skillPoint += 4;
                    break;
                case DianaStrawberryType.N:
                    _skillPoint += 6;
                    break;
                case DianaStrawberryType.O:
                    _skillPoint += 8;
                    break;
                case DianaStrawberryType.P:
                    _skillPoint += 10;
                    break;
                default:
                    _skillPoint += 0;
                    break;
            }
            OnPropertyChanged(nameof(SkillPoint));
        }
    }
}
