﻿// Copyright (c) 2017 Gil Yoder <gil.yoder@oabs.org>
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
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// Interaction logic for ButtonOption.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class ButtonOption : UserControl
    {
        /// <summary>
        /// The image source property
        /// </summary>
        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
            "ImageSource", typeof(ImageSource), typeof(ButtonOption), new PropertyMetadata(null));

        /// <summary>
        /// The is checked property
        /// </summary>
        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register(
            "IsChecked", typeof(bool), typeof(ButtonOption), new PropertyMetadata(default(bool)));

        /// <summary>
        /// The text property
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(ButtonOption), new PropertyMetadata(default(string)));

        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonOption"/> class.
        /// </summary>
        public ButtonOption()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the image source.
        /// </summary>
        /// <value>The image source.</value>
        public ImageSource ImageSource
        {
            get => (ImageSource) GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is checked.
        /// </summary>
        /// <value><c>true</c> if this instance is checked; otherwise, <c>false</c>.</value>
        public bool IsChecked
        {
            get => (bool) GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
    }
}