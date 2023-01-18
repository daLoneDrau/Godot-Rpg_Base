using Base.Exceptions;
using Base.Resources.Bus;
using Bus.Services;
using Godot;
using System;

namespace Base.Resources.Data
{
    public abstract class IoNpcData : IoData
    {
        /// <summary>
        /// the current behaviour.
        /// </summary>
        /// <returns></returns>
        private IoBehaviourData behaviour = new IoBehaviourData();
        /// <summary>
        /// the NPC's stack of behaviours.
        /// </summary>
        private IoBehaviourData[] behaviours = new IoBehaviourData[] { new IoBehaviourData(), new IoBehaviourData(), new IoBehaviourData(), new IoBehaviourData(), new IoBehaviourData() };
        private int movementMode = Globals.WALKMODE;
        /// <summary>
        /// the movement mode.
        /// </summary>
        /// <value></value>
        public int MovementMode
        {
            get
            {
                return this.movementMode;
            }
            set
            {
                if (value < Globals.WALKMODE || value > Globals.SNEAKMODE) {
                    throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "IoBehaviourData.MovementMode - '" + value + "' is not recognized");
                }
                this.movementMode = value;
            }
        }
        /// <summary>
        /// The weapon the NPC is attacking with.
        /// </summary>
        /// <value></value>
        public InteractiveObject Weapon { get; set; }
        /// <summary>
        /// Changes an NPC behaviour.
        /// </summary>
        /// <param name="behaviour">the behaviour</param>
        public void ChangeBehaviour(IoBehaviourData behaviour) {
            if (behaviour == null) {
                throw new RPGException(ErrorMessage.INTERNAL_BAD_ARGUMENT, "IoNpcData.changeBehaviour() requires a valid behaviour");
            }
            // check to see if current behaviour is fighting
            if (this.behaviour.Is(Globals.BEHAVIOUR_FIGHT)
                && !behaviour.Is(Globals.BEHAVIOUR_FIGHT))
            {
                // changing from fighting to somethine else. STOP ALL FIGHT ANIMATIONS
                /*
                ANIM_USE * ause1 = &io->animlayer[1];
                AcquireLastAnim(io);
                FinishAnim(io, ause1->cur_anim);
                ause1->cur_anim = NULL;
                */
            }

            if (this.behaviour.Is(Globals.BEHAVIOUR_NONE)
                && !behaviour.Is(Globals.BEHAVIOUR_NONE)) {
                // stop all ANIMATIONS
                /*
                ANIM_USE * ause0 = &io->animlayer[0];
                AcquireLastAnim(io);
                FinishAnim(io, ause0->cur_anim);
                ause0->cur_anim = NULL;
                ANIM_Set(ause0, io->anims[ANIM_DEFAULT]);
                ause0->flags &= ~EA_LOOP;

                ANIM_USE * ause1 = &io->animlayer[1];
                AcquireLastAnim(io);
                FinishAnim(io, ause1->cur_anim);
                ause1->cur_anim = NULL;
                ause1->flags &= ~EA_LOOP;

                ANIM_USE * ause2 = &io->animlayer[2];
                AcquireLastAnim(io);
                FinishAnim(io, ause2->cur_anim);
                ause2->cur_anim = NULL;
                ause2->flags &= ~EA_LOOP;
                */
            }

            if (behaviour.Is(Globals.BEHAVIOUR_FRIENDLY)) {
                /*
                ANIM_USE * ause0 = &io->animlayer[0];
                AcquireLastAnim(io);
                FinishAnim(io, ause0->cur_anim);
                ANIM_Set(ause0, io->anims[ANIM_DEFAULT]);
                ause0->altidx_cur = 0;
                */
            }
            this.behaviour = behaviour;
        }
        public abstract bool IsDead();
        public void ResetBehaviour() {
            this.behaviour.Behaviour.Clear();
            this.behaviour.Behaviour.Add(Globals.BEHAVIOUR_NONE);

            for (int i = this.behaviours.Length - 1; i >= 0; i--) {
                this.behaviours[i].Exists = false;
            }
        }
        public void Revive() {
            this.Io.MainEvent = "MAIN";
            Io.Reset();

            // TODO - clean the texture, removing all gore and cuts
        }
        /// <summary>
        /// Stacks an existing NPC behaviour.
        /// </summary>
        public void StackBehaviour() {
            for (int i = this.behaviours.Length - 1; i >= 0; i--) {
                if (!this.behaviours[i].Exists) {
                    IoBehaviourData behaviour = this.behaviours[i];
                    behaviour = this.behaviour;
                    behaviour.MovementMode = this.movementMode;
                    behaviour.Exists = true;
                }
            }
        }
        /// <summary>
        /// Unstacks an existing NPC behaviour. 
        /// </summary>
        public void UnstackBehaviour() {
            for (int i = this.behaviours.Length - 1; i >= 0; i--)
            {
                if (this.behaviours[i].Exists)
                {
                    this.behaviour = this.behaviours[i];
                }
            }
        }
    }
}
