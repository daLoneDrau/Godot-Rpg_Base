using System;
using Base.Resources.Ui;
using Godot;

namespace Base.Resources.Ui.Themes.C64GEOS
{
    public class C64GEOSTabButton : FlatStyleButton
    {
        private Texture borderDefault;
        private Texture borderSelected;
        private ButtonGroup group;
        /*
        private Label inner;
        private StyleBoxEmpty innerStyle;
        */
        private StyleBoxFlat outerStyle;
        protected override void Initialize()
        {
            borderDefault = (Texture)ResourceLoader.Load("res://art/themes/commodore_64_geos/widgets/nav-item-border.jpg");
            borderSelected = (Texture)ResourceLoader.Load("res://art/themes/commodore_64_geos/widgets/nav-item-border-selected.jpg");
            group = this.GetButtonGroup();
            // GD.Print("Initialize ", this.GetParent().GetChildren());
        }
        protected override async void ButtonDown()
        {
            // GD.Print("Button Down");
        }
        protected override async void ButtonPressed()
        {
            // GD.Print("Button Pressed ", this.Name);
            foreach (Button button in group.GetButtons())
            {
                if (!button.Name.Equals(Name, StringComparison.OrdinalIgnoreCase))
                {
                    ((C64GEOSTabButton)button).SetAppearance();
                }                
            }
            SetAppearance();
        }
        protected override async void ButtonUp()
        {
            // GD.Print("Button Up");
        }
        public void SetAppearance()
        {
            Node last = null;
            bool lastWasMe = false;
            // iterate over all siblings
            foreach (Node node in this.GetParent().GetChildren())
            {
                if (node.Name.Equals(this.Name, StringComparison.OrdinalIgnoreCase))
                {
                    // if the sibling is Me
                    lastWasMe = true;
                    if (last != null)
                    {
                        // if there was a previous sibling, it must be a TextureRect. change the image.
                        if (Pressed)
                        {
                            ((TextureRect)last).Texture = borderSelected;
                        }
                        else
                        {
                            ((TextureRect)last).Texture = borderDefault;
                        }
                    }
                }
                else
                {
                    // current sibling is not me
                    if (lastWasMe)
                    {
                        // i was the last sibling in the iteration, this one must be a TextureRect. change the image.
                        if (Pressed)
                        {
                            ((TextureRect)node).Texture = borderSelected;
                        }
                        else
                        {
                            ((TextureRect)node).Texture = borderDefault;
                        }
                        // change the flag; I am no longer the last sibling
                        lastWasMe = false;
                    }
                    else
                    {
                        // this sibling is now the last node
                        last = node;
                    }
                }
            }
        }
    }
}