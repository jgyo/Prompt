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
    public partial class ColorPicker : Window
    {
        public ColorPicker(Color current)
        {
            InitializeComponent();
            SetCurrentValue(LastColorProperty, current);
            SetCurrentValue(SelectedColorProperty, current);
        }

        public static readonly DependencyProperty LastColorProperty = DependencyProperty.Register(
            "LastColor", typeof(Color), typeof(ColorPicker), new PropertyMetadata(default(Color)));

        public Color LastColor
        {
            get => (Color) GetValue(LastColorProperty);
            set => SetValue(LastColorProperty, value);
        }

        public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register(
            "SelectedColor", typeof(Color), typeof(ColorPicker), new PropertyMetadata(default(Color)));

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
