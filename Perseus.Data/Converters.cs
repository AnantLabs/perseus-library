using System;
using System.Windows;
using System.Windows.Data;

namespace Perseus.Converters {
    [ValueConversion(typeof(Enum), typeof(int))]
    [Localizability(LocalizationCategory.NeverLocalize)]
    public class EnumToIntConverter : IValueConverter {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return (int)(value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return Enum.Parse(targetType, value.ToString());
        }
        #endregion
    }

    [ValueConversion(typeof(bool), typeof(Visibility), ParameterType = typeof(bool))]
    [Localizability(LocalizationCategory.NeverLocalize)]
    public class BoolToVisibilityConverter : IValueConverter {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if ((bool)value == true) {
                return Visibility.Visible;
            }
            else {
                if (parameter != null && bool.Parse(parameter.ToString()) == true) {
                    return Visibility.Hidden;
                }
                else {
                    return Visibility.Collapsed;
                }
            }            
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if ((Visibility)value == Visibility.Visible) {
                return true;
            }
            else {
                return false;
            }  
        }
        #endregion
    }

    [ValueConversion(typeof(int), typeof(bool), ParameterType = typeof(int))]
    [Localizability(LocalizationCategory.NeverLocalize)]
    public class SelectedIndexToBoolConverter : IValueConverter {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            int index = -1;
            if (parameter != null) {
                index = (int)parameter;
            }
                
            if ((int)value == index) {
                return false;
            }
            else {
                return true;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotSupportedException();
        }
        #endregion
    }

    [ValueConversion(typeof(bool), typeof(Visibility), ParameterType = typeof(bool))]
    [Localizability(LocalizationCategory.NeverLocalize)]
    public class BoolToVisibilityReverseConverter : IValueConverter {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if ((bool)value == false) {
                return Visibility.Visible;
            }
            else {
                if (parameter != null && bool.Parse(parameter.ToString()) == true) {
                    return Visibility.Hidden;
                }
                else {
                    return Visibility.Collapsed;
                }
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if ((Visibility)value == Visibility.Visible) {
                return false;
            }
            else {
                return true;
            }
        }
        #endregion
    }

    [ValueConversion(typeof(bool?), typeof(Visibility))]
    [Localizability(LocalizationCategory.NeverLocalize)]
    public class NullableBoolToVisibilityConverter : IValueConverter {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            bool? tmp = (bool?)value;
            if (tmp == true) {
                return Visibility.Visible;
            }
            else if (tmp == false) {
                return Visibility.Hidden;
            }
            else {
                return Visibility.Collapsed;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            Visibility tmp = (Visibility)value;
            if (tmp == Visibility.Visible) {
                return true;
            }
            else if (tmp == Visibility.Hidden) {
                return false;
            }
            else {
                return null;
            }
        }
        #endregion
    }

    [ValueConversion(typeof(object), typeof(Visibility), ParameterType = typeof(bool))]
    [Localizability(LocalizationCategory.NeverLocalize)]
    public class NullToVisibilityConverter : IValueConverter {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value == null) {
                if (parameter != null && (bool)parameter == true) {
                    return Visibility.Hidden;
                }
                else {
                    return Visibility.Collapsed;
                }
            }            
            else {
                return Visibility.Visible;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotSupportedException();
        }
        #endregion
    }

    [ValueConversion(typeof(string), typeof(string))]
    [Localizability(LocalizationCategory.NeverLocalize)]
    public class NullToStringConverter : IValueConverter {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value == null) {
                return string.Empty;
            }
            else {
                return value;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotSupportedException();
        }
        #endregion
    }

    [ValueConversion(typeof(Visibility), typeof(Visibility), ParameterType = typeof(bool))]
    [Localizability(LocalizationCategory.NeverLocalize)]
    public class VisibilityToVisibilityConverter : IValueConverter {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if ((Visibility)value == Visibility.Visible) {
                if (parameter != null && (bool)parameter == true) {
                    return Visibility.Hidden;
                }
                else {
                    return Visibility.Collapsed;
                }
            }
            else {
                return Visibility.Visible;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return Convert(value, targetType, parameter, culture);
        }
        #endregion
    }

    [ValueConversion(typeof(Thickness), typeof(Thickness), ParameterType = typeof(double))]
    [Localizability(LocalizationCategory.NeverLocalize)]
    public class LeftRightBorderThicknessConverter : IValueConverter {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            Thickness t = (Thickness)value;
            double n;
            if (parameter != null) {
                n = (double)parameter;
            }
            else {
                n = 0;
            }
            return new Thickness(t.Left, n, t.Right, n);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            Thickness t = (Thickness)value;
            if (parameter != null) {
                double n = (double)parameter;
                return new Thickness(t.Left, n, t.Right, n);
            }
            else {
                return new Thickness(t.Left, t.Left, t.Right, t.Right);
            }
        }
        #endregion
    }

    [ValueConversion(typeof(double), typeof(double))]
    [Localizability(LocalizationCategory.NeverLocalize)]
    public class DoubleAdditionConverter : IMultiValueConverter {
        #region IMultiValueConverter Members
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            double value = 0d;
            foreach (object o in values) {
                double d;
                double.TryParse(o.ToString(), out d);
                if (!double.IsNaN(d)) {
                    value += d;
                }
            }
            return value;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotSupportedException();
        }
        #endregion
    }


    [ValueConversion(typeof(double), typeof(double))]
    [Localizability(LocalizationCategory.NeverLocalize)]
    public class DoubleSubtractionConverter : IValueConverter {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return (-1 * (double)value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return (-1 * (double)value);
        }
        #endregion
    }

    [ValueConversion(typeof(int), typeof(Visibility), ParameterType = typeof(int))]
    [Localizability(LocalizationCategory.NeverLocalize)]
    public class IntToVisibilityConverter : IValueConverter {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (parameter != null) {
                int n = int.Parse(parameter.ToString());
                int index = int.Parse(value.ToString());
                if (n == index) {
                    return Visibility.Visible;
                }
                else {
                    return Visibility.Collapsed;
                }
            }
            else {
                throw new ArgumentNullException("Parameter required.");
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotSupportedException();
        }
        #endregion
    }

    [ValueConversion(typeof(double), typeof(Thickness), ParameterType = typeof(bool))]
    [Localizability(LocalizationCategory.NeverLocalize)]
    public class DoubleToThicknessConverter : IValueConverter {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if ((bool)value == false) {
                return Visibility.Visible;
            }
            else {
                if (parameter != null && bool.Parse(parameter.ToString()) == true) {
                    return Visibility.Hidden;
                }
                else {
                    return Visibility.Collapsed;
                }
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if ((Visibility)value == Visibility.Visible) {
                return false;
            }
            else {
                return true;
            }
        }
        #endregion
    }

    //[ValueConversion(typeof(string), typeof(string))]
    //public class StringJoinConverter : IMultiValueConverter {
    //    #region IMultiValueConverter Members
    //    public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
    //        string join = string.Empty;
    //        foreach (string s in values) {
    //            join += s;
    //        }
    //        return join;
    //    }

    //    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture) {
    //        throw new NotSupportedException();
    //    }
    //    #endregion
    //}
}
