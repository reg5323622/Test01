using UnityEngine;
using System.Collections;
using System.Threading;

public class Shooter2D : MonoBehaviour {
	bool isThis=false;
	float m_time = 0.1f;
	bool m_select = false;
	bool m_CanPress = true;
	bool m_CanShoot = false;
	public GameObject SelectObject;
	public GameObject m_Torque;
	public GameObject m_point;
	Vector3 direction;
	float magnitide;
	public GameObject BallPrefab;
	bool CanCreat = false;
	bool CanDrag = true;
	//float m_Time = 0.2f;
	// Use this for initialization
	void Start () {
		CreatTable ();
	
	}
	
	// Update is called once per frame
	void Update () {
		CheckObject2D ();
		Shooter ();
		CheckTableBall ();
	}

	void CheckObject2D(){


		if (Input.GetMouseButton (0)&&m_CanPress) {



			Ray ray = UICamera.mainCamera.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, 10, 32);
			if (hit.collider!=null){
				if (hit.collider.tag == "ball"||hit.collider.tag == "StopBall"&&CanDrag) {
					m_select = true;
					m_CanPress=false;
					//Debug.Log("GG");
					SelectObject = hit.transform.gameObject;
					if (SelectObject.GetComponent<Rigidbody2D>().isKinematic == false) SelectObject.GetComponent<Rigidbody2D>().isKinematic = true;
					m_Torque.transform.position = SelectObject.transform.position;

				}

			}else{
				//m_click = false;
				Debug.Log("null");
				m_CanPress=false;
			}
			//Debug.Log(hit.collider.name);
			//Debug.Log(m_CanPress);

		}
		if (Input.GetMouseButtonUp (0)) {
			m_CanPress=true;
			m_select=false;
			if (SelectObject){
				if (m_Torque.GetComponent<BoxCollider>().enabled==true) m_Torque.GetComponent<BoxCollider>().enabled=false;
				if (SelectObject.GetComponent<Rigidbody2D>().isKinematic == true) SelectObject.GetComponent<Rigidbody2D>().isKinematic = false;
				if (m_point.activeSelf == true) m_point.SetActive(false);
				if (m_CanShoot) SelectObject.GetComponent<Rigidbody2D>().velocity = direction * magnitide * 10f;
				SelectObject =null;
				CanCreat = true;
				m_time = 0.1f;
				CanDrag = false;
			}
		}

	}

	void Shooter(){
		if (SelectObject != null) {
			if (m_Torque.GetComponent<BoxCollider>().enabled==false) m_Torque.GetComponent<BoxCollider>().enabled=true;
			Ray ray = UICamera.mainCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray,out hit)){
				if (hit.collider.tag=="Torque"){
//					Debug.Log("in");
					if (Vector3.Distance(SelectObject.transform.position,hit.point)>0.05f){
						if (m_CanShoot==false) m_CanShoot=true;
						if (m_point.activeSelf == false) m_point.SetActive(true);
						direction = SelectObject.transform.position-hit.point;
						direction.Normalize();

						m_point.transform.position = hit.point;

						m_point.transform.localPosition =SelectObject.transform.localPosition+ new Vector3(Vector3.ClampMagnitude(m_point.transform.localPosition - SelectObject.transform.localPosition, 50f).x,Vector3.ClampMagnitude(m_point.transform.localPosition - SelectObject.transform.localPosition, 50f).y, 0f);

						magnitide = Vector3.Distance(SelectObject.transform.position,m_point.transform.position);
//						Debug.Log(magnitide);
//						Debug.Log(Vector3.Distance(SelectObject.transform.position,hit.point));
						//Debug.Log(m_point.transform.localPosition);
						//Debug.Log(SelectObject.transform.localPosition);
					}else if (Vector3.Distance(SelectObject.transform.position,hit.point)<=0.05f){
						if (m_point.activeSelf == true) m_point.SetActive(false);
						if (m_CanShoot==true)m_CanShoot=false;
					}
				}
			}
		}
	}

	void CheckTableBall(){
		if (m_time > 0 && CanCreat) {
			m_time -= Time.deltaTime;
		}
		if (GameObject.FindGameObjectWithTag ("MoveBall") == null && CanCreat && m_time<=0) {

			CanCreat = false;

			int _random = GameManager._GameScore/300;

			if (GameManager._GameScore<200){
				GameObject m_ball = (GameObject)Instantiate(BallPrefab);
				int j = Mathf.FloorToInt(Random.Range(0,2));
				switch(j){
				case 0:
					m_ball.GetComponent<Detect2D>().CreatBall(1,0);
					break;
				case 1:
					m_ball.GetComponent<Detect2D>().CreatBall(2,0);
					break;
				}

			}

			if (GameManager._GameScore>=200&&GameManager._GameScore<500){
				int j = Mathf.FloorToInt(Random.Range(0,3));
				GameObject m_ball = (GameObject)Instantiate(BallPrefab);
				switch(j){
				case 0:
					m_ball.GetComponent<Detect2D>().CreatBall(1,0);
					break;
				case 1:
					m_ball.GetComponent<Detect2D>().CreatBall(2,0);
					break;
				case 2:
					m_ball.GetComponent<Detect2D>().CreatBall(3,0);
					break;
				}
				
			}

			if (GameManager._GameScore>=500&&GameManager._GameScore<1000){
				int j = Mathf.FloorToInt(Random.Range(0,4));
				GameObject m_ball = (GameObject)Instantiate(BallPrefab);
				switch(j){
				case 0:
					m_ball.GetComponent<Detect2D>().CreatBall(1,0);
					break;
				case 1:
					m_ball.GetComponent<Detect2D>().CreatBall(2,0);
					break;
				case 2:
					m_ball.GetComponent<Detect2D>().CreatBall(3,0);
					break;
				case 3:
					m_ball.GetComponent<Detect2D>().CreatBall(4,0);
					break;
				}
			}
			if (GameManager._GameScore>=1000){
				for (int i=0;i<GameManager._GameScore/300;i++){

						StartCoroutine(sleep01());



				}

			
			}

				
				

			CanDrag=true;
		}
	}




	IEnumerator sleep01(){
		GameObject m_ball = (GameObject)Instantiate(BallPrefab);
		int j = Mathf.FloorToInt(Random.Range(0,4));
		switch (j) {
		case 0:
			m_ball.GetComponent<Detect2D> ().CreatBall (1, 0);
			break;
		case 1:
			m_ball.GetComponent<Detect2D> ().CreatBall (2, 0);
			break;
		case 2:
			m_ball.GetComponent<Detect2D> ().CreatBall (3, 0);
			break;
		case 3:
			m_ball.GetComponent<Detect2D> ().CreatBall (4, 0);
			break;
		}
		yield return new WaitForSeconds(0.2f);
	} 


	void CreatTable(){
		CanCreat = true;
		for (int i=0; i<5; i++) {
			GameObject m_ball = (GameObject)Instantiate(BallPrefab);
			m_ball.GetComponent<Detect2D>().CreatBall(1,0);
		}
		CanCreat = false;
	}



}
