using Godot;

namespace Base.Resources.Ui
{
    public abstract class ResourceTooltipParent<R> : TooltipParent where R : Resource
    {        
        /// <summary>
        /// the resource data displayed in the tooltip.
        /// </summary>
        /// <value></value>
        public R Data { get; set; }
    }
}