﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MiniPaint
{
    abstract class Step
    {
        Pen pen;
        Point start;
        Point end;

       public abstract void Draw(); 
    }
}
