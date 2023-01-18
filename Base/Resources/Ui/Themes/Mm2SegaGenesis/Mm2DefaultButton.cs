using Godot;

namespace Base.Resources.Ui.Themes.Mm2SegaGenesis
{
    /// <summary>
    /// A default button borrowing the look from Might and Magic II: Gates to Another World - Sega Genesis platform.
    /// </summary>
    public class Mm2DefaultButton : HoverActionButton
    {
        public override void OnMouseExited()
        {
            GetParent().GetNode<AnimatedSprite>("./decoration").Visible = false;
        }
        public override void OnMouseEntered()
        {
            if (!Disabled)
            {
                GetParent().GetNode<AnimatedSprite>("./decoration").Visible = true;
            }
        }
    }
}