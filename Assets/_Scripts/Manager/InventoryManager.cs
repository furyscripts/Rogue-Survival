using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<WeaponController> weaponSlots = new List<WeaponController>(6);
    public int[] weaponLevels = new int[6];
    public List<Image> weaponUISlots = new List<Image>(6);
    public List<PassiveItem> passiveItemSlots = new List<PassiveItem>(6);
    public int[] passItemLevels = new int[6];
    public List<Image> passiveUISlots = new List<Image>(6);

    public void AddWeapon(int slotIndex, WeaponController weapon)
    {
        weaponSlots[slotIndex] = weapon;
        weaponLevels[slotIndex] = weapon.weaponSO.Level;
        weaponUISlots[slotIndex].enabled = true;
        weaponUISlots[slotIndex].sprite = weapon.weaponSO.Icon;
    }

    public void AddPassiveItem(int slotIndex, PassiveItem passiveItem)
    {
        passiveItemSlots[slotIndex] = passiveItem;
        passItemLevels[slotIndex] = passiveItem.passiveItemSO.Level;
        passiveUISlots[slotIndex].enabled = true;
        passiveUISlots[slotIndex].sprite = passiveItem.passiveItemSO.Icon;
    }

    public void LevelWeapon(int slotIndex)
    {
        if (weaponSlots.Count > slotIndex)
        {
            WeaponController weaponCtrl = weaponSlots[slotIndex];
            if (!weaponCtrl.weaponSO.NextLevelPrefab)
            {
                Debug.LogError("NO NEXT LEVEL FOR " + weaponCtrl.name);
                return;
            }
            GameObject upgradeWeapon = Instantiate(weaponCtrl.weaponSO.NextLevelPrefab, transform.position, Quaternion.identity);
            upgradeWeapon.transform.SetParent(transform);
            AddWeapon(slotIndex, upgradeWeapon.GetComponent<WeaponController>());
            Destroy(weaponCtrl.gameObject);
            weaponLevels[slotIndex] = upgradeWeapon.GetComponent<WeaponController>().weaponSO.Level;
        }
    }

    public void LevelPassiveItem(int slotIndex)
    {
        if (passiveItemSlots.Count > slotIndex)
        {
            PassiveItem passiveItem = passiveItemSlots[slotIndex];
            if (!passiveItem.passiveItemSO.NextLevelPrefab)
            {
                Debug.LogError("NO NEXT LEVEL FOR " + passiveItem.name);
                return;
            }
            GameObject upgradePassiveItem = Instantiate(passiveItem.passiveItemSO.NextLevelPrefab, transform.position, Quaternion.identity);
            upgradePassiveItem.transform.SetParent(transform);
            AddPassiveItem(slotIndex, upgradePassiveItem.GetComponent<PassiveItem>());
            Destroy(passiveItem.gameObject);
            passItemLevels[slotIndex] = upgradePassiveItem.GetComponent<PassiveItem>().passiveItemSO.Level;
        }
    }
}
