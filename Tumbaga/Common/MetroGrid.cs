#region

using System;
using System.Collections.Generic;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

#endregion

namespace Tumbaga.Common
{
    public class MetroGrid : Grid
    {
        public MetroGrid()
        {
            SizeChanged += (s, e) => Rebuild();
            IsHitTestVisible = false;
            Opacity = 0.2;
            Visibility=Visibility.Collapsed;
        }

        private void Rebuild()
        {
            var isSnapped = ApplicationView.Value == ApplicationViewState.Snapped;
            var isPortrait = DisplayProperties.CurrentOrientation == DisplayOrientations.Portrait || DisplayProperties.CurrentOrientation == DisplayOrientations.PortraitFlipped;
            var margin = isSnapped ? 20 : (isPortrait ? 100 : 120);
            Children.Clear();
           
            var shapes = GetGridShapesForMargin(margin);
            foreach (var shape in shapes)
            {
                Children.Add(shape);
            }
        }

        private IEnumerable<Shape> GetGridShapesForMargin(int margin)
        {
            var brush = new SolidColorBrush(Colors.Fuchsia);
            var max = Math.Max(ActualWidth, ActualHeight);

            const double strokeWidth = 2.0;

            var horizontalLine = new Line
            {
                IsHitTestVisible = false,
                Stroke = brush,
                X1 = 0,
                X2 = max,
                Y1 = 100 + (strokeWidth/2),
                Y2 = 100 + (strokeWidth/2),
                StrokeThickness = strokeWidth,
            };
            yield return horizontalLine;
            var horizontalLine2 = new Line
            {
                IsHitTestVisible = false,
                Stroke = brush,
                X1 = 0,
                X2 = max,
                Y1 = 140 + (strokeWidth/2),
                Y2 = 140 + (strokeWidth/2),
                StrokeThickness = strokeWidth,
            };
            yield return horizontalLine2;

            var verticalLine = new Line
            {
                IsHitTestVisible = false,
                Stroke = brush,
                X1 = margin - (strokeWidth/2),
                X2 = margin - (strokeWidth/2),
                Y1 = 0,
                Y2 = max,
                StrokeThickness = strokeWidth,
            };
            yield return verticalLine;

            var horizontalBottomLine = new Line
            {
                IsHitTestVisible = false,
                Stroke = brush,
                X1 = 0,
                X2 = max,
                Y1 = ActualHeight - 130 + (strokeWidth/2),
                Y2 = ActualHeight - 130 + (strokeWidth/2),
                StrokeThickness = strokeWidth,
            };
            yield return  horizontalBottomLine;
            var horizontalBottomLine2 = new Line
            {
                IsHitTestVisible = false,
                Stroke = brush,
                X1 = 0,
                X2 = max,
                Y1 = ActualHeight - 50 + (strokeWidth/2),
                Y2 = ActualHeight - 50 + (strokeWidth/2),
                StrokeThickness = strokeWidth,
            };
            yield return horizontalBottomLine2;

            const int tileHeight = 20;

            for (var x = margin; x < max; x += (tileHeight*2))
            {
                for (var y = 140; y < max; y += (tileHeight*2))
                {
                    var rect = new Rectangle
                    {
                        Width = tileHeight,
                        Height = tileHeight,
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = new Thickness(x, y, 0, 0),
                        IsHitTestVisible = false,
                        Fill = brush,
                    };
                    yield return rect;
                }
            }
        }
    }
}
