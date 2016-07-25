using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GUITexture background;
    private Vector3 offset;
    public GameObject player;

    void Start()
    {
        this.offset = transform.position - this.player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = this.player.transform.position + this.offset;
    }
}