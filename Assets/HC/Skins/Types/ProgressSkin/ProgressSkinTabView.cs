using System.Collections.Generic;

namespace HC
{
    public class ProgressSkinTabView : HCSkinsTabView<ProgressSkin>
    {
        public override string TabName => "Backgrounds";
        public override string TabHint => "Unlock backgrounds by passing levels.";

        protected override IReadOnlyList<SkinViewConfig> viewConfigs => ProgressSkinsViewConfig.instance.items;
    }
}