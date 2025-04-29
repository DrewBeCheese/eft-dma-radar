using eft_dma_shared.Common.Misc.Commercial;
using eft_dma_shared.Common.Unity;

namespace LonesEFTRadar.Tarkov.GameWorld.Interactables
{
    public sealed class Door
    {
        private static readonly uint[] _transformInternalChain =
        [
            ObjectClass.MonoBehaviourOffset,
            MonoBehaviour.GameObjectOffset,
            GameObject.ComponentsOffset,
            0x8
        ];
        public ulong Base { get; set; }
        public EDoorState DoorState { get; set; }
        public string Id { get; set; }
        public string? KeyId { get; set; }
        public Vector3 Position { get; set; }

        public Door(ulong ptr)
        {
            try
            {
                //Setting the values that aren't going to change. That way we can just check the state on refresh
                Base = ptr;
                var keyidPtr = Memory.ReadPtr(Base + Offsets.Interactable.KeyId, false);
                var doorIdPtr = Memory.ReadPtr(Base + Offsets.Interactable.Id, false);
                var transformInternal = Memory.ReadPtrChain(Base, _transformInternalChain, false);
                var transform = new UnityTransform(transformInternal);

                Position = transform.UpdatePosition();
                KeyId = Memory.ReadUnityString(keyidPtr);
                Id = Memory.ReadUnityString(doorIdPtr);
                DoorState = (EDoorState)Memory.ReadValue<byte>(Base + Offsets.Interactable._doorState);
            }
            catch (Exception e)
            {
                LoneLogging.WriteLine($"ANDREW: Error in door class {e.Message}");
            }
        }

        public void Refresh()
        {
            DoorState = (EDoorState)Memory.ReadValue<byte>(Base + Offsets.Interactable._doorState);
        }
    }

    public enum EDoorState
    {
        // Token: 0x0400E90E RID: 59662
        None = 0,
        // Token: 0x0400E90F RID: 59663
        Locked = 1,
        // Token: 0x0400E910 RID: 59664
        Shut = 2,
        // Token: 0x0400E911 RID: 59665
        Open = 4,
        // Token: 0x0400E912 RID: 59666
        Interacting = 8,
        // Token: 0x0400E913 RID: 59667
        Breaching = 16
    }
}
