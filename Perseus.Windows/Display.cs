using System;
using System.Windows;
using System.Windows.Forms;

namespace Perseus.Windows {
    public static class Display {
        public static Screen FromIndex(int display) {
            if (display >= 0 && display < Screen.AllScreens.Length) {
                return Screen.AllScreens[display];
            }

            return Screen.PrimaryScreen;
        }

        public static Point Position(int display, string alignment, double left, double top, double width, double height) {
            return Display.Position(Display.FromIndex(display), alignment, left, top, width, height);
        }
        public static Point Position(Screen display, string alignment, double left, double top, double width, double height) {         
            var displayRect = display.WorkingArea;
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
