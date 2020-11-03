using FriendsGamesTools;
using FriendsGamesTools.UI;
using TMPro;
using UnityEngine;

namespace HC
{
    public class GainedRewardView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI rewardText;
        [SerializeField] TweenInTime tween;
        public void SetReward(double reward) => rewardText.text = reward.ToStringWithSuffixes();
        public void PlayTween() => tween.SetEnabled(true);
    }
}