using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class GameManager : MonoBehaviour
{
	public Player player;

	public TextMeshProUGUI scoreText;
	
	public GameObject playButton;
	
	public GameObject gameOver;
	
	public Sprite[] digitSprites; // Array of digit sprites (0-9)
	
    private int score;
	
	private void Awake()
	{
		Application.targetFrameRate = 60;
	
		Pause();
	}
	
	private void Start()
	{
		
	}
	
	public void Play()
	{
		score = 0;
		//scoreText.text = score.ToString();
		
		playButton.SetActive(false);
		gameOver.SetActive(false);
		
		Time.timeScale = 1f;
		player.enabled = true;
		
		Pipes[] pipes = FindObjectsOfType<Pipes>();
		
		for (int i=0; i < pipes.Length; i++) {
			Destroy(pipes[i].gameObject);
		}
	}
	
	public void Pause()
	{
		Time.timeScale = 0f;
		player.enabled = false;
	}
	
	public void GameOver()
	{
		gameOver.SetActive(true);
		playButton.SetActive(true);
		
		Pause();
	}
	
	public void IncreaseScore()
	{
        score++;
        // Update the score text (if needed)
        scoreText.text = score.ToString();

        // Get the digits of the score
        int[] scoreDigits = GetDigits(score);

        // Clear existing digit sprites
        foreach (Transform child in scoreText.transform)
        {
            Destroy(child.gameObject);
        }

        // Create digit sprites based on the score
        for (int i = 0; i < scoreDigits.Length; i++)
        {
            int digitValue = scoreDigits[i];
            GameObject digitObject = new GameObject("Digit" + i); // Create a new GameObject for each digit
            digitObject.transform.SetParent(scoreText.transform); // Set the scoreText as parent
            Image digitImage = digitObject.AddComponent<Image>(); // Add Image component
            digitImage.sprite = digitSprites[digitValue]; // Assign the sprite from the digitSprites array
        }
		
		int[] GetDigits(int n)
		{
			System.Collections.Generic.List<int> digitsList = new System.Collections.Generic.List<int>();

			do
			{
				int digit = n % 10;
				digitsList.Add(digit);
				n /= 10;
			} while (n > 0);

			// Reverse the list so that the digits are in the correct order
			digitsList.Reverse();

			// Convert the list to an array
			return digitsList.ToArray();

		}
		
	}
}
