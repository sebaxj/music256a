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

spork ~ listenForEdit();

// drum section
me.dir() + "Heartbeat.wav" => string filename;
if(me.args()) me.arg(0) => filename;

// patch
SndBuf buf => dac;

// zombie section
me.dir() + "zombie.wav" => string filename2;
if(me.args()) me.arg(0) => filename;

// patch
SndBuf buf2 => dac;

// lightning section
me.dir() + "lightning.wav" => string filename3;
if(me.args()) me.arg(0) => filename;

// patch
SndBuf buf3 => dac;

// Global UGen //
// patch low -> rev -> dax ->
LPF low => NRev rev => dac;

// set LPF
500 => low.freq;
1 => low.Q;
0.05 => low.gain;

// Set Param for UGen //
// mix reverb
.1 => rev.mix;

// infinite time-loop
while( true ) {
    play(60, maj); // c maj
    
    // play 4 measure repeating melody
    BEAT => now;
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

fun void heart() {
    while(true) {
        editHappened => now;
        while(HEART == 1) {
        // add heart sound
        
        filename => buf.read;
        0 => buf.pos;
        1.0 => buf.rate;
        0.6 => buf.gain;
        1::second => now;
        
        }
    }
}

fun void lungs() {
    while(true) {
        editHappened => now;
        while(LUNGS == 1) {
            // noise generator, biquad filter, dac (audio output) 
            Noise n => BiQuad f => Gain g => low;
            2 => g.gain;
            // set biquad pole radius
            .99 => f.prad;
            // set biquad gain
            .05 => f.gain;
            // set equal zeros 
            1 => f.eqzs;
            // our float
            0.0 => float t;
            
            
            // infinite time-loop
            while( LUNGS == 1 )
            {
                // sweep the filter resonant frequency
                100.0 + Std.fabs(Math.sin(t)) * 15000.0 => f.pfreq;
                t + .01 => t;
                // advance time
                5::ms => now;
            }
            0.0 => g.gain;
            g !=> low;
            
        }
    }
}

fun void brain() {
    while(true) {
        editHappened => now;
        while(BRAIN == 1) {
            // add zombie and lightning sound
        
            filename2 => buf2.read;
            0 => buf2.pos;
            1.0 => buf2.rate;
            0.08 => buf2.gain;
            3::second => now;
            
            filename3 => buf3.read;
            0 => buf3.pos;
            1.0 => buf3.rate;
            0.1 => buf3.gain;
            10::second => now;
        }
    }
}


// function to listen for an edit
fun void listenForEdit() {
    spork ~ heart();
    spork ~ lungs();
    spork ~ brain();
    while(true) {
        editHappened => now;
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