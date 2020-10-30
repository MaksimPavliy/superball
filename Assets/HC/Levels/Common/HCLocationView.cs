using FriendsGamesTools;
using FriendsGamesTools.ECSGame.Locations;
using UnityEngine;

namespace HC
{
    public class HCLocationView: LocationView
    {
        [SerializeField] string _locationName;
        public string locationName => _locationName.IsNullOrEmpty() ? $"Level {HCRoot.instance.levels.currLocationInd + 1}" : _locationName;
    }
}