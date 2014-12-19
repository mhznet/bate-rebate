using UnityEngine;
using System.Collections;

public class PaddleBehaviour : MonoBehaviour 
{
    public bool enableTouch = true;
    public float speed = 15f;
    public GameObject paddleAsset;
    public GameObject ball;
    private bool isDragging = false;
    private Vector3 padPosInit;
    private float ia_percentage = 0.3f;
    public bool paused = false;
    
    void Start() 
    {
        padPosInit = this.paddleAsset.transform.position;
    }
    void Update() { }
    void OnMouseUp()
    {
        isDragging = false;
    }
    void OnMouseDown()
    {
        isDragging = true;
    }
    public void Pause()
    {
        isDragging = false;
        paused = !paused;
    }
    void FixedUpdate()
    {
        if (!paused)
        {
            if (enableTouch)
            {
                if (isDragging)
                {
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePos.z = padPosInit.z;
                    mousePos.x = padPosInit.x;
                    this.paddleAsset.transform.position = Vector3.MoveTowards(this.paddleAsset.transform.position, mousePos, speed * Time.deltaTime);
                }
            }
            else
            {
                Vector3 ballPos = ball.transform.position;
                Vector3 padPos = paddleAsset.transform.position;
                if (ballPos.x > 0f && padPos.x > 0f || ballPos.x < 0f && padPos.x < 0f)
                {
                    ballPos.z = padPosInit.z;
                    ballPos.x = padPosInit.x;
                    this.paddleAsset.transform.position = Vector3.MoveTowards(this.paddleAsset.transform.position, ballPos, (speed * ia_percentage) * Time.deltaTime);
                }
                else if (ballPos.x > 0f && padPos.x < 0f || ballPos.x < 0f && padPos.x < 0f)
                {
                    ballPos.z = padPosInit.z;
                    ballPos.x = padPosInit.x;
                    ballPos.y = 0f;
                    this.paddleAsset.transform.position = Vector3.MoveTowards(this.paddleAsset.transform.position, ballPos, (speed * ia_percentage) * Time.deltaTime);
                }
            }
        }
    }
}
