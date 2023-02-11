using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    // The speed at which the character will climb the ladder
    public float climbSpeed = 5.0f;
    public Vector2 lowadnhighlim;
    bool climbing;

    // The character controller that will be climbing the ladder
    public CharacterController characterController;

    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown("e") && Mathf.Abs(transform.position.x - characterController.transform.position.x) + Mathf.Abs(transform.position.z - characterController.transform.position.z) < 2f) || climbing && (characterController.transform.position.y < lowadnhighlim.x|| characterController.transform.position.y > lowadnhighlim.y))
        {
            if (climbing) characterController.GetComponent<movement>().enabled = true;
            climbing = !climbing;

        }
        if (climbing)
        {
            characterController.GetComponent<movement>().enabled = false;
            // If the character is climbing up, move them up the ladder
            if (Input.GetKey(KeyCode.W))
            {
                characterController.Move(Vector3.up * climbSpeed * Time.deltaTime);
            }
            // If the character is climbing down, move them down the ladder
            else if (Input.GetKey(KeyCode.S))
            {
                characterController.Move(Vector3.down * climbSpeed * Time.deltaTime);
            }
        }
    }
}