using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWeapon : MonoBehaviour
{
    public AlertConfirmPanel alertConfirmPanel;

    public void Random()
    {
        Debug.Log("Random Weapon");
    }

    public void SilverRandomButton() => alertConfirmPanel.Confirm("Silver Random, it costs 1 point", Random);

    public void GoldRandomButton() => alertConfirmPanel.Confirm("Gold Random, it costs 3 point", Random);

    public void PlatinumRandomButton() => alertConfirmPanel.Confirm("Platinum Random, it costs 5 point", Random);
}
