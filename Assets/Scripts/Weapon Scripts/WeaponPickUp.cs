using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    //[SerializeField] private Weapon _weaponScript;
    //[SerializeField] private Transform _weapon;
    //[SerializeField] private Transform _gunHolder;
    [SerializeField] private Transform _camera;

    [SerializeField] private float _pickUpRange = 4;
    [SerializeField] private float _dropForwardForce = 25f;
    [SerializeField] private float _dropUpwardForce = 3f;
    //[SerializeField] private bool _equiped;

    private WeaponSwitching _weaponSwitching;
    private PlayerController _playerController;
    private GameObject _weaponToEquip;

    private void Awake()
    {
        _pickUpRange = 4;
        _dropForwardForce = 35f;
        _dropUpwardForce = 9f;
    }

    void Start()
    {
        _camera = Camera.main.GetComponent<Transform>();
        _weaponSwitching = GetComponent<WeaponSwitching>();
        _playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        CheckPlayerInRangeOfWeapon();
    }

    private void CheckPlayerInRangeOfWeapon()
    {
        //Vector3 distanceToWeapon = transform.position - _weapon.position;

        //if (_weaponSwitching.HasWeaponSelected == false && distanceToWeapon.magnitude <= _pickUpRange && Input.GetKeyDown(KeyCode.E))
        //{
        //    PickUpWeapon();
        //}
        //else if (transform.childCount&& _weaponSwitching.HasWeaponSelected == false && distanceToWeapon.magnitude <= _pickUpRange && Input.GetKeyDown(KeyCode.E))
        //{

        //}
        if (_weaponSwitching.HasWeaponSelected == true && Input.GetKeyDown(KeyCode.Q))
        {
            DropWeapon();
        }
    }

    private void PickUpWeapon()
    {
        _weaponSwitching.HasWeaponSelected = true;

        //_rigidbody.isKinematic = true;
        //_collider.isTrigger = true;



        //currently only implementing for 2 weapons
        //check if no weapon is selected then just pick weapon up
        //if weapon is selected check how many weapons player has
    }

    private void DropWeapon()
    {
        Debug.Log("throw the gun!!!!");

        _weaponSwitching.HasWeaponSelected = false;

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeInHierarchy)
            {
                if (transform.childCount == 1)
                {
                    _weaponToEquip = null;
                }
                else if (transform.GetChild(i + 1) != null)
                {
                    _weaponToEquip = transform.GetChild(i + 1).gameObject;
                }
                else
                {
                    _weaponToEquip = transform.GetChild(0).gameObject;
                }


                transform.GetChild(i).GetComponent<Weapon>().enabled = false;
                var rigidbody = transform.GetChild(i).GetComponent<Rigidbody>();
                rigidbody.isKinematic = false;
                transform.GetChild(i).GetComponent<Collider>().isTrigger = false;

                transform.GetChild(i).SetParent(null);

                //gun carries momentum of player
                rigidbody.velocity = _playerController.Move;

                //add external force
                rigidbody.AddForce(_camera.forward * _dropForwardForce, ForceMode.Impulse);
                rigidbody.AddForce(_camera.up * _dropUpwardForce, ForceMode.Impulse);

                float randomRotation = Random.Range(-1, 1);

                rigidbody.AddTorque(new Vector3(randomRotation, randomRotation, randomRotation));
            }
        }

        bool weaponToEquipExists = _weaponToEquip != null;
        if (weaponToEquipExists)
        {
            _weaponToEquip.SetActive(value: true);
        }

        //check if player has a weapon selected
        //if player has no weapon pick up weapon
        //if player has weapon selected then drop the weapon then check if player has secondary weapon then make that primary and select
    }
}
