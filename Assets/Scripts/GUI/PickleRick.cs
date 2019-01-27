using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickleRick : MonoBehaviour
{

    public GameObject Pickle;

    private Image _pickleImage;

    public int Value = 0;

    private List<Sprite> _sprites = new List<Sprite>();
	// Use this for initialization
	void Start ()
	{
	    _pickleImage = Pickle.GetComponent<Image>();
	    for (int i = 1; i <= 6; ++i)
	    {
            _sprites.Add(Resources.Load<Sprite>(string.Format("Images/GUI/GameInterface/timeline/pickle_{0}_6", i)));
	    }
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Value = Math.Max(0, Value);
	    Value = Math.Min(Value, _sprites.Count - 1);
	    _pickleImage.sprite = _sprites[Value];
	}
}
