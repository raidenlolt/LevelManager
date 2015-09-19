using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : StateBehaviour 
{
#region variables

	private static GameController gameController;

	public enum States { Init, TurnStart, TurnPlayer1, TurnPlayer2, TurnEnd };

	public Text button;

#endregion

#region Unity lifecycle

	protected void Awake ()
	{
		gameController = this;
		Initialize<States> ();
		ChangeState (States.Init);
	}
	
	protected void OnDestroy ()
	{
		if (gameController != null)
			gameController = null;
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
//		if (Input.GetMouseButtonDown (0))
//			SceneLoader.SwitchScene ("Menu Scene");
	}

#endregion

#region State methods
	private void Init_Enter()
	{
		button.text = "Start";
	}

	private IEnumerator TurnStart_Enter()
	{
		button.text = "Starting in 3...";
		yield return new WaitForSeconds(0.5f);
		button.text = "Starting in 2...";
		yield return new WaitForSeconds(0.5f);
		button.text = "Starting in 1...";
		yield return new WaitForSeconds(0.5f);
		ChangeState (States.TurnPlayer1);
	}

	private void TurnPlayer1_Enter()
	{
		button.text = "Player 1 End Turn";
	}

	private void TurnPlayer2_Enter()
	{
		button.text = "Player 2 End Turn";
	}

	private void TurnEnd_Enter()
	{
		button.text = "Turn Ended";
	}

#endregion

#region Ui events

	public void OnButtonClick()
	{
		var state = GetState ();
		if (state == null)
			return;
		if (state.Equals (States.Init))
			ChangeState (States.TurnStart);
		if (state.Equals (States.TurnPlayer1))
			ChangeState (States.TurnPlayer2);
		if (state.Equals (States.TurnPlayer2))
			ChangeState (States.TurnEnd);
	}

#endregion

}
