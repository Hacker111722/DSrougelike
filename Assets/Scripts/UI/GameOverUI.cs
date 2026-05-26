using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace Game.UI
{
public class GameOverUI : MonoBehaviour
{
    [Header("UI 组件")]
    public GameObject gameOverPanel;

    [Header("显示文本")]
    public TextMeshProUGUI levelText;
    //击杀数量
    public TextMeshProUGUI killCountText;

    



    // Start is called before the first frame update
    void Start()
    {
        //初始隐藏
        gameOverPanel.SetActive(false);
        
    }
    
    //显示游戏结束界面
    public void ShowGameOver(int level)
    {
        gameOverPanel.SetActive(true);

        if(levelText!=null)
        {
            levelText.text = "最终等级： "+level;
        }
    }

    //重新开始
    public void RestartGame()
    {
        Time.timeScale = 1f;   //恢复时间
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //退出游戏
    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    




}
}