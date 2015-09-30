using UnityEngine;
using System.Collections;

public class LevelManager : StateBehaviour 
{
	public enum States 
	{ 
		Init,
		Login,
		SubjectSelect,
		LevelSelect,
		PreAssessment, 
		ShotScene, 
		CoolTalk, 
		Practice, 
		PostAssessment 
	};

#region Unity lifecycle

	void Awake()
	{
		Initialize<States> ();
		ChangeState (States.Init);
	}

#endregion

#region State methods

#region State Init
	
	public void OnInitDone ()
	{
		ChangeState (States.Login);
	}

#endregion

#region State Login

	void Login_Enter ()
	{
		//SceneLoader.SwitchScene 
	}

	void Login_Exit ()
	{
	}

	public void OnLoginDone ()
	{
		ChangeState (States.PreAssessment);
	}

#endregion

#region State PreAssessmanet

	void PreAssessment_Enter ()
	{
		//
	}
	void PreAssessment_Exit ()
	{
		//
	}

	public void OnPreAssementDone ()
	{
	}

#endregion

#endregion
}
