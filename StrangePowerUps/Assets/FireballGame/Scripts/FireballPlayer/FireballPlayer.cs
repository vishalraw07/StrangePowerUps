using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballPlayer : MonoBehaviour {

    private enum PosState { ON_GROUND, IN_AIR };
    private enum GroundState { MOVE, DASH };
    private enum AirState { ASCEND, DESCEND, DASH };

    private PosState current_ps = PosState.ON_GROUND;
    private GroundState current_gs = GroundState.MOVE;
    private AirState current_as = AirState.ASCEND;

    private Rigidbody2D _rigidBody;
    private GameObject _sprite;
    private SoundsManager _sounds;

    [SerializeField] private float vMax = 8;

    [SerializeField] private float vDash = 24;
    [SerializeField] private float tDash = 0.1f;

    // H ~ v*v / g
    [SerializeField] private float vJump1 = 8;
    [SerializeField] private float vJump2 = 6;
    [SerializeField] private float gAscend = 16;
    [SerializeField] private float gDescend = 32;
    [SerializeField] private float fAir = 0.1f;

    private Vector3 _vel;
    private int facing = 1;
    private bool second_jump_avail = false;
    private bool air_dash_avail = false;

    private void Awake() {
        _rigidBody = GetComponent<Rigidbody2D>();
        _sprite = transform.GetChild(0).gameObject;
        _sounds = transform.GetChild(1).GetComponent<SoundsManager>();
    }

    private void Update() {
        if (current_ps == PosState.ON_GROUND) {

            if (current_gs == GroundState.MOVE) {
                if (Input.GetButtonDown("Dash")) {
                    StartDash(tDash);

                } else if (Input.GetButtonDown("Jump")) {
                    StartJump();
                }

            } else if (current_gs == GroundState.DASH) {
                if (Input.GetButtonDown("Jump")) {
                    CancelInvoke();
                    EndDash();
                    StartJump();
                }
            }

        } else if (current_ps == PosState.IN_AIR) {
            if (second_jump_avail && Input.GetButtonDown("Jump")) {
                if (current_as == AirState.DASH) {
                    CancelInvoke();
                    EndAirDash();
                }

                StartSecondJump();

            } else if (air_dash_avail && Input.GetButtonDown("Dash")) {
                StartAirDash(tDash);
            }
        }

        RotateSprite(_vel);
    }

    private void FixedUpdate() {

        if (current_ps == PosState.ON_GROUND) {

            if (current_gs == GroundState.MOVE) {
                float x = Input.GetAxisRaw("Horizontal");
                _vel.x = x * vMax;
                _vel.y = 0;

                if (x > 0) { facing = 1; } else if (x < 0) { facing = -1; }

                if (!IsGrounded()) {
                    StartJump(true);
                }

            } else if (current_gs == GroundState.DASH) {
                _vel.x = facing * vDash;
                _vel.y = 0;
            }


        } else if (current_ps == PosState.IN_AIR) {

            if (current_as != AirState.DASH) {
                float x = Input.GetAxisRaw("Horizontal");
                _vel.x += 60 * fAir * (x * vMax - _vel.x) * Time.fixedDeltaTime;
                _vel.x = Mathf.Clamp(_vel.x, -vMax, vMax);

                if (x > 0) { facing = 1; } else if (x < 0) { facing = -1; }
            }

            if (current_as == AirState.ASCEND) {
                //Debug.Log("ASC");
                _vel.y -= gAscend * Time.fixedDeltaTime;
                if (_vel.y <= 0 || !Input.GetButton("Jump")) { current_as = AirState.DESCEND; }

            } else if (current_as == AirState.DESCEND) {
                //Debug.Log("DESC");
                _vel.y -= gDescend * Time.fixedDeltaTime;
                _vel.y = Mathf.Clamp(_vel.y, -vJump1, 1000);

                if (IsGrounded()) { EndJump(); }

            } else if (current_as == AirState.DASH) {
                _vel.x = facing * vDash;
                _vel.y = 0;
            }
        }

        _rigidBody.velocity = _vel;
        Debug.DrawLine(transform.position, transform.position + _vel / 8, Color.white, 0.0f, false);

        //yield return new WaitForFixedUpdate();
    }

    private void StartDash(float dashTime) {
        //Debug.Log("DASH");
        current_gs = GroundState.DASH;
        Invoke("EndDash", dashTime);

        _sounds.PlayDashSound();
    }

    private void EndDash() {
        //Debug.Log("DASH END");
        current_gs = GroundState.MOVE;
    }

    public void Jump(float vJump) {
        //Debug.Log("Cutsom JUMP");
        current_ps = PosState.IN_AIR;

        current_as = AirState.ASCEND;
        _vel.y = vJump;

        second_jump_avail = true;
        air_dash_avail = true;

        _sounds.PlayJumpSound();
    }

    private void StartJump(bool isFalling = false) {
        //Debug.Log("JUMP");
        current_ps = PosState.IN_AIR;

        if (!isFalling) {
            current_as = AirState.ASCEND;
            _vel.y = vJump1;

        } else {
            current_as = AirState.DESCEND;
            _vel.y = 0;
        }

        second_jump_avail = true;
        air_dash_avail = true;

        if (!isFalling) {
            _sounds.PlayJumpSound();
        }
    }

    private void StartSecondJump() {
        //Debug.Log("SECOND JUMP");
        second_jump_avail = false;
        current_as = AirState.ASCEND;
        _vel.y = vJump2;

        _sounds.PlayJumpSound();
    }

    private void EndJump() {
        //Debug.Log("LAND");
        current_ps = PosState.ON_GROUND;
    }

    private void StartAirDash(float dashTime) {
        //Debug.Log("AIR DASH END");
        air_dash_avail = false;
        current_as = AirState.DASH;
        Invoke("EndAirDash", dashTime);

        _sounds.PlayDashSound();
    }

    private void EndAirDash() {
        //Debug.Log("AIR DASH");
        current_as = AirState.DESCEND;
    }

    private bool IsGrounded() {
        //float excessLength = 0.0050f;
        //return Physics2D.Raycast(transform.position, Vector2.down, 0.5f + excessLength).collider != null;

        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(0.4f, 0.05f), 0, Vector2.down, 0.5f);
        //Debug.Log(hit.collider);
        return hit.collider != null;
    }

    // change it to be accessible at any framerate
    private void RotateSprite(Vector3 v) {
        if (v == Vector3.zero) {
            v.x = facing;
        }

        float angle = -90 + Vector2.SignedAngle(Vector3.right, v);

        _sprite.transform.rotation = Quaternion.Lerp(_sprite.transform.rotation, Quaternion.Euler(0, 0, angle), 15 * Time.deltaTime);
        //_sprite.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public int GetFacing() { return facing; }
    public Vector2 GetVelocity() { return _vel; }

    public void Disable() {
        gameObject.SetActive(false);
    }

    /*private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log(collision.collider);
    }*/
}

// Dashing and Jumping have a common problem.
// Dashing and Ascending Jump donot stop even when collision occurs.
// They stay stuck!
// ONE potential solution is to somehow get back the velocity after update
// and set it back to _vel variable.
// ANOTHER solution is to dabble with OnCollisionEnter2D and fuck with it.