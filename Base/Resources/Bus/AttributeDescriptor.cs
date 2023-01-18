using Base.Resources.Variables;
using Godot;
using System;

namespace Base.Resources.Bus
{
    public class AttributeDescriptor : Resource
    {
        /// <summary>
        /// The abbreviation.
        /// </summary>
        [Export]
        public string Abbreviation { get; set; }
        /// <summary>
        /// The Description.
        /// </summary>
        [Export]
        public string Description { get; set; }
        /// <summary>
        /// The abbreviation.
        /// </summary>
        [Export]
        public string DisplayName { get; set; }
        /// <summary>
        /// The attribute's id. Optional.
        /// </summary>
        [Export]
        public int Id { get; set; }
        /// <summary>
        /// The id of the Equipment Element modifier that will affect the Attribute's score. For example, if the attribute asset being created is STRENGTH, then there would need to be a corresponding STRENGTH_MODIFIER constant created as well, with a unique value that is the modifier's id.
        /// </summary>
        [Export]
        public IntVariable EquipmentElement { get; set; }
        /// <summary>
        /// The attribute's sort order. Optional.
        /// </summary>
        [Export]
        public int SortOrder { get; set; }
        /// <summary>
        /// The Title.
        /// </summary>
        [Export]
        public string Title { get; set; }
        [Export]
        public AttributeAssignmentStrategy AssignmentStrategy;
    }
}