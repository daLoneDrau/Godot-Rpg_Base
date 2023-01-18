using System;
using Base.Exceptions;
using Base.Pooled;
using Godot;

namespace Base.Resources.Ui
{
    public class TextEntryWidget
    {
        /// <summary>
        /// The callback action when the player presses enter.
        /// </summary>
        /// <value></value>
        public Action Callback { get; set; }
        /// <summary>
        /// The animated cursor instance.
        /// </summary>
        /// <value></value>
        public AnimatedSprite Cursor { get; set; }
        /// <summary>
        /// The Label instance where text is being entered.
        /// </summary>
        /// <value></value>
        public Label Label { get; set; }
        /// <summary>
        /// The maximum # of characters that can be entered.
        /// </summary>
        /// <value></value>
        public int MaxLength { get; set; } = -1;
        /// <summary>
        /// The offset amount the cursor is moved.
        /// </summary>
        /// <value></value>
        public int Offset { get; set; } = -1;
        /// <summary>
        /// The parent control.
        /// </summary>
        /// <value></value>
        public Node Parent { get; set; }
        /// <summary>
        /// the stringbuilder instance
        /// </summary>
        private PooledStringBuilder sb;
        public string Text {
            get
            {
                return sb.ToString();
            }
        }
        /// <summary>
        /// Creates a new NameEntryWidget instance.
        /// </summary>
        public TextEntryWidget()
        {
            sb = StringBuilderPool.Instance.GetStringBuilder();
        }
        /// <summary>
        /// Validates the NameEntryWidget.
        /// </summary>
        private void Validate()
        {
            if (Offset < 0)
            {
                throw new RPGException(ErrorMessage.INTERNAL_ERROR, "Offset was never set");
            }
            if (MaxLength < 0)
            {
                throw new RPGException(ErrorMessage.INTERNAL_ERROR, "MaxLength was never set");
            }
            if (Parent == null)
            {
                throw new RPGException(ErrorMessage.INTERNAL_ERROR, "Parent was never set");
            }
            if (Label == null)
            {
                throw new RPGException(ErrorMessage.INTERNAL_ERROR, "Label was never set");
            }
            if (Cursor == null)
            {
                throw new RPGException(ErrorMessage.INTERNAL_ERROR, "Cursor was never set");
            }
            if (Callback == null)
            {
                throw new RPGException(ErrorMessage.INTERNAL_ERROR, "Callback was never set");
            }
        }
        /// <summary>
        /// Handles key input.
        /// </summary>
        public void HandleKeyInput(InputEventKey @event)
        {
            switch (((InputEventKey)@event).Scancode)
            {
                case (uint)KeyList.Alt:
                case (uint)KeyList.Shift:
                    // do not handle these
                    break;
                case (uint)KeyList.Escape:
                    // allow Escape to close the dialog
                    break;
                case (uint)KeyList.Enter:
                    Callback();
                    Parent.GetTree().SetInputAsHandled();
                    break;
                case (uint)KeyList.Space:
                    if (sb.Length >= 8)
                    {
                        sb.Length = 7;
                        Cursor.MoveLocalX(-16f);
                    }
                    sb.Append(" ");
                    Cursor.MoveLocalX(16f);
                    Label.Text = sb.ToString();
                    Parent.GetTree().SetInputAsHandled();
                    break;
                case (uint)KeyList.Backspace:
                    if (sb.Length > 0)
                    {
                        sb.Length = sb.Length - 1;
                        Cursor.MoveLocalX(-16f);
                        Label.Text = sb.ToString();
                    }
                    break;
                default:
                    string character = Godot.OS.GetScancodeString(((InputEventKey)@event).Scancode);
                    if (character.Length > 1)
                    {
                        character = character.Substr(character.Length - 1, 1);
                    }
                    if (sb.Length >= 8)
                    {
                        sb.Length = 7;
                        Cursor.MoveLocalX(-16f);
                    }
                    sb.Append(character);
                    Cursor.MoveLocalX(16f);
                    Label.Text = sb.ToString();
                    break;
            }
        }
    }
}