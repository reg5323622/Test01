using UnityEngine;
using System.Collections;

public class Detect2D : MonoBehaviour {
	int life;
	int NowLife;
	public bool CreatFinish = false;
	Vector3 RandomVector;
	public Vector3 m_position;
	public Vector2 m_velocity;
	float m_Angle;
	Vector2 m_vel;
	Transform _panel;
	// Use this for initialization
	void Start () {
		//Debug.Log (new Vector3 (Mathf.Sin (180*Mathf.Deg2Rad), Mathf.Cos (180*Mathf.Deg2Rad), 0));
		_panel = GameObject.Find ("Panel").transform;
	}
	
	// Update is called once per frame
	void Update () {
		SetBallStats ();
	}

	void SetBallStats(){
		if (gameObject.GetComponent<Rigidbody2D> ().velocity == Vector2.zero) {
			if (gameObject.tag != "StopBall")
				gameObject.tag = "StopBall";
		} else if (gameObject.GetComponent<Rigidbody2D> ().velocity != Vector2.zero) {
			if (gameObject.tag != "MoveBall")
				gameObject.tag = "MoveBall";
		}
	}


	public void CreatBall(int _life,int _type){
		gameObject.SetActive(true);

	


		switch(_type){
		case 0:
			life = _life;
			NowLife = life;
			gameObject.GetComponent<Rigidbody2D> ().mass *= (float)life;
			gameObject.transform.parent = GameObject.Find ("Panel").transform;
			gameObject.transform.localScale = new Vector3(20f*(float)life,20f*(float)life,0f); 
			gameObject.transform.localPosition = RandomPosition (_life);
			gameObject.tag = "StopBall";
			CreatFinish = true;
			break;
		case 1:
			//gameObject.GetComponent<CircleCollider2D>().enabled=false;
			Debug.Log(m_Angle);
			life = _life;
			NowLife = life;
			gameObject.GetComponent<Rigidbody2D> ().mass *= (float)life;
			gameObject.transform.parent = GameObject.Find ("Panel").transform;
			gameObject.transform.localScale = new Vector3(20f*(float)life,20f*(float)life,0f); 
			gameObject.transform.localPosition = m_position+new Vector3(Mathf.Sin((m_Angle+60)*Mathf.Deg2Rad),Mathf.Cos((m_Angle+60)*Mathf.Deg2Rad),0f)*20f*life;
			gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2(Mathf.Sin((m_Angle+60)*Mathf.Deg2Rad),Mathf.Cos((m_Angle+60)*Mathf.Deg2Rad))*m_velocity.magnitude;
			gameObject.tag = "MoveBall";
			CreatFinish = true;
			Debug.Log("localPosition01="+gameObject.transform.localPosition+" velocity01="+gameObject.GetComponent<Rigidbody2D> ().velocity);
			break;
		case 2:
			//gameObject.GetComponent<CircleCollider2D>().enabled=false;
//			Debug.Log(m_Angle);
			life = _life;
			NowLife = life;
			gameObject.GetComponent<Rigidbody2D> ().mass *= (float)life;
			gameObject.transform.parent = GameObject.Find ("Panel").transform;
			gameObject.transform.localScale = new Vector3(20f*(float)life,20f*(float)life,0f); 
			gameObject.transform.localPosition = m_position+new Vector3(Mathf.Sin((m_Angle-60)*Mathf.Deg2Rad),Mathf.Cos((m_Angle-60)*Mathf.Deg2Rad),0f)*15f*life;
			gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2(Mathf.Sin((m_Angle-60)*Mathf.Deg2Rad),Mathf.Cos((m_Angle-60)*Mathf.Deg2Rad))*m_velocity.magnitude;
			gameObject.tag = "MoveBall";
			CreatFinish = true;
			Debug.Log("localPosition02="+gameObject.transform.localPosition+" velocity02="+gameObject.GetComponent<Rigidbody2D> ().velocity);
			break;
			break;
		}
	}

