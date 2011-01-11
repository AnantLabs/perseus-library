using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
/** TODO
 * Add cylce feature for when max min is set, using scroll wheel will go back to beginning
 */
namespace Perseus.Controls {
    public class NumericTextBox : TextBox {
        #region Private Members
        /// <summary>
        /// Set to true to enable on text change processing. If set to null, it will be 
        /// set to true after frist on text change processing.
        /// </summary>
        private bool? _IsApply = false;
        #endregion
        #region Constructor
        public NumericTextBox() {
            InputMethod.SetIsInputMethodEnabled(this, false);
            DataObject.AddPastingHandler(this, new DataObjectPastingEventHandler(OnPaste));
            DataObject.AddSettingDataHandler(this, new DataObjectSettingDataEventHandler(OnSetData));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Cut, OnCut));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Undo, OnUndo));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Redo, OnRedo));
            this.Loaded += new RoutedEventHandler(NumericTextbox_Loaded);
            this.MouseWheel += new MouseWheelEventHandler(NumericTextbox_MouseWheel);
            this.AllowDrop = false;
        }
        private void NumericTextbox_Loaded(object sender, RoutedEventArgs e) {
            if (this.Text.Trim() == string.Empty) {
                this.Text = ApplyPrecision(PMath.MaxMin(0, this.MinValue, this.MaxValue).ToString());
            }
            else if (!IsValidText(this.Text)) {
                throw new InvalidCastException("+ " + ((int)char.Parse(this.Text)).ToString() + " + " + "Text must be a numeric value and fit in the constraints specified by 'MaxValue' and 'MinValue'.");
            }
            else {
                this.Text = ApplyPrecision(this.Text);
            }
            this.UndoLimit = 0; // Clear Initial undo value;
            this.UndoLimit = -1;
            this._IsApply = true;
        }
        #endregion
        #region Dependency Properties
        public static readonly DependencyProperty MaxValueProperty;
        public static readonly DependencyProperty MinValueProperty;
        public static readonly DependencyProperty PrecisionProperty;
        public static readonly DependencyProperty IncrementProperty;

        static NumericTextBox() {
            NumericTextBox.MaxValueProperty = DependencyProperty.Register(
                "MaxValue",
                typeof(double),
                typeof(NumericTextBox),
                new FrameworkPropertyMetadata(double.MaxValue, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(OnMaxValueChanged), new CoerceValueCallback(CoerceMaxValue)),
                new ValidateValueCallback(IsValidValue)
            );

            NumericTextBox.MinValueProperty = DependencyProperty.Register(
                "MinValue",
                typeof(double),
                typeof(NumericTextBox),
                new FrameworkPropertyMetadata(double.MinValue, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(OnMinValueChanged), new CoerceValueCallback(CoerceMinValue)),
                new ValidateValueCallback(IsValidValue)
            );

            NumericTextBox.PrecisionProperty = DependencyProperty.Register(
                "Precision",
                typeof(int),
                typeof(NumericTextBox),
                new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(OnPrecisionChanged)),
                new ValidateValueCallback(IsValidPrecision)
            );

            NumericTextBox.IncrementProperty = DependencyProperty.Register(
                "Increment",
                typeof(double),
                typeof(NumericTextBox),
                new FrameworkPropertyMetadata(1d, new PropertyChangedCallback(OnIncrementChanged), new CoerceValueCallback(CoerceIncrement)),
                new ValidateValueCallback(IsValidIncrement)
            );

            TextProperty.OverrideMetadata(typeof(NumericTextBox), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnTextChanged)));
        }

        public static bool IsValidValue(object value) {
            Double v = (Double)value;
            return (!v.Equals(Double.NegativeInfinity) && !v.Equals(Double.PositiveInfinity));
        }
        public static bool IsValidPrecision(object value) {
            int v = (int)value;
            return (v >= -1);
        }
        public static bool IsValidIncrement(object value) {
            Double v = (Double)value;
            return (v >= 0 && !v.Equals(Double.PositiveInfinity));
        }

        private static object CoerceMaxValue(DependencyObject o, object value) {
            NumericTextBox nt = (NumericTextBox)o;
            double max = (double)value;
            if (max < nt.MinValue) {
                max = nt.MinValue;
            }
            if (nt.Precision == -1) {
                return max;
            }
            else {
                double tmp;
                if (nt.Precision > 0) {
                    tmp = 0.4 / Math.Pow(10, nt.Precision);
                }
                else {
                    tmp = 0.4;
                }
                return Math.Round(max - tmp, nt.Precision, MidpointRounding.AwayFromZero);
            }
        }
        private static object CoerceMinValue(DependencyObject o, object value) {
            NumericTextBox nt = (NumericTextBox)o;
            double min = (double)value;
            if (min > nt.MaxValue) {
                min = nt.MaxValue;
            }
            if (nt.Precision == -1) {
                return min;
            }
            else {
                double tmp;
                if (nt.Precision > 0) {
                    tmp = 0.4 / Math.Pow(10, nt.Precision);
                }
                else {
                    tmp = 0.4;
                }
                return Math.Round(min + tmp, nt.Precision, MidpointRounding.AwayFromZero);
            }
        }
        private static object CoerceIncrement(DependencyObject o, object value) {
            NumericTextBox nt = (NumericTextBox)o;
            double inc = (double)value;
            if (nt.Precision == -1) {
                return inc;
            }
            else {
                return Math.Round(inc, nt.Precision, MidpointRounding.AwayFromZero);
            }
        }

        private static void OnMaxValueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e) {
            o.CoerceValue(MinValueProperty);
        }
        private static void OnMinValueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e) {
            o.CoerceValue(MaxValueProperty);
        }
        private static void OnPrecisionChanged(DependencyObject o, DependencyPropertyChangedEventArgs e) {
            o.CoerceValue(MinValueProperty);
            o.CoerceValue(MaxValueProperty);
            o.CoerceValue(IncrementProperty);
        }
        private static void OnIncrementChanged(DependencyObject o, DependencyPropertyChangedEventArgs e) { }
        private static void OnTextChanged(DependencyObject o, DependencyPropertyChangedEventArgs e) {
            NumericTextBox nt = (NumericTextBox)o;

            if (nt._IsApply == null) {
                nt._IsApply = true;
            }
            else if (nt._IsApply == true && e.NewValue != e.OldValue) { // If value was set via its property
                nt.LockCurrentUndoUnit();
                string tmp = (string)e.NewValue;

                if (tmp.Trim() == string.Empty) {
                    tmp = nt.ApplyPrecision(PMath.MaxMin(0, nt.MinValue, nt.MaxValue).ToString());
                }
                else if (!nt.IsValidText(tmp)) {
                    throw new InvalidCastException("Text must be a numeric value and fit in the constraints specified by 'MaxValue' and 'MinValue'.");
                }
                nt.Text = nt.ApplyPrecision(tmp);
                nt.UndoLimit = 0; // Clear Initial undo value;
                nt.UndoLimit = -1;
            }
        }

        public double MaxValue {
            get { return (double)GetValue(NumericTextBox.MaxValueProperty); }
            set { SetValue(NumericTextBox.MaxValueProperty, value); }
        }
        public double MinValue {
            get { return (double)GetValue(NumericTextBox.MinValueProperty); }
            set { SetValue(NumericTextBox.MinValueProperty, value); }
        }
        public int Precision {
            get { return (int)GetValue(NumericTextBox.PrecisionProperty); }
            set { SetValue(NumericTextBox.PrecisionProperty, value); }
        }
        public double Increment {
            get { return (double)GetValue(NumericTextBox.IncrementProperty); }
            set { SetValue(NumericTextBox.IncrementProperty, value); }
        }
        public double Value {
            get {
                double d;

                double.TryParse(this.Text, out d);

                return d;
            }
        }
        #endregion
        #region Events
        protected override void OnPreviewKeyDown(KeyEventArgs e) {
            if (e.Key == Key.Back || e.Key == Key.Delete) {
                int start = this.SelectionStart;
                int len = this.SelectionLength;
                int neg = (this.Text.Substring(0, 1) == "-" ? 1 : 0);

                this._IsApply = false;

                #region Modify Selections
                if (Keyboard.Modifiers == ModifierKeys.Control && len == 0) {
                    if (e.Key == Key.Back) {
                        len = start;
                        start = 0;
                    }
                    else if (e.Key == Key.Delete) {
                        len = this.Text.Length - start;
                    }
                }
                else if (len == 0) {
                    if (e.Key == Key.Back) {
                        if (start == 0) {
                            e.Handled = true;
                            this.SelectionLength = this.Text.Length;
                            base.OnPreviewKeyDown(e);
                            return;
                        }
                        else {
                            start--;
                            len = 1;
                        }
                    }
                    else if (e.Key == Key.Delete) {
                        if (start == this.Text.Length) {
                            e.Handled = true;
                            base.OnPreviewKeyDown(e);
                            return;
                        }
                        else {
                            len = 1;
                        }
                    }
                }
                else if (start == neg && len == 1 && this.Text.Substring(start, len) == "0") {
                    if (e.Key == Key.Back) {
                        if (neg == 1) {
                            this.Text = this.Text.Substring(1);
                            this.SelectionStart = 0;
                            this.SelectionLength = 1;
                        }
                        else {
                            this.SelectionStart = 0;
                            this.SelectionLength = 0;
                        }
                    }
                    else {
                        this.SelectionStart = neg + 1;
                        this.SelectionLength = 0;
                    }
                    e.Handled = true;
                    base.OnPreviewKeyDown(e);
                    return;
                }
                #endregion

                int index = start + len;
                if (this.Precision == -1) {
                    int dec = this.Text.LastIndexOf('.');

                    if (dec != -1 && start <= neg && index == dec) {
                        this.Text = this.Text.Substring(0, start) + "0" + this.Text.Substring(index);
                        ;
                        if (e.Key == Key.Back) {
                            this.SelectionStart = start;
                            this.SelectionLength = 1;
                        }
                        else {
                            this.SelectionStart = start + 1;
                            this.SelectionLength = 0;
                        }
                    }
                    else if (start <= neg && index == this.Text.Length) {
                        this.Text = this.Text.Substring(0, start) + "0";
                        this.SelectionStart = start;
                        this.SelectionLength = 1;
                    }
                    else {
                        this.Text = this.Text.Substring(0, start) + this.Text.Substring(index);
                        this.SelectionStart = start;
                        this.SelectionLength = 0;
                    }
                }
                else if (this.Precision == 0) {
                    if (start <= neg && index == this.Text.Length) {
                        this.Text = this.Text.Substring(0, start) + "0";
                        this.SelectionStart = start;
                        this.SelectionLength = 1;
                    }
                    else {
                        this.Text = this.Text.Substring(0, start) + this.Text.Substring(index);
                        this.SelectionStart = start;
                        this.SelectionLength = 0;
                    }
                }
                else {
                    int dec = this.Text.LastIndexOf('.');

                    if (start >= dec) {
                        string delete = PString.SafeSubstring(("." + string.Empty.PadRight(this.Precision, '0')), start - dec, len);

                        this.Text = this.Text.Substring(0, start) + delete + this.Text.Substring(start + len);
                        this.SelectionStart = (e.Key == Key.Back ? start : this.Text.Length - this.Precision + index - dec - 1);
                        this.SelectionLength = 0;
                    }
                    else if (start <= neg) {
                        if (index > dec) {
                            string delete = "0" + ("." + string.Empty.PadRight(this.Precision, '0')).Substring(0, index - dec);

                            this.Text = this.Text.Substring(0, start) + delete + this.Text.Substring(index);
                            if (e.Key == Key.Back) {
                                this.SelectionStart = start;
                                this.SelectionLength = 1;
                            }
                            else {
                                this.SelectionStart = this.Text.Length - this.Precision + index - dec - 1;
                                this.SelectionLength = 0;
                            }
                        }
                        else if (index == dec) {
                            this.Text = this.Text.Substring(0, start) + "0" + this.Text.Substring(index);
                            if (e.Key == Key.Back) {
                                this.SelectionStart = start;
                                this.SelectionLength = 1;
                            }
                            else {
                                this.SelectionStart = start + 1;
                                this.SelectionLength = 0;
                            }
                        }
                        else {
                            this.Text = this.Text.Substring(0, start) + this.Text.Substring(index);
                            this.SelectionStart = start;
                            this.SelectionLength = 0;
                        }
                    }
                    else {
                        if (index > dec) {
                            string delete = ("." + string.Empty.PadRight(this.Precision, '0')).Substring(0, index - dec);

                            this.Text = this.Text.Substring(0, start) + delete + this.Text.Substring(index);
                            this.SelectionStart = (e.Key == Key.Back ? start : this.Text.Length - this.Precision + index - dec - 1);
                            this.SelectionLength = 0;
                        }
                        else {
                            this.Text = this.Text.Substring(0, start) + this.Text.Substring(index);
                            this.SelectionStart = start;
                            this.SelectionLength = 0;
                        }
                    }
                }
                this._IsApply = false;
                e.Handled = true;
            }
            base.OnPreviewKeyDown(e);
        }
        protected override void OnPreviewTextInput(TextCompositionEventArgs e) {
            InputText(e.Text);
            e.Handled = true;
            base.OnPreviewTextInput(e);
        }
        private void OnSetData(object sender, DataObjectSettingDataEventArgs e) {
            e.CancelCommand();
        }
        private void OnPaste(object sender, DataObjectPastingEventArgs e) {
            InputText((string)e.SourceDataObject.GetData(DataFormats.UnicodeText));
            e.CancelCommand();
        }
        private void OnCut(object sender, ExecutedRoutedEventArgs e) {
            InputText(string.Empty);
            e.Handled = true;
        }
        private void OnUndo(object sender, ExecutedRoutedEventArgs e) {
            _IsApply = null;
            this.Undo();
            e.Handled = true;

        }
        private void OnRedo(object sender, ExecutedRoutedEventArgs e) {
            _IsApply = null;
            this.Redo();
            e.Handled = true;
        }
        protected override void OnLostFocus(RoutedEventArgs e) {
            double n;
            this._IsApply = false;
            if (double.TryParse(this.Text, out n)) {
                n = PMath.MaxMin(n, this.MinValue, this.MaxValue);
                this.Text = ApplyPrecision(n.ToString());
            }
            else {
                this.Text = ApplyPrecision(PMath.MaxMin(0, this.MinValue, this.MaxValue).ToString());
            }
            this._IsApply = true;
            base.OnLostFocus(e);
        }

        private void NumericTextbox_MouseWheel(object sender, MouseWheelEventArgs e) {
            double n = double.Parse(this.Text);
            double inc = this.Increment;

            if (Keyboard.Modifiers == (ModifierKeys.Shift | ModifierKeys.Control)) { inc *= 1000; }
            else if (Keyboard.Modifiers == ModifierKeys.Shift) { inc *= 100; }
            else if (Keyboard.Modifiers == ModifierKeys.Control) { inc *= 10; }

            if (e.Delta > 0) { n += inc; }
            else { n -= inc; }

            int pos;
            if (this.SelectionStart == 0) { pos = 0; }
            else if (this.SelectionStart == this.Text.Length) { pos = -1; }
            else { pos = this.Text.Length - this.SelectionStart; }

            n = PMath.MaxMin(n, this.MinValue, this.MaxValue);
            this._IsApply = false;
            if (this.Precision == -1) { this.Text = n.ToString(); }
            else { this.Text = PMath.StringRound(n, this.Precision); }
            this._IsApply = true;

            if (pos == 0) { this.SelectionStart = 0; }
            else if (pos == -1 || pos > this.Text.Length) { this.SelectionStart = this.Text.Length; }
            else { this.SelectionStart = Math.Max(0, this.Text.Length - pos); }
        }
        #endregion
        #region Private Methods
        private void InputText(string text) {
            int start = this.SelectionStart;
            int len = this.SelectionLength;

            string tmp = this.Text.Substring(0, start) + text + this.Text.Substring(start + len);
            int neg = (PString.SafeSubstring(tmp, 0, 1) == "-" ? 1 : 0);

            this._IsApply = null;
            if (!(text == "0" && start == neg && len == 0) && !(neg == 1 && this.MinValue >= 0)) {
                if (this.Precision == -1) {
                    if (tmp == "-") {
                        this.Text = "-0";
                        this.SelectionStart = 1;
                        this.SelectionLength = 1;
                    }
                    else if (tmp == ".") {
                        this.Text = "0.";
                        this.SelectionStart = 2;
                        this.SelectionLength = 0;
                    }
                    else if (text == "-" && (tmp == "-0" || PString.SafeSubstring(tmp, 0, 3) == "-0.")) {
                        this.Text = tmp;
                        this.SelectionStart = 1;
                        this.SelectionLength = 1;
                    }
                    else if (PString.SafeSubstring(tmp, 0, 2) == "-." && PString.IsNumeric(tmp.Substring(2) + "0", false, false)) {
                        this.Text = "-0." + PString.SafeSubstring(tmp, 2);
                        if (text == "-") {
                            this.SelectionStart = 1;
                            this.SelectionLength = 1;
                        }
                        else {
                            this.SelectionStart = start + text.Length + 1;
                            this.SelectionLength = 0;
                        }
                    }
                    else if (tmp.Substring(0, 1) == "." && PString.IsNumeric(tmp.Substring(1) + "0", false, false)) {
                        this.Text = "0" + tmp.Substring(0);
                        this.SelectionStart = start + text.Length + 1;
                        this.SelectionLength = 0;
                    }
                    else if (tmp.Substring(tmp.Length - 1) == "." && PString.IsNumeric(tmp.Substring(0, tmp.Length - 1), false, true)) {
                        this.Text = tmp;
                        this.SelectionStart = start + text.Length;
                        this.SelectionLength = 0;
                    }
                    else if (PString.IsNumeric(tmp, true, true)) {
                        this.Text = tmp;
                        this.SelectionStart = start + text.Length;
                        this.SelectionLength = 0;
                    }
                }
                else if (this.Precision == 0) {
                    if (tmp == "-") {
                        this.Text = "-0";
                        this.SelectionStart = 1;
                        this.SelectionLength = 1;
                    }
                    else if (text == "-" && tmp == "-0") {
                        this.Text = tmp;
                        this.SelectionStart = 1;
                        this.SelectionLength = 1;
                    }
                    else if (PString.IsNumeric(tmp, false, true)) {
                        this.Text = tmp;
                        this.SelectionStart = start + text.Length;
                        this.SelectionLength = 0;
                    }
                }
                else {
                    if (tmp == "-") {
                        this.Text = "-0." + string.Empty.PadRight(this.Precision, '0');
                        this.SelectionStart = 1;
                        this.SelectionLength = 1;
                    }
                    else if (tmp == ".") {
                        this.Text = "0." + string.Empty.PadRight(this.Precision, '0');
                        this.SelectionStart = 2;
                        this.SelectionLength = 0;
                    }
                    else if (text == "-" && start == 0 && PString.SafeSubstring(tmp, 0, 3) == "-0.") {
                        this.Text = tmp;
                        this.SelectionStart = 1;
                        this.SelectionLength = 1;
                    }
                    else if (text == "0" && start == 1 && PString.SafeSubstring(tmp, 0, 3) == "-0.") {
                        this.Text = tmp;
                        this.SelectionStart = 2;
                        this.SelectionLength = 0;
                    }
                    else if (PString.SafeSubstring(tmp, 0, 3) == "-0." && PString.IsNumeric(PString.SafeSubstring(tmp, 3), false, false) && int.Parse(tmp.Substring(3)) == 0) {
                        this.Text = "-0." + PString.SafeSubstring(tmp, 3, this.Precision).PadRight(this.Precision, '0');
                        this.SelectionStart = start + text.Length;
                        this.SelectionLength = 0;
                    }
                    // "-.34" -> -0.34
                    else if (PString.SafeSubstring(tmp, 0, 2) == "-." && PString.IsNumeric(tmp.Substring(2), false, false)) {
                        this.Text = "-0." + PString.SafeSubstring(tmp, 2, this.Precision).PadRight(this.Precision, '0');
                        if (text == "-") {
                            this.SelectionStart = 1;
                            this.SelectionLength = 1;
                        }
                        else {
                            this.SelectionStart = start + text.Length + 1;
                            this.SelectionLength = 0;
                        }
                    }
                    // "0|[3].34" -> 3|.34
                    else if (PString.IsNumeric(text, false, false) && start == neg + 1 && PString.SafeSubstring(tmp, neg, 1) == "0" && PString.SafeSubstring(tmp, neg + 2, 1) == ".") {
                        this.Text = tmp.Substring(0, neg) + tmp.Substring(neg + 1);
                        this.SelectionStart = neg + 1;
                        this.SelectionLength = 0;
                    }
                    else if (PString.IsNumeric(tmp, true, true)) {
                        double n;
                        int dec = tmp.LastIndexOf(".");
                        if (dec != -1) {
                            n = double.Parse(PString.SafeSubstring(tmp, 0, dec + 1 + this.Precision));
                        }
                        else {
                            n = double.Parse(tmp);
                        }

                        this.Text = PMath.StringRound(n, this.Precision);

                        // Correct for difference if user pastes things like "003" at the start or pastes over the decimal without including a new one
                        if (dec == -1) { dec = tmp.Length; }
                        int tdec = this.Text.LastIndexOf('.');
                        int difference = dec - tdec;

                        this.SelectionStart = start + text.Length - difference + (tmp.Substring(0, 1) == "." ? 1 : 0);
                        this.SelectionLength = 0;
                    }
                }
            }
            
            this._IsApply = true;
        }
        private bool IsValidText(string text) {
            bool neg = (this.MinValue < 0);
            bool dec = (this.Precision != 0);
            if (PString.IsNumeric(text, dec, neg)) {
                double n = double.Parse(text);
                
                if (PMath.IsBetween(n, this.MinValue, this.MaxValue)) {
                    return true;
                }
            }
            return false;
        }
        private string ApplyPrecision(string text) {
            if (this.Precision > 0) {
                return PMath.StringRound(double.Parse(text), this.Precision);
            }
            else if (this.Precision == 0) {
                int dec = text.LastIndexOf(".");
                if (dec != -1) {
                    return text.Substring(0, dec);
                }
            }
            return text;
        }
        #endregion
    }
}
