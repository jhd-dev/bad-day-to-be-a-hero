using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "EnemyProfile")]
public class Enemy : ScriptableObject
{
    public string name;
    public int cost;
    public string desc;
    public Sprite artwork;
    public GameObject enemyGameObject;
}
