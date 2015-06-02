using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elias
{
    public class MarkdownStringBuilder
    {
        public enum HeaderLevel
        {
            First,
            Second,
            Third
        }

        private readonly StringBuilder builder = new StringBuilder();

        private bool hasTableBeenStarted;

        public void WriteHeader(string text,
            HeaderLevel level = HeaderLevel.First)
        {
            var headerStyle = string.Empty;

            switch (level)
            {
                case HeaderLevel.First:
                    headerStyle = "# ";
                    break;
                case HeaderLevel.Second:
                    headerStyle = "## ";
                    break;
                default:
                    headerStyle = "### ";
                    break;
            }

            this.builder.AppendFormat("{1}{0}\r\n\r\n", text, headerStyle);
        }

        public void WriteText(string text)
        {
            this.builder.AppendFormat("{0}\r\n\r\n", text);
        }

        [Obsolete("Use the WriteTableRow(params) overload.")]
        public void WriteTableRow(IEnumerable<string> columns)
        {
            this.WriteTableRow(columns.ToArray());
        }

        public void WriteTableRow(params string[] columns)
        {
            this.builder.AppendFormat("{0}\r\n", string.Join("|", columns));
            if (!this.hasTableBeenStarted)
            {
                this.builder.Append("-------|\r\n");
                this.hasTableBeenStarted = true;
            }
        }

        public void EndTable()
        {
            this.builder.AppendLine();
            this.builder.AppendLine();
            this.hasTableBeenStarted = false;
        }

        public override string ToString()
        {
            return this.builder.ToString();
        }
    }


}
