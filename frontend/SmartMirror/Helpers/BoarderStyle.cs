using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMirror.Helpers
{
    public static class BoarderStyle
    {
        public static GraphicsPath RoundSquare(int width, int height)
        {
            GraphicsPath path = new GraphicsPath();

            int cornerRadius = 10;

            // 좌상단
            path.AddArc(new Rectangle(0, 0, cornerRadius, cornerRadius), 180, 90);
            // 우상단
            path.AddArc(new Rectangle(width - cornerRadius, 0, cornerRadius, cornerRadius), 270, 90);
            // 우하단
            path.AddArc(new Rectangle(width - cornerRadius, height - cornerRadius, cornerRadius, cornerRadius), 0, 90);
            // 좌하단
            path.AddArc(new Rectangle(0, height - cornerRadius, cornerRadius, cornerRadius), 90, 90);

            path.CloseFigure();

            return path;
        }
    }
}
