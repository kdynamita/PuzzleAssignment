﻿using UnityEngine;

public class Player : MonoBehaviour {

    public float speed = 1f;
	public float initialVelocity = 2f;
	public float decay = 0.1f;

    private float velocity = 0;

    public float time = 0f;
    private int score = 0;
	private int moveCount = 0;
	private CharacterController cc;
    public Manager manager;
    //private Toolbox toolbox;


    enum State {
        Idle,
        Moving,
		Won
    }

    private State currentState = State.Idle;

	// Use this for initialization
	void Start() {
		cc = this.GetComponent<CharacterController>();
        manager = Toolbox.GetInstance().GetManager();
        StartTimer();
    }

	// Update is called once per frame
	void Update() {
        Vector3 direction = Physics.gravity;
		direction += this.transform.forward * velocity;

		cc.Move(direction * Time.deltaTime);

		switch (this.currentState) {
			case State.Idle:
				this.Idle();
				break;
			case State.Moving:
				this.Moving();
				break;
			case State.Won:
				this.Won();
				break;
		}

        StartTimer();
    }

	void Idle () {
		if (Input.GetKey(KeyCode.LeftArrow)) {
			this.transform.rotation *= Quaternion.Euler(0, -this.speed * Time.deltaTime, 0);
		}

		if (Input.GetKey(KeyCode.RightArrow)) {
			this.transform.rotation *= Quaternion.Euler(0, this.speed * Time.deltaTime, 0);
		}

		if (Input.GetKey(KeyCode.Space)) {
			// let's gooo
			this.currentState = State.Moving;
			this.velocity = this.initialVelocity;
			this.moveCount++;
            this.manager.UpdateMoves();
        }
	}

	void Moving () {
		if (velocity > 0f) {
			this.velocity -= this.decay;
			this.velocity = Mathf.Clamp(this.velocity, 0, float.MaxValue);
		} else {
			this.velocity = 0;
			this.currentState = State.Idle;
		}
	}

	void Won () {
		//print("I Win");
		this.velocity = 0;
	}

	void OnControllerColliderHit(ControllerColliderHit hit) {
		if (this.currentState != State.Won) {
			//hit.gameObject.GetComponent<PlayerInteractable>()?.OnHit(hit, this);
			PlayerInteractable pi = hit.gameObject.GetComponent<PlayerInteractable>();
			if (pi) {
				pi.OnHit(hit, this);
			}
		}
	}

	public float GetVelocity () {
		return this.velocity;
	}

	public void SetVelocity (float vel) {
		this.velocity = vel;
	}

	public void HasWon () {
		this.currentState = State.Won;
        Debug.Log("You won!");
        manager.Won();
	}

	public int AccumulateScore (int scoreAdd) {
		this.score += scoreAdd;
        this.manager.UpdateScore();
		return this.score;
    }

    public void StartTimer()
    {
        time += 0.016f;
    }

    void OnDestroy()
    {
        this.manager.totalTime += time;
        this.manager.onScene = false;
        this.manager.DeleteAll();
    }
}
