using System;
using System.Windows.Documents;

namespace Perseus.Data {
    public static class Extensions {
        public static string Text(this InlineCollection inlines) {
            string text = string.Empty;

            foreach (Inline i in inlines) {
                if (i is Run) {
                    text += ((Run)i).Text;
                }
                else if (i is Span) {
                    text += Extensions.Text(((Span)i).Inlines);
                }
            }

            return text;
        }
    }
}
