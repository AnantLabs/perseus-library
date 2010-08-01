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
            if (element == null) {
                return string.Empty;
            }
            else { return element.Value; }
        }
        public static string ValueOrEmpty(this XAttribute attribute) {
            if (attribute == null) {
                return string.Empty;
            }
            else { return attribute.Value; }
        }
        public static bool ValueOrFalse(this XElement element) {
            if (element != null && element.Value.ToLower() == "true") {
                return true;
            }

            return false;
        }
        public static bool ValueOrFalse(this XAttribute attribute) {
            if (attribute != null && attribute.Value.ToLower() == "true") {
                return true;
            }

            return false;
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
