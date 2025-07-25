using UnityEngine;

public class Player : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;
	
	public Sprite[] sprites;
	
	private int spriteIndex;
	

	private Vector3 direction;
	
    public float gravity = -9.8f;
	
	public float strength = 5f;
	
	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	private void Start()
	{
		InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
	}
	
	private void OnEnable()
	{
		Vector3 position = transform.position;
		position.y = 0f;
		transform.position = position;
		direction = Vector3.zero;
	}
	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
		{
			Jump();
		}
		
		if(Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			
			if (touch.phase == TouchPhase.Began)
			{
				Jump();
			}
		}
		direction.y += gravity * Time.deltaTime;
		transform.position += direction * Time.deltaTime;
	}
	
	private void AnimateSprite()
	{
		spriteIndex++;
		
		if (spriteIndex >= sprites.Length)
		{
			spriteIndex = 0;
		}
		spriteRenderer.sprite = sprites[spriteIndex];
	}
	
	void Jump()
    {
        direction = Vector3.up * strength;
    }
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Obstacle")
		{
			FindObjectOfType<GameManager>().GameOver(); //Not allways the best funtion to use, very costly, there are better ways
		} else if(other.gameObject.tag == "Scoring")
		{
			FindObjectOfType<GameManager>().IncreaseScore();
		}
	}
}
