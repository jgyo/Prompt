// Copyright (c) 2017 Gil Yoder <gil.yoder@oabs.org>
// 
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without 
// modification, are permitted provided that the following conditions 
// are met:
// 
// * Redistributions of source code must retain the above copyright notice, 
//   this list of conditions and the following disclaimer. 
// 
// * Redistributions in binary form must reproduce the above copyright notice,
//   this list of conditions and the following disclaimer in the documentation
//   and/or other materials provided with the distribution. 
// 
// * Neither the name of Prompt nor the names of its 
//   contributors may be used to endorse or promote products derived from this
//   software without specific prior written permission. 
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF 
// THE POSSIBILITY OF SUCH DAMAGE.

namespace Prompt.View
{
    using System;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Media;
    using GalaSoft.MvvmLight.Messaging;
    using ViewModel;

    /// <summary>
    ///     Interaction logic for EditorControl.xaml
    /// </summary>
    public partial class EditorControl : UserControl
    {
        public static readonly DependencyProperty ColumnProperty = DependencyProperty.Register(
            "Column", typeof(int), typeof(EditorControl), new PropertyMetadata(default(int)));

        public static readonly DependencyProperty RowProperty = DependencyProperty.Register(
            "Row", typeof(int), typeof(EditorControl), new PropertyMetadata(default(int)));

        public static readonly DependencyProperty LineCountProperty = DependencyProperty.Register(
            "LineCount", typeof(int), typeof(EditorControl), new PropertyMetadata(default(int)));

        public static readonly DependencyProperty CountOfCharsProperty = DependencyProperty.Register(
            "CountOfChars", typeof(int), typeof(EditorControl), new PropertyMetadata(default(int)));

        public static readonly DependencyProperty CountOfWordsProperty = DependencyProperty.Register(
            "CountOfWords", typeof(int), typeof(EditorControl), new PropertyMetadata(default(int)));

        public static readonly DependencyProperty SelectionLengthProperty = DependencyProperty.Register(
            "SelectionLength", typeof(int), typeof(EditorControl), new PropertyMetadata(default(int)));

        public static readonly DependencyProperty IsSelectionEmptyProperty = DependencyProperty.Register(
            "IsSelectionEmpty", typeof(bool), typeof(EditorControl), new PropertyMetadata(true));

        public static readonly DependencyProperty FilePathProperty = DependencyProperty.Register(
            "FilePath", typeof(string), typeof(EditorControl), new PropertyMetadata(default(string)));

        public EditorControl()
        {
            InitializeComponent();
            Messenger.Default.Register<CommandMessage>(this, HandleCommandMessage);
        }

        public int LineCount
        {
            get => (int) GetValue(LineCountProperty);
            set => SetValue(LineCountProperty, value);
        }

        public string FilePath
        {
            get => (string) GetValue(FilePathProperty);
            set => SetValue(FilePathProperty, value);
        }

        public int Column
        {
            get => (int) GetValue(ColumnProperty);
            set => SetValue(ColumnProperty, value);
        }

        public int Row
        {
            get => (int) GetValue(RowProperty);
            set => SetValue(RowProperty, value);
        }

        public int CountOfChars
        {
            get => (int) GetValue(CountOfCharsProperty);
            set => SetValue(CountOfCharsProperty, value);
        }

        public int CountOfWords
        {
            get => (int) GetValue(CountOfWordsProperty);
            set => SetValue(CountOfWordsProperty, value);
        }

        public int SelectionLength
        {
            get => (int) GetValue(SelectionLengthProperty);
            set => SetValue(SelectionLengthProperty, value);
        }

        public bool IsSelectionEmpty
        {
            get => (bool) GetValue(IsSelectionEmptyProperty);
            set => SetValue(IsSelectionEmptyProperty, value);
        }

