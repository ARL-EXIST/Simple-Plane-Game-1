using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //private int count = 1;

    private GameManager gM;

    [Header("Screen Bounding")]
    private Vector3 screenBounds;
    public Camera view;
    public float depth;
    public Vector3 velocity;

    [Header("Movement")]
    private Rigidbody rb;
    //speed increase and force increase effect the increase in speed over time,
    //2 variables so I can vary the increase in acceleration
    public float speedIncrease = 0.01f;
    private float forceIncrease;
    public float xAndYMovement = 0.08f;
    public float _xDrag, _yDrag, _zDrag = 0;        //Drag dependent on direction
    private bool xDragOn, yDragOn, zDragOn = false;
    public bool getInput;

    // Get components, grab initial fields and start forces
    private void Start()
    {
        gM = GameObject.Find("Game Manager").GetComponent<GameManager>();

        rb = GetComponent<Rigidbody>();
        
        //get distance of player from camera (used for screen movement limitations)
        depth = transform.position.z - view.transform.position.z;
        //Modify the drag values to be more user friendly       
        _xDrag = 1 + (_xDrag / 1000);
        _yDrag = 1 + (_yDrag / 1000);
        _zDrag = 1 + (_zDrag / 1000);

        forceIncrease = speedIncrease/50;
        //initial movement speed
        rb.AddForce(0, 0, 4, ForceMode.VelocityChange);
        getInput = true;
    }

    // Update is called once per frame
    private void Update()
    {
        velocity = rb.velocity;         //reference to rigidbody's velocity, as you cannot directly convert

        //increase speed every frame
        //forceIncrease += speedIncrease;
        rb.AddForce(0, 0, forceIncrease, ForceMode.VelocityChange);

        if(getInput)
        {
        MyInput();
        }

        ScreenBounding();
        ApplyDrag();

        rb.velocity = velocity;
    }

    private void MyInput()
    {
        //when moving on the x axis, turn off drag on x axis
        if(Input.GetKey("a") || Input.GetKey("d")){
            xDragOn = false;
        }
        else{
            xDragOn = true;
        }

        if(Input.GetKey("a")){
            rb.AddForce(-xAndYMovement, 0, 0, ForceMode.VelocityChange);
        }

        if(Input.GetKey("d")){
            rb.AddForce(xAndYMovement, 0, 0, ForceMode.VelocityChange);
        }

        //when moving on the y axis, turn off drag on y axis
        if(Input.GetKey("w") || Input.GetKey("s")){
            yDragOn = false;
        }
        else{
            yDragOn = true;
        }

        if(Input.GetKey("w")){
            rb.AddForce(0, xAndYMovement, 0, ForceMode.VelocityChange);
        }

        if(Input.GetKey("s")){
            rb.AddForce(0, -xAndYMovement, 0, ForceMode.VelocityChange);
        }
    }

    private void ScreenBounding()
    {
        //Get screen borders
        var screenBounds = gM.cameraRect;
        
        //If reaching  screen border, prevent anymore movement in direction towards screenborder
        //this prevents plane from getting stuck on screen border due to unseen velocity/forces
        if(transform.position.x >= screenBounds.xMax && velocity.x > 0){
            velocity.x = 0;
        }
        if(transform.position.x <= screenBounds.xMin && rb.velocity.x < 0){
            velocity.x = 0;
        }
        if(transform.position.y >= screenBounds.yMax && rb.velocity.y > 0){
            velocity.y = 0;
        }
        if(transform.position.y <= screenBounds.yMin && rb.velocity.y < 0){
            velocity.y = 0;
        }

        //Clamping position
        transform.position = new Vector3(
        Mathf.Clamp(transform.position.x, screenBounds.xMin, screenBounds.xMax),
        Mathf.Clamp(transform.position.y, screenBounds.yMin, screenBounds.yMax),
        transform.position.z);
    }

    private void ApplyDrag()
    {
        if(xDragOn){
        velocity.x /= _xDrag;
        }
        if(yDragOn){
        velocity.y /= _yDrag;
        }
        if(zDragOn){
        velocity.z /= _zDrag;
        }
    }

    /*void OnTriggerEnter(Collider colliderInfo)
    {
        Debug.Log("hi" + count);
        count++;
    }*/
}
