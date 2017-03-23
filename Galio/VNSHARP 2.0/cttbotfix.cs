using SharpDX;

namespace VNSHARP
{
    class cttbotfix
    {
        public static void DrawLine(float x, float y, float x2, float y2, float thickness, System.Drawing.Color color)
        {
            EloBuddy.SDK.Rendering.Line.DrawLine(color, thickness, new Vector2(x, y), new Vector2(x2, y2));
        }
    }
}
