using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField] private Animator transition;
    public void StartButtonClick()
    {
        StartCoroutine(nameof(ChangeScene));
    }

    IEnumerator ChangeScene()
    {        
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Levels Menu");
    }
}
