using MergeDiana.GameLib;
using System.Windows;

namespace MergeDiana_GUI {
    public class DClickEventArgs : RoutedEventArgs {
        private Direction _direction;
        public Direction Direction {
            get {
                return _direction;
            }
        }

        public DClickEventArgs(Direction direction) {
            _direction = direction;
        }
    }
}
