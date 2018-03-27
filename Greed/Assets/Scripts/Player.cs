using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Player : MonoBehaviour
{

    private float change = 50f;
    private float maxChange = 100f;
    public TextMeshProUGUI changeText;

    [SerializeField]
    private float movementSpeed = 5f;
    [SerializeField]
    private float mouseSensitivity = 2f;
    [SerializeField]
    private Camera firstPersonCamera;

    private Rigidbody rb;

    public float jumpStrength = 5f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    private bool isGrounded = false;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        changeText.text = "Change: " + change.ToString();

        // Character movement
        float xMovement = Input.GetAxisRaw("Horizontal");
        float zMovement = Input.GetAxisRaw("Vertical");

        Vector3 velocity = ((transform.right * xMovement) + (transform.forward * zMovement)).normalized * movementSpeed;

        if (velocity != Vector3.zero)
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

        // Camera Movement
        float yRotation = Input.GetAxisRaw("Mouse X");
        float xRotation = Input.GetAxisRaw("Mouse Y");

        Vector3 rotation = new Vector3(0f, yRotation, 0f) * mouseSensitivity;
        Vector3 cameraRotation = new Vector3(-xRotation, 0f, 0f) * mouseSensitivity;

        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        firstPersonCamera.transform.Rotate(cameraRotation);

        // Jump
        if (!isGrounded)
            if (Physics.Raycast(transform.position, Vector3.down, 1f))
                isGrounded = true;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = Vector3.up * jumpStrength;
            isGrounded = false;
        }
        if (rb.velocity.y < 0)
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;

    }



    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            change++;
            Destroy(other.gameObject);
        }
    }

    public void AddChange(float addedChange)
    {
        change = (addedChange + change > maxChange) ? change = 100 : change += addedChange;
    }

    public void loseChange(float lostChange)
    {
        change -= lostChange;
    }


}
