using Godot;

namespace Base.Ui
{
    [Tool]
    public class Ghost : RichTextEffect
    {
        public static string bbcode = "ghost";
        public override bool _ProcessCustomFx(CharFXTransform charFx)
        {
            // Get parameters, or use the provided default value if missing.
            float speed = 5f, span = 10f;
            if (charFx.Env.Contains("freq"))
            {
                speed = (float)charFx.Env["freq"];
            }
            if (charFx.Env.Contains("span"))
            {
                span = (float)charFx.Env["span"];
            }

            float alpha = Mathf.Sin(charFx.ElapsedTime * speed + (charFx.AbsoluteIndex / span)) * 0.5f + 0.5f;
            charFx.Color = new Color(charFx.Color, alpha);
            return true;
        }
    }
}