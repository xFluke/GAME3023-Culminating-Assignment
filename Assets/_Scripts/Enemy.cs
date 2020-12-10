using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy", order = 1)]
public class Enemy : ScriptableObject
{
    [SerializeField]
    private new string name;

    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    private float maxHP;
}
