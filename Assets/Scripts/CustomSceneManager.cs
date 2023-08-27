using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour
{
    [SerializeField] Animator uiAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TransitionScene(int buildNumber) {
        StartCoroutine(LoadSceneAfterTime(1.5f, buildNumber));
    }

    IEnumerator LoadSceneAfterTime(float delay, int buildNumber) {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(buildNumber);
    }
}
