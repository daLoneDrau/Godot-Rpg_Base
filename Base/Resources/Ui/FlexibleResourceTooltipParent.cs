using Godot;

namespace Base.Resources.Ui
{
    public abstract class FlexibleResourceTooltipParent<RESOURCE, TOOLTIP> : FlexibleTooltipParent
    where RESOURCE : Resource
    where TOOLTIP : ResourceTooltip<RESOURCE>
    {        
        /// <summary>
        /// the resource data displayed in the tooltip.
        /// </summary>
        /// <value></value>
        public RESOURCE Data { get; set; }
        protected override void SetTooltipDisplay(Tooltip tooltip)
        {
            // uncomment these if there's any casting issues to verify the data and tooltip types
            // GD.Print("Data ", Data.GetType());
            // GD.Print("tooltip ", tooltip.GetType());
            ((TOOLTIP)tooltip).Data = Data;
        }
    }
}