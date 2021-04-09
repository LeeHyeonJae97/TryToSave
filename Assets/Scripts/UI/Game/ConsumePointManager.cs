using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumePointManager : MonoBehaviour
{
    [Header("Overall")]
    public Player player;
    public GameObject consumePointCanvas;
    public Text point;
    public GameObject backButton;

    public GameObject options;
    private GameObject curOption;

    public void SetActive(bool value)
    {
        if(value)
        {
            Time.timeScale = 0;

            consumePointCanvas.SetActive(true);
            if (curOption != null) curOption.SetActive(false);
            options.SetActive(true);

            backButton.SetActive(false);

            isUpgradeWeaponInit = isUpgradePlayerInit = false;

            point.text = string.Format("Point {0}", player.Point);
        }
        else
        {
            consumePointCanvas.SetActive(false);

            //**
            Time.timeScale = 1;
            //Timer.instance.SetTimer(3, () => Time.timeScale = 1);
        }
    }

    public void GoBack()
    {
        if(curOption == null)
        {
            Debug.LogError("Error");
            return;
        }

        options.SetActive(true);
        curOption.SetActive(false);
        backButton.SetActive(false);
    }

    private void LosePoint(int point)
    {
        player.Point -= point;
        this.point.text = string.Format("Point {0}", player.Point);
    }

    #region 랜덤 무기 획득
    [Header("Random Weapon")]
    public GameObject randomWeapon;

    public WeaponManager weaponManager;
    private WeaponInfo newWeaponInfo;

    public GameObject randomButtons;

    public GameObject decideEquip;
    public Image newWeaponImage;
    public Text newWeaponName;
    public Text[] newWeaponStats;
    public Button[] weaponSlots;
    public Image[] weaponSlotImages;
    private int slotIndex;

    public GameObject equipCancel;
    
    public void SelectRandomWeapon()
    {
        curOption = randomWeapon;

        options.SetActive(false);
        curOption.SetActive(true);

        randomButtons.SetActive(true);
        decideEquip.SetActive(false);
        equipCancel.SetActive(false);
        backButton.SetActive(true);        
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

    // 랜덤 무기 획득 버튼들 중 한가지를 선택해 클릭한 경우 랜덤한 무기 획득
    private void GetRandomWeapon(int point, int level, string msg)
    {
        AlertConfirmPanel.instance.Confirm(string.Format(msg), () =>
        {
            if (weaponManager.GetRandomWeapon(level, player.Point, out WeaponInfo info))
            {
                randomButtons.SetActive(false);
                decideEquip.SetActive(true);
                backButton.SetActive(false);

                LosePoint(point);

                newWeaponInfo = info;
                newWeaponImage.sprite = newWeaponInfo.weaponImage;
                newWeaponName.text = newWeaponInfo.weaponName;
                Stat[] stats = newWeaponInfo.stats;
                for (int i = 0; i < stats.Length; i++)
                    newWeaponStats[i].text = stats[i].GetValue(0).ToString();

                SetSlotImages();
            }
            else AlertConfirmPanel.instance.Alert("Need more point");
        });
    }

    // 현재 보유 중인 무기들의 이미지만 간단하게 표시
    private void SetSlotImages()
    {
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            if (player.weapons[i] != null)
                weaponSlotImages[i].sprite = player.weapons[i].WeaponImage;
        }
    }

    // 몇번째 슬롯에 새롭게 획득한 무기를 장착할지 선택
    public void ChooseWeaponSlot(int index)
    {
        slotIndex = index;
        if (!equipCancel.activeInHierarchy) equipCancel.SetActive(true);
        equipCancel.transform.position = weaponSlots[index].transform.position;
    }

    // 선택한 슬롯에 새로운 무기 장착
    public void EquipWeapon()
    {
        // 해당 슬롯에 이미 무기를 장착하고 있었다면 기존의 무기는 버려진다.
        string msg = player.weapons[slotIndex] == null ? "really want to Equip weapon?" :
            "Existed weapon will be lost. Really want to equip?";

        AlertConfirmPanel.instance.Confirm(msg, () =>
        {
            player.weapons[slotIndex] = weaponManager.GetWeapon(newWeaponInfo);

            if (player.Point > 0) SelectRandomWeapon();
            else SetActive(false);
        });
    }

    // 새롭게 획득한 무기를 버린다.
    public void DumpWeapon()
    {
        AlertConfirmPanel.instance.Confirm("Really want to dump new weapon?", () =>
        {
            if (player.Point > 0) SelectRandomWeapon();
            else SetActive(false);
        });
    }
    #endregion

    #region 무기 업그레이드
    [Header("Upgrade Weapon")]
    public GameObject upgradeWeapon;

    private bool isUpgradeWeaponInit;

    private Stat[] weaponStats;

    public Button[] equippedWeapons;
    public Image[] equippedWeaponImages;

    public Text weaponName;
    public Image weaponImage;
    public Text[] weaponStatTexts;
    public Button[] weaponLevelUps;

    public void SelectUpgradeWeapon()
    {
        curOption = upgradeWeapon;

        options.SetActive(false);
        curOption.SetActive(true);

        backButton.SetActive(true);

        if (!isUpgradeWeaponInit)
        {
            isUpgradeWeaponInit = true;

            for (int i = 0; i < equippedWeapons.Length; i++)
            {
                if (player.weapons[i] != null)
                {
                    equippedWeapons[i].interactable = true;
                    equippedWeaponImages[i].sprite = player.weapons[i].WeaponImage;
                }
                else
                {
                    equippedWeapons[i].interactable = false;
                    equippedWeaponImages[i].sprite = null;
                }
            }
        }

        ShowWeapon(0);
    }

    public void ShowWeapon(int index)
    {
        Weapon weapon = player.weapons[index];
        weapon.GetAllStats(out weaponStats);

        weaponName.text = weapon.WeaponName;
        weaponImage.sprite = weapon.WeaponImage;
        for (int i = 0; i < weaponStats.Length; i++)
            SetWeaponStat(i);        
    }

    private void SetWeaponStat(int index)
    {
        if (weaponStats[index].MaxLevel)
        {
            weaponStatTexts[index].text = string.Format("{0} -> Max Level", weaponStats[index].Value);
            weaponLevelUps[index].interactable = false;
        }
        else
        {
            weaponStatTexts[index].text = string.Format("{0} -> {1}", weaponStats[index].Value, weaponStats[index].NextLevelValue);
            weaponLevelUps[index].interactable = true;
        }
    }

    public void UpgradeWeapon(int index)
    {
        AlertConfirmPanel.instance.Confirm("Really wanto to level up this stat? You will lose 1 point", () => {

            // **
            // 값을 복사해오는 방식이라 Player에 직접적으로 스탯이 적용되지 않았을 수도 있다.
            weaponStats[index].Level += 1;
            LosePoint(1);

            if (player.Point > 0) SetWeaponStat(index);
            else SetActive(false);
        });
    }
    #endregion

    #region 플레이어 업그레이드
    [Header("Upgrade Player")]
    public GameObject upgradePlayer;

    private bool isUpgradePlayerInit;

    private Stat[] playerStats;
    
    public Text[] playerStatTexts;
    public Button[] playerLevelUps;

    public void SelectUpgradePlayer()
    {
        curOption = upgradePlayer;

        options.SetActive(false);
        curOption.SetActive(true);

        backButton.SetActive(true);

        if (!isUpgradePlayerInit)
        {
            isUpgradePlayerInit = true;

            playerStats = player.Stats;
            for (int i = 0; i < playerStats.Length; i++)
                SetPlayerStat(i);
        }
    }

    private void SetPlayerStat(int index)
    {
        if (playerStats[index].MaxLevel)
        {
            playerStatTexts[index].text = string.Format("{0} -> Max Level", playerStats[index].Value);
            playerLevelUps[index].interactable = false;
        }
        else
        {
            playerStatTexts[index].text = string.Format("{0} -> {1}", playerStats[index].Value, playerStats[index].NextLevelValue);
            playerLevelUps[index].interactable = true;
        }
    }

    public void UpgradePlayer(int index)
    {
       AlertConfirmPanel.instance.Confirm("Really want to level up this stat? You will lose 1 point", () => {
           playerStats[index].Level += 1;
           LosePoint(1);

           if (player.Point > 0) SetPlayerStat(index);
           else SetActive(false);
        });
    }
    #endregion

    #region 포인트 저장 (사용 보류)
    //[Header("Save Point")]

    public void SelectSavePoint()
    {
        AlertConfirmPanel.instance.Confirm("Really want to save your point?", () => { SetActive(false); });
    }
    #endregion
}
