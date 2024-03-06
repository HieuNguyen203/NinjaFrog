using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasButton : MonoBehaviour
{
    [SerializeField] private Animator transition;
    public void ReplayButtonClick()
    {
        StartCoroutine(Replay());
    }

    public void ReturnMenuButtonClick()
    {
        StartCoroutine(Return());
    }

    IEnumerator Replay()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator Return()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Levels Menu");
    }
}
