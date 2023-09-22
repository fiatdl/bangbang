using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pauseUI : MonoBehaviour
{
    [SerializeField] private Button continueBtn;
    [SerializeField] private Button playAgainBtn;
    [SerializeField] private Button cancelBtn;
    void Start()
    {

        playAgainBtn.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
         
            SceneManager.LoadScene(gameManagement.Instance.nextSceneIndex-1);

        });
        cancelBtn.onClick.AddListener(() =>
        {
         
            SceneManager.LoadScene(0);

        });
        continueBtn.onClick.AddListener(() =>
        {
            Time.timeScale = 1.0f;
            gameObject.SetActive(false);
        });
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
