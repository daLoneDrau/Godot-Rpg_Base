using Base.Resources.Ui;
using Godot;

namespace Base.Resources.Ui.Themes.C64GEOS
{
    public class C64GEOSFlatButton : FlatStyleButton
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
            // outerStyle = (StyleBoxFlat)GetStylebox("focus").Duplicate();
            // this.Set("custom_styles/focus", outerStyle);
            // GD.Print("Initialize drawMode ", GetDrawMode(), OS.GetUnixTime());
        }
        protected override async void ButtonDown()
        {
            GD.Print("Button Down");
            /*
            // GD.Print("ButtonDown drawMode ", GetDrawMode(), OS.GetUnixTime());
            // when the button is down, change the inner color to light purple.
            outerStyle.BgColor = new Godot.Color("4a4a4a");

            // change the border color
            outerStyle.BorderWidthBottom = 0;
            outerStyle.BorderWidthLeft = 0;
            outerStyle.BorderWidthRight = 0;
            outerStyle.BorderWidthTop = 0;

            // remove the shadow
            outerStyle.ShadowSize = 0;

            // change the label's style
            // innerStyle.BgColor = new Godot.Color("887ecb");
            // inner.AddColorOverride("custom_colors/font_color", new Godot.Color("887ecb"));
            */
        }
        protected override async void ButtonPressed()
        {
            GD.Print("Button Pressed");
            /*
            // GD.Print("ButtonPressed drawMode ", GetDrawMode(), OS.GetUnixTime());
            // after release, change the inner color to dark purple.
            outerStyle.BgColor = new Godot.Color("a3a3a3");
            outerStyle.BorderWidthBottom = 2;
            outerStyle.BorderWidthLeft = 2;
            outerStyle.BorderWidthRight = 2;
            outerStyle.BorderWidthTop = 2;
            outerStyle.ShadowSize = 4;

            // innerStyle.BgColor = new Godot.Color("50459b");
            // inner.AddColorOverride("font_color", new Godot.Color("887ecb"));
            */
        }
        protected override async void ButtonUp()
        {
            GD.Print("Button Up");
            // this.SetPressedNoSignal(false);
        }
    }
}