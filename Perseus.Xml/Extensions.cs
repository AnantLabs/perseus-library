using System;
using System.Xml.Linq;

namespace Perseus.Xml {
    public static class Extensions {
        public static string ValueOr(this XElement element, string value) {
            if (element == null) {
                return value;
            }
            else { return element.Value; }
        }
        public static string ValueOr(this XAttribute attribute, string value) {
            if (attribute == null) {
                return value;
            }
            else { return attribute.Value; }
        }
        public static string ValueOrEmpty(this XElement element) {
            return Extensions.ValueOr(element, string.Empty);            
        }
        public static string ValueOrEmpty(this XAttribute attribute) {
            return Extensions.ValueOr(attribute, string.Empty);            
        }

        [Obsolete("Use BoolOrFalse instead", true)]      
        public static bool ValueOrFalse(this XElement element) {
            return Extensions.BoolOrFalse(element);
        }
        [Obsolete("Use BoolOrFalse instead", true)]
        public static bool ValueOrFalse(this XAttribute attribute) {
            return Extensions.BoolOrFalse(attribute);
        }

        public static bool BoolOr(this XElement element, bool value) {
            if (element != null) {
                if (element.Value.ToLower() == "true") {
                    return true;
                }
                else if (element.Value.ToLower() == "false") {
                    return false;
                }
            }

            return value;
        }
        public static bool BoolOr(this XAttribute attribute, bool value) {
            if (attribute != null) {
                if (attribute.Value.ToLower() == "true") {
                    return true;
                }
                else if (attribute.Value.ToLower() == "false") {
                    return false;
                }
            }

            return value;
        }
        public static bool BoolOrFalse(this XElement element) {
            return Extensions.BoolOr(element, false);
        }
        public static bool BoolOrFalse(this XAttribute attribute) {
            return Extensions.BoolOr(attribute, false);
        }

        public static int IntOr(this XElement element, int value) {
            if (element != null) {
                int i;
                if (int.TryParse(element.Value, out i)) {
                    return i;
                }
            }

            return value;
        }
        public static int IntOr(this XAttribute attribute, int value) {
            if (attribute != null) {
                int i;
                if (int.TryParse(attribute.Value, out i)) {
                    return i;
                }
            }

            return value;
        }
        public static int IntOrZero(this XElement element) {
            return Extensions.IntOr(element, 0);
        }
        public static int IntOrZero(this XAttribute attribute) {
            return Extensions.IntOr(attribute, 0);
        }

        public static double DoubleOr(this XElement element, double value) {
            if (element != null) {
                double d;
                if (double.TryParse(element.Value, out d)) {
                    return d;
                }
            }

            return value;
        }
        public static double DoubleOr(this XAttribute attribute, double value) {
            if (attribute != null) {
                double d;
                if (double.TryParse(attribute.Value, out d)) {
                    return d;
                }
            }

            return value;
        }
        public static double DoubleOrZero(this XElement element) {
            return Extensions.DoubleOr(element, 0);            
        }
        public static double DoubleOrZero(this XAttribute attribute) {
            return Extensions.DoubleOr(attribute, 0);
        }

        public static string AttributeFirstValue(this XElement element, params string[] names) {
            string value;

            foreach (string name in names) {
                value = element.Attribute(name).ValueOrEmpty();
                if (!string.IsNullOrEmpty(value)) {
                    return value;
                }
            }

            return string.Empty;
        }
    }
}
