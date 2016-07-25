using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;

    private Rigidbody body;
    private int count;

    public void Start()
    {
        this.count = 0;
        this.body = GetComponent<Rigidbody>();
        UpdateCountText();
    }

    public void FixedUpdate()
    {
        float moveHorizontal;
        float moveVertical;
        if (Input.touches.Length > 0)
        {
            moveHorizontal = Input.touches.Sum(x => x.deltaPosition.x);
            moveVertical = Input.touches.Sum(x => x.deltaPosition.y);
        }
        else
        {
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");
        }

        var movement = new Vector3(moveHorizontal, 0, moveVertical);
        this.body.AddForce(movement * this.speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickUp")
        {
            other.gameObject.SetActive(false);
            this.count++;
            UpdateCountText();
        }
    }

    private void UpdateCountText()
    {
        this.countText.text = "Count: " + this.count;
        this.winText.enabled = this.count >= 12;
    }
}
