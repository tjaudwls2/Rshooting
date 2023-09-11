using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchZoom : MonoBehaviour
{
    public float perspectiveZoomSpeed = 0.5f; 
    public float orthoZoomSpeed = 0.5f;
    public GameObject Player;
    public float min, max;
    public float lastzoom;
    public bool turnOn;

    private void Start()
    {
        lastzoom = GetComponent<Camera>().orthographicSize;
    }
    void Update()
    {
        if (GameManager.GameManagerthis.Turn == turn.≥ª≈œ)
        {   
            if(GetComponent<Camera>().orthographicSize > lastzoom)
            {
                if(turnOn)
                GetComponent<Camera>().orthographicSize -= 20 * orthoZoomSpeed;
                
            }
            else
            {
                turnOn = false;

            }

           if (Input.touchCount == 2 && !GameManager.GameManagerthis.dragOn)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne  = Input.GetTouch(1);


                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;


                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;



                
                GetComponent<Camera>().orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
                lastzoom = GetComponent<Camera>().orthographicSize;




            }
      
          

        }
        else
        {
            GetComponent<Camera>().orthographicSize += 20 * orthoZoomSpeed;
            

        }
        GetComponent<Camera>().orthographicSize = Mathf.Clamp(GetComponent<Camera>().orthographicSize, min, max);
        transform.position = new Vector3(Vector2.Lerp(Player.transform.position, new Vector3(0, -4f, 0), (GetComponent<Camera>().orthographicSize - min) / (max - min)).x, Vector2.Lerp(Player.transform.position, new Vector3(0, -4f, 0), (GetComponent<Camera>().orthographicSize - min) / (max - min)).y, -10);




    }



}