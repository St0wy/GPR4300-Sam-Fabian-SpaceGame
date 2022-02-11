using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fire", menuName = "Weapons")]
public class FireSO : ScriptableObject
{
    [SerializeField] private GameObject fireObject;
    [SerializeField] private int damage = 0;
    [SerializeField] private float speed = 0.0f;
}
