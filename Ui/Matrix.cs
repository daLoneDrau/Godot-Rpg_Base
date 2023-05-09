using Godot;

namespace Base.Ui
{
    [Tool]
    public class Matrix : RichTextEffect
    {
        public static string bbcode = "matrix";
        public override bool _ProcessCustomFx(CharFXTransform charFx)
        {
            // Get parameters, or use the provided default value if missing.
            float clearTime = 2f, dirtyTime = 1f;
            if (charFx.Env.Contains("clean"))
            {
                clearTime = (float)charFx.Env["clean"];
            }
            if (charFx.Env.Contains("dirty"))
            {
                dirtyTime = (float)charFx.Env["dirty"];
            }
            int textSpan = 50;
            if (charFx.Env.Contains("span"))
            {
                textSpan = (int)charFx.Env["span"];
            }
            int value = charFx.Character;

            float matrixTime = (float)((charFx.ElapsedTime + (charFx.AbsoluteIndex / (float)textSpan)) % (clearTime + dirtyTime));

            matrixTime = matrixTime < clearTime  ? 0f : (matrixTime - clearTime) / dirtyTime;

            if (value >= 65 && value < 126 && matrixTime > 0f)
            {
                value -= 65;
                value = value + (int)(1 * matrixTime * (126 - 65));
                value %= (126 - 65);
                value += 65;
            }
            charFx.Character = value;

            return true;
        }
    }
}