using System;
using System.Collections.Generic;
using Base.Resources.Bus;
using Base.Resources.EnumeratedTypes;
using Godot;

namespace Base.Resources.Bus
{
    public class GenderResource : Resource
    {
        /// <summary>
        /// The Gender's Title.
        /// </summary>
        /// <value></value>
        [Export]
        public string Title { get; set; }
        /// <summary>
        /// The Gender's Title.
        /// </summary>
        /// <value></value>
        [Export]
        public string DisplayName { get; set; }
        /// <summary>
        /// The Gender's Noun.
        /// </summary>
        /// <value></value>
        [Export]
        public string Noun { get; set; }
        /// <summary>
        /// The Gender's Pronoun.
        /// </summary>
        /// <value></value>
        [Export]
        public string Pronoun { get; set; }
        /// <summary>
        /// The Gender's ChildRelation.
        /// </summary>
        /// <value></value>
        [Export]
        public string ChildRelation { get; set; }
        /// <summary>
        /// The Gender's Objective.
        /// </summary>
        /// <value></value>
        [Export]
        public string Objective { get; set; }
        /// <summary>
        /// The Gender's Possessive.
        /// </summary>
        /// <value></value>
        [Export]
        public string Possessive { get; set; }
        /// <summary>
        /// The Gender's PossessiveObjective.
        /// </summary>
        /// <value></value>
        [Export]
        public string PossessiveObjective { get; set; }
        /// <summary>
        /// The Gender enumerated type this instance represents.
        /// </summary>
        /// <value></value>
        public Gender GenderEnum { get; set; }
    }
}
