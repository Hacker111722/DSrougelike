using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
//玩家状态属性
public class PlayerStats : MonoBehaviour
{
    [Header("玩家等级")]
    public int level = 1;

    [Header("当前经验值")]
    public int currentExp = 0;

    [Header("升到下一个等级的经验值")]
    public int expToNextLevel = 5;

    [Header("当前生命值")]
    public int currentHealth = 10;

    [Header("最大生命值")]
    public int maxHealth = 10;

    [Header("移动速度")]
    public float moveSpeed = 7f;

    [Header("受伤无敌时间")]
    public float invincibleTime = 0.5f;

    //是否无敌
    private bool isInvincible = false;

    [Header("技能选择UI")]
    public SkillSelectionUI skillSelectionUI;

    [Header("游戏结束UI")]
    public GameOverUI gameOverUI;

    [Header("击杀回血量")]
    public int healthRegenAmount = 0;

    [Header("受伤闪烁")]
    public SpriteRenderer playerSprite;   //玩家的精灵渲染器
    public float flashInterval = 0.08f;   //闪烁间隔（秒）


    //获得经验
    public void AddExperience(int amount)
    {
        currentExp += amount;
        Debug.Log("得到了经验");
        //升级
        if (currentExp >= expToNextLevel)
        {
            LevelUp();
        }
    }


    //升级
    private void LevelUp()
    {
        level++;
        //消耗经验值
        currentExp -= expToNextLevel;
        //升到下一级的经验增加
        expToNextLevel += 5;
        //提升生命值
        maxHealth += 2;
        moveSpeed += 0.2f;
        Debug.Log("玩家升到了"+level);

        skillSelectionUI.ShowSkillSelection();

        //获取玩家控制类以便于对玩家行为属性数值进行调整
        PlayerController playerController = GetComponent<PlayerController>();

        if (playerController != null)
        {
            playerController.moveSpeed = moveSpeed;
        }

    }

    //受伤
    public void TakeDamage(int damage)
    {
        //无敌状态不受伤害
        if(isInvincible) return;

        currentHealth -= damage;
        Debug.Log("收到伤害，当前生命值："+currentHealth);

        //进入无敌状态
        StartCoroutine(InvincibleCoroutine());
        //检测死亡
        if(currentHealth <=0)
        {
            Die();
        }
    }

    private IEnumerator InvincibleCoroutine()
    {
        isInvincible = true;
        //计算需要闪烁多少次
        int flashCount = Mathf.FloorToInt(invincibleTime / flashInterval);
        //如果是奇数，补一次确保最后恢复显示
        if(flashCount % 2 != 0) flashCount++;

        for(int i=0;i<flashCount;i++)
        {
            //切换sprite的可见性
            if(playerSprite != null)
            {
                playerSprite.enabled = !playerSprite.enabled;
            }
            
            yield return new WaitForSeconds(flashInterval);
        }
        //确保最后恢复为可见
        if(playerSprite != null)
        {
            playerSprite.enabled = true;
        }
        isInvincible = false;
    }

    //死亡
    private void Die()
    {
        Debug.Log("游戏结束！");

        //显示游戏结束界面
        if(gameOverUI != null)
        {
            gameOverUI.ShowGameOver(level);
        }
        else
        {
            Time.timeScale = 0f;
        }
    }




}
}