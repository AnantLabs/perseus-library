using System;
using System.Windows;
using System.Windows.Forms;

namespace Perseus.Windows {
    public static class Extensions {
        public static System.Drawing.Rectangle Rect32(this Window window) {
            return new System.Drawing.Rectangle(
                (int)Math.Round(window.Left, MidpointRounding.AwayFromZero),
                (int)Math.Round(window.Top, MidpointRounding.AwayFromZero),
                (int)Math.Round(window.ActualWidth, MidpointRounding.AwayFromZero),
                (int)Math.Round(window.ActualHeight, MidpointRounding.AwayFromZero)
            );
        }
        public static Rect Rect(this Window window) {
            return new Rect(
                window.Left,
                window.Top,
                window.ActualWidth,
                window.ActualHeight
            );
        }

        public static bool IsEmpty(this Point p) {
            return (p.X == 0 && p.Y == 0);
        }

        public static void EnsureOnScreen(this Window window) {
            var rect = window.Rect32();

            int x = rect.X;
            int y = rect.Y;
            bool updated = false;

            var screen = Screen.GetWorkingArea(rect);

            if (!screen.Contains(rect)) {
                if (rect.X < screen.X) {
                    x = screen.X;
                    updated = true;
                }
                else if (rect.X + rect.Width > screen.X + screen.Width) {
                    x = screen.X + screen.Width - rect.Width;
                    updated = true;
                }

                if (rect.Y < screen.Y) {
                    y = screen.Y;
                    updated = true;
                }
                else if (rect.Y + rect.Height > screen.Y + screen.Height) {
                    y = screen.Y + screen.Height - rect.Height;
                    updated = true;
                }

                if (updated) {
                    window.Left = x;
                    window.Top = y;
                }
            }
        }        
    }
}
