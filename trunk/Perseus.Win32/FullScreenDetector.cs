using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Threading;

namespace Perseus.Win32 {
    /// <summary>
    /// Class for detecting when the currently focused window enter and exits full screen mode
    /// 
    /// Code based on the code at:
    /// http://www.richard-banks.org/2007/09/how-to-detect-if-another-application-is.html
    /// </summary>
    public class FullScreenDetector {
        private static IntPtr _DesktopHandle; //Window handle for the desktop    
        private static IntPtr _ShellHandle; //Window handle for the shell
        private DispatcherTimer _Timer;
        private bool _State;

        static FullScreenDetector() {
            FullScreenDetector._DesktopHandle = User32.GetDesktopWindow();
            FullScreenDetector._ShellHandle = User32.GetShellWindow();
        }

        public FullScreenDetector() : this(1000) {}
        public FullScreenDetector(int interval) {
            if (interval <= 0) {
                throw new ArgumentOutOfRangeException("Interval must be greater than or equal to 1");
            }

            this._Timer = new DispatcherTimer();
            this._Timer.Tick += new EventHandler(Timer_Tick);
            this._Timer.Interval = TimeSpan.FromMilliseconds(interval);

            this._State = FullScreenDetector.IsFullScreen();
        }

        private void Timer_Tick(object sender, EventArgs e) {
            bool state = this._State;
            this._State = FullScreenDetector.IsFullScreen();
            if (this._State) {
                if (state == false && this.FullScreenEnter != null) {
                    this.FullScreenEnter(this, EventArgs.Empty);                                            
                }
            }
            else {
                if (state == true && this.FullScreenExit != null) {
                    this.FullScreenExit(this, EventArgs.Empty);                    
                }
            }
        }

        public event EventHandler FullScreenEnter;
        public event EventHandler FullScreenExit;

        public bool State {
            get { return this._State; }
            set { this._State = value; }
        }

        public bool IsEnabled {
            get { return this._Timer.IsEnabled; }
            set { this._Timer.IsEnabled = value; }
        }        
        public static bool IsFullScreen() {
            RECT appBounds;
            Rectangle screenBounds;
            IntPtr hWnd;

            //get the dimensions of the active window    
            hWnd = User32.GetForegroundWindow();
            if (hWnd != null && !hWnd.Equals(IntPtr.Zero)) {
                //Check we haven't picked up the desktop or the shell        
                if (!(hWnd.Equals(FullScreenDetector._DesktopHandle) || hWnd.Equals(FullScreenDetector._ShellHandle))) {
                    User32.GetWindowRect(hWnd, out appBounds);

                    //determine if window is fullscreen            
                    screenBounds = Screen.FromHandle(hWnd).Bounds;
                    if ((appBounds.Bottom - appBounds.Top) == screenBounds.Height && 
                        (appBounds.Right - appBounds.Left) == screenBounds.Width
                    ) {                        
                        return true;
                    }
                }
            }
            
            return false;
        }
    }
}
