using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.Load();
    }

    public void SetHeroName(string heroName)
    {
        GameManager.Instance.HeroData.SetHeroName(heroName);
    }

    public void LoadPlayScene()
    {
        SceneManager.LoadScene("Play");
    }
}
