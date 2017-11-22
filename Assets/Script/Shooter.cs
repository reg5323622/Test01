using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {
	public GameObject _This;
	public Vector3 direction;
	public Vector2 position2D;
	public float magnitude;
	public bool isThis = false;
	// Use this for initialization
	void Start () {
		_This = gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		/*if (Input.GetMouseButton (0)&&isThis==false) {
			Ray ray2D = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray2D,100);

			if (hit2D.transform.gameObject == _This){
				isThis = true;
				Debug.Log("GG");
			}
		
		}

		if (isThis) {
			if (_This.GetComponent<Rigidbody2D>().isKinematic==false)_This.GetComponent<Rigidbody2D>().isKinematic=true;
			if ( gameObject.transform.parent.gameObject.GetComponent<SphereCollider>().enabled ==false)  gameObject.transform.parent.gameObject.GetComponent<SphereCollider>().enabled = true;

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
			RaycastHit hit;
			if(Physics.Raycast(ray , out hit)){
				Debug.Log(hit.point);
				if (hit.transform.tag == "Shooter") {

					direction = gameObject.transform.position - hit.point;
					direction.Normalize();
					magnitude = Vector3.Distance(gameObject.transform.position,hit.point);
					Debug.Log(magnitude);
				}
			}
		}

		if (Input.GetMouseButtonUp (0)) {
			if (isThis){
				isThis = false;
				if (_This.GetComponent<CircleCollider2D>().radius!= 5.12f) _This.GetComponent<CircleCollider2D>().radius=5.12f;
				if (_This.GetComponent<Rigidbody2D>().isKinematic==true)_This.GetComponent<Rigidbody2D>().isKinematic=false;
				_This.GetComponent<Rigidbody2D>().velocity = direction * magnitude * 20f;
			}
		}
	}*/
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition); 
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			if (hit.transform.tag == "shooter") {
				Debug.Log ("GGGGGGGGGG");
			}
		}
		Debug.Log (hit.point);
	}
}
