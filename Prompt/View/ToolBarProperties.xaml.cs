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
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media;
    using Commands;
    using JetBrains.Annotations;
    using Properties;

    /// <summary>
    /// Interaction logic for ToolBarProperties.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class ToolBarProperties : Window
    {
        /// <summary>
        /// The highlight color property
        /// </summary>
        [NotNull] public static readonly DependencyProperty HighlightColorProperty = DependencyProperty.Register(
            "HighlightColor", typeof(Color), typeof(ToolBarProperties), new PropertyMetadata(default(Color)));

        /// <summary>
        /// The is light green checked property
        /// </summary>
        [NotNull] public static readonly DependencyProperty IsLightGreenCheckedProperty =
            DependencyProperty.Register("IsLightGreenChecked", typeof(bool), typeof(ToolBarProperties),
                new PropertyMetadata(default(bool)));

        /// <summary>
        /// The is light pink checked property
        /// </summary>
        [NotNull] public static readonly DependencyProperty IsLightPinkCheckedProperty =
            DependencyProperty.Register("IsLightPinkChecked", typeof(bool), typeof(ToolBarProperties),
                new PropertyMetadata(default(bool)));

        /// <summary>
        /// The is power blue checked property
        /// </summary>
        [NotNull] public static readonly DependencyProperty IsPowerBlueCheckedProperty =
            DependencyProperty.Register("IsPowerBlueChecked", typeof(bool), typeof(ToolBarProperties),
                new PropertyMetadata(default(bool)));

        /// <summary>
        /// The is yellow checked property
        /// </summary>
        [NotNull] public static readonly DependencyProperty IsYellowCheckedProperty =
            DependencyProperty.Register("IsYellowChecked", typeof(bool), typeof(ToolBarProperties),
                new PropertyMetadata(default(bool)));

        /// <summary>
        /// Initializes a new instance of the <see cref="ToolBarProperties"/> class.
        /// </summary>
        public ToolBarProperties()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the color of the highlight.
        /// </summary>
        /// <value>The color of the highlight.</value>
        public Color HighlightColor
        {
            get => (Color) GetValue(HighlightColorProperty);
            set => SetValue(HighlightColorProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is light green checked.
        /// </summary>
        /// <value><c>true</c> if this instance is light green checked; otherwise, <c>false</c>.</value>
        public bool IsLightGreenChecked
        {
            get => (bool) GetValue(IsLightGreenCheckedProperty);
            set => SetValue(IsLightGreenCheckedProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is light pink checked.
        /// </summary>
        /// <value><c>true</c> if this instance is light pink checked; otherwise, <c>false</c>.</value>
        public bool IsLightPinkChecked
        {
            get => (bool) GetValue(IsLightPinkCheckedProperty);
            set => SetValue(IsLightPinkCheckedProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is power blue checked.
        /// </summary>
        /// <value><c>true</c> if this instance is power blue checked; otherwise, <c>false</c>.</value>
        public bool IsPowerBlueChecked
        {
            get => (bool) GetValue(IsPowerBlueCheckedProperty);
            set => SetValue(IsPowerBlueCheckedProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is yellow checked.
        /// </summary>
        /// <value><c>true</c> if this instance is yellow checked; otherwise, <c>false</c>.</value>
        public bool IsYellowChecked
        {
            get => (bool) GetValue(IsYellowCheckedProperty);
            set => SetValue(IsYellowCheckedProperty, value);
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate" />.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">DataContext invalid.</exception>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var binding = new Binding("HighlightColor");
            var editCommands = DataContext as EditCommands;
            binding.Source = editCommands ?? throw new InvalidOperationException("DataContext invalid.");
            binding.Mode = BindingMode.TwoWay;
            BindingOperations.SetBinding(this, HighlightColorProperty, binding);
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

        /// <summary>
        /// Invoked whenever the effective value of any dependency property on this <see cref="T:System.Windows.FrameworkElement" /> has been updated. The specific dependency property that changed is reported in the arguments parameter. Overrides <see cref="M:System.Windows.DependencyObject.OnPropertyChanged(System.Windows.DependencyPropertyChangedEventArgs)" />.
        /// </summary>
        /// <param name="e">The event data that describes the property that changed, as well as old and new values.</param>
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            switch (e.Property.Name)
            {
                case "IsYellowChecked":
                    if (IsYellowChecked)
                        SetCurrentValue(HighlightColorProperty, Colors.Yellow);
                    break;

                case "IsLightGreenChecked":
                    if (IsLightGreenChecked)
                        SetCurrentValue(HighlightColorProperty, Colors.LightGreen);
                    break;

                case "IsLightPinkChecked":
                    if (IsLightPinkChecked)
                        SetCurrentValue(HighlightColorProperty, Colors.LightPink);
                    break;

                case "IsPowerBlueChecked":
                    if (IsPowerBlueChecked)
                        SetCurrentValue(HighlightColorProperty, Colors.PowderBlue);
                    break;

                case "HighlightColor":
                    if (HighlightColor == Colors.Yellow)
                    {
                        SetCurrentValue(IsYellowCheckedProperty, true);
                        break;
                    }
                    if (HighlightColor == Colors.LightGreen)
                    {
                        SetCurrentValue(IsLightGreenCheckedProperty, true);
                        break;
                    }
                    if (HighlightColor == Colors.LightPink)
                    {
                        SetCurrentValue(IsLightPinkCheckedProperty, true);
                        break;
                    }
                    if (HighlightColor == Colors.PowderBlue)
                        SetCurrentValue(IsPowerBlueCheckedProperty, true);
                    break;
            }
        }

        private void CloseButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            if (IsVisible)
                Close();
        }

        private void Window_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
                return;

            DragMove();
        }
    }
}