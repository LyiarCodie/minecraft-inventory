using minecraft_inventory.classes;

namespace minecraft_inventory
{
    internal interface Observer
    {
        public void UpdateByNotifier(Message message);
    }
}