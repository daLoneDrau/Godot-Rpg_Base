using Godot;

namespace Base.Scenes.Controllers
{
  public class Commodore64Controller : Control
    {
        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// </summary>
        public override void _Ready()
        {
            base._Ready();
            // Load the custom images for the mouse cursor.
            var arrow = ResourceLoader.Load("res://theme/commodore_64/widgets/cursor_arrow.png");
            var forbidden = ResourceLoader.Load("res://theme/commodore_64/widgets/cursor_forbidden.png");
            var ibeam = ResourceLoader.Load("res://theme/commodore_64/widgets/cursor_ibeam.png");
            var pointingHand = ResourceLoader.Load("res://theme/commodore_64/widgets/cursor_pointing_hand.png");
            // Changes only the arrow shape of the cursor.
            // This is similar to changing it in the project settings.
            Input.SetCustomMouseCursor(arrow, Input.CursorShape.Arrow, new Vector2(0, 0));
            Input.SetCustomMouseCursor(forbidden, Input.CursorShape.Forbidden, new Vector2(0, 0));
            Input.SetCustomMouseCursor(ibeam, Input.CursorShape.Ibeam, new Vector2(0, 0));
            Input.SetCustomMouseCursor(pointingHand, Input.CursorShape.PointingHand, new Vector2(0, 0));
        }
    }
}