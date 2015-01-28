using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveTrains : MonoBehaviour {

	public GameObject m_trainPrefab;
	public GameObject m_drawLines, m_newControler;
	public int m_speed = 0;

	private GameObject[] m_trains;
	private bool m_isForward = true;
	private int m_nuOfLines = 0;
	private int m_curntPntLine1 = 0;
	private List<Vector2> m_linePnts1;

	// Use this for initialization
	void Start () {
		m_nuOfLines = m_drawLines.GetComponent<DrawLines> ().NuOfLines;
		m_trains = new GameObject[1];
		m_trains [0] = (GameObject)Instantiate (m_trainPrefab);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
//		moveTrains ();
	}

	void moveTrains () {
		Vector2[] _tempVectr = m_drawLines.GetComponent<DrawLines> ().getPointsForLine (0);	// .getScreenPntsForLine (0);

		if (_tempVectr.Length == 0)
			return;

		Vector3[] _tempVectr3 = new Vector3[_tempVectr.Length];
		for(int i=0; i<_tempVectr.Length; i++) {
			_tempVectr3[i] = new Vector3( _tempVectr[i].x, -43f, _tempVectr[i].y);
//			Debug.Log ("_tempVectr["+i+"]= "+_tempVectr[i]);
//			Debug.Log ("_tempVectr3["+i+"]= "+_tempVectr3[i]);
		}
		float step = m_speed * Time.deltaTime;
		m_trains[0].transform.position = Vector3.MoveTowards (m_trains[0].transform.position, _tempVectr3[m_curntPntLine1], step);

		Vector3 _trainPos = m_trains [0].transform.position;
		if (m_curntPntLine1 == 0 && !m_isForward) {
			m_isForward = true;
		}
		if (_trainPos == _tempVectr3 [m_curntPntLine1]) {
			if(m_curntPntLine1 < (_tempVectr3.Length-1) && m_isForward) {
				m_curntPntLine1++;
			}else {
				m_isForward = false;
				m_curntPntLine1--;
			}

		}
	}


	void getTrains () {
		Vector2[] _tempVect = m_drawLines.GetComponent<DrawLines> ().getScreenPntsForLine (0);

	}
}
