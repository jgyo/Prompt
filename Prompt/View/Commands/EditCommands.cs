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

namespace Prompt.View.Commands
{
    using System.Windows.Media;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.CommandWpf;
    using Properties;

    /// <summary>
    /// Class EditCommands.
    /// </summary>
    /// <seealso cref="GalaSoft.MvvmLight.ViewModelBase" />
    public class EditCommands : ViewModelBase
    {
        private bool _showBoldButton;
        private bool _showCopyButton;
        private bool _showCutButton;
        private bool _showDecreaseFontSizeButton;
        private bool _showDeleteButton;
        private bool _showFontColorButton;
        private bool _showFontHighlightButton;
        private bool _showIncreaseFontSizeButton;
        private bool _showItalicsButton;
        private bool _showOpenButton;
        private bool _showPasteButton;
        private bool _showPrintButton;
        private bool _showPropertiesButton;
        private bool _showRedoButton;
        private bool _showSaveAsButton;
        private bool _showSaveButton;
        private bool _showSeperator1;
        private bool _showSeperator2;
        private bool _showSeperator3;
        private bool _showSeperator4;
        private bool _showSeperator5;
        private bool _showSpellCheckButton;
        private bool _showUnderlineButton;
        private bool _showUndoButton;
        private Color _highlightColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditCommands"/> class.
        /// </summary>
        public EditCommands()
        {
            ShowBoldButton = Settings.Default.ShowBoldButton;
            ShowCopyButton = Settings.Default.ShowCopyButton;
            ShowCutButton = Settings.Default.ShowCutButton;
            ShowDecreaseFontSizeButton = Settings.Default.ShowDecreaseSizeButton;
            ShowDeleteButton = Settings.Default.ShowDeleteButton;
            ShowFontColorButton = Settings.Default.ShowFontColorButton;
            ShowFontHighlightButton = Settings.Default.ShowFontHighlightButton;
            ShowIncreaseFontSizeButton = Settings.Default.ShowIncreaseFontSizeButton;
            ShowItalicsButton = Settings.Default.ShowItalicsButton;
            ShowOpenButton = Settings.Default.ShowOpenButton;
            ShowPasteButton = Settings.Default.ShowPasteButton;
            ShowPrintButton = Settings.Default.ShowPrintButton;
            ShowPropertiesButton = true;
            ShowRedoButton = Settings.Default.ShowRedoButton;
            ShowSaveAsButton = Settings.Default.ShowSaveAsButton;
            ShowSaveButton = Settings.Default.ShowSaveButton;
            ShowSeperator1 = Settings.Default.ShowSeperator1;
            ShowSeperator2 = Settings.Default.ShowSeperator2;
            ShowSeperator3 = Settings.Default.ShowSeperator3;
            ShowSeperator4 = Settings.Default.ShowSeperator4;
            ShowSeperator5 = Settings.Default.ShowSeperator5;
            ShowSpellCheckButton = Settings.Default.ShowSpellCheckButton;
            ShowUnderlineButton = Settings.Default.ShowUnderlineButton;
            ShowUndoButton = Settings.Default.ShowUndoButton;
            HighlightColor = Settings.Default.HighlightColor;

            Settings.Default.SettingsSaving += this.Default_SettingsSaving;
        }

        private void Default_SettingsSaving(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Settings.Default.ShowBoldButton = ShowBoldButton;
            Settings.Default.ShowCopyButton = ShowCopyButton;
            Settings.Default.ShowCutButton = ShowCutButton;
            Settings.Default.ShowDecreaseSizeButton = ShowDecreaseFontSizeButton;
            Settings.Default.ShowDeleteButton = ShowDeleteButton;
            Settings.Default.ShowFontColorButton = ShowFontColorButton;
            Settings.Default.ShowFontHighlightButton = ShowFontHighlightButton;
            Settings.Default.ShowIncreaseFontSizeButton = ShowIncreaseFontSizeButton;
            Settings.Default.ShowItalicsButton = ShowItalicsButton;
            Settings.Default.ShowOpenButton = ShowOpenButton;
            Settings.Default.ShowPasteButton = ShowPasteButton;
            Settings.Default.ShowPrintButton = ShowPrintButton;
            Settings.Default.ShowRedoButton = ShowRedoButton;
            Settings.Default.ShowSaveAsButton = ShowSaveAsButton;
            Settings.Default.ShowSaveButton = ShowSaveButton;
            Settings.Default.ShowSeperator1 = ShowSeperator1;
            Settings.Default.ShowSeperator2 = ShowSeperator2;
            Settings.Default.ShowSeperator3 = ShowSeperator3;
            Settings.Default.ShowSeperator4 = ShowSeperator4;
            Settings.Default.ShowSeperator5 = ShowSeperator5;
            Settings.Default.ShowSpellCheckButton = ShowSpellCheckButton;
            Settings.Default.ShowUnderlineButton = ShowUnderlineButton;
            Settings.Default.ShowUndoButton = ShowUndoButton;
            Settings.Default.HighlightColor = HighlightColor;
        }

