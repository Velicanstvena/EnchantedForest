using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesDisplay : MonoBehaviour
{
    [SerializeField] float baseLives = 3f;
    float lives;
    Text livesText;

    
    private void Awake()
    {
        int num = FindObjectsOfType<LivesDisplay>().Length;
        if (num > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        lives = baseLives - PlayerPrefsController.GetDifficulty();
        livesText = GetComponent<Text>();
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        livesText.text = lives.ToString();
    }

    public void TakeLife()
    {
        lives -= 1;
        UpdateDisplay();

        if (lives >= 1) {
            FindObjectOfType<LevelLoader>().RestartScene();
        }

        if (lives <= 0)
        {
            FindObjectOfType<LevelLoader>().LoadYouLose();
        }
    }
}
