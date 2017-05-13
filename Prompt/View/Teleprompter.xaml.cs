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
    using System.Drawing.Printing;
    using ViewModel;

    /// <summary>
    /// Interaction logic for Teleprompter.xaml
    /// </summary>
    public partial class Teleprompter : UserControl
    {
        private bool _isMovingPolygon;
        private double _startPosition;
        private double _startPolyTop;

        public Teleprompter()
        {
            InitializeComponent();
        }

        private void Polygon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton != MouseButton.Left)
                return;

            var polygon = sender as Polygon;
            if (polygon == null)
            {
                return;
            }

            Mouse.Capture(this);
            _isMovingPolygon = true;
            _startPosition = e.GetPosition(this).Y;
            _startPolyTop = LeftPoly.Margin.Top;
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            if (_isMovingPolygon)
            {
                _isMovingPolygon = false;
                this.ReleaseMouseCapture();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if(!_isMovingPolygon)
                return;

            var currentPosition = e.GetPosition(this).Y;
            var newPolyTop = _startPolyTop - _startPosition + currentPosition;
            newPolyTop = Math.Min(this.ActualHeight - LeftPoly.ActualHeight*2, newPolyTop);
            newPolyTop = newPolyTop < 0 ? 0d : newPolyTop;
            ((TeleprompterViewModel) DataContext).EyelineTop = newPolyTop;
        }
    }
}
