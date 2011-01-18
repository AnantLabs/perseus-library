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

        public static void EnsureOnScreen(Window window) {
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

        public static Screen Display(int monitor) {
            if (monitor >= 0 && monitor < Screen.AllScreens.Length) {
                return Screen.AllScreens[monitor];
            }

            return Screen.PrimaryScreen;
        }

        public static Point Position(int display, string alignment, double left, double top, double width, double height) {
            var displayRect = Extensions.Display(display).WorkingArea;
            alignment = alignment.ToLower();

            double x = displayRect.X;
            double y = displayRect.Y;

            switch (alignment) {
                case "left top":
                case "top left":
                    x += left;
                    y += top;
                    break;
                case "centre top":
                case "center top":
                case "top centre":
                case "top center":
                    x += ((displayRect.Width - width) / 2) + left;
                    y += top;
                    break;
                case "right top":
                case "top right":
                    x += displayRect.Width - width + left;
                    y += top;
                    break;
                case "left centre":
                case "left center":
                case "centre left":
                case "center left":
                    x += left;
                    y += ((displayRect.Height - height) / 2) + top;
                    break;
                case "right centre":
                case "right center":
                case "centre right":
                case "center right":
                    x += displayRect.Width - width + left;
                    y += ((displayRect.Height - height) / 2) + top;
                    break;
                case "left bottom":
                case "bottom left":
                    x += left;
                    y += displayRect.Height - height + top;
                    break;
                case "centre bottom":
                case "center bottom":
                case "bottom centre":
                case "bottom center":
                    x += ((displayRect.Width - width) / 2) + left;
                    y += displayRect.Height - height + top;
                    break;
                case "right bottom":
                case "bottom right":
                    x += displayRect.Width - width + left;
                    y += displayRect.Height - height + top;
                    break;
                default: // Centre
                    x += ((displayRect.Width - width) / 2) + left;
                    y += ((displayRect.Height - height) / 2) + top;
                    break;
            }

            return new System.Windows.Point(x, y);
        }
    }
}
