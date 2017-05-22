using System.Collections;
using System;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour {
	public Animator animator;
	public int count;
	private int walkCount;
	public float trans;
	public float positionX;
	public float positionY;
	public float inverseMoveTime;
	H h = new H();
	public Thread mo = new Thread(new ThreadStart(H.DoIt));
	private Rigidbody2D self;

	public class H {
		public H() { }	

		public static void DoIt() {
			Debug.Log("DoIt method");
		}	
	}	

	// Use this for initialization
	void Start () {
		Debug.Log("Starting it");	
		animator = GetComponent<Animator>();
		self = GetComponent<Rigidbody2D>();
		count = 0;
		walkCount = 0;
		inverseMoveTime = 1f / 0.1f;
	}
	
	void OldMove() {
		if (Input.GetKey("w"))
		{
			if (walkCount>4)
			{
				animator.SetBool("GoUp", true);
				Move();
			}	
			Debug.Log("Key \'w\' was pressed");
			animator.SetBool("MoveUp", true);
			Debug.Log(Input.GetAxis("Vertical"));
			++walkCount;
		}
		else if (Input.GetKey("s"))
		{
			/**
			Debug.Log("Key \'s\' was pressed");
			animator.SetBool("MoveDown", true);	
			Move();
			*/
			animator.SetBool("MoveDown", true);
			trans = Input.GetAxis("Vertical");
			transform.Translate(0, (trans * .01f), 0);
		}
		else if (Input.GetKey("a"))
		{
			Debug.Log("Key \'a\' was pressed");
			animator.SetBool("MoveLeft", true);
			Move();
		}
		else if (Input.GetKey("d"))
		{
			if (walkCount>4)
			{
				animator.SetBool("GoRight", true);
				Move();
			}	
			Debug.Log("Key \'d\' was pressed");
			animator.SetBool("MoveRight", true);
			++walkCount;
		}	

		if (!Input.GetKey("w") && !Input.anyKey)
		{
			animator.SetBool("MoveUp", false);
			animator.SetBool("GoUp", false);
			walkCount = 0;
		}	
		if (!Input.GetKey("s") && !Input.anyKey)		
		{
			animator.SetBool("MoveDown", false);
		}	
		if (!Input.GetKey("a") && !Input.anyKey)
		{
			animator.SetBool("MoveLeft", false);
		}	
		if (!Input.GetKey("d") && !Input.anyKey)
		{
			animator.SetBool("MoveRight", false);
			animator.SetBool("GoRight", false);
			walkCount = 0;
		}

	}	
	void NewMove() {
		if (Input.GetKey("w"))
		{
			if (walkCount>4)
			{	
				animator.SetBool("GoUp", true);
				trans = Input.GetAxis("Vertical");
				trans*= 0.01f;
				transform.Translate(0, trans, 0);
			}	
			Debug.Log("key \'w\' is being pressed");
			animator.SetBool("MoveUp", true);
			++walkCount;
		}	
		else if (Input.GetKey("s"))
		{
			if (walkCount>4)
			{
				animator.SetBool("GoDown", true);
				trans = Input.GetAxis("Vertical");
				trans*= .01f;
				transform.Translate(0, trans, 0);
			}	
			Debug.Log("Key \'w\' is being pressed");
			animator.SetBool("MoveDown", true);
			++walkCount;
		}
		else if (Input.GetKey("a"))
		{
			if (walkCount>4)
			{
				animator.SetBool("GoLeft", true);
				trans = Input.GetAxis("Horizontal");
				trans*= .01f;
				transform.Translate(trans, 0, 0);
			}	
			Debug.Log("Key \'a\' is being pressed");
			animator.SetBool("MoveLeft", true);
			++walkCount;
		}
		else if (Input.GetKey("d"))
		{
			if (walkCount>4)
			{
				animator.SetBool("GoRight", true);
				trans = Input.GetAxis("Horizontal");
				trans*= .01f;
				transform.Translate(trans, 0, 0);
			}	
			Debug.Log("Key \'d\' was pressed");
			animator.SetBool("MoveRight", true);
			++walkCount;
		}	

		if (!Input.GetKey("w") && !Input.anyKey)
		{
			animator.SetBool("MoveUp", false);
			animator.SetBool("GoUp", false);
			walkCount = 0;
		}	
		if (!Input.GetKey("s") && !Input.anyKey)		
		{
			animator.SetBool("MoveDown", false);
			animator.SetBool("GoDown", false);
			walkCount = 0;
		}	
		if (!Input.GetKey("a") && !Input.anyKey)
		{
			animator.SetBool("MoveLeft", false);
			animator.SetBool("GoLeft", false);
			walkCount = 0;
		}	
		if (!Input.GetKey("d") && !Input.anyKey)
		{
			animator.SetBool("MoveRight", false);
			animator.SetBool("GoRight", false);
			walkCount = 0;
		}
	}	
	// Update is called once per frame
	void Update () {
		NewMove();
	}
	void Move () {
		float horizontal = (Input.GetAxisRaw("Horizontal")) * .10f;
		float vertical = (Input.GetAxisRaw("Vertical")) * .10f;
		Vector2 start = transform.position;
		Vector2 end = start + new Vector2(horizontal, vertical);
		StartCoroutine ( DoIt(end));
	}	
	IEnumerator DoIt (Vector3 end) {
		float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
		while (sqrRemainingDistance  > float.Epsilon)
		{
			Vector3 newPosition = Vector3.MoveTowards(self.position, end, inverseMoveTime * Time.deltaTime);
			self.MovePosition(newPosition);
			sqrRemainingDistance = (transform.position - end).sqrMagnitude;

			yield return null;
		}	
	}	
	void CountItUp () {
		Debug.Log(count);
		++count;
	}	
}
