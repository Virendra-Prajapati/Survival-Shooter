using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator), typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private const string PLAYER_POSITION = "PlayerPosition";
 
	public float speed = 6f;

	Vector3 movement;
	Animator animator;
	Rigidbody playerRigidbody;
	int floorMask;
	float camRayLength = 100f;

	void Awake()
	{
		floorMask = LayerMask.GetMask ("Floor");
		animator = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
	}

    private void Start()
    {
        DatabaseManager.Instance.OnLoadGame += DatabaseManager_OnLoadGame;
        DatabaseManager.Instance.OnSaveGame += DatabaseManager_OnSaveGame;
    }

    private void DatabaseManager_OnSaveGame(object sender, System.EventArgs e)
    {
        CharacterData data = new(transform.position, transform.eulerAngles);
        string jsonString = DatabaseManager.GetJsonFromData(data);
        PlayerPrefs.SetString(PLAYER_POSITION, jsonString);
    }

    private void DatabaseManager_OnLoadGame(object sender, System.EventArgs e)
    {
        if (PlayerPrefs.HasKey(PLAYER_POSITION))
        {
            string jsonString = PlayerPrefs.GetString(PLAYER_POSITION);
            CharacterData data = DatabaseManager.GetDataFromJson<CharacterData>(jsonString);
            transform.position = data.Position;
            transform.eulerAngles = data.Rotation;
        }
    }

    void FixedUpdate()
	{
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		Move(h, v);
		Turning();
		Animating(h, v);
	}

	void Move(float h, float v)
	{
		movement.Set (h, 0, v);
		movement = movement.normalized * speed * Time.deltaTime;

		playerRigidbody.MovePosition (transform.position + movement);
	}

	void Turning()
	{
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;
		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) 
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0;

			var rotation = Quaternion.LookRotation(playerToMouse);
			playerRigidbody.MoveRotation(rotation);
		}
	}

	void Animating(float h, float v)
	{
		bool walking = h != 0f || v != 0f;
		animator.SetBool("IsWalking", walking);
	}

    
}