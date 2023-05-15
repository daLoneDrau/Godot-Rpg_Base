using Godot;

namespace Base.Scenes.Controllers
{
  public class GeosController : Control
    {
        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// </summary>
        public override void _Ready()
        {
            base._Ready();
            // Load the custom images for the mouse cursor.
            /*
            var arrow = ResourceLoader.Load("res://theme/nes/widgets/cursor.png");
            var hand = ResourceLoader.Load("res://theme/nes/widgets/cursor-click.png");
            // Changes only the arrow shape of the cursor.
            // This is similar to changing it in the project settings.
            Input.SetCustomMouseCursor(arrow, Input.CursorShape.Arrow, new Vector2(0, 0));
            Input.SetCustomMouseCursor(hand, Input.CursorShape.PointingHand, new Vector2(0, 0));
            */
        }
    }
}