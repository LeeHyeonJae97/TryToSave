using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumePointManager : MonoBehaviour
{
    public Player player;
    public GameObject consumePointCanvas;
    public Text point;
    public Button backButton;

    public void SetActive(bool value)
    {
        if (value)
        {
            Time.timeScale = 0;

            consumePointCanvas.SetActive(true);
            point.text = string.Format("Point {0}", player.Point);
        }
        else
        {
            isUpgradeWeaponInit = isUpgradePlayerInit = false;
            backButton.onClick.Invoke();
            consumePointCanvas.SetActive(false);

            Time.timeScale = 1;
            //Timer.instance.SetTimer(3, () => Time.timeScale = 1);
        }
    }

    #region 랜덤 무기 획득
    //private bool isRandomWeaponInit;
    public WeaponManager weaponManager;
    private WeaponInfo newWeaponInfo;
    public GameObject randomButtons;
    public GameObject decideEquip;
    public Image newWeaponImage;
    public Text newWeaponName;
    public Text[] newWeaponValues;
    public Button[] weaponSlots;
    public Transform equipCancel;
    private int slotIndex;

    public void SelectRandomWeapon()
    {
        ;
    }

    public void RandomWeaponBronze()
    {
        GetRandomWeapon(1, 0, "Bronze random weapon, you will lose 1 point.");
    }

    public void RandomWeaponSilver()
    {
        GetRandomWeapon(3, 1, "Silver random weapon, you will lose 3 point.");
    }

    public void RandomWeaponGold()
    {
        GetRandomWeapon(5, 2, "Gold random weapon, you will lose 5 point.");
    }

    private void GetRandomWeapon(int point, int level, string msg)
    {
        AlertConfirmPanel.instance.Confirm(string.Format(msg), () =>
        {
            if (weaponManager.GetRandomWeapon(level, player.Point, out WeaponInfo info))
            {
                randomButtons.SetActive(false);
                decideEquip.SetActive(true);
                backButton.gameObject.SetActive(false);

                player.Point -= point;

                newWeaponInfo = info;
                //newWeaponImage.sprite = newWeaponInfo.weaponImage;
                newWeaponName.text = newWeaponInfo.weaponName;
                Stat[] stats = newWeaponInfo.stats;
                for (int i = 0; i < stats.Length; i++)
                    newWeaponValues[i].text = stats[i].GetValue(0).ToString();

                SetSlotImages();
            }
            else AlertConfirmPanel.instance.Alert("Need more point");
        });
    }

    private void SetSlotImages()
    {
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            if (player.weapons[i] != null)
                weaponSlots[i].gameObject.GetComponent<Image>().color = Color.black;
            //weaponSlots[i].gameObject.GetComponent<Image>().sprite = player.weapons[i].WeaponImage;
        }
    }

    // 몇번째 슬롯에 새롭게 획득한 무기를 장착할지 선택
    public void ChooseWeaponSlot(int index)
    {
        slotIndex = index;
        equipCancel.gameObject.SetActive(true);
        equipCancel.position = weaponSlots[index].transform.position;
    }

    public void EquipWeapon()
    {
        string msg = player.weapons[slotIndex] == null ? "really want to Equip weapon?" : "Existed weapon will be lost. Really want to equip?";

        AlertConfirmPanel.instance.Confirm(msg, () =>
        {
            player.weapons[slotIndex] = weaponManager.GetWeapon(newWeaponInfo);
            randomButtons.SetActive(true);
            decideEquip.SetActive(false);
            equipCancel.gameObject.SetActive(false);
            SetActive(false);
        });
    }

    public void DumpWeapon()
    {
        AlertConfirmPanel.instance.Confirm("really want to dump new weapon?", () =>
        {
            randomButtons.SetActive(true);
            decideEquip.SetActive(false);
            equipCancel.gameObject.SetActive(false);
            SetActive(false);
        });
    }
    #endregion

    #region 무기 업그레이드
    private bool isUpgradeWeaponInit;
    //private Weapon[] weapons;
    private Stat[] weaponStats;
    public Button[] weaponImages;
    public Text[] weaponStatValues;
    public Button[] weaponLevelUps;

    public void SelectUpgradeWeapon()
    {
        if (!isUpgradeWeaponInit)
        {
            isUpgradeWeaponInit = true;

            for (int i = 0; i < weaponImages.Length; i++)
                weaponImages[i].interactable = player.weapons[i] != null;                
        }

        ShowWeapon(0);
    }

    public void ShowWeapon(int index)
    {
        Weapon weapon = player.weapons[index];
        weapon.GetAllStats(out weaponStats);

        for (int i = 0; i < weaponStats.Length; i++)
        {
            if (weaponStats[i].maxLevel)
            {
                weaponStatValues[i].text = string.Format("{0} -> MAX LEVEL", weaponStats[i].Value);
                weaponLevelUps[i].interactable = false;
            }
            else
            {
                weaponStatValues[i].text = string.Format("{0} -> {1}", weaponStats[i].Value, weaponStats[i].NextLevelValue);
                weaponLevelUps[i].interactable = true;
            }
        }
    }

    public void UpgradeWeapon(int index)
    {
        AlertConfirmPanel.instance.Confirm("Really want to level up this stat? You will lose 1 point.", () =>
        {
            weaponStats[index].Level += 1;
            player.Point -= 1;
            SetActive(false);
        });
    }
    #endregion

    #region 플레이어 업그레이드
    private bool isUpgradePlayerInit;
    private Stat[] playerStats;
    public Text[] playerStatValues;
    public Button[] playerLevelUps;

    public void SelectUpgradePlayer()
    {
        if (!isUpgradePlayerInit)
        {
            isUpgradePlayerInit = true;

            playerStats = player.Stats;
            for (int i = 0; i < playerStats.Length; i++)
            {
                if (playerStats[i].maxLevel)
                {
                    playerStatValues[i].text = string.Format("{0} -> MAX LEVEL", playerStats[i].Value);
                    playerLevelUps[i].interactable = false;
                }
                else
                {
                    playerStatValues[i].text = string.Format("{0} -> {1}", playerStats[i].Value, playerStats[i].NextLevelValue);
                    playerLevelUps[i].interactable = true;
                }
            }
        }
    }

    public void UpgradePlayer(int index)
    {
        AlertConfirmPanel.instance.Confirm("Really want to level up this stat? You will lose 1 point.", () =>
        {
            playerStats[index].Level += 1;
            player.Point -= 1;
            SetActive(false);
        });
    }
    #endregion

    #region 포인트 저장 (사용 보류)
    public void SelectSavePoint()
    {
        AlertConfirmPanel.instance.Confirm("Really want to save your point?", () => { SetActive(false); });
    }
    #endregion
}
