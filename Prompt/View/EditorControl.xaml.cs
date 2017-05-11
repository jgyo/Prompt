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
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using Commands;
    using GalaSoft.MvvmLight.CommandWpf;
    using GalaSoft.MvvmLight.Threading;
    using Microsoft.Win32;
    using Properties;
    using ViewModel;

    /// <summary>
    /// Interaction logic for EditorControl.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class EditorControl : UserControl
    {
        /// <summary>
        /// The column property
        /// </summary>
        public static readonly DependencyProperty ColumnProperty = DependencyProperty.Register(
            "Column", typeof(int), typeof(EditorControl), new PropertyMetadata(default(int)));

        /// <summary>
        /// The commands property
        /// </summary>
        public static readonly DependencyProperty CommandsProperty = DependencyProperty.Register(
            "Commands", typeof(EditCommands), typeof(EditorControl), new PropertyMetadata(default(EditCommands)));

        /// <summary>
        /// The count of chars property
        /// </summary>
        public static readonly DependencyProperty CountOfCharsProperty = DependencyProperty.Register(
            "CountOfChars", typeof(int), typeof(EditorControl), new PropertyMetadata(default(int)));

        /// <summary>
        /// The count of words property
        /// </summary>
        public static readonly DependencyProperty CountOfWordsProperty = DependencyProperty.Register(
            "CountOfWords", typeof(int), typeof(EditorControl), new PropertyMetadata(default(int)));

        /// <summary>
        /// The file path property
        /// </summary>
        public static readonly DependencyProperty FilePathProperty = DependencyProperty.Register(
            "FilePath", typeof(string), typeof(EditorControl), new PropertyMetadata(default(string)));

        /// <summary>
        /// The font background color property
        /// </summary>
        public static readonly DependencyProperty FontBackgroundColorProperty = DependencyProperty.Register(
            "FontBackgroundColor", typeof(SolidColorBrush), typeof(EditorControl),
            new PropertyMetadata(default(SolidColorBrush)));

        /// <summary>
        /// The font color property
        /// </summary>
        public static readonly DependencyProperty FontColorProperty = DependencyProperty.Register(
            "FontColor", typeof(SolidColorBrush), typeof(EditorControl),
            new PropertyMetadata(default(SolidColorBrush)));

        /// <summary>
        /// The highlight color property
        /// </summary>
        public static readonly DependencyProperty HighlightColorProperty = DependencyProperty.Register(
            "HighlightColor", typeof(SolidColorBrush), typeof(EditorControl),
            new PropertyMetadata(new SolidColorBrush(Colors.Yellow)));

        /// <summary>
        /// The is bold property
        /// </summary>
        public static readonly DependencyProperty IsBoldProperty = DependencyProperty.Register(
            "IsBold", typeof(bool), typeof(EditorControl), new PropertyMetadata(default(bool)));

        /// <summary>
        /// The is insert mode on property
        /// </summary>
        public static readonly DependencyProperty IsInsertModeOnProperty = DependencyProperty.Register(
            "IsInsertModeOn", typeof(bool), typeof(EditorControl), new PropertyMetadata(default(bool)));

        /// <summary>
        /// The is italics property
        /// </summary>
        public static readonly DependencyProperty IsItalicsProperty = DependencyProperty.Register(
            "IsItalics", typeof(bool), typeof(EditorControl), new PropertyMetadata(default(bool)));

        /// <summary>
        /// The is modified property
        /// </summary>
        public static readonly DependencyProperty IsModifiedProperty = DependencyProperty.Register(
            "IsModified", typeof(bool), typeof(EditorControl),
            new PropertyMetadata(default(bool), IsModifiedPropertyChanged));

        /// <summary>
        /// The is selection empty property
        /// </summary>
        public static readonly DependencyProperty IsSelectionEmptyProperty = DependencyProperty.Register(
            "IsSelectionEmpty", typeof(bool), typeof(EditorControl), new PropertyMetadata(true));

        /// <summary>
        /// The is underlined property
        /// </summary>
        public static readonly DependencyProperty IsUnderlinedProperty = DependencyProperty.Register(
            "IsUnderlined", typeof(bool), typeof(EditorControl), new PropertyMetadata(default(bool)));

        /// <summary>
        /// The line count property
        /// </summary>
        public static readonly DependencyProperty LineCountProperty = DependencyProperty.Register(
            "LineCount", typeof(int), typeof(EditorControl), new PropertyMetadata(default(int)));

        /// <summary>
        /// The row property
        /// </summary>
        public static readonly DependencyProperty RowProperty = DependencyProperty.Register(
            "Row", typeof(int), typeof(EditorControl), new PropertyMetadata(default(int)));

        /// <summary>
        /// The selection length property
        /// </summary>
        public static readonly DependencyProperty SelectionLengthProperty = DependencyProperty.Register(
            "SelectionLength", typeof(int), typeof(EditorControl), new PropertyMetadata(default(int)));

        /// <summary>
        /// The text size property
        /// </summary>
        public static readonly DependencyProperty TextSizeProperty = DependencyProperty.Register(
            "TextSize", typeof(double), typeof(EditorControl), new PropertyMetadata(default(double)));

        /// <summary>
        /// Initializes a new instance of the <see cref="EditorControl" /> class.
        /// </summary>
        public EditorControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the column.
        /// </summary>
        /// <value>The column.</value>
        public int Column
        {
            get => (int) GetValue(ColumnProperty);
            set => SetValue(ColumnProperty, value);
        }

        /// <summary>
        /// Gets or sets the commands.
        /// </summary>
        /// <value>The commands.</value>
        public EditCommands Commands
        {
            get => (EditCommands) GetValue(CommandsProperty);
            set => SetValue(CommandsProperty, value);
        }

        /// <summary>
        /// Gets or sets the count of chars.
        /// </summary>
        /// <value>The count of chars.</value>
        public int CountOfChars
        {
            get => (int) GetValue(CountOfCharsProperty);
            set => SetValue(CountOfCharsProperty, value);
        }

        /// <summary>
        /// Gets or sets the count of words.
        /// </summary>
        /// <value>The count of words.</value>
        public int CountOfWords
        {
            get => (int) GetValue(CountOfWordsProperty);
            set => SetValue(CountOfWordsProperty, value);
        }

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>The file path.</value>
        public string FilePath
        {
            get => (string) GetValue(FilePathProperty);
            set => SetValue(FilePathProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of the font background.
        /// </summary>
        /// <value>The color of the font background.</value>
        public SolidColorBrush FontBackgroundColor
        {
            get => (SolidColorBrush) GetValue(FontBackgroundColorProperty);
            set => SetValue(FontBackgroundColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of the font.
        /// </summary>
        /// <value>The color of the font.</value>
        public SolidColorBrush FontColor
        {
            get => (SolidColorBrush) GetValue(FontColorProperty);
            set => SetValue(FontColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of the highlight.
        /// </summary>
        /// <value>The color of the highlight.</value>
        public SolidColorBrush HighlightColor
        {
            get => (SolidColorBrush) GetValue(HighlightColorProperty);
            set => SetValue(HighlightColorProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is bold.
        /// </summary>
        /// <value><c>true</c> if this instance is bold; otherwise, <c>false</c>.</value>
        public bool IsBold
        {
            get => (bool) GetValue(IsBoldProperty);
            set => SetValue(IsBoldProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is insert mode on.
        /// </summary>
        /// <value><c>true</c> if this instance is insert mode on; otherwise, <c>false</c>.</value>
        public bool IsInsertModeOn
        {
            get => (bool) GetValue(IsInsertModeOnProperty);
            set => SetValue(IsInsertModeOnProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is italics.
        /// </summary>
        /// <value><c>true</c> if this instance is italics; otherwise, <c>false</c>.</value>
        public bool IsItalics
        {
            get => (bool) GetValue(IsItalicsProperty);
            set => SetValue(IsItalicsProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is modified.
        /// </summary>
        /// <value><c>true</c> if this instance is modified; otherwise, <c>false</c>.</value>
        public bool IsModified
        {
            get => (bool) GetValue(IsModifiedProperty);
            set => SetValue(IsModifiedProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is
        /// selection empty.
        /// </summary>
        /// <value><c>true</c> if this instance is selection empty; otherwise, <c>false</c>.</value>
        public bool IsSelectionEmpty
        {
            get => (bool) GetValue(IsSelectionEmptyProperty);
            set => SetValue(IsSelectionEmptyProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is underlined.
        /// </summary>
        /// <value><c>true</c> if this instance is underlined; otherwise, <c>false</c>.</value>
        public bool IsUnderlined
        {
            get => (bool) GetValue(IsUnderlinedProperty);
            set => SetValue(IsUnderlinedProperty, value);
        }

        /// <summary>
        /// Gets or sets the line count.
        /// </summary>
        /// <value>The line count.</value>
        public int LineCount
        {
            get => (int) GetValue(LineCountProperty);
            set => SetValue(LineCountProperty, value);
        }

        /// <summary>
        /// Gets or sets the row.
        /// </summary>
        /// <value>The row.</value>
        public int Row
        {
            get => (int) GetValue(RowProperty);
            set => SetValue(RowProperty, value);
        }

        /// <summary>
        /// Gets or sets the length of the selection.
        /// </summary>
        /// <value>The length of the selection.</value>
        public int SelectionLength
        {
            get => (int) GetValue(SelectionLengthProperty);
            set => SetValue(SelectionLengthProperty, value);
        }

        /// <summary>
        /// Gets or sets the size of the text.
        /// </summary>
        /// <value>The size of the text.</value>
        public double TextSize
        {
            get => (double) GetValue(TextSizeProperty);
            set => SetValue(TextSizeProperty, value);
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever
        /// application code or internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate" />.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">locator</exception>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var locator = FindResource("Locator") as ViewModelLocator;
            if (locator == null) throw new ArgumentNullException(nameof(locator));
            var commands = locator.EditCommands;

            commands.PrintDocumentCommand = new RelayCommand(
                PrintDocument);
            commands.DecreaseFontSizeCommand = new RelayCommand(
                DecreaseFontSize
            );
            commands.DeleteCommand = new RelayCommand(
                DeleteSelection,
                delegate
                {
                    var selection = RichTextBox.Selection;
                    return selection.IsEmpty == false;
                });
            commands.FontColorCommand = new RelayCommand(
                ColorText);
            commands.FontHighlightCommand = new RelayCommand(
                HighlightSelected);
            commands.IncreaseFontSizeCommand = new RelayCommand(
                IncreaseFontSize);
            commands.OpenCommand = new RelayCommand(
                OpenNewFile);
            commands.SaveCommand = new RelayCommand(
                SaveFile,
                delegate { return IsModified; });
            commands.SaveAsCommand = new RelayCommand(
                SaveFileAs,
                delegate { return IsModified; });
            commands.SpellCheckCommand = new RelayCommand(
                ToggleSpellCheck);
            commands.PropertiesCommand = new RelayCommand(
                EditToolBarProperties);

            SetCurrentValue(CommandsProperty, commands);

            var dictionairies = Settings.Default.CustomDictionairies;
            var spellCheckDictionaries = SpellCheck.GetCustomDictionaries(RichTextBox);
            foreach (var dictionairy in dictionairies)
            {
                var dictionaryPath = new List<string>
                {
                    Environment.SpecialFolder.DesktopDirectory.ToString(),
                    Environment.SpecialFolder.MyDocuments.ToString(),
                    Environment.SpecialFolder.CommonDocuments.ToString(),
                    Environment.SpecialFolder.CommonDesktopDirectory.ToString(),
                    Environment.SpecialFolder.Personal.ToString(),
                    Environment.SpecialFolder.ApplicationData.ToString()
                }.Aggregate(dictionairy,
                    (current, specialFolder) =>
                        current.Replace($"${specialFolder}$",
                            Environment.GetFolderPath(
                                (Environment.SpecialFolder) Enum.Parse(typeof(Environment.SpecialFolder),
                                    specialFolder))));

                if (File.Exists(dictionaryPath) == false)
                {
                    CreatePathIfNecessary(Path.GetDirectoryName(dictionaryPath));
                    File.AppendAllText(dictionaryPath, null);
                }

                spellCheckDictionaries.Add(new Uri(dictionaryPath));
                RichTextBox.Focus();
            }
        }

        /// <summary>
        /// Invoked when an unhandled <see cref="E:System.Windows.Input.Keyboard.PreviewKeyDown" /> attached event reaches an element in its route that is derived from this class. Implement this method to add class handling for this event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Input.KeyEventArgs" /> that contains the event data.</param>
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            if (e.Key == Key.Insert)
                SetCurrentValue(IsInsertModeOnProperty, !Keyboard.IsKeyToggled(Key.Insert));
        }

        private static void IsModifiedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var editorControl = d as EditorControl;

            editorControl?.Commands.DeleteCommand.RaiseCanExecuteChanged();
        }

        private void AddToDictionary(string text)
        {
            var dictionaries = SpellCheck.GetCustomDictionaries(RichTextBox);
            if (dictionaries.Count == 0)
                return;

            var uri = dictionaries[0] as Uri;
            if (uri == null || uri.IsFile == false)
                return;

            File.AppendAllText(uri.AbsolutePath, text);
        }

        private async void AppCmdButtonClicked(object sender, RoutedEventArgs e)
        {
            await DispatcherHelper.RunAsync(() =>
            {
                Thread.Sleep(25);
                GetCurrentFormat(RichTextBox.Selection);
            });
        }

        private void ColorText()
        {
            var textSelection = RichTextBox.Selection;
            if (textSelection == null)
                return;

            var brush = textSelection.GetPropertyValue(TextElement.ForegroundProperty) as SolidColorBrush;
            Color color;
            color = brush?.Color ?? Colors.Black;

            var colorPicker = new ColorPicker(color);
            colorPicker.ShowDialog();

            if (colorPicker.DialogResult != true)
                return;

            brush = new SolidColorBrush(colorPicker.SelectedColor);
            textSelection.ApplyPropertyValue(TextElement.ForegroundProperty, brush);
            GetCurrentFormat(textSelection);
        }

        private void CreatePathIfNecessary(string path)
        {
            if (!Directory.Exists(path))
            {
                CreatePathIfNecessary(Path.GetDirectoryName(path));
                Directory.CreateDirectory(path);
            }
        }

        private void DecreaseFontSize()
        {
            var textSelection = RichTextBox.Selection;
            if (textSelection == null)
                return;

            var size = textSelection.GetPropertyValue(TextElement.FontSizeProperty) as double?;
            if (!size.HasValue)
                size = 12d;
            size -= size / 4;
            textSelection.ApplyPropertyValue(TextElement.FontSizeProperty, size);
            GetCurrentFormat(textSelection);
        }

        private void DeleteSelection()
        {
            var textSelection = RichTextBox.Selection;
            if (textSelection == null || textSelection.IsEmpty)
                return;

            textSelection.Text = string.Empty;
            GetCurrentFormat(textSelection);
        }

        private void EditToolBarProperties()
        {
            var properties = new ToolBarProperties
            {
                ShowInTaskbar = false
            };
            properties.Show();
        }

        private void GetCountOfWordsAndChars(RichTextBox richTextBox)
        {
            var regex = new Regex("\\b.\\w*?\\b", RegexOptions.Compiled);
            var text = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text;
            var count = regex.Matches(text).Count;
            SetCurrentValue(CountOfWordsProperty, count);
            SetCurrentValue(CountOfCharsProperty, text.Length);
        }

        private void GetCurrentFormat(TextSelection selection)
        {
            var fontSize = selection.GetPropertyValue(TextElement.FontSizeProperty) is double
                ? (double) selection.GetPropertyValue(TextElement.FontSizeProperty)
                : default(double);
            var weight = selection.GetPropertyValue(TextElement.FontWeightProperty) is FontWeight
                ? (FontWeight) selection.GetPropertyValue(TextElement.FontWeightProperty)
                : default(FontWeight);
            var style = selection.GetPropertyValue(TextElement.FontStyleProperty) is FontStyle
                ? (FontStyle) selection.GetPropertyValue(TextElement.FontStyleProperty)
                : new FontStyle();
            var background = selection.GetPropertyValue(TextElement.BackgroundProperty) as Brush;
            var foreground = selection.GetPropertyValue(TextElement.ForegroundProperty) as Brush;
            var decorations = selection.GetPropertyValue(Inline.TextDecorationsProperty) as TextDecorationCollection;

            SetCurrentValue(TextSizeProperty, fontSize);
            SetCurrentValue(IsBoldProperty, weight == FontWeights.Bold);
            SetCurrentValue(IsItalicsProperty, style == FontStyles.Italic);
            SetCurrentValue(FontBackgroundColorProperty, background ?? new SolidColorBrush(Colors.Transparent));
            SetCurrentValue(FontColorProperty, foreground);
            SetCurrentValue(IsUnderlinedProperty, Equals(decorations, TextDecorations.Underline));
            SetCurrentValue(IsInsertModeOnProperty, !Keyboard.IsKeyToggled(Key.Insert));
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

        private void HighlightSelected()
        {
            var textSelection = RichTextBox.Selection;
            if (textSelection == null)
                return;

            var brush = textSelection.GetPropertyValue(TextElement.BackgroundProperty) as SolidColorBrush;
            if (brush == null)
                textSelection.ApplyPropertyValue(TextElement.BackgroundProperty,
                    HighlightColor);
            else
                textSelection.ApplyPropertyValue(TextElement.BackgroundProperty,
                    brush.Color == HighlightColor.Color
                        ? RichTextBox.Background
                        : HighlightColor);

            GetCurrentFormat(textSelection);
        }

        private void IncreaseFontSize()
        {
            var textSelection = RichTextBox.Selection;
            if (textSelection == null)
                return;

            var size = textSelection.GetPropertyValue(TextElement.FontSizeProperty) as double?;
            if (!size.HasValue)
                size = 12d;
            size += size / 3;
            textSelection.ApplyPropertyValue(TextElement.FontSizeProperty, size);
            GetCurrentFormat(textSelection);
        }

        private void OpenNewFile()
        {
            if (IsModified)
            {
                var result = MessageBox.Show(
                    "The current file has changes that have not been saved. If you open another file now, your changes will be lost. Do you want to do this?",
                    "Ignore Changes?", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No)
                    return;
            }

            var ofd = new OpenFileDialog
            {
                Multiselect = false,
                AddExtension = true,
                DefaultExt = ".rtf",
                Filter = @"Rich text files|*.rtf|All files|*.*",
                FilterIndex = 0,
                CheckFileExists = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            var result2 = ofd.ShowDialog();
            if (result2 != true)
                return;

            FilePath = ofd.FileName;
            var range = new TextRange(RichTextBox.Document.ContentStart, RichTextBox.Document.ContentEnd);
            var stream = new FileStream(FilePath, FileMode.Open);
            range.Load(stream, DataFormats.Rtf);
            stream.Close();
            SetCurrentValue(IsModifiedProperty, false);
        }

        private void PrintDocument()
        {
            var prd = new PrintDialog();
            if (prd.ShowDialog() != true)
                return;

            prd.PrintVisual(RichTextBox, "Print Visual");
            //prd.PrintDocument((((IDocumentPaginatorSource)RichTextBox.Document).DocumentPaginator), "Print Document");
        }

        private void RichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var richTextBox = sender as RichTextBox;

            var currentSelection = richTextBox?.Selection;
            if (currentSelection == null)
                return;

            GetRowAndColumnPositions(richTextBox);
            GetNumberOfLines(richTextBox);
            GetSelectionStats(richTextBox);
            GetCountOfWordsAndChars(richTextBox);
            GetCurrentFormat(currentSelection);

            Commands.DeleteCommand.RaiseCanExecuteChanged();
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetCurrentValue(IsModifiedProperty, true);
            Commands.DeleteCommand.RaiseCanExecuteChanged();
        }

        private void RtbContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            var contextMenu = RichTextBox.ContextMenu;
            if (contextMenu == null)
                return;

            MenuItem menuItem;
            Separator separator;

            var index = 0;
            contextMenu.Items.Clear();
            var spellingError = RichTextBox.GetSpellingError(RichTextBox.CaretPosition);
            if (spellingError != null)
            {
                if (spellingError.Suggestions.Any())
                {
                    foreach (var suggestion in spellingError.Suggestions)
                    {
                        // Add each suggestion
                        menuItem = new MenuItem
                        {
                            Header = suggestion,
                            Command = EditingCommands.CorrectSpellingError,
                            FontWeight = FontWeights.Bold,
                            CommandParameter = suggestion,
                            CommandTarget = RichTextBox
                        };

                        contextMenu.Items.Insert(index++, menuItem);
                    }

                    separator = new Separator();
                    contextMenu.Items.Insert(index++, separator);
                }

                // Add ignore all option
                menuItem = new MenuItem
                {
                    Header = "Ignore all",
                    Command = EditingCommands.IgnoreSpellingError,
                    CommandTarget = RichTextBox
                };

                contextMenu.Items.Insert(index++, menuItem);

                var range = RichTextBox.GetSpellingErrorRange(RichTextBox.CaretPosition);
                if (range == null || range.Text.Length <= 0) return;

                // Add add to dictionary option
                menuItem = new MenuItem
                {
                    Header = "Add to dictionary",
                    Command = EditingCommands.IgnoreSpellingError,
                    CommandTarget = RichTextBox
                };

                menuItem.Click += (o, arg) => { AddToDictionary(range.Text); };
                contextMenu.Items.Insert(index++, menuItem);
                separator = new Separator();
                contextMenu.Items.Insert(index++, separator);
            }

            menuItem = new MenuItem
            {
                Command = ApplicationCommands.Cut,
                CommandTarget = RichTextBox
            };
            contextMenu.Items.Insert(index++, menuItem);

            menuItem = new MenuItem
            {
                Command = ApplicationCommands.Copy,
                CommandTarget = RichTextBox
            };
            contextMenu.Items.Insert(index++, menuItem);

            menuItem = new MenuItem
            {
                Command = ApplicationCommands.Paste,
                CommandTarget = RichTextBox
            };

            contextMenu.Items.Insert(index, menuItem);
        }

        private void SaveFile()
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                SaveFileAs();
                return;
            }

            if (File.Exists(FilePath))
            {
                var backupName =
                    $"{Path.GetDirectoryName(FilePath)}\\{Path.GetFileNameWithoutExtension(FilePath)}.bak";
                if (File.Exists(backupName))
                    File.Delete(backupName);

                File.Move(FilePath, backupName);
            }

            var range = new TextRange(RichTextBox.Document.ContentStart, RichTextBox.Document.ContentEnd);
            var stream = new FileStream(FilePath, FileMode.Create);
            range.Save(stream, DataFormats.Rtf);
            stream.Close();
            SetCurrentValue(IsModifiedProperty, false);
        }

        private void SaveFileAs()
        {
            var sfd = new SaveFileDialog
            {
                CreatePrompt = false,
                OverwritePrompt = true,
                AddExtension = true,
                CheckPathExists = true,
                DefaultExt = ".rtf",
                InitialDirectory = string.IsNullOrEmpty(FilePath)
                    ? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                    : Path.GetDirectoryName(FilePath),
                Filter = "Rich text files|*.rtf|All files|*.*",
                FilterIndex = 0
            };

            var result = sfd.ShowDialog();
            if (result != true)
                return;

            FilePath = sfd.FileName;
            SaveFile();
        }

        private async void SplitButtonOnSplitButtonClick(object sender, RoutedEventArgs e)
        {
            var position = Mouse.GetPosition(this);
            position = PointToScreen(position);
            var highlighters = new Highlighters();
            highlighters.Left = position.X;
            highlighters.Top = position.Y;
            highlighters.Show();

            await highlighters.Wait();

            if (!highlighters.Result)
                return;
        }

        private void ToggleSpellCheck()
        {
            var isEnabled = SpellCheck.GetIsEnabled(RichTextBox);
            SpellCheck.SetIsEnabled(RichTextBox, !isEnabled);
        }
    }
}