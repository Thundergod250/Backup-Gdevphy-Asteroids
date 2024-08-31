using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameplay : MonoBehaviour
{
    public static Gameplay instance;
    public static int score;
    public static bool isGameRunning;
    public TextMeshProUGUI scoreText;
    private Vector2 camBounds;
    [SerializeField] private GameObject asteroidObj;
    [SerializeField] private GameObject youWinText;
    public LayerMask playerLayer;

    private void Awake()
    {
        instance = this; 
        Application.targetFrameRate = 60;
        camBounds = Camera.main.ScreenToWorldPoint(Vector2.zero); 
        asteroidObj.SetActive(false);
    }
    private void Update()
    {
        if (score >= 100)
        {
            Time.timeScale = 0;
            youWinText.SetActive(true);
        }
        scoreText.text = score.ToString(); 
    }

    private void OnDestroy()
    {
        instance = null;
    }

    private void Start()
    {
        StartCoroutine(CO_SpawnAsteroids(1)); 
    }

    IEnumerator CO_SpawnAsteroids(int interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            Vector3 spawnPoint = new Vector2(Random.Range(-camBounds.x, camBounds.x), Random.Range(-camBounds.y, camBounds.y));
            if (!Physics2D.OverlapCircle(spawnPoint, 2f, playerLayer))
                Instantiate(asteroidObj, spawnPoint, Quaternion.identity).SetActive(true);
        }
    }

    public void ScreenWrapping(Transform _obj)
    {
        Vector2 _objPos = _obj.position;
        float _offset = _obj.localScale.x / 2;

        if (_objPos.x < camBounds.x - _offset)
            _objPos.x = -camBounds.x + _offset;
        else if (_objPos.x > -camBounds.x + _offset)
            _objPos.x = camBounds.x - _offset; 

        if (_objPos.y < camBounds.y - _offset)
            _objPos.y = -camBounds.y + _offset;
        else if (_objPos.y > -camBounds.y + _offset)
            _objPos.y = camBounds.y - _offset;
        _obj.position = _objPos;
    }
}
