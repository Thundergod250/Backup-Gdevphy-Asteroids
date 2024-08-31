using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    private void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (Input.GetKeyDown(KeyCode.E) && Time.timeScale == 0)
        {
            Gameplay.score = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1; 
        }            
    }
}
