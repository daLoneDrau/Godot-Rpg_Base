using Godot;

namespace Base.Ui
{
    [Tool]
    public class Pulse : RichTextEffect
    {
        public static string bbcode = "pulse";
        public override bool _ProcessCustomFx(CharFXTransform charFx)
        {
            // Get parameters, or use the provided default value if missing.
            Color color = charFx.Color;
            if (charFx.Env.Contains("color"))
            {
                color = (Color)charFx.Env["color"];
            }

            float height = 0f, freq = 2f;
            if (charFx.Env.Contains("height"))
            {
                height = (float)charFx.Env["height"];
            }
            if (charFx.Env.Contains("freq"))
            {
                freq = (float)charFx.Env["freq"];
            }
            float sinedTime = (Mathf.Sin(charFx.ElapsedTime * freq) + 1f) / 2f;
            float yOffset = sinedTime * height;
            color = new Color(color, 1f);
            charFx.Color = charFx.Color.LinearInterpolate(color, sinedTime);
            charFx.Offset = new Vector2(0, -1) * yOffset;
            
            return true;
        }
    }
}