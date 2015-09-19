using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour 
{
	//The FirstScene to be loaded
	public string defaultSceneName = "Menu Scene";

#region Variables

	private static SceneLoader mainController;					//Static singleton.

	private string currentSceneName;							
	private string nextSceneName;
	private AsyncOperation resourceUnloadTask;					//Operation ref of UnloadUnusedResorces.
	private AsyncOperation sceneLoadTask;						//Operration ref of LoadLevelAsync.

	//The states in scene loading
	private enum SceneState { Reset, Preload, Load, Unload, Postload, Ready, Run, Count };

	private SceneState sceneState;
	private delegate void UpdateDelegate ();					//Ref of update function in current state.
	private UpdateDelegate[] updateDelegates;					//All update functions.

#endregion

#region static methods

	//Switch the scene. Call SceneLoader.SwitchScene instead of Application.LoadLevel
	public static void SwitchScene (string nextSceneName)
	{
		if (mainController != null) 
		{
			if (mainController.currentSceneName != nextSceneName) 
			{
				mainController.nextSceneName = nextSceneName;
			}
		}
	}

#endregion

#region Unity lifecycle

	protected void Awake ()
	{
		//Make this a singleton
		Object.DontDestroyOnLoad (gameObject);
		mainController = this;

		//Store all update functions in an array of delegates.
		updateDelegates = new UpdateDelegate[(int)SceneState.Count];
		updateDelegates [(int)SceneState.Reset] = UpdateSceneReset;
		updateDelegates [(int)SceneState.Preload] = UpdateScenePreload;
		updateDelegates [(int)SceneState.Load] = UpdateSceneLoad;
		updateDelegates [(int)SceneState.Unload] = UpdateSceneUnload;
		updateDelegates [(int)SceneState.Postload] = UpdateScenePostload;
		updateDelegates [(int)SceneState.Ready] = UpdateSceneReady;
		updateDelegates [(int)SceneState.Run] = UpdateSceneRun;


		nextSceneName = defaultSceneName;
		sceneState = SceneState.Reset;
	}

	protected void OnDestroy ()
	{
		if (updateDelegates != null) 
		{
			for (int i = 0; i < (int)SceneState.Count; i++) 
			{
				updateDelegates [i] = null;
			}
			updateDelegates = null;
		}

		if (mainController != null) 
		{
			mainController = null;
		}
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
		//run the current state update function
		if (updateDelegates [(int)sceneState] != null) 
		{
			updateDelegates [(int)sceneState] ();
		}
	}

#endregion

#region methods for each state

	private void UpdateSceneReset ()
	{
		System.GC.Collect ();											//collect memory.
		sceneState = SceneState.Preload;								//switch to next state.
	}

	private void UpdateScenePreload ()
	{
		sceneLoadTask = Application.LoadLevelAsync (nextSceneName);		//load the scene.
		sceneState = SceneState.Load;
	}

	private void UpdateSceneLoad ()
	{
		if (sceneLoadTask.isDone == true) 
		{
			sceneState = SceneState.Unload;
		} else 
		{
			//Loading screen logic goes here;
		}

	}

	private void UpdateSceneUnload ()
	{
		if (resourceUnloadTask == null) 
		{
			resourceUnloadTask = Resources.UnloadUnusedAssets ();			//unload the unused assets
		} else 
		{
			if (resourceUnloadTask.isDone) 
			{
				resourceUnloadTask = null;
				sceneState = SceneState.Postload;
			}
		}
	}

	private void UpdateScenePostload ()
	{
		currentSceneName = nextSceneName;
		sceneState = SceneState.Ready;
	}

	private void UpdateSceneReady ()
	{
		//call collect if needed
		System.GC.Collect();
		sceneState = SceneState.Run;
	}

	private void UpdateSceneRun ()
	{
		if (currentSceneName != nextSceneName) 
		{
			sceneState = SceneState.Reset;
		}
	}
	
#endregion

}
