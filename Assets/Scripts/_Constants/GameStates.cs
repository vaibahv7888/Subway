
using UnityEngine;
using System.Collections;

public static class GameStates
{

	public static GAME_STATE currentState, previousState;
	public static CurrentState _currentState;
	static  Hashtable  States = new Hashtable ();
	
	public  static void SetCurrent_State_TO (GAME_STATE state)
	{		
		previousState = currentState;
		
		currentState = state;
		_currentState = States [currentState] as CurrentState;		
		Images.ReleaseAllImages ();
	}
	
	public static void RegisterStates (GAME_STATE enum_states, CurrentState deleg_State)
	{
		if (!States.ContainsKey (enum_states))
			States.Add (enum_states, (CurrentState)deleg_State);
	}
	
	
	}