        private void HandleCommandMessage(CommandMessage obj)
        {
            var textSelection = RichTextBox.Selection;
            switch (obj.Command)
            {
                case CommandMessage.Commands.Open:
                    break;
                case CommandMessage.Commands.Save:
                    break;
                case CommandMessage.Commands.SaveAs:
                    break;
                case CommandMessage.Commands.SpellCheck:
                    break;
                case CommandMessage.Commands.Delete:
                    DeleteSelection(textSelection);
                    break;
                case CommandMessage.Commands.IncreaseFont:
                    IncreaseFontSize(textSelection);
                    break;
                case CommandMessage.Commands.DecreaseFont:
                    DecreaseFontSize(textSelection);
                    break;
                case CommandMessage.Commands.FontColor:
                    ColorText(textSelection);
                    break;
                case CommandMessage.Commands.Highlight:
                    HighlightSelected(textSelection);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void DeleteSelection(TextSelection textSelection)
        {
            if (textSelection == null || textSelection.IsEmpty)
                return;

            textSelection.Text = string.Empty;
        }

        private void ColorText(TextSelection textSelection)
        {
            if (textSelection == null || textSelection.IsEmpty)
                return;

            var brush = textSelection.GetPropertyValue(TextElement.ForegroundProperty) as SolidColorBrush;
            Color color;
            if (brush != null)
                color = brush.Color;
            else
                color = Colors.Black;

            var colorPicker = new ColorPicker(color);
            colorPicker.ShowDialog();

            if (colorPicker.DialogResult != true)
                return;

            brush = new SolidColorBrush(colorPicker.SelectedColor);
            textSelection.ApplyPropertyValue(TextElement.ForegroundProperty, brush);
        }

        private void DecreaseFontSize(TextSelection textSelection)
        {
            if (textSelection == null || textSelection.IsEmpty)
                return;

            var size = textSelection.GetPropertyValue(TextElement.FontSizeProperty) as double?;
            if (!size.HasValue)
                size = 12d;
            size -= size / 4;
            textSelection.ApplyPropertyValue(TextElement.FontSizeProperty, size);
        }

        private void IncreaseFontSize(TextSelection textSelection)
        {
            if (textSelection == null || textSelection.IsEmpty)
                return;

            var size = textSelection.GetPropertyValue(TextElement.FontSizeProperty) as double?;
            if (!size.HasValue)
                size = 12d;
            size += size / 3;
            textSelection.ApplyPropertyValue(TextElement.FontSizeProperty, size);
        }

        private void HighlightSelected(TextSelection textSelection)
        {
            if (textSelection == null || textSelection.IsEmpty)
                return;

            var brush = textSelection.GetPropertyValue(TextElement.BackgroundProperty) as SolidColorBrush;
            if (brush == null)
                textSelection.ApplyPropertyValue(TextElement.BackgroundProperty,
                    new SolidColorBrush(Colors.LightGreen));
            else
                textSelection.ApplyPropertyValue(TextElement.BackgroundProperty,
                    brush.Color == Colors.LightGreen
                        ? new SolidColorBrush(Colors.White)
                        : new SolidColorBrush(Colors.LightGreen));
        }

        private void RichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var richTextBox = sender as RichTextBox;

            var textSelection = richTextBox?.Selection;
            if (textSelection == null)
                return;

            GetRowAndColumnPositions(richTextBox);
            GetNumberOfLines(richTextBox);
            GetSelectionStats(richTextBox);
            GetCountOfWordsAndChars(richTextBox);
            // Get a count of words
        }

        private void GetCountOfWordsAndChars(RichTextBox richTextBox)
        {
            var regex = new Regex("\\b.\\w*?\\b", RegexOptions.Compiled);
            var text = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text;
            var count = regex.Matches(text).Count;
            SetCurrentValue(CountOfWordsProperty, count);
            SetCurrentValue(CountOfCharsProperty, text.Length);
        }

        private void GetSelectionStats(RichTextBox richTextBox)
        {
            var selectionIsEmpty = richTextBox.Selection.IsEmpty;
            SetCurrentValue(IsSelectionEmptyProperty, selectionIsEmpty);
            if (!selectionIsEmpty)
            {
                var offsetToPosition = richTextBox.Selection.Start.GetOffsetToPosition(richTextBox.Selection.End);
                SetCurrentValue(SelectionLengthProperty, offsetToPosition);
            }
            else
            {
                SetCurrentValue(SelectionLengthProperty, 0);
            }
        }

        private void GetNumberOfLines(RichTextBox richTextBox)
        {
            var firstLinePos = richTextBox.Document.ContentStart.GetLineStartPosition(0);
            var actualCount = 1;
            firstLinePos?.GetLineStartPosition(int.MaxValue, out actualCount);

            SetCurrentValue(LineCountProperty, actualCount + 1);
        }

        private void GetRowAndColumnPositions(RichTextBox richTextBox)
        {
            var lineStart = richTextBox.CaretPosition.GetLineStartPosition(0);
            var caretPosition = richTextBox.CaretPosition;

            var column = 0;
            if (lineStart != null)
                column = lineStart.GetOffsetToPosition(caretPosition);

            richTextBox.CaretPosition.GetLineStartPosition(int.MinValue, out var actualCount);
            var caretInFirstParagraphLine = true;
            if (richTextBox.CaretPosition.Paragraph != null)
            {
                richTextBox.CaretPosition.Paragraph.ContentStart.GetLineStartPosition(int.MinValue,
                    out var actualCount2);
                caretInFirstParagraphLine = actualCount == actualCount2;
                //richTextBox.CaretPosition.Paragraph.ContentStart.GetLineStartPosition(int.MaxValue, out var actual );
            }

            column = column - (caretInFirstParagraphLine ? 1 : 0);
            column = column < 0 ? 0 : column;
            SetCurrentValue(ColumnProperty, column);
            SetCurrentValue(RowProperty, -actualCount + 1);
        }
    }
}