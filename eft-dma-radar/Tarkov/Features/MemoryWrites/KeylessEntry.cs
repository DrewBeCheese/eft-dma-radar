using eft_dma_radar.Tarkov.Features;
using eft_dma_shared.Common.DMA.ScatterAPI;
using eft_dma_shared.Common.Features;
using eft_dma_shared.Common.Misc.Commercial;
using eft_dma_radar.Tarkov.GameWorld;
using eft_dma_radar.Tarkov.EFTPlayer;

namespace LonesEFTRadar.Tarkov.Features.MemoryWrites
{
    public sealed class KeylessEntry : MemWriteFeature<KeylessEntry>
    {
        public override bool Enabled
        {
            get => MemWrites.Config.KeylessEntry;
            set => MemWrites.Config.KeylessEntry = value;
        }

        protected override TimeSpan Delay => TimeSpan.FromMilliseconds(500);

        public override void TryApply(ScatterWriteHandle writes)
        {
            //try
            //{
            //    if (Enabled && Memory.Game is LocalGameWorld game)
            //    {
            //        if (Memory.LocalPlayer is LocalPlayer localPlayer)
            //        {
            //            var listItemPtr = Memory.ReadPtr(localPlayer.InventoryControllerAddr + Offsets.InventoryController.ListItem);

            //        }
                   
            //    }
            //}
            //catch (Exception ex)
            //{
            //    LoneLogging.WriteLine($"ERROR configuring NoVisor: {ex}");
            //}
        }
    }
}
