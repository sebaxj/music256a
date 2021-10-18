//
//
// MUSICAL STATEMENT /////////////////////////////////////////////////////////////////////////
Drone imp1;
Drone imp2;
Drone imp3;
Drone imp4;
Drone imp5;

sporkImpulse();

// spork beeping

// spork sweeping panning of a whoosing
Beep b1;
spork ~ b1.runLeft();

5::second => now;

sporkImpulse();

spork ~ b1.runRight();
5::second => now;

sporkImpulse();

Beep b2;
spork ~ b2.runLeft();
400::ms => now;
Beep b3;
spork ~ b3.runLeft();
200::ms => now;
spork ~ b2.runRight();
5::second => now;

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
    SinOsc s => Pan2 p => dac;
    0.0 => s.gain;
    
    0.0 => float t;
    10::ms => dur T;
    
    fun void buzz(float src, float target, dur duration, dur misc) {
        0.1 => s.gain;
        
        src => float freq;
        
        // calculate steps between src and target
        duration / misc => float steps;
        
        // calculate increment size based on steps between src and target
        (target - src) / steps => float increment;
        
        // to keep track of distance to-go
        float count;
        
        while(count < steps) {
            freq + increment => freq;
            1 +=> count;
            Std.mtof(freq) => s.freq;
            misc => now;
        }
    }
    
    // function to sweep panning Left
    fun void runLeft() {
        0.1 => s.gain;
        1::second => now;
        spork ~ buzz(127, 20, 1::second, 1::ms);
        while(Math.sin(t) < 0.99) {
            // pan the sound from left to right
            Math.sin(t) => p.pan;
            
            T / second * 2.5 +=> t;
            
            T => now;
        }
        0.0 => s.gain;
     }
     
    // function to sweep panning Right
    fun void runRight() {
        0.1 => s.gain;
        1::second => now;
        0.99 => t;
        spork ~ buzz(20, 127, 1::second, 1::ms);
        while(Math.sin(t) > -0.99) {
            // pan the sound from left to right
            Math.sin(t) => p.pan;
            
            T / second * 2.5 +=> t;
            
            T => now;
        }
        0.0 => s.gain;
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
