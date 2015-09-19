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

	void Init_Enter ()
	{
	}

	void Init_Exit ()
	{
	}

#endregion

#region State Login

	void Login_Enter ()
	{
	}

	void Login_Exit ()
	{
	}

#endregion

	void PreAssessment_Enter ()
	{
		//
	}
	void PreAssessment_Exit ()
	{
		//
	}

#endregion
}