        /// <summary>
        /// Gets or sets the decrease font size command.
        /// </summary>
        /// <value>The decrease font size command.</value>
        public RelayCommand DecreaseFontSizeCommand { get; set; }

        /// <summary>
        /// Gets or sets the delete command.
        /// </summary>
        /// <value>The delete command.</value>
        public RelayCommand DeleteCommand { get; set; }

        /// <summary>
        /// Gets or sets the font color command.
        /// </summary>
        /// <value>The font color command.</value>
        public RelayCommand FontColorCommand { get; set; }

        /// <summary>
        /// Gets or sets the font highlight command.
        /// </summary>
        /// <value>The font highlight command.</value>
        public RelayCommand FontHighlightCommand { get; set; }

        /// <summary>
        /// Gets or sets the increase font size command.
        /// </summary>
        /// <value>The increase font size command.</value>
        public RelayCommand IncreaseFontSizeCommand { get; set; }

        /// <summary>
        /// Gets or sets the open command.
        /// </summary>
        /// <value>The open command.</value>
        public RelayCommand OpenCommand { get; set; }

        /// <summary>
        /// Gets or sets the print document command.
        /// </summary>
        /// <value>The print document command.</value>
        public RelayCommand PrintDocumentCommand { get; set; }

        /// <summary>
        /// Gets or sets the properties command.
        /// </summary>
        /// <value>The properties command.</value>
        public RelayCommand PropertiesCommand { get; set; }

        /// <summary>
        /// Gets or sets the save as command.
        /// </summary>
        /// <value>The save as command.</value>
        public RelayCommand SaveAsCommand { get; set; }

