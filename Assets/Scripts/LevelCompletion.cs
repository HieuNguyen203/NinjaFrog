using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompletion : MonoBehaviour
{
    private AudioSource complete_sound;
    private bool is_enter = false;

    [SerializeField] private CollectItems collectItems;

    [SerializeField] private int appleTotal = 0;
    [SerializeField] private Animator transition;

    // Start is called before the first frame update
    private void Start()
    {
        complete_sound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !is_enter) 
        {
            complete_sound.Play();
            is_enter = true;

            int appleCount = collectItems.GetAppleCount();

            StartCoroutine(CompleteLevel(appleCount, appleTotal));
        }
    }

    private int CalculateStar(int appleCount, int appleTotal)
    {
        if (appleCount == appleTotal)
            return 3;

        if(appleCount >= 0.75 * appleTotal) 
            return 2;

        if(appleTotal >= 0.5 * appleTotal) 
            return 1;

        return 0;
    }

    private IEnumerator CompleteLevel(int appleCount, int appleTotal)
    {
        transition.SetTrigger("Start");
        
        yield return new WaitForSeconds(1f);

        if (LevelMenuManager.currentLevel == LevelMenuManager.unlockedLevels)
        {
            LevelMenuManager.unlockedLevels++;
            PlayerPrefs.SetInt("unlockedLevels", LevelMenuManager.unlockedLevels);
        }

        int stars = CalculateStar(appleCount, appleTotal);

        if (stars > PlayerPrefs.GetInt("stars" + LevelMenuManager.currentLevel.ToString(), 0))
        {
            PlayerPrefs.SetInt("stars" + LevelMenuManager.currentLevel.ToString(), stars);
        }

        SceneManager.LoadScene("Levels Menu");
    }
}
