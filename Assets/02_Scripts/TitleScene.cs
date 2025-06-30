using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    public void SetHeroName(string heroName)
    {
        GameManager.Instance.SetHeroName(heroName);
    }

    public void LoadPlayScene()
    {
        SceneManager.LoadScene("Play");
    }
}
