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
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media;
    using Commands;
    using Properties;

    /// <summary>
    /// Interaction logic for Highlighters.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class Highlighters : Window
    {
        /// <summary>
        /// The selected color property
        /// </summary>
        public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register(
            "SelectedColor", typeof(Color), typeof(Highlighters), new PropertyMetadata(default(Color)));

        /// <summary>
        /// Initializes a new instance of the <see cref="Highlighters" /> class.
        /// </summary>
        public Highlighters()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Highlighters" /> is result.
        /// </summary>
        /// <value><c>true</c> if result; otherwise, <c>false</c>.</value>
        public bool Result { get; set; }

        /// <summary>
        /// Gets or sets the color of the selected.
        /// </summary>
        /// <value>The color of the selected.</value>
        public Color SelectedColor
        {
            get => (Color) GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate" />.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">editingCommands</exception>
        /// <exception cref="ArgumentNullException">editingCommands</exception>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var binding = new Binding("HighlightColor");

            var editingCommands = DataContext as EditCommands;
            if (editingCommands == null) throw new ArgumentNullException(nameof(editingCommands));
            binding.Source = editingCommands;
            binding.Mode = BindingMode.TwoWay;
            BindingOperations.SetBinding(this, SelectedColorProperty, binding);
        }

        /// <summary>
        /// Waits this instance.
        /// </summary>
        /// <returns>Task.</returns>
        public async Task Wait()
        {
            while (IsVisible)
                await Task.Delay(50);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Window.Closed" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Settings.Default.Save();
        }

        private void CloseButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ColorButtonClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            var border = button?.Content as Border;
            if (border == null)
                return;

            var solidColorBrush = border.Background as SolidColorBrush;
            if (solidColorBrush != null)
                SetCurrentValue(SelectedColorProperty, solidColorBrush.Color);
            Result = true;
        }

        private void Highlighters_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
                return;

            DragMove();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            if (IsVisible)
                Close();
        }
    }
}