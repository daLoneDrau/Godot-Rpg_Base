using Base.Resources.Ui;
using Godot;

namespace Base.Resources.Ui.Themes.C64GEOS
{
    public class C64GEOSFlatButton : FlatStyleButton
    {
        private Label inner;
        private StyleBoxFlat innerStyle;
        protected override void Initialize()
        {
            inner = GetChild<Label>(0);
            innerStyle = new StyleBoxFlat();
            innerStyle.ContentMarginLeft = 2f;
            innerStyle.ContentMarginRight = 2f;
            innerStyle.ContentMarginTop = 2f;
            innerStyle.ContentMarginBottom = 2f;
            innerStyle.BgColor = new Godot.Color("50459b");
            inner.AddStyleboxOverride("normal", innerStyle);
        }

        protected override async void ButtonDown()
        {
            innerStyle.BgColor = new Godot.Color("887ecb");
            inner.AddColorOverride("font_color", new Godot.Color("50459b"));
        }
        protected override async void ButtonPressed()
        {
            innerStyle.BgColor = new Godot.Color("50459b");
            inner.AddColorOverride("font_color", new Godot.Color("887ecb"));
        }
    }
}