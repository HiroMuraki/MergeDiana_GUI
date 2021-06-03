using System;
using System.Collections.Generic;

namespace MergeDiana {
    public class MergedEventArgs : EventArgs {
        private DianaStrawberryType _mergedType;

        public DianaStrawberryType MergedType {
            get {
                return _mergedType;
            }
        }

        public MergedEventArgs(DianaStrawberryType mergedType) {
            _mergedType = mergedType;
        }

    }
}
