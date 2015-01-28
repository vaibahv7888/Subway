#pragma strict 
 
var forceFactor = 1.0;
var up = 125.0;
 
private var startTime : float;
private var startPos : Vector3;
private var orgPos : Vector3;
 
function Start() {
    orgPos = transform.position;
    rigidbody.isKinematic = true;
}
 
function OnMouseDown() {
    startTime = Time.time;
    startPos = Input.mousePosition;
}
 
function OnMouseUp() {
    var deltaTime = Time.time - startTime;
    var dir = Input.mousePosition - startPos;
    var speed = dir.magnitude / deltaTime;
 
    var force = Vector3(dir.x, up, dir.y).normalized * speed * forceFactor;
    rigidbody.isKinematic = false;
    rigidbody.AddForce(force);
    Invoke("ReturnBall", 2.5);
}
 
function ReturnBall() {
    transform.position = orgPos;
    rigidbody.velocity = Vector3.zero;
    rigidbody.isKinematic = true;
}