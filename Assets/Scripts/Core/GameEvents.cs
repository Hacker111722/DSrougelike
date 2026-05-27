using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Game.Core
{
    ///<summary>
    /// 游戏全局事件中心（Event bus）
    /// 所有跨模块通信通过这里的静态事件完成
    /// 
    /// 设计模式：观察者模式
    /// 
    /// 任何脚本都可以 .Invoke()触发事件
    /// 任何脚本都可以 += 订阅事件
    /// 发布者和订阅者互不知晓对方存在
    /// </summary>
    public static class GameEvents
    {
        //敌人死亡事件
        public static event Action<Vector2> OnEnemyKilled;

        //敌人死亡时调用，触发“敌人死亡事件”，position死亡位置用于特效、掉落物生成
        public static void EnemyKilled(Vector2 position)
        {
            OnEnemyKilled?.Invoke(position);
        }
    }
}
