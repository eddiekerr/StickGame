using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class GunRotation : MonoBehaviour {

    public int rotationOffset = 90;

    private GameObject parentGameObject;
    private PlatformerCharacter2D playerScript;


    private const float RIGHT_MAX_ROT = 50f;
    private const float RIGHT_MIN_ROT = -50f;

    private const float MOUSE_ROT_CONSTANT = 10f;

    void Start() {
      parentGameObject = transform.parent.gameObject;
      playerScript = parentGameObject.GetComponent<PlatformerCharacter2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isFacingRight = playerScript.getIsFacingRight();

        float worldMouseY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        float worldMouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float currentPosY = transform.position.y;
        float currentPlayerPosX = parentGameObject.transform.position.x;
        float deltaY = worldMouseY - currentPosY;

        // if((currentPlayerPosX < worldMouseX && !isFacingRight) || (currentPlayerPosX > worldMouseX && isFacingRight)) {
        //   playerScript.Flip();
        // }

        deltaY *= MOUSE_ROT_CONSTANT;

        deltaY = Mathf.Max(RIGHT_MIN_ROT, Mathf.Min(deltaY, RIGHT_MAX_ROT));
        if(!isFacingRight) deltaY = deltaY * -1f;

        transform.rotation = Quaternion.Euler(0f, 0f, 0f + deltaY);


    }
}
