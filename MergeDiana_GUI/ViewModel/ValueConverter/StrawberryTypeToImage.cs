using MergeDiana.GameLib;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MergeDiana_GUI.ViewModel.ValueConverter {
    [ValueConversion(typeof(DianaStrawberryType), typeof(Brush))]
    public class StrawberryTypeToImage : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            try {
                DianaStrawberryType dianaStrawberryType = (DianaStrawberryType)value;
                switch (dianaStrawberryType) {
                    case DianaStrawberryType.A:
                        return App.ResDict["A"] as ImageBrush;
                    case DianaStrawberryType.B:
                        return App.ResDict["B"] as ImageBrush;
                    case DianaStrawberryType.C:
                        return App.ResDict["C"] as ImageBrush;
                    case DianaStrawberryType.D:
                        return App.ResDict["D"] as ImageBrush;
                    case DianaStrawberryType.E:
                        return App.ResDict["E"] as ImageBrush;
                    case DianaStrawberryType.F:
                        return App.ResDict["F"] as ImageBrush;
                    case DianaStrawberryType.G:
                        return App.ResDict["G"] as ImageBrush;
                    case DianaStrawberryType.H:
                        return App.ResDict["H"] as ImageBrush;
                    case DianaStrawberryType.I:
                        return App.ResDict["I"] as ImageBrush;
                    case DianaStrawberryType.J:
                        return App.ResDict["J"] as ImageBrush;
                    case DianaStrawberryType.K:
                        return App.ResDict["K"] as ImageBrush;
                    case DianaStrawberryType.L:
                        return App.ResDict["L"] as ImageBrush;
                    case DianaStrawberryType.M:
                        return App.ResDict["M"] as ImageBrush;
                    case DianaStrawberryType.N:
                        return App.ResDict["N"] as ImageBrush;
                    case DianaStrawberryType.O:
                        return App.ResDict["O"] as ImageBrush;
                    case DianaStrawberryType.P:
                        return App.ResDict["P"] as ImageBrush;
                    case DianaStrawberryType.None:
                        return null;
                    default:
                        return null;
                }
            }
            catch (Exception) {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
