using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public List<BallAttributes> ballAttributes;

    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public float force;
    // Start is called before the first frame update
    void Start()
    {
        SetBall();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb.AddForce(Vector2.up * force);
        }
    }

    private void SetBall()
    {
        foreach (var item in ballAttributes)
        {
            if (item.isUnlocked && item.isSelected)
            {
                transform.localScale = new Vector2(item.ballSize, item.ballSize);
                spriteRenderer.color = Utility.GetColorFromString(item.ballColor);
                rb.mass = item.ballWeight;
            }
        }
    }
}
