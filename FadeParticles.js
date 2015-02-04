#pragma strict

var time: float = 10;

function Start () {

}

function Update () {

	time = time - 1;
	if(time<0)
		KillFire();
}

function KillFire(){
//Unparent the effect
    this.transform.parent = null;
   
    //Stop emitting particles
    var particleSystem:ParticleEmitter = this.GetComponent(ParticleEmitter);
    particleSystem.emit = false;

}