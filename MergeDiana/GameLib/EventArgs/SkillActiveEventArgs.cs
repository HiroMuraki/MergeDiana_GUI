using System;

namespace MergeDiana.GameLib {
    public class SkillActiveEventArgs : EventArgs {
        private MergeDianaGameSkill _activedSkill;
        private bool _activeResult;

        public MergeDianaGameSkill ActivedSkill {
            get {
                return _activedSkill;
            }
        }
        public bool ActiveResult {
            get {
                return _activeResult;
            }
        }

        public SkillActiveEventArgs(MergeDianaGameSkill activedSkill, bool activeResult) {
            _activedSkill = activedSkill;
            _activeResult = activeResult;
        }
    }
}
