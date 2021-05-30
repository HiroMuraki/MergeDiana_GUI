using System;

namespace MergeDiana.GameLib {
    public class GameCompletedEventArgs : EventArgs {
        private DianaStrawberryType _gameTarget;
        private int _totalScores;

        public DianaStrawberryType DianaStrawberryType {
            get {
                return _gameTarget;
            }
        }
        public int TotalScores {
            get {
                return _totalScores;
            }
        }

        public GameCompletedEventArgs(DianaStrawberryType gameTarget, int totalScore) {
            _gameTarget = gameTarget;
            _totalScores = totalScore;
        }
    }
}
