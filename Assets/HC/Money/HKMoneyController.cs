using FriendsGamesTools.ECSGame.Player.Money;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HC
{
    public class HCMoneyController : PlayerMoneyController
    {
        public new int amount => Mathf.RoundToInt((float)base.amount);
        public void AddWinMoney(double amount) => AddMoneySoaked(amount);
        public override void InitDefault()
        {
            base.InitDefault();
            SetStartMoney(HСGeneralConfig.instance.StartMoney);
        }
    }
}