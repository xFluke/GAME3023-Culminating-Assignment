using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GroundType
{
    GRASS,
    ROAD,
    TALLGRASS
}
public class PlayerController : MonoBehaviour
{
    public GroundType groundType;

    [SerializeField]
    float speed = 5;

    Rigidbody2D rb;

    public bool isWalking = false;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Grass":
                groundType = GroundType.GRASS;
                break;
            case "Road":
                groundType = GroundType.ROAD;
                break;
            case "TallGrass":
                groundType = GroundType.TALLGRASS;
                break;
            default:
                groundType = GroundType.GRASS;
                break;
        }
    }

    void Update()
    {
        Vector2 movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementVector *= speed;
        rb.velocity = movementVector;

        if (movementVector.magnitude > 0.1f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }
}
