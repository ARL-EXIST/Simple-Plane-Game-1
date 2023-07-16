using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private bool gameHasEnded = false;

    [Header("GameObjects")]
    public GameObject rock;
    public GameObject brokenRock;
    public GameObject failLevelUI;

    [Header("Components")]
    public PlayerMovement pM;
    public Camera view;
    private Rigidbody rb;
    public float depth;

    private Vector3 velocity;

    public Rect cameraRect;
    private float farClip;

    public float restartDelay = 2f;
    public int playerHealth = 3;
    public float crashSlow = 2/3;
    private float distanceSpawn;
    //private float initialSpawn;
    private float distancePlayer;
    private float spawnAmount;
    private float iterableSpawnDistance = 20f;

    private float xSpawn, ySpawn;

    private void Start()
    {
        //get distance of player from camera (used for screen movement limitations)
        depth = GameObject.Find("Player").GetComponent<Transform>().transform.position.z - view.transform.position.z;

        screenSize();

        rb = GameObject.Find("Player").GetComponent<Rigidbody>();
        //rock = GameObject.Find("Rock");
        //brokenRock = GameObject.Find("BrokenRock");

        farClip = view.farClipPlane;
        
        while(distanceSpawn < farClip){
            SpawnNextRocks();
        }
        //initialSpawn = distanceSpawn;
    }

    private void Update()
    {
        if(playerHealth <= 0){
            EndGame();
        }
        else if(view.transform.position.z > 0 && view.transform.position.z > distanceSpawn - farClip){
            //Debug.Log("spawn");
            SpawnNextRocks();
            pM._xDrag += 0.0001f;
            pM._yDrag += 0.0001f;
        }
        else{
            //Debug.Log(view.transform.position.z);
        }
        screenSize();
    }

    public void screenSize(){
        var bottomLeft = view.ScreenToWorldPoint(new Vector3 (0, 0, depth));
        var topRight = view.ScreenToWorldPoint(new Vector3(view.pixelWidth, view.pixelHeight, depth));

        cameraRect = new Rect(bottomLeft.x, bottomLeft.y, topRight.x - bottomLeft.x, topRight.y - bottomLeft.y);
    }

    public void FailLevel()
    {
        failLevelUI.SetActive(true);
    }

    public void SpawnNextRocks(){
        for(float i = 0; i < spawnAmount; i++){
            SpawnRock();
        }
        Debug.Log("Spawn " + spawnAmount);
        spawnAmount += 0.1f;
        distanceSpawn += iterableSpawnDistance;
    }
    public void SpawnRock(){
        xSpawn = Random.Range(cameraRect.xMin, cameraRect.xMax);
        //Debug.Log("x: " + cameraRect.xMin + ", " + cameraRect.xMax);
        ySpawn = Random.Range(cameraRect.yMin, cameraRect.yMax);
        //Debug.Log("y: " + cameraRect.yMin + ", " + cameraRect.yMax);
        //Debug.Log(xSpawn + ", " + ySpawn);
        Vector3 spawnPosition = new Vector3(xSpawn, ySpawn, distanceSpawn);
        Instantiate(rock, spawnPosition, Random.rotation);
        //a.GetComponent<Interactable>().clone = true;
    }

    public void Damaged()
    {
        velocity = rb.velocity;
        //Debug.Log("Damaged!");
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
            if(restartDelay > 1){
                Invoke("FailLevel", restartDelay/4);
            }
                Invoke("Restart", restartDelay + 1);
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
