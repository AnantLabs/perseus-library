using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Perseus.Controls {
    public class HotKeyTextBox : TextBox {
        private bool _IsApply = true;
        private bool _ResolveKey = false;

        public HotKeyTextBox() {
            InputMethod.SetIsInputMethodEnabled(this, false);
            DataObject.AddPastingHandler(this, new DataObjectPastingEventHandler(OnPaste));
            DataObject.AddSettingDataHandler(this, new DataObjectSettingDataEventHandler(OnSetData));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Cut, OnCut));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Undo, OnUndo));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Redo, OnRedo));
            this.AllowDrop = false;
            this.ContextMenu = null;               
        }

        #region Dependency Properties
        public static readonly DependencyProperty KeyProperty;
        public static readonly DependencyProperty IsShiftProperty;
        public static readonly DependencyProperty IsControlProperty;
        public static readonly DependencyProperty IsAltProperty;
        //public static readonly DependencyProperty IsWindowsProperty;

        static HotKeyTextBox() {
            HotKeyTextBox.KeyProperty = DependencyProperty.Register(
                "Key",
                typeof(Key),
                typeof(HotKeyTextBox),
                new FrameworkPropertyMetadata(
                    Key.None,
                    FrameworkPropertyMetadataOptions.AffectsMeasure,
                    new PropertyChangedCallback(OnKeyChanged)
                ),
                new ValidateValueCallback(IsValidKey)
            );

            HotKeyTextBox.IsControlProperty = DependencyProperty.Register(
                "IsControl",
                typeof(bool),
                typeof(HotKeyTextBox),
                new FrameworkPropertyMetadata(
                    false,
                    FrameworkPropertyMetadataOptions.AffectsMeasure,
                    new PropertyChangedCallback(OnIsControlChanged)
                )
            );
            HotKeyTextBox.IsShiftProperty = DependencyProperty.Register(
                "IsShift",
                typeof(bool),
                typeof(HotKeyTextBox),
                new FrameworkPropertyMetadata(
                    false,
                    FrameworkPropertyMetadataOptions.AffectsMeasure,
                    new PropertyChangedCallback(OnIsShiftChanged)
                )
            );
            HotKeyTextBox.IsAltProperty = DependencyProperty.Register(
                "IsAlt",
                typeof(bool),
                typeof(HotKeyTextBox),
                new FrameworkPropertyMetadata(
                    false,
                    FrameworkPropertyMetadataOptions.AffectsMeasure,
                    new PropertyChangedCallback(OnIsAltChanged)
                )
            );
            //HotKeyTextBox.IsWindowsProperty = DependencyProperty.Register(
            //    "IsWindows",
            //    typeof(bool),
            //    typeof(HotKeyTextBox),
            //    new FrameworkPropertyMetadata(
            //        false,
            //        FrameworkPropertyMetadataOptions.AffectsMeasure,
            //        new PropertyChangedCallback(OnIsWindowsChanged)
            //    )
            //);

            TextProperty.OverrideMetadata(
                typeof(HotKeyTextBox), 
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnTextChanged))
            );
        }

        public static bool IsValidKey(object value) {
            Key k = (Key)value;
            switch (k) {
                case Key.Escape:
                case Key.Apps:
                case Key.Tab:
                case Key.Clear:
                case Key.Capital:
                case Key.Scroll:                
                case Key.NumLock:
                case Key.LeftCtrl:
                case Key.RightCtrl:
                case Key.LeftShift:
                case Key.RightShift:
                case Key.LeftAlt:
                case Key.RightAlt:
                case Key.LWin:
                case Key.RWin:
                    return false;
            }

            return true;
        }

        private static void OnKeyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e) {
            HotKeyTextBox nt = (HotKeyTextBox)o;
            nt.UpdateText(false);
        }
        private static void OnIsControlChanged(DependencyObject o, DependencyPropertyChangedEventArgs e) {
            HotKeyTextBox nt = (HotKeyTextBox)o;
            nt.UpdateText(true);
        }
        private static void OnIsShiftChanged(DependencyObject o, DependencyPropertyChangedEventArgs e) {
            HotKeyTextBox nt = (HotKeyTextBox)o;
            nt.UpdateText(true);
        }
        private static void OnIsAltChanged(DependencyObject o, DependencyPropertyChangedEventArgs e) {
            HotKeyTextBox nt = (HotKeyTextBox)o;
            nt.UpdateText(true);
        }
        //private static void OnIsWindowsChanged(DependencyObject o, DependencyPropertyChangedEventArgs e) {
        //    HotKeyTextBox nt = (HotKeyTextBox)o;
        //    nt.UpdateText();
        //}

        private static void OnTextChanged(DependencyObject o, DependencyPropertyChangedEventArgs e) {
            HotKeyTextBox nt = (HotKeyTextBox)o;
            
            if (nt._IsApply && e.NewValue != e.OldValue) { // If value was set via its property                
                nt._IsApply = false;

                nt.IsControl = false;
                nt.IsShift = false;
                nt.IsAlt = false;
                //nt.IsWindows = false;
                nt.Key = Key.None;

                string[] parts = PString.Split(
                    (string)e.NewValue,
                    "+",
                    SplitOptions.Trim
                );

                string text_key = string.Empty;
                foreach (string part in parts) {
                    Key key = PInput.ResolveKey(part);
                    if (key == Key.LeftCtrl || key == Key.RightCtrl) {
                        nt.IsControl = true;
                    }
                    else if (key == Key.LeftShift || key == Key.RightShift) {
                        nt.IsShift = true;
                    }
                    else if (key == Key.LeftAlt || key == Key.RightAlt) {
                        nt.IsAlt = true;
                    }
                    //else if (key == Key.LWin || key == Key.RWin) {
                    //    nt.IsWindows = true;
                    //}
                    else if (key != Key.None && HotKeyTextBox.IsValidKey(key)) {
                        text_key = part;
                        nt.Key = key;                                                
                    }
                }

                nt._IsApply = true;

                nt.UpdateText(text_key);
            }

            nt.CaretIndex = nt.Text.Length;
        }

        public Key Key {
            get { return (Key)GetValue(HotKeyTextBox.KeyProperty); }
            set { SetValue(HotKeyTextBox.KeyProperty, value); }
        }
        public bool IsControl {
            get { return (bool)GetValue(HotKeyTextBox.IsControlProperty); }
            set { SetValue(HotKeyTextBox.IsControlProperty, value); }
        }
        public bool IsShift {
            get { return (bool)GetValue(HotKeyTextBox.IsShiftProperty); }
            set { SetValue(HotKeyTextBox.IsShiftProperty, value); }
        }
        public bool IsAlt {
            get { return (bool)GetValue(HotKeyTextBox.IsAltProperty); }
            set { SetValue(HotKeyTextBox.IsAltProperty, value); }
        }
        //public bool IsWindows {
        //    get { return (bool)GetValue(HotKeyTextBox.IsWindowsProperty); }
        //    set { SetValue(HotKeyTextBox.IsWindowsProperty, value); }
        //}
        #endregion

        public string KeyValue { get; protected set; }
        public ModifierKeys Modifiers {
            get {
                ModifierKeys m = ModifierKeys.None;
                if (this.IsControl) {
                    m |= ModifierKeys.Control;
                }
                if (this.IsShift) {
                    m |= ModifierKeys.Shift;
                }
                if (this.IsAlt) {
                    m |= ModifierKeys.Alt;
                }
                return m;
            }
            set {
                this._IsApply = false;
                if ((value | ModifierKeys.Control) == value) {
                    this.IsControl = true;
                }
                if ((value | ModifierKeys.Shift) == value) {
                    this.IsShift = true;
                }
                if ((value | ModifierKeys.Alt) == value) {
                    this.IsAlt = true;
                }
                this._IsApply = true;
                this.UpdateText(true);
            }
        }

        #region Events
        protected override void OnPreviewKeyDown(KeyEventArgs e) {
            if (!this._IsApply) {
                return;
            }

            Key k = e.Key;
            if (k == Key.System) {
                k = e.SystemKey;
            }

            this._ResolveKey = false;

            if (k == Key.Escape) {
                this.Clear();
            }
            else if (k == Key.LeftCtrl || k == Key.RightCtrl) {
                this.IsControl = !this.IsControl;
            }
            else if (k == Key.LeftShift || k == Key.RightShift) {
                this.IsShift = !this.IsShift;
            }
            else if (k == Key.LeftAlt || k == Key.RightAlt) {
                this.IsAlt = !this.IsAlt;
            }
            //else if (k == Key.LWin || k == Key.RWin) {
            //    this.IsWindows = !this.IsWindows;
                //this._IsApply = false;
                //keybd_event(91, 0, 0, 0);
                //keybd_event(91, 0, 0x2, 0);
                
                //this._IsApply = true;
            //}
            else if (HotKeyTextBox.IsValidKey(k) && Keyboard.Modifiers == ModifierKeys.None) {                
                // Becuase keys like Oem6 are meaningless to most people, 
                // get actual text typed from preview text event
                if (k.ToString().StartsWith("Oem")) {
                    this._ResolveKey = true;
                    this._IsApply = false;
                    this.Key = e.Key;
                    this._IsApply = true;                
                }
                else {
                    if (this.Key == e.Key) {
                        this.UpdateText(false);
                    }
                    else {
                        this.Key = e.Key;
                    }
                }
            }

            // Continue to text input
            if (!this._ResolveKey && k != Key.Tab) {
                e.Handled = true;
            }
            base.OnPreviewKeyDown(e);
        }
        protected override void OnPreviewTextInput(TextCompositionEventArgs e) {
            if (this._ResolveKey) {               
                this.UpdateText(e.Text);
                this._ResolveKey = false;
            }            
            e.Handled = true;                        
            base.OnPreviewTextInput(e);
        }
        private void OnSetData(object sender, DataObjectSettingDataEventArgs e) {
            e.CancelCommand();
        }
        private void OnPaste(object sender, DataObjectPastingEventArgs e) {            
            e.CancelCommand();            
        }
        private void OnCut(object sender, ExecutedRoutedEventArgs e) {
            this.Text = string.Empty;
            e.Handled = true;            
        }
        private void OnUndo(object sender, ExecutedRoutedEventArgs e) {
            e.Handled = true;
        }
        private void OnRedo(object sender, ExecutedRoutedEventArgs e) {
            e.Handled = true;
        }
        #endregion

        private void UpdateText(bool sameKey) {
            this.UpdateText(string.Empty, sameKey);
        }
        private void UpdateText(string key) {
            this.UpdateText(key, false);
        }
        private void UpdateText(string key, bool sameKey) {
            if (!this._IsApply) {
                return;
            }

            string hotkey = string.Empty;

            if (this.IsControl) {
                hotkey += "Ctrl + ";
            }
            if (this.IsShift) {
                hotkey += "Shift + ";
            }
            if (this.IsAlt) {
                hotkey += "Alt + ";
            }
            //if (this.IsWindows) {
            //    hotkey += "Win + ";
            //}
            if (this.Key != Key.None) {
                if (sameKey) {
                    key = this.Text.Replace("Ctrl +", "").Replace("Shift +", "").Replace("Alt +", "").Trim();
                }
                else if (key == string.Empty) {
                    key = this.FormatKey(this.Key);
                }
                this.KeyValue = key;
                hotkey += key + " ";
            }
            else {
                this.KeyValue = string.Empty;
            }

            this._IsApply = false;
            this.Text = hotkey;
            this._IsApply = true;            
        }

        private string FormatKey(Key key) {
            string k = this.Key.ToString();

            // If it's a number key, show remove the D from infront of it.
            if (k.Length == 2 && k.Substring(0, 1) == "D") {
                return k.Substring(1);
            }

            if (k.SafeSubstring(0, 6) == "NumPad") {
                return "Num " + k.Substring(6);
            }

            switch (k) {
                case "Divide":
                    return "Num /";
                case "Multiply":
                    return "Num *";
                case "Subtract":
                    return "Num -";
                case "Add":
                    return "Num +";
                case "Decimal":
                    return "Num .";
                case "Return":
                    return "Enter";
                case "PageUp":
                case "Prior":
                    return "Page Up";
                case "PageDown":
                case "Next":
                    return "Page Down";
                case "Back":
                    return "Backspace";
            }

            return k;
        }
    }
}
