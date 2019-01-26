using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParallaxButton : MonoBehaviour
{
    public GameObject RedImage;
    public GameObject SourceImage;
    public GameObject BlueImage;

    public int MaxShiftDistance = 10;

    // Start is called before the first frame update
    void Start()
    {
    }

    
    
    // Update is called once per frame
    void Update()
    {
        Debug.Log(string.Format("Mouse position: {0}, {1}, {2}", Input.mousePosition.x, Input.mousePosition.y,
            Input.mousePosition.z));

        Debug.Log(string.Format("Image position: {0}, {1}, {2}", RedImage.transform.position.x,
            RedImage.transform.position.y, RedImage.transform.position.z));
        var direction = Input.mousePosition - SourceImage.gameObject.transform.position;

        var theta = Math.Atan2(direction.y, direction.x);
        var delta = 10;

//        var img = RedImage.gameObject.GetComponent<Image>();
//        img.color = new Color(img.color.r, img.color.g, img.color.b, 0);

//        RedImage.gameObject.transform.position.x 
        RedImage.gameObject.transform.position = SourceImage.gameObject.transform.position - 10 * direction.normalized;
        BlueImage.gameObject.transform.position = SourceImage.gameObject.transform.position + 10 * direction.normalized;
    }
}
