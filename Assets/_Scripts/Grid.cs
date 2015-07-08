using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{

    public int XPosition;

    public GameController GameController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnMouseDown()
    {
        GameController.CreateCircle(XPosition);
    }
}
