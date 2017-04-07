﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System.Collections.Generic;
using Draw2D.Core.Renderers;
using Draw2D.Core.Shapes;

namespace Draw2D.Core.Editor.Tools.Helpers
{
    public class QuadraticBezierHelper : CommonHelper
    {
        public void Draw(object dc, ShapeRenderer r, QuadraticBezierShape quadraticBezier)
        {
            DrawLine(dc, r, quadraticBezier.StartPoint, quadraticBezier.Point1);
            DrawLine(dc, r, quadraticBezier.Point1, quadraticBezier.Point2);
        }

        public override void Draw(object dc, ShapeRenderer r, ShapeObject shape, ISet<ShapeObject> selected)
        {
            if (shape is QuadraticBezierShape quadraticBezier)
            {
                Draw(dc, r, quadraticBezier);
            }
        }
    }
}