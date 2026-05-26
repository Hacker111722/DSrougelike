using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Enemy
{

[System.Serializable]
public class EnemyConfig
{
    [Header("敌人类型")]
    public EnemyType enemyType;

    [Header("基础属性")]
    public float moveSpeed =2f;
    public int maxHealth = 3;
    public int contactDamage = 1;    //碰撞伤害
    public float sizeScale = 1f;     //体型缩放

    [Header("外观")]
    public Color spriteColor = Color.white;

    [Header("额外效果参数")]
    public float explosionRadius = 0f;    //自爆半径，0表示无爆炸

    [Header("所需最低玩家等级")]
    public int minPlayerLevel = 1;      //至少玩家等级 >= 此值才出现

}
}