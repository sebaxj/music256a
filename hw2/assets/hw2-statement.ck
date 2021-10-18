//
//
// MUSICAL STATEMENT /////////////////////////////////////////////////////////////////////////
Drone imp1;
Drone imp2;
Drone imp3;
Drone imp4;
Drone imp5;

spork ~ sporkImpulse();

// spork beeping

// spork sweeping panning of a whoosing
Beep b1;
spork ~ b1.run();

5::second => now;

spork ~ sporkImpulse();

fun void sporkImpulse() {
    spork ~ imp1.run(50);
    300::ms => now;

    spork ~ imp2.run(65);
    600::ms => now;

    spork ~ imp3.run(69);
    1200::ms => now;

    spork ~ imp2.run(77);
    2400::ms => now;

    spork ~ imp3.run(81);
    4800::ms => now;
}
//////////////////////////////////////////////////////////////////////////////////////////////
class Beep {
    Noise n => Pan2 p => dac;
    0.1 => n.gain;
    
    0.0 => float t;
    10::ms => dur T;
    
    1::second => now;
    
    // function to sweep panning
    fun void run() {
        while(true) {
            // pan the sound from left to right
            Math.sin(t) => p.pan;
            
            T / second * 2.5 +=> t;
            
            T => now;
        }
     }
}

class Drone {
    // Patch
    Impulse i => TwoZero t => OnePole p;

    // formant filters
    p => TwoPole f1 => Gain g;

    // reverbs
    g => JCRev r => dac;
    g => JCRev rL => dac.left;
    g => JCRev rR => dac.right;

    // delays
    g => Delay d1 => Gain g1 => r;
    g => Delay d2 => Gain g2 => rL;
    g => Delay d3 => Gain g3 => rR;

    // connect gains to delays
    g1 => d1; g2 => d2; g3 => d3;

    // source gain (amplitude of the impulse train)
    0.25 => float sourceGain;

    // set filter coefficients
    1.0 => t.b0;  0.0 => t.b1; -1.0 => t.b2;

    // set gains
    0.1 => g1.gain;	0.1 => g2.gain;	0.1 => f1.gain;

    // set reverb mix
    0.04 => r.mix;

    // set delay max and length
    1.5 :: second => d1.max;
    2.0 :: second => d2.max;
    2.8 :: second => d3.max;
    1.41459 :: second => d1.delay;
    1.97511 :: second => d2.delay;
    2.71793 :: second => d3.delay;

    // set two pole filter radii and gain
    0.997 => f1.radius; 
    1.0 => f1.gain; 

    // leaky integrator
    0.99 => p.pole;
    1.0 => p.gain;

    // variables that control impulse train source
    0.013 => float period;
    0.013 => float targetPeriod;
    0.0 => float modphase;
    0.0001 => float vibratoDepth;

    fun void run(int freq) {
        Std.mtof(freq) => f1.freq;
        
        spork ~ doImpulse(); // generate voice source
        spork ~ sweepGain(1.0);
        
        // main shred loop
        10::second => now;
    }
    
    // function to sweep gain
    fun void sweepGain(float n) {
        0 => float t;
        while(true) {
            // sweep the gain from 0.0 to n
            0.0 + Std.fabs(Math.sin(t)) * n => f1.gain;
            
            // move to next wave number
            t + .01  => t;
            
            // wait 10 seconds to change filter resonance
            10::ms => now;
        }
}

    // entry point for shred: generate source impulse train
    fun void doImpulse()
    {
        // infinite time-loop
        while( true )
        {
            // fire impulse
            sourceGain => i.next;
            // phase variable
            modphase + period => modphase;
            // vibrato depth
            .0001 => vibratoDepth;
            // modulate wait time until next impulse: vibrato
            (period + vibratoDepth*Math.sin(2*pi*modphase*6.0))::second => now;
        }
    }
}
