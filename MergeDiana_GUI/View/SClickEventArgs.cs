using MergeDiana.GameLib;
using System.Windows;

namespace MergeDiana_GUI {
    public class SClickEventArgs : RoutedEventArgs {
        private MergeDianaGameSkill _skill;
        public MergeDianaGameSkill Skill {
            get {
                return _skill;
            }
        }

        public SClickEventArgs(MergeDianaGameSkill skill) {
            _skill = skill;
        }
    }
}
