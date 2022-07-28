using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int _selectedWeapon = 0;
    public bool HasWeaponSelected = false;
    //public bool WeaponSlotFull = false;

    private void OnEnable()
    {
        SelectWeapon();
    }

    private void Update()
    {
        ChangeWeaponInput();
        CheckIfWeaponSelected();
    }

    private void ChangeWeaponInput()
    {
        int previousSelectedWeapon = _selectedWeapon;

        bool hasScrolledUp = Input.GetAxis("Mouse ScrollWheel") > 0;
        bool hasScrolledDown = Input.GetAxis("Mouse ScrollWheel") < 0;
        bool selectFirstWeaponUsingKey = Input.GetKeyDown(KeyCode.Alpha1);
        bool selectSecondWeaponUsingKey = Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2;
        bool selectThirdFirstWeaponUsingKey = Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount > 2;


        if (hasScrolledUp /*|| Input.GetKeyDown(KeyCode.Alpha1)*/)
        {
            if (_selectedWeapon >= transform.childCount - 1)
            {
                _selectedWeapon = 0;
            }
            else
            {
                _selectedWeapon++;
            }
        }

        if (hasScrolledDown /*|| Input.GetKeyDown(KeyCode.Alpha2)*/)
        {
            if (_selectedWeapon <= 0)
            {
                _selectedWeapon = transform.childCount - 1;
            }
            else
            {
                _selectedWeapon--;
            }
        }

        if (selectFirstWeaponUsingKey)
        {
            _selectedWeapon = 0;
        }

        if (selectSecondWeaponUsingKey)
        {
            _selectedWeapon = 1;
        }

        if (selectThirdFirstWeaponUsingKey)
        {
            _selectedWeapon = 1;
        }

        if (previousSelectedWeapon != _selectedWeapon)
        {
            SelectWeapon();
        }
    }

    private void SelectWeapon()
    {
        Debug.Log($"My child count is: {transform.childCount}");
        int i = 0;

        foreach (Transform weapon in transform)
        {
            bool shouldEnableWeapon = i == _selectedWeapon;
            if (shouldEnableWeapon)
            {
                weapon.gameObject.SetActive(value: true);
            }
            else
            {
                weapon.gameObject.SetActive(value: false);
            }
            i++;
        }

        HasWeaponSelected = true;
    }

    private bool CheckIfWeaponSelected()
    {
        if (transform.childCount == 0)
        {
            HasWeaponSelected = false;
        }
        else if (transform.childCount > 0)
        {
            HasWeaponSelected = true;
        }

        return HasWeaponSelected;
    }
}
