// 0 = off
// 1 = on

///////////////////////////////////////////////////////////
// Global Variables
0 => global int HEART;
0 => global int LUNGS;
0 => global int BRAIN;
global Event editHappened;
///////////////////////////////////////////////////////////

spork ~ listenForEdit();

// infinite time-loop
while( true ) {
    500::ms => now;
}

// function to play a sound
fun void playSound() {
    // patch
    SinOsc s => ADSR e => NRev rev => dac;
    
    e.set( 200::ms, 80::ms, .5, 500::ms );
    
    // set gain
    .5 => s.gain;
    
    .01 => rev.mix;
    
    // key on: begin ATTACK
    // (note: ATTACK automatically transitions to DECAY;
    //        DECAY automatically transitions to SUSTAIN)
    e.keyOn();
    
    // advance time by 500 ms
    // (note: this is the duration from the
    //        beginning of ATTACK to the end of SUSTAIN)
    500::ms => now;
    
    // key off; start RELEASE
    e.keyOff();
    
    // allow the RELEASE to ramp down to 0
    e.releaseTime() => now;
    
    // advance time by 300 ms (duration until the next sound)
    300::ms => now;
}

// function to listen for an edit
fun void listenForEdit() {
    while(true) {
        editHappened => now;
        
        while(HEART == 1) {
            // add heart sound
            
            spork ~ playSound();
            
            2::second => now;
            
        }
        
        while(LUNGS == 1) {
            // add lungs sound
            
            spork ~ playSound();
            
            2::second => now;
            
        }
        
        while(BRAIN == 1) {
            // add brain sound
            
            spork ~ playSound();
            
            2::second => now;
            
        }
    }
}



