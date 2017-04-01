﻿using Draw2D.Editor;
using Draw2D.Models.Shapes;

namespace Draw2D.PathDemo.Tools
{
    public class QuadraticBezierTool : ToolBase
    {
        public QuadraticBezierShape _quadraticBezier = null;

        public enum QuadraticBezierToolState { StartPoint, Point1, Point2 }
        public QuadraticBezierToolState CurrentState = QuadraticBezierToolState.StartPoint;

        public override string Name { get { return "QuadraticBezier"; } }

        public override void LeftDown(IToolContext context, double x, double y, Modifier modifier)
        {
            switch (CurrentState)
            {
                case QuadraticBezierToolState.StartPoint:
                    var next = context.GetNextPoint(x, y, false, 0.0);
                    _quadraticBezier = new QuadraticBezierShape()
                    {
                        StartPoint = next,
                        Point1 = next.Clone(),
                        Point2 = next.Clone(),
                        Style = context.CurrentStyle
                    };
                    context.CurrentContainer.Shapes.Add(_quadraticBezier);
                    context.Selected.Add(_quadraticBezier);
                    context.Capture();
                    context.Invalidate();
                    CurrentState = QuadraticBezierToolState.Point2;
                    break;
                case QuadraticBezierToolState.Point1:
                    _quadraticBezier.Point1 = context.GetNextPoint(x, y, false, 0.0);
                    CurrentState = QuadraticBezierToolState.StartPoint;
                    context.Selected.Remove(_quadraticBezier);
                    _quadraticBezier = null;
                    context.Release();
                    context.Invalidate();
                    break;
                case QuadraticBezierToolState.Point2:
                    _quadraticBezier.Point1.X = x;
                    _quadraticBezier.Point1.Y = y;
                    _quadraticBezier.Point2 = context.GetNextPoint(x, y, false, 0.0);
                    CurrentState = QuadraticBezierToolState.Point1;
                    context.Invalidate();
                    break;
            }
        }

        public override void RightDown(IToolContext context, double x, double y, Modifier modifier)
        {
            switch (CurrentState)
            {
                case QuadraticBezierToolState.Point1:
                case QuadraticBezierToolState.Point2:
                    {
                        this.Clean(context);
                    }
                    break;
            }
        }

        public override void Move(IToolContext context, double x, double y, Modifier modifier)
        {
            switch (CurrentState)
            {
                case QuadraticBezierToolState.Point1:
                    _quadraticBezier.Point1.X = x;
                    _quadraticBezier.Point1.Y = y;
                    context.Invalidate();
                    break;
                case QuadraticBezierToolState.Point2:
                    _quadraticBezier.Point1.X = x;
                    _quadraticBezier.Point1.Y = y;
                    _quadraticBezier.Point2.X = x;
                    _quadraticBezier.Point2.Y = y;
                    context.Invalidate();
                    break;
            }
        }

        public override void Clean(IToolContext context)
        {
            base.Clean(context);

            CurrentState = QuadraticBezierToolState.StartPoint;
            if (_quadraticBezier != null)
            {
                context.CurrentContainer.Shapes.Remove(_quadraticBezier);
                context.Selected.Remove(_quadraticBezier); 
            }
            _quadraticBezier = null;
            context.Release();
            context.Invalidate();
        }
    }
}
