using System.Collections.Generic;
using FriendsGamesTools;
using FriendsGamesTools.ECSGame;
using Unity.Entities;

namespace HC
{
    public struct ProgressSkin : IComponentData {
        public int percentsProgress;
        public int skinIndToUnlock;
    }
    public class ProgressSkinController : HCSkinsController<ProgressSkin>
    {
        ProgressSkinConfig config => ProgressSkinConfig.instance;
        ProgressSkinsViewConfig viewConfig => ProgressSkinsViewConfig.instance;
        public override IReadOnlyList<SkinViewConfig> viewConfigs => ProgressSkinsViewConfig.instance.items;
        ProgressSkin data => entity.GetComponentData<ProgressSkin>();
        public int percents => data.percentsProgress;
        public int skinIndToUnlock => data.skinIndToUnlock;
        public override void InitDefault()
        {
            base.InitDefault();
            entity.ModifyComponent((ref ProgressSkin p) => {
                p.percentsProgress = 0;
                p.skinIndToUnlock = GetNextSkinIndToUnlock(-1);
            });
        }
        public void GiveWinProgress(int multiplier)
        {
            var skinIndToUnlock = this.skinIndToUnlock;
            var percents = Utils.Random(config.percentsPerLevelMin, config.percentsPerLevelMax);
            percents *= multiplier;
            percents += this.percents;
            if (percents >= 100)
            {
                percents = 0;
                UnlockSkin(skinIndToUnlock);
                skinIndToUnlock = GetNextSkinIndToUnlock(skinIndToUnlock);
            }

            entity.ModifyComponent((ref ProgressSkin p)=> {
                p.percentsProgress = percents;
                p.skinIndToUnlock = skinIndToUnlock;
            });
        }
        int GetNextSkinIndToUnlock(int prevSkinIndToUnlock)
        {
            if (!anySkinLocked) return -1;
            var skinIndToUnlock = prevSkinIndToUnlock;

            do
            {
                skinIndToUnlock++;
            } while (IsLocked(skinIndToUnlock));
            return skinIndToUnlock;
        }
    }
}