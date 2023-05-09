using Godot;
using Godot.Collections;

namespace Base.Ui
{
    [Tool]
    public class Reveal : RichTextEffect
    {
        public static string bbcode = "reveal";
        public override bool _ProcessCustomFx(CharFXTransform charFx)
        {
            // hide each character for 0.5 seconds
            // Get parameters, or use the provided default value if missing.
            float speed = 0.2f;
            if (charFx.Env.Contains("speed"))
            {
                speed = (float)charFx.Env["speed"];
            }
            float hiddenDuration = speed * (float)charFx.AbsoluteIndex;
            if (charFx.ElapsedTime < hiddenDuration)
            {
                charFx.Color = new Color(1, 1, 1, 0);
            }

            return true;
        }
    }
}