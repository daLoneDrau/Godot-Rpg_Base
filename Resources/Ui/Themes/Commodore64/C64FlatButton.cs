using Godot;

namespace Base.Resources.Ui.Themes.Commodore64
{
    public class C64FlatButton : FlatStyleButton
    {
        /*
        private Label inner;
        private StyleBoxEmpty innerStyle;
        */
        private StyleBoxFlat outerStyle;
        protected override void Initialize()
        {
            /* code below is a legacy from when there was an inner label
            inner = GetChild<Label>(0);
            innerStyle = new StyleBoxEmpty();
            {
                innerStyle.ContentMarginLeft = -1f;
                innerStyle.ContentMarginRight = -1f;
                innerStyle.ContentMarginTop = 1f;
                innerStyle.ContentMarginBottom = -1f;
                // innerStyle.BgColor = new Godot.Color("50459b");
                inner.AddStyleboxOverride("normal", innerStyle);
            }
            */
            outerStyle = (StyleBoxFlat)GetStylebox("focus").Duplicate();
            this.Set("custom_styles/focus", outerStyle);
            // GD.Print("Initialize drawMode ", GetDrawMode(), OS.GetUnixTime());
        }
        protected override async void ButtonDown()
        {
            GD.Print("Button Down");
            // GD.Print("ButtonDown drawMode ", GetDrawMode(), OS.GetUnixTime());
            // when the button is down, change the inner color to light purple.
            outerStyle.BgColor = new Godot.Color("887ecb");

            // change the label's style
            // innerStyle.BgColor = new Godot.Color("887ecb");
            // inner.AddColorOverride("custom_colors/font_color", new Godot.Color("887ecb"));
        }
        protected override async void ButtonPressed()
        {
            GD.Print("Button Pressed");
            // GD.Print("ButtonPressed drawMode ", GetDrawMode(), OS.GetUnixTime());
            // after release, change the inner color to dark purple.
            outerStyle.BgColor = new Godot.Color("50459b");

            // innerStyle.BgColor = new Godot.Color("50459b");
            // inner.AddColorOverride("font_color", new Godot.Color("887ecb"));
        }
        protected override async void ButtonUp()
        {
            GD.Print("Button Up");
        }
    }
}