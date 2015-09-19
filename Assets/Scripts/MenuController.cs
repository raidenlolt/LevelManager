using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour 
{
	private static MenuController menuController;

	protected void Awake ()
	{
		menuController = this;
	}

	protected void OnDestroy ()
	{
		if (menuController != null)
			menuController = null;
	}

	protected void OnDisable ()
	{
	}

	protected void OnEnable ()
	{
	}

	protected void Start ()
	{
	}

	protected void Update ()
	{
		if (Input.GetMouseButtonDown (0))
			SceneLoader.SwitchScene ("Game Scene");
	}
}
