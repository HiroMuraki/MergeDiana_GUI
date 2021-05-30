﻿using System;

namespace MergeDiana.GameLib {
    public class MovedEventArgs : EventArgs {
        private Direction _direction;
        public Direction Direction {
            get {
                return _direction;
            }
        }

        public MovedEventArgs(Direction direction) {
            _direction = direction;
        }
    }
}
