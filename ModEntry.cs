using System;
using System.Linq;
using StardewValley;
using StardewValley.TerrainFeatures;
using StardewModdingAPI;
using Netcode;

namespace walkthroughtrellis
{
    //Entry
    public class ModEntry : Mod
    {
        NetInt[] cropKeys =
        {
            new NetInt(473),
            new NetInt(301),
            new NetInt(302)
            //473, 301, 302
        };
        //Entry Method
        public override void Entry(IModHelper helper)
        {
            //implementing the event
            helper.Events.Player.Warped += cropUpdate;
            helper.Events.Player.InventoryChanged += cropUpdate;
        }
        //location change event
        public void cropUpdate(object sender, EventArgs e)
        {
            foreach (HoeDirt dirt in Game1.currentLocation.terrainFeatures.Values.OfType<HoeDirt>().Where(dirt => dirt.crop != null).ToArray())
            {
               //obtaining the actual crop ID using netcode.
               NetInt isCorrectCrop = this.Helper.Reflection.GetField<NetInt>(dirt.crop, "netSeedIndex").GetValue();
                foreach (int value in cropKeys)
                {
                    if(isCorrectCrop == value)
                    {
                        dirt.crop.raisedSeeds.Value = true;
                    }
                }
            }
        }
    }
}
