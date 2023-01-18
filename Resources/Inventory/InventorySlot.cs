namespace Base.Resources.Inventory
{
  public class InventorySlot
    {
        /// <summary>
        /// The reference id of the IO in the Inventory Slot. A value of -1 means the slot is empty.
        /// </summary>
        public int Io { get; set; } = -1;
        /// <summary>
        /// The flag indicating whether the Inventory Slot item is shown.
        /// </summary>
        public bool Show { get; set; }
    }
}
