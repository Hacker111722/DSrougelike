using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//玩家属性系统
public class PlayerStats : MonoBehaviour
{
    [Header("当前等级")]
    public int level = 1;

    [Header("当前经验")]
    public int currentExp = 0;

    [Header("升级所需经验")]
    public int expToNextLevel = 5;

    [Header("最大生命值")]
    public int maxHealth = 10;

    [Header("移动速度")]
    public float moveSpeed = 7f;

    //获得经验
    public void AddExperience(int amount)
    {
        currentExp += amount;
        Debug.Log("获得经验");
        //升级检测
        if (currentExp >= expToNextLevel)
        {
            LevelUp();
        }
    }

    //升级
    private void LevelUp()
    {
        level++;
        //扣除经验
        currentExp -= expToNextLevel;
        //下一等级需要更多经验
        expToNextLevel += 5;
        //属性成长
        maxHealth += 2;
        moveSpeed += 0.2f;
        Debug.Log("升级！当前等级："+level);
        //更新玩家移动速度
        PlayerController playerController = GetComponent<PlayerController>();

        if (playerController != null)
        {
            playerController.moveSpeed = moveSpeed;
        }

    }



    }
