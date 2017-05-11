using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Prompt.View
{
    /// <summary>
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class ColorPicker : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorPicker"/> class.
        /// </summary>
        /// <param name="current">The current.</param>
        public ColorPicker(Color current)
        {
            InitializeComponent();
            SetCurrentValue(LastColorProperty, current);
            SetCurrentValue(SelectedColorProperty, current);
        }

        /// <summary>
        /// The last color property
        /// </summary>
        public static readonly DependencyProperty LastColorProperty = DependencyProperty.Register(
            "LastColor", typeof(Color), typeof(ColorPicker), new PropertyMetadata(default(Color)));

        /// <summary>
        /// Gets or sets the last color.
        /// </summary>
        /// <value>The last color.</value>
        public Color LastColor
        {
            get => (Color) GetValue(LastColorProperty);
            set => SetValue(LastColorProperty, value);
        }

        /// <summary>
        /// The selected color property
        /// </summary>
        public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register(
            "SelectedColor", typeof(Color), typeof(ColorPicker), new PropertyMetadata(default(Color)));

        /// <summary>
        /// Gets or sets the color of the selected.
        /// </summary>
        /// <value>The color of the selected.</value>
        public Color SelectedColor
        {
            get => (Color) GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