	Vector3 RandomPosition(int _life){
		Vector3 m_Position = new Vector3 ((float)Mathf.FloorToInt (Random.Range (-320, 320)), (float)Mathf.FloorToInt (Random.Range (-170, 170)), 0f);
		float _x = m_Position.x;
		float _y = m_Position.y;
		int _lengh = 0;


		GameObject[] otherball = GameObject.FindGameObjectsWithTag("StopBall");

		/*foreach (GameObject _ball in otherball) {
			Debug.Log(otherball.Length);
			int m_life = _ball.GetComponent<Detect2D>().life;
			float interval = (life*10f+m_life*10f+10f);
			if (_x<=_ball.transform.localPosition.x+interval&&_x>=_ball.transform.localPosition.x-interval&&_y<=_ball.transform.localPosition.y+interval&&_y>=_ball.transform.localPosition.y-interval) m_Position = RandomPosition(_life);
			else continue;
		}*/

		for (int i=0; i<otherball.Length; i++) {
//			Debug.Log(i);
			int m_life = otherball[i].GetComponent<Detect2D>().life;

			float interval = (life*10f+m_life*10f+10f);
			if (_x<=otherball[i].transform.localPosition.x+interval&&_x>=otherball[i].transform.localPosition.x-interval&&_y<=otherball[i].transform.localPosition.y+interval&&_y>=otherball[i].transform.localPosition.y-interval) {
				m_Position = RandomVector3(-320,320,-170,170);
				_x = m_Position.x;
				_y = m_Position.y;
				i=-1;
			}
		
		}



		return m_Position;
		Debug.Log (_lengh);
	}

	Vector3 RandomVector3(float w1,float w2,float h1,float h2){

		float r_x = (float)Mathf.FloorToInt (Random.Range  (w1, w2));
		float r_y = (float)Mathf.FloorToInt (Random.Range  (h1, h2));

		return new Vector3 (r_x, r_y, 0f);
		//return 
	}

	void OnCollisionEnter2D(Collision2D m_2D){
		/*if (this.transform.tag == "StopBall") {
			Debug.Log (gameObject.GetComponent<Rigidbody2D> ().velocity);
			m_velocity = gameObject.GetComponent<Rigidbody2D> ().velocity;
		}*/
		if ((m_2D.transform.tag == "MoveBall" || m_2D.transform.tag == "StopBall")&&CreatFinish) {
			m_velocity = gameObject.GetComponent<Rigidbody2D> ().velocity;
			NowLife--;
			gameObject.SetActive (false);
			if (NowLife>0){
				for (int i=0; i<2; i++) {
					GameObject NewBall = (GameObject)Instantiate (gameObject);
					NewBall.GetComponent<Detect2D> ().m_position = gameObject.transform.localPosition;
					NewBall.GetComponent<Detect2D> ().m_velocity = m_velocity;
					NewBall.GetComponent<Detect2D> ().CalAngle(m_velocity);
					NewBall.GetComponent<Detect2D> ().CreatBall (NowLife, i + 1);
//					Debug.Log("Succ");
				}
			}
			GameManager._GameScore+=life*life*10;
			Destroy(gameObject);
		}

		if (m_2D.transform.tag == "wall"&&gameObject.transform.tag=="StopBall") {
			ContactPoint2D _CP2D = m_2D.contacts [0];
			Vector3 _local = _panel.InverseTransformPoint(_CP2D.point);
			Vector3 _Push = gameObject.transform.localPosition - _local;
			_Push.Normalize();
			gameObject.GetComponent<Rigidbody2D>().velocity = _Push*0.05f;
			//Debug.Log (m_2D.transform.name + " " + _local);
		}
	}

	public void CalAngle(Vector2 vel){
		if (vel != null) {
			m_vel = vel;
			m_vel.Normalize();
			//Debug.Log("VEL="+m_vel);
			if (m_vel.x>0){
				m_Angle = (Mathf.Acos(Vector2.Dot(m_vel,Vector2.up)))*180f/Mathf.PI;
			}

			if (m_vel.x<0){
				m_Angle = (-Mathf.Acos(Vector2.Dot(m_vel,Vector2.up)))*180f/Mathf.PI+360f;
			}
				//Debug.Log(m_Angle);

			Debug.Log(m_Angle);

		}

	}

	void OnCollisionStay2D(Collision2D _collision){

	}


}
