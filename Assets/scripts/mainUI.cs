using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainUI : MonoBehaviour
{
    [SerializeField] private Button play;
    [SerializeField] private Button quuit;
    
    // Start is called before the first frame update
    void Start()
    {
      
    }
    private void Awake()
    {
       
          play.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1);
        });
        quuit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        Time.timeScale = 1.0f;
    
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
