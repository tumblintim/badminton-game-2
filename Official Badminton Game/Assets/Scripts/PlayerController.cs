using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    float sideSpeed = 4f;
    float forwardSpeed = 5f;
    float backwardSpeed = 3f;

    float powerLevel = 0.1f;
    float pLevelButtonSens = 1f;
    float pLevelScrollSens = 0.1f;

    float shotAngle = 0f;
    float shotAngleSens = 30f;

    public float drawAngle = 0f;
    Vector3 mousePos1;
    Vector3 mousePos2;

    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(view.IsMine)
        {
            float z = Input.GetAxis("Horizontal");
            float x = Input.GetAxis("Vertical");

            transform.Translate(Vector3.back * z * Time.deltaTime * sideSpeed);
            if(x < 0)
            {
                transform.Translate(Vector3.right * x * Time.deltaTime * backwardSpeed);
            }
            if(x > 0)
            {
                transform.Translate(Vector3.right * x * Time.deltaTime * forwardSpeed);
            }

            ChangePowerLevel();
            ChangeShotAngle();
            DrawShot();
        }
    }

    void LateUpdate()
    {
        float deltaX = mousePos2.x - mousePos1.x;
        float deltaY = mousePos2.y - mousePos1.y;
        drawAngle = Mathf.Atan(deltaY / deltaX) * Mathf.Rad2Deg;
    }

    void ChangePowerLevel()
    {
        if(powerLevel > 2f)
        {
            powerLevel = 2f;
        }
        if(powerLevel < 0.1f)
        {
            powerLevel = 0.1f;
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            powerLevel += Time.deltaTime * pLevelButtonSens;
        }
        if(Input.GetKey(KeyCode.LeftControl))
        {
            powerLevel -= Time.deltaTime * pLevelButtonSens;
        }
        powerLevel += Input.mouseScrollDelta.y * pLevelScrollSens;
    }

    void ChangeShotAngle()
    {
        if(shotAngle > 45)
        {
            shotAngle = 45;
        }
        if(shotAngle < -45)
        {
            shotAngle = -45;
        }

        if(Input.GetKey(KeyCode.E))
        {
            shotAngle += Time.deltaTime * shotAngleSens;
        }
        if(Input.GetKey(KeyCode.Q))
        {
            shotAngle -= Time.deltaTime * shotAngleSens;
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            shotAngle = -30f;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            shotAngle = 0f;
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            shotAngle = 30f;
        }
    }

    void DrawShot()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            mousePos1 = Input.mousePosition;
        }
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            mousePos2 = Input.mousePosition;
        }
    }
}
