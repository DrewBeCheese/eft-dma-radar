using eft_dma_shared.Common.Misc.Commercial;
using eft_dma_shared.Common.Unity;
using eft_dma_shared.Common.Unity.Collections;
using LonesEFTRadar.Tarkov.GameWorld.Interactables;

namespace LonesEFTRadar.Tarkov.GameWorld
{
    public sealed class WorldInteractablesManager
    {
        private readonly ulong _localGameWorld;
        public readonly HashSet<Door> _Doors;
        private HashSet<string> _Containers;
        private HashSet<string> _Trucks;
        private HashSet<string> _Swicthes;
        private HashSet<string> _KeyCard_Doors;

        public WorldInteractablesManager(ulong localGameWorld)
        {
            _localGameWorld = localGameWorld;
            _Doors = new();
            Init();
        }

        public void Init()
        {
            try
            {
                var world = Memory.ReadPtr(_localGameWorld + Offsets.ClientLocalGameWorld.World, false);
                var interactableArrayPtr = Memory.ReadPtr(world + Offsets.World.Interactables, false);
                using var array = MemArray<ulong>.Get(interactableArrayPtr, false);
                var set = array.Where(x => x != 0x0).ToHashSet();
                array.Dispose();

                foreach (var item in set)
                {
                    var itemName = ObjectClass.ReadName(item);
                    if (itemName == "Door")
                    {
                        _Doors.Add(new Door(item));
                    }
                }
            }
            catch (Exception e)
            {
                LoneLogging.WriteLine($"ANDREW: Error loading interactables, {e.Message}");
            }
        }

        public void Refresh()
        {
            if (_Doors.Count == 0)
            {
                LoneLogging.WriteLine($"ANDREW: No doors");
                Init();
            }
            foreach (var door in _Doors)
            {
                door.Refresh();
            }
        }
    }
}
