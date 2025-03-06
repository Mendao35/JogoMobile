using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCoins : PowerUpBase
{
    [Header("Coin Collector")]
    public float sizeAmount = 9f;
    

    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        PlayerController.Instance.ChangeCoinCollectorSize(sizeAmount);
        PlayerController.Instance.SetPowerUpText("Coin Collect");
    }

    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        PlayerController.Instance.ChangeCoinCollectorSize(1f);
        PlayerController.Instance.SetPowerUpText("");
    }
}
