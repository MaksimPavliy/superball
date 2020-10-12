using FriendsGamesTools.ECSGame.Player.Money;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HCMoneyController : PlayerMoneyController
{
    public void AddWinMoney(double amount) => AddMoneySoaked(amount);
    public override void InitDefault()
    {
        base.InitDefault();
        SetStartMoney(HСGeneralConfig.instance.StartMoney);
    }
}