        /// <summary>
        /// Gets or sets the save command.
        /// </summary>
        /// <value>The save command.</value>
        public RelayCommand SaveCommand { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show bold button].
        /// </summary>
        /// <value><c>true</c> if [show bold button]; otherwise, <c>false</c>.</value>
        public bool ShowBoldButton
        {
            get => _showBoldButton;
            set => Set(ref _showBoldButton, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show copy button].
        /// </summary>
        /// <value><c>true</c> if [show copy button]; otherwise, <c>false</c>.</value>
        public bool ShowCopyButton
        {
            get => _showCopyButton;
            set => Set(ref _showCopyButton, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show cut button].
        /// </summary>
        /// <value><c>true</c> if [show cut button]; otherwise, <c>false</c>.</value>
        public bool ShowCutButton
        {
            get => _showCutButton;
            set => Set(ref _showCutButton, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show decrease font size button].
        /// </summary>
        /// <value><c>true</c> if [show decrease font size button]; otherwise, <c>false</c>.</value>
        public bool ShowDecreaseFontSizeButton
        {
            get => _showDecreaseFontSizeButton;
            set => Set(ref _showDecreaseFontSizeButton, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show delete button].
        /// </summary>
        /// <value><c>true</c> if [show delete button]; otherwise, <c>false</c>.</value>
        public bool ShowDeleteButton
        {
            get => _showDeleteButton;
            set => Set(ref _showDeleteButton, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show font color button].
        /// </summary>
        /// <value><c>true</c> if [show font color button]; otherwise, <c>false</c>.</value>
        public bool ShowFontColorButton
        {
            get => _showFontColorButton;
            set => Set(ref _showFontColorButton, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show font highlight button].
        /// </summary>
        /// <value><c>true</c> if [show font highlight button]; otherwise, <c>false</c>.</value>
        public bool ShowFontHighlightButton
        {
            get => _showFontHighlightButton;
            set => Set(ref _showFontHighlightButton, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show increase font size button].
        /// </summary>
        /// <value><c>true</c> if [show increase font size button]; otherwise, <c>false</c>.</value>
        public bool ShowIncreaseFontSizeButton
        {
            get => _showIncreaseFontSizeButton;
            set => Set(ref _showIncreaseFontSizeButton, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show italics button].
        /// </summary>
        /// <value><c>true</c> if [show italics button]; otherwise, <c>false</c>.</value>
        public bool ShowItalicsButton
        {
            get => _showItalicsButton;
            set => Set(ref _showItalicsButton, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show open button].
        /// </summary>
        /// <value><c>true</c> if [show open button]; otherwise, <c>false</c>.</value>
        public bool ShowOpenButton
        {
            get => _showOpenButton;
            set => Set(ref _showOpenButton, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show paste button].
        /// </summary>
        /// <value><c>true</c> if [show paste button]; otherwise, <c>false</c>.</value>
        public bool ShowPasteButton
        {
            get => _showPasteButton;
            set => Set(ref _showPasteButton, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show print button].
        /// </summary>
        /// <value><c>true</c> if [show print button]; otherwise, <c>false</c>.</value>
        public bool ShowPrintButton
        {
            get => _showPrintButton;
            set => Set(ref _showPrintButton, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show properties button].
        /// </summary>
        /// <value><c>true</c> if [show properties button]; otherwise, <c>false</c>.</value>
        public bool ShowPropertiesButton
        {
            get => _showPropertiesButton;
            set => Set(ref _showPropertiesButton, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show redo button].
        /// </summary>
        /// <value><c>true</c> if [show redo button]; otherwise, <c>false</c>.</value>
        public bool ShowRedoButton
        {
            get => _showRedoButton;
            set => Set(ref _showRedoButton, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show save as button].
        /// </summary>
        /// <value><c>true</c> if [show save as button]; otherwise, <c>false</c>.</value>
        public bool ShowSaveAsButton
        {
            get => _showSaveAsButton;
            set => Set(ref _showSaveAsButton, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show save button].
        /// </summary>
        /// <value><c>true</c> if [show save button]; otherwise, <c>false</c>.</value>
        public bool ShowSaveButton
        {
            get => _showSaveButton;
            set => Set(ref _showSaveButton, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show seperator1].
        /// </summary>
        /// <value><c>true</c> if [show seperator1]; otherwise, <c>false</c>.</value>
        public bool ShowSeperator1
        {
            get => _showSeperator1;
            set => Set(ref _showSeperator1, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show seperator2].
        /// </summary>
        /// <value><c>true</c> if [show seperator2]; otherwise, <c>false</c>.</value>
        public bool ShowSeperator2
        {
            get => _showSeperator2;
            set => Set(ref _showSeperator2, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show seperator3].
        /// </summary>
        /// <value><c>true</c> if [show seperator3]; otherwise, <c>false</c>.</value>
        public bool ShowSeperator3
        {
            get => _showSeperator3;
            set => Set(ref _showSeperator3, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show seperator4].
        /// </summary>
        /// <value><c>true</c> if [show seperator4]; otherwise, <c>false</c>.</value>
        public bool ShowSeperator4
        {
            get => _showSeperator4;
            set => Set(ref _showSeperator4, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show seperator5].
        /// </summary>
        /// <value><c>true</c> if [show seperator5]; otherwise, <c>false</c>.</value>
        public bool ShowSeperator5
        {
            get => _showSeperator5;
            set => Set(ref _showSeperator5, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show spell check button].
        /// </summary>
        /// <value><c>true</c> if [show spell check button]; otherwise, <c>false</c>.</value>
        public bool ShowSpellCheckButton
        {
            get => _showSpellCheckButton;
            set => Set(ref _showSpellCheckButton, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show underline button].
        /// </summary>
        /// <value><c>true</c> if [show underline button]; otherwise, <c>false</c>.</value>
        public bool ShowUnderlineButton
        {
            get => _showUnderlineButton;
            set => Set(ref _showUnderlineButton, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show undo button].
        /// </summary>
        /// <value><c>true</c> if [show undo button]; otherwise, <c>false</c>.</value>
        public bool ShowUndoButton
        {
            get => _showUndoButton;
            set => Set(ref _showUndoButton, value);
        }

        /// <summary>
        /// Gets or sets the color of the highlight.
        /// </summary>
        /// <value>The color of the highlight.</value>
        public Color HighlightColor
        {
            get => _highlightColor;
            set => Set(ref _highlightColor, value);
        }

        /// <summary>
        /// Gets or sets the spell check command.
        /// </summary>
        /// <value>The spell check command.</value>
        public RelayCommand SpellCheckCommand { get; set; }
    }
}