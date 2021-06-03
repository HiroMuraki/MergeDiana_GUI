using MergeDiana;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MergeDiana_GUI.ViewModel.ValueConverter {
    [ValueConversion(typeof(DianaStrawberryType), typeof(Brush))]
    public class StrawberryTypeToColorFrame : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            DianaStrawberryType dianaStrawberryType = (DianaStrawberryType)value;
            switch (dianaStrawberryType) {
                case DianaStrawberryType.A:
                case DianaStrawberryType.C:
                case DianaStrawberryType.E:
                case DianaStrawberryType.G:
                case DianaStrawberryType.I:
                case DianaStrawberryType.K:
                case DianaStrawberryType.M:
                case DianaStrawberryType.O:
                    return new SolidColorBrush(Colors.Silver);
                case DianaStrawberryType.B:
                case DianaStrawberryType.D:
                case DianaStrawberryType.F:
                case DianaStrawberryType.H:
                case DianaStrawberryType.J:
                case DianaStrawberryType.L:
                case DianaStrawberryType.N:
                case DianaStrawberryType.P:
                    return new SolidColorBrush(Colors.Gold);
                case DianaStrawberryType.None:
                    return null;
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
