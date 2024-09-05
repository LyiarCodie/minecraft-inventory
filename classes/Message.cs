namespace minecraft_inventory.classes
{
    internal struct Message
    {
        public MessageOrder Order;
        public byte SlotId;
        public Item Item;
        public Slot[] CurrentSlots;

        public Message(MessageOrder order, byte slotId)
        {
            Order = order;
            SlotId = slotId;
        }

        public Message(MessageOrder order, byte slotId, Item item)
        {
            Order = order;
            SlotId = slotId;
            Item = item;
        }
        public Message(MessageOrder order, Item item)
        {
            Order = order;
            Item = item;
        }
        public Message(MessageOrder order, Slot[] slots)
        {
            Order = order;
            CurrentSlots = slots;
        }
    }
}