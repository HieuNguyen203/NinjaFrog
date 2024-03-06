using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenuManager : MonoBehaviour
{
    [SerializeField] private LevelObject[] levelObjects;
    [SerializeField] private Sprite goldenStar;
    [SerializeField] private Animator transition;

    public static int currentLevel;
    public static int unlockedLevels;

    public void ReturnToStart()
    {
        StartCoroutine(nameof(ChangeScene));
    }

    public void LevelClick(int level)
    {
        currentLevel = level;
        StartCoroutine(nameof(ChangeLevel), level);
    }

    private void Start()
    {
        unlockedLevels = PlayerPrefs.GetInt("unlockedLevels", 1);

        for (int i = 0; i < levelObjects.Length; i++) 
        {
            if (i >= unlockedLevels)
                return;

            levelObjects[i].button.interactable = true;
            int stars = PlayerPrefs.GetInt("stars" + (i + 1).ToString(), 0);
            for(int j = 0; j < stars; j++)
            {
                levelObjects[i].stars[j].sprite = goldenStar;
            }
        }
    }

    IEnumerator ChangeScene()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Start");
    }

    IEnumerator ChangeLevel(int level)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Level " + level);
    }
}
