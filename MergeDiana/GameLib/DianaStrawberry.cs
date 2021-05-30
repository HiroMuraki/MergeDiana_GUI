using System;
using System.ComponentModel;

namespace MergeDiana.GameLib {
    public class DianaStrawberry : INotifyPropertyChanged {
        private DianaStrawberryType _strawberryType;

        #region 公开事件
        public event EventHandler<DianaStrawberryType> TypeChanged;
        public event EventHandler<MovedEventArgs> Moved;
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region 公开属性
        public DianaStrawberryType StrawberryType {
            get {
                return _strawberryType;
            }
            set {
                _strawberryType = value;
                OnPropertyChanged(nameof(StrawberryType));
            }
        }
        #endregion

        #region 构造方法
        public DianaStrawberry(DianaStrawberryType type) {
            _strawberryType = type;
        }
        #endregion

        #region 公开方法
        /// <summary>
        /// 检查是否是同类型草莓
        /// </summary>
        /// <param name="strawberryA">草莓A</param>
        /// <param name="strawberryB">草莓B</param>
        /// <returns>比较结果</returns>
        public static bool IsSameType(DianaStrawberry strawberryA, DianaStrawberry strawberryB) {
            return strawberryA._strawberryType == strawberryB._strawberryType;
        }
        /// <summary>
        /// 检查是否是同类型草莓
        /// </summary>
        /// <param name="dianaStrawberry">要比较的草莓</param>
        /// <returns>比较结果</returns>
        public bool IsSameType(DianaStrawberry dianaStrawberry) {
            return IsSameType(this, dianaStrawberry);
        }
        /// <summary>
        /// 将草莓等级提升一级
        /// </summary>
        /// <returns>提升是否成功</returns>
        public bool Upgrade() {
            if (_strawberryType == DianaStrawberryType.P) {
                return false;
            }
            _strawberryType = (DianaStrawberryType)((int)_strawberryType << 1);
            OnPropertyChanged(nameof(StrawberryType));
            TypeChanged?.Invoke(this, _strawberryType);
            return true;
        }
        /// <summary>
        /// 将草莓等级降低一级
        /// </summary>
        /// <returns>降低是否成功</returns>
        public bool Downgrade() {
            if (_strawberryType == DianaStrawberryType.A) {
                return false;
            }
            _strawberryType = (DianaStrawberryType)((int)_strawberryType >> 1);
            OnPropertyChanged(nameof(StrawberryType));
            TypeChanged?.Invoke(this, _strawberryType);
            return true;
        }
        /// <summary>
        /// 重置草莓等级
        /// </summary>
        /// <returns>重置是否成功</returns>
        public bool Reset() {
            _strawberryType = DianaStrawberryType.None;
            return true;
        }
        #endregion

        private void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public override string ToString() {
            if (StrawberryType == DianaStrawberryType.None) {
                return "";
            }
            return $"{Enum.GetName(typeof(DianaStrawberryType), _strawberryType)}";
        }
    }
}
