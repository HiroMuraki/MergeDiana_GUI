namespace MergeDiana.GameLib {
    public readonly struct Coordinate {
        private readonly int _row;
        private readonly int _column;
        public int Row {
            get {
                return _row;
            }
        }
        public int Column {
            get {
                return _column;
            }
        }
        public Coordinate Up {
            get {
                return new Coordinate(_row - 1, _column);
            }
        }
        public Coordinate Down {
            get {
                return new Coordinate(_row + 1, _column);
            }
        }
        public Coordinate Left {
            get {
                return new Coordinate(_row, _column - 1);
            }
        }
        public Coordinate Right {
            get {
                return new Coordinate(_row, _column + 1);
            }
        }

        public Coordinate(int row, int column) {
            _row = row;
            _column = column;
        }

        public static bool operator ==(Coordinate left, Coordinate right) {
            return left._row == right._row && left._column == right._column;
        }
        public static bool operator !=(Coordinate left, Coordinate right) {
            return !(left == right);
        }
        public override bool Equals(object obj) {
            return base.Equals(obj);
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}
