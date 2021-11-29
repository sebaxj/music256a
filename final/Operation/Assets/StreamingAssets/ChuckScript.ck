// 0 = off
// 1 = on

///////////////////////////////////////////////////////////
// Global Variables
0 => global int HEART;
0 => global int LUNGS;
0 => global int BRAIN;
0 => global int STOMACH;
0 => global int KIDNEY_L;
0 => global int KIDNEY_R;
0 => global int INTESTINE;

global Event editHappened;

// Constants
240::ms => dur BEAT;

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

// pentatonic scale notes
[0., 2., 4., 7., 9., 12.] @=> float pent[];
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
    // chose random 0 or 1
    // if 1, play note
        // chose random pentatonic note
    // else, silence
    // move time
    
    Math.random2(0,5) => int determiner;
    
    <<< determiner >>>;
    
    if(determiner == 1) {
        // chose random pentatonic note
        // play()
        
        play(60, maj); // c maj
        BEAT * 24 => now;
    } else if(determiner == 2) {
        play(55, v7);
        BEAT * 24 => now;
    } else if(determiner == 3) {
        play(65, maj);
        BEAT * 24 => now;
    } else {
        BEAT * 16 => now;
    }
}

fun void play(int root, float chord[]) {
    
    .5 => float vel;
    
    for(0 => int i; i < 8; i++) {
        playChord(root, chord, vel, 50::ms, 50::ms, 0.5, 100::ms);
        
        80::ms => now;
        vel - .2 => vel;
    }
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

fun void pentatonic() {
    
    TriOsc note => ADSR e2;
    
    200::ms => dur a;
    160::ms => dur d;
    0.5 => float s;
    120::ms => dur r;
    
    while(true) {
        
        Math.random2(0,1) => int determiner;
        
        <<< "NOTE:", determiner >>>;
        
        if(determiner == 1) {
        
        
            // random frequency
            Std.mtof(48 + pent[Math.random2(0, 5)]) => note.freq;
        
        
            // random gain
            Math.random2f(0.6, 2.0) => note.gain;
        
            e2 => low;
        
        
            // open env (e is your envelope)
            e2.set(a, d, s, r);
            e2.keyOn();
        
            // A through end of S
            e2.releaseTime() => now;
        
            // close env
            e2.keyOff();
        
            // release
            e2.releaseTime() => now;
        
            e2 !=> low;
        }
        
        BEAT => now;
     
    }
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

fun void stomach() {
    TriOsc note => ADSR e2;
    
    200::ms => dur a;
    160::ms => dur d;
    0.5 => float s;
    120::ms => dur r;
    
    
    
    while(true) {
        editHappened => now;
        while(STOMACH == 1) {
            
            Math.random2(0,2) => int determiner;
            
            <<< "NOTE:", determiner >>>;
            
            if(determiner == 1) {
                
                
                // random frequency
                Std.mtof(48 + pent[Math.random2(0, 5)]) => note.freq;
                
                
                // random gain
                Math.random2f(6.6, 8.0) => note.gain;
                
                e2 => low;
                
                
                // open env (e is your envelope)
                e2.set(a, d, s, r);
                e2.keyOn();
                
                // A through end of S
                e2.releaseTime() => now;
                
                // close env
                e2.keyOff();
                
                // release
                e2.releaseTime() => now;
                
                e2 !=> low;
            }
            
            BEAT => now;
            
        }
    }
}

fun void kidney_L() {
    TriOsc note => ADSR e2;
    
    200::ms => dur a;
    160::ms => dur d;
    0.5 => float s;
    120::ms => dur r;
    
    while(true) {
        editHappened => now;
        while(KIDNEY_L == 1) {
            
            Math.random2(0,4) => int determiner;
            
            <<< "NOTE:", determiner >>>;
            
            if(determiner == 1) {
                
                
                // random frequency
                Std.mtof(72 + pent[Math.random2(0, 5)]) => note.freq;
                
                
                // random gain
                Math.random2f(7.2, 8.5) => note.gain;
                
                e2 => low;
                
                
                // open env (e is your envelope)
                e2.set(a, d, s, r);
                e2.keyOn();
                
                // A through end of S
                e2.releaseTime() => now;
                
                // close env
                e2.keyOff();
                
                // release
                e2.releaseTime() => now;
                
                e2 !=> low;
            }
            
            BEAT => now;
            
        }
    }        
}

fun void kidney_R() {
    TriOsc note => ADSR e2;
    
    200::ms => dur a;
    160::ms => dur d;
    0.5 => float s;
    120::ms => dur r;
    
    while(true) {
        editHappened => now;
        while(KIDNEY_R == 1) {
            
            Math.random2(0,4) => int determiner;
            
            <<< "NOTE:", determiner >>>;
            
            if(determiner == 1) {
                
                
                // random frequency
                Std.mtof(72 + pent[Math.random2(0, 5)]) => note.freq;
                
                
                // random gain
                Math.random2f(7.2, 8.5) => note.gain;
                
                e2 => low;
                
                
                // open env (e is your envelope)
                e2.set(a, d, s, r);
                e2.keyOn();
                
                // A through end of S
                e2.releaseTime() => now;
                
                // close env
                e2.keyOff();
                
                // release
                e2.releaseTime() => now;
                
                e2 !=> low;
            }
            
            BEAT => now;
            
        }
    }
}

fun void intestine() {
    TriOsc note => ADSR e2;
    
    200::ms => dur a;
    160::ms => dur d;
    0.5 => float s;
    120::ms => dur r;
    
    while(true) {
        editHappened => now;
        while(INTESTINE == 1) {
            
            Math.random2(0,1) => int determiner;
            
            <<< "NOTE:", determiner >>>;
            
            if(determiner == 1) {
                
                
                // random frequency
                Std.mtof(36 + pent[Math.random2(0, 5)]) => note.freq;
                
                
                // random gain
                Math.random2f(7.0, 8.5) => note.gain;
                
                e2 => low;
                
                
                // open env (e is your envelope)
                e2.set(a, d, s, r);
                e2.keyOn();
                
                // A through end of S
                e2.releaseTime() => now;
                
                // close env
                e2.keyOff();
                
                // release
                e2.releaseTime() => now;
                
                e2 !=> low;
            }
            
            BEAT => now;
            
        }
    }
}

// function to listen for an edit
fun void listenForEdit() {
    spork ~ heart();
    spork ~ lungs();
    spork ~ brain();
    spork ~ stomach();
    spork ~ kidney_L();
    spork ~ kidney_R();
    spork ~ intestine();
    
    while(true) {
        editHappened => now;
    }
}


