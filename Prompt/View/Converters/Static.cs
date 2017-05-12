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

namespace Prompt.View.Converters
{
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Media;
    using JetBrains.Annotations;

    /// <summary>
    ///     Class Static.
    /// </summary>
    public static class Static
    {
        /// <summary>
        ///     Gets the bool to collapsed or visible.
        /// </summary>
        /// <value>The bool to collapsed or visible.</value>
        [NotNull]
        public static IValueConverter BoolToCollapsedOrVisible => new ValueConverter<bool, Visibility>(
            e => e.Value ? Visibility.Collapsed : Visibility.Visible);

        /// <summary>
        ///     Gets the bool to enabled or disabled brush.
        /// </summary>
        /// <value>The bool to enabled or disabled brush.</value>
        [NotNull]
        public static IValueConverter BoolToEnabledOrDisabledBrush => new ValueConverter<bool, SolidColorBrush>(
            e => e.Value ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.DarkGray));

        /// <summary>
        ///     Gets the bool to visible or collapsed.
        /// </summary>
        /// <value>The bool to visible or collapsed.</value>
        [NotNull]
        public static IValueConverter BoolToVisibleOrCollapsed => new ValueConverter<bool, Visibility>(
            e => e.Value ? Visibility.Visible : Visibility.Collapsed);

        /// <summary>
        ///     Gets the color to solid color brush.
        /// </summary>
        /// <value>The color to solid color brush.</value>
        [NotNull]
        public static IValueConverter ColorToSolidColorBrush => new ValueConverter<Color, SolidColorBrush>(
            e => new SolidColorBrush(e.Value));

        /// <summary>
        ///     Gets the color to string.
        /// </summary>
        /// <value>The color to string.</value>
        [NotNull]
        public static IValueConverter ColorToString => new ValueConverter<Color, string>(
            e =>
            {
                var colors = typeof(Colors);
                foreach (var prop in colors.GetProperties())
                    if ((Color) prop.GetValue(null, null) == e.Value)
                        return prop.Name;

                return e.Value.ToString();
            });
    }
}