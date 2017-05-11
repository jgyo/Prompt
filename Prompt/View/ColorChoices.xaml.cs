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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Prompt.View
{
    /// <summary>
    /// Interaction logic for ColorChoices.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class ColorChoices : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorChoices"/> class.
        /// </summary>
        public ColorChoices()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The color property
        /// </summary>
        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            "Color", typeof(Color), typeof(ColorChoices), new PropertyMetadata(default(Color)));

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>The color.</value>
        public Color Color
        {
            get => (Color) GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        /// <summary>
        /// The is checked property
        /// </summary>
        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register(
            "IsChecked", typeof(bool), typeof(ColorChoices), new PropertyMetadata(default(bool)));

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
        /// The text property
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(ColorChoices), new PropertyMetadata(default(string)));

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if(IsChecked)
                return;
            SetCurrentValue(IsCheckedProperty, true);
        }
    }
}
