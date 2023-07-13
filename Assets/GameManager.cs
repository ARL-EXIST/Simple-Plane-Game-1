using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private bool gameHasEnded = false;

    [Header("GameObjects")]
    [SerializeField] private Transform Rock;
    [SerializeField] private Transform BrokenRock;

    [Header("Components")]
    public PlayerMovement pM;
    public Camera view;
    private Rigidbody rb;

    private Vector3 velocity;

    public Rect cameraRect;

    public float restartDelay = 2f;
    public int playerHealth = 3;
    public float crashSlow = 2/3;

    private float xSpawn, ySpawn;

    private void Start()
    {
        screenSize();

        rb = GameObject.Find("Player").GetComponent<Rigidbody>();

        float farClip = view.farClipPlane;
        
        for(float i = 0; i < farClip; i += 50){
            xSpawn = Random.Range(cameraRect.xMin, cameraRect.xMax);
            ySpawn = Random.Range(cameraRect.yMin, cameraRect.yMax);
            Vector3 spawnPosition = new Vector3(xSpawn, ySpawn, i);
            Instantiate(Rock, spawnPosition, Random.rotation);
        }
    }

    private void Update()
    {
        if(playerHealth <= 0){
            EndGame();
        }
        else
        {

        }
        screenSize();
    }

    public void screenSize(){
        var bottomLeft = view.ScreenToWorldPoint(new Vector3 (0, 0, pM.depth));
        var topRight = view.ScreenToWorldPoint(new Vector3(view.pixelWidth, view.pixelHeight, pM.depth));

        cameraRect = new Rect(bottomLeft.x, bottomLeft.y, topRight.x - bottomLeft.x, topRight.y - bottomLeft.y);
    }

    public void Damaged()
    {
        velocity = rb.velocity;
        Debug.Log("Damaged!");
        playerHealth--;
        velocity.z *= crashSlow;
        rb.velocity = velocity;
    }

    public void EndGame()
    {
        if(!gameHasEnded){
            pM.getInput = false;
            pM._xDrag = pM._yDrag = pM._zDrag = 1.8f;
            gameHasEnded = true;
            Debug.Log("Game Over");
            Invoke("Restart", restartDelay);
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
