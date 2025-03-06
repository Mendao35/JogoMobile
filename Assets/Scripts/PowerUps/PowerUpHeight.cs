using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHeight : PowerUpBase
{
    [Header("Power Up Height")]
    public float amountHeight = 2f;
    public float animationDuration = .1f;

    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        PlayerController.Instance.ChangeHeight(amountHeight, animationDuration);
        PlayerController.Instance.SetPowerUpText("Height");
    }

    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        PlayerController.Instance.ResetHeight();
        PlayerController.Instance.SetPowerUpText("");
    }
}
