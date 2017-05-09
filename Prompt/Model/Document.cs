using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prompt.Model
{
    using System.Windows;
    using System.Windows.Media;

    public class Document : Formattable
    {
        private List<Paragraph> _paragraphs;
    }

    public class Paragraph : Formattable
    {
        private List<Run> _runs;

    }

    public class Run : Formattable
    {
        public static Run Empty => new Run(null);

        public Run(string text)
        {
            this.Text = text;
        }

        public string Text { get; }
        public string FormattedText { get; }
    }

    public class Formattable : IFormattable
    {
        public FontFamily Font { get; set; }
        public bool IsItalic { get; set; }
        public bool IsUnderlined { get; set; }
        public bool IsBold { get; set; }
        public Color FontColor { get; set; }
        public bool IsAllCaps { get; set; }
    }

    public interface IFormattable
    {
        FontFamily Font { get; set; }
        bool IsItalic { get; set; }
        bool IsUnderlined { get; set; }
        bool IsBold { get; set; }
        Color FontColor { get; set; }
        bool IsAllCaps { get; set; }
    }
}
