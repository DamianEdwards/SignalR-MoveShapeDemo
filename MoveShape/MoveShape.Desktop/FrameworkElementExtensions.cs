using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MoveShape.Desktop
{
    public static class FrameworkElementExtensions
    {
        public static void Draggable(this FrameworkElement element, Action<double, double> onDrag = null)
        {
            var canvas = element.Parent as Canvas;

            if (canvas == null)
            {
                throw new InvalidOperationException("Element must be in a Canvas control");
            }

            var dragging = false;
            var offset = new Point();

            element.MouseLeftButtonDown += (_, e) =>
            {
                dragging = true;
                offset = e.GetPosition(element);
            };

            element.MouseLeftButtonUp += (_, __) => dragging = false;
            canvas.LostFocus += (_, __) => dragging = false;

            canvas.MouseEnter += (_, e) =>
            {
                dragging = dragging && e.LeftButton == MouseButtonState.Pressed;
                UpdateShapePosition(dragging, element, onDrag, canvas, offset, e);
            };

            element.MouseMove += (_, e) => UpdateShapePosition(dragging, element, onDrag, canvas, offset, e);

            element.Cursor = Cursors.SizeAll;
        }

        private static void UpdateShapePosition(bool dragging, FrameworkElement element, Action<double, double> onDrag, Canvas canvas, Point offset, MouseEventArgs e)
        {
            if (!dragging) return;

            var position = e.GetPosition(canvas);
            var left = position.X - offset.X;
            var top = position.Y - offset.Y;
            left = left < 0 ? 0 : left;
            top = top < 0 ? 0 : top;
            if (left + element.ActualWidth > canvas.ActualWidth)
            {
                left = canvas.ActualWidth - element.ActualWidth;
            }
            if (top + element.ActualHeight > canvas.ActualHeight)
            {
                top = canvas.ActualHeight - element.ActualHeight;
            }
            Canvas.SetLeft(element, left);
            Canvas.SetTop(element, top);
            if (onDrag != null)
            {
                onDrag(left, top);
            }
        }
    }
}
