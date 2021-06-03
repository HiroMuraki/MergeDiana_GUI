using MergeDiana;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MergeDiana_GUI.View {
    public class SButton : Button {
        public static readonly DependencyProperty SkillProperty =
            DependencyProperty.Register(nameof(Skill), typeof(MergeDianaGameSkill), typeof(SButton), new PropertyMetadata(MergeDianaGameSkill.None));

        public event EventHandler<SClickEventArgs> SClick;
        public MergeDianaGameSkill Skill {
            get {
                return (MergeDianaGameSkill)GetValue(SkillProperty);
            }
            set {
                SetValue(SkillProperty, value);
            }
        }


        static SButton() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SButton), new FrameworkPropertyMetadata(typeof(SButton)));
        }

        protected override void OnClick() {
            base.OnClick();
            SClick?.Invoke(this, new SClickEventArgs(Skill));
        }
    }
}
