using Godot;

namespace Base.Resources.Ui
{
    /// <summary>
    /// Abstract class for displaying tooltips.
    /// </summary>
    public abstract class ResourceTooltip<R> : Tooltip where R : Resource
    {
        /// <summary>
        /// the resource data displayed in the tooltip.
        /// </summary>
        /// <value></value>
        public R Data { get; set; }
    }
}