// 0 = off
// 1 = on

///////////////////////////////////////////////////////////
// Global Variables
0 => global int HEART;
0 => global int LUNGS;
0 => global int BRAIN;
global Event editHappened;

// Constants
0::ms => dur BEAT;

// CHORDS ARRAYS //
// min
[0., 3., 7., 0.] @=> float min[];

// min4/2
[-2., 0., 3., 7.] @=> float min42[];

// dim
[-12., 3., 6., 0.] @=> float dim[];

// Maj
[0., 4., 7., 12.] @=> float maj[];

// V7
[0., 4., 7., 10.] @=> float v7[];

// fully dim 7
[0., 3., 6., 9.] @=> float dim7[];
///////////////////////////////////////////////////////////

///////////////

// Global UGen //
// patch low -> rev -> dax ->
LPF low => NRev rev => dac;

// Set Param for UGen //
// mix reverb
.1 => rev.mix;

// drum section
me.dir() + "Heartbeat.wav" => string filename;
if(me.args()) me.arg(0) => filename;

// patch
SndBuf buf => dac;

// set LPF
500 => low.freq;
1 => low.Q;
0.05 => low.gain;

// noise generator, biquad filter, dac (audio output) 
Noise n => BiQuad f => Gain g => dac;
// set biquad pole radius
.99 => f.prad;
// set biquad gain
0.05 => f.gain;
// set equal zeros 
1 => f.eqzs;
// our float
0.0 => float t;

0.0 => g.gain;

spork ~ listenForEdit();

// infinite time-loop
while( true ) {
    
    
    play(60, maj); // c maj
    
    // play 4 measure repeating melody
    BEAT => now;
}

// function to play a sound for heart
fun void playHeartSound() {
    filename => buf.read;
    0 => buf.pos;
    1.0 => buf.rate;
    10.0 => buf.gain;
    1::second => now;
}

// function to play a sound for lungs
fun void playLungSound() {
    while(LUNGS == 1) {
        
    }
}

// function to play a sound brain
fun void playBrainSound() {
    while(BRAIN == 1) {
        
    }
}

// function to listen for an edit
fun void listenForEdit() {
    while(true) {
        editHappened => now;
    }
    
    while(HEART == 1) {
        play(80, maj);
        spork ~ playHeartSound();
        1::second => now;
    }
    
    while(LUNGS == 1) {
        1.0 => g.gain;
        // sweep the filter resonant frequency
        100.0 + Std.fabs(Math.sin(t)) * 15000.0 => f.pfreq;
        t + .01 => t;
        // advance time
        5::ms => now;
    }
}

fun void playChord(int root, float chord[], float vel, 
dur a, dur d, float s, dur r)
{
    // ugens "local" to the function
    TriOsc osc[4];
    ADSR e => low;
    
    // patch
    for(0 => int i; i < osc.cap(); i++) {
        osc[i] => e;
    }
    
    // freq and gain
    for(0 => int i; i < osc.cap(); i++) {
        Std.mtof(root + chord[i]) => osc[i].freq;
        vel => osc[i].gain;
    }
    
    
    // open env (e is your envelope)
    e.set(a, d, s, r);
    e.keyOn();
    
    // A through end of S
    e.releaseTime() => now;
    
    // close env
    e.keyOff();
    
    // release
    e.releaseTime() => now;
    
    e !=> low;
}


fun void play(int root, float chord[]) {
    
    .5 => float vel;
    
    for(0 => int i; i < 8; i++) {
        playChord(root, chord, vel, 50::ms, 50::ms, 0.5, 100::ms);
        
        80::ms => now;
        vel - .2 => vel;
    }
}



