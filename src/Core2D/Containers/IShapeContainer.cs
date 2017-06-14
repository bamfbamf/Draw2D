﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Core2D.Shape;
using Core2D.Shapes;

namespace Core2D.Containers
{
    public interface IShapeContainer : IDrawable
    {
        double Width { get; set; }
        double Height { get; set; }
        ObservableCollection<LineShape> Guides { get; set; }
        ObservableCollection<BaseShape> Shapes { get; set; }
        IEnumerable<PointShape> GetPoints();
    }
}
