using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{

    public GameObject RedLayer;
    public GameObject BlackLayer;
    public GameObject BlueLayer;
    public int ShiftScale = 10;

	void Start () {
		
	}
	

	void Update () {
	    var direction = Input.mousePosition - BlackLayer.gameObject.transform.position;
	    var shift = ShiftScale * direction.normalized;
	    RedLayer.gameObject.transform.position = BlackLayer.gameObject.transform.position - shift;
	    BlueLayer.gameObject.transform.position = BlackLayer.gameObject.transform.position + shift;
    }

	public void LoadNextScene()
	{
		SceneManager.LoadScene(1);
	}

	public void Quit()
	{
		Application.Quit();
	}
}
