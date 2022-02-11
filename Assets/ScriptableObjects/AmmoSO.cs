using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "defaultAmmo", menuName = "Ammo")]
public class AmmoSO : ScriptableObject
{
    [SerializeField] private GameObject ammoObject;
    [SerializeField] private int damage = 0;
    [SerializeField] private float speed = 0.0f;
    [SerializeField] private float disableTimer = 5.0f;
    public enum AmmoTypes { Primary, Secondary}
    [SerializeField] private AmmoTypes ammoType;

    #region Getters

    public GameObject AmmoObject => ammoObject;
    public int Damage => damage;
    public float Speed => speed;
    public float DisableTimer => disableTimer;
    public int AmmoType => ((int)ammoType);

    #endregion

}