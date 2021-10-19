// spork beeping
Beep b1;
Beep b2;
Beep b3;
Beep b4;
Beep b5;
Beep b6;
Beep b7;
Beep b8;
Beep b9;
Beep b10;
Beep b11;
Beep b12;
Beep b13;
Beep b14;
Beep b15;
Beep b16;
Beep b17;
Beep b18;
Beep b19;
Beep b20;

spork ~ drone(50);
5::second => now;

spork ~ drone(53);
1::second => now;

spork ~ b6.runLeft(2::second);
1000::ms => now;

spork ~ b7.runRight(2::second);
1000::ms => now;

spork ~ b8.runLeft(1::second);
1000::ms => now;

spork ~ b9.runRight(500::ms);
900::ms => now;

spork ~ b10.runRight(500::ms);
1000::ms => now;

spork ~ b11.runLeft(3::second);
1000::ms => now;

spork ~ b12.runRight(2::second);
1000::ms => now;

spork ~ drone(62);
800::ms => now;
spork ~ drone(50);
200::ms => now;
spork ~ drone(53);
100::ms => now;

spork ~ b1.runLeft(5::second);
3::second => now;

spork ~ b2.runRight(400::ms);
2::second => now;

spork ~ drone(57);
4::second => now;

spork ~ drone(64);
1::second => now;

spork ~ drone(72);
2::second => now;

spork ~ b3.runLeft(600::ms);
400::ms => now;

spork ~ b4.runLeft(12::second);
200::ms => now;

spork ~ drone(64);
1::second => now;
spork ~ b5.runRight(8::second);
100::ms => now;
spork ~ drone(80);
400::ms => now;
spork ~ drone(81);
1::second => now;
spork ~ drone(83);
8::second => now;

dac => Gain g => Delay delay => dac;

1.0 => g.gain;

// set delay
500::samp => delay.delay;

// set dissipation factor for delay
Math.pow(.99999, 500) => delay.gain;

// advance time propotionally to samples (bc of comb filter)
(Math.log(.0001) / Math.log(.99999))::samp => now;

spork ~ b13.runRight(2000::ms);
400::ms => now;
spork ~ b14.runLeft(2000::ms);
400::ms => now;
spork ~ b15.runRight(2000::ms);
400::ms => now;
spork ~ b16.runLeft(2000::ms);
400::ms => now;
spork ~ b17.runRight(2000::ms);
400::ms => now;
spork ~ b18.runLeft(2000::ms);
400::ms => now;
spork ~ b19.runRight(2000::ms);
400::ms => now;
spork ~ b20.runLeft(2000::ms);
400::ms => now;
spork ~ b13.runRight(2000::ms);
400::ms => now;
spork ~ b14.runLeft(2000::ms);
400::ms => now;
spork ~ b15.runRight(2000::ms);
400::ms => now;
spork ~ b16.runLeft(2000::ms);
400::ms => now;
spork ~ b17.runRight(2000::ms);
400::ms => now;
spork ~ b18.runLeft(2000::ms);
400::ms => now;
spork ~ b19.runRight(2000::ms);
400::ms => now;
spork ~ b20.runLeft(2000::ms);
15::second => now;

fun void drone(int freq) {
    SinOsc s => ADSR e => NRev rev => dac;
    
    e.set(300::ms, 100::ms, .5, 100::ms);

    .5 => s.gain;
    .2 => rev.mix;
    // choose freq
    Std.mtof(freq) => s.freq;
    
    for(0 => int i; i < 9600; 800+=>i) {
        e.keyOn();
        500::ms => now;
        e.keyOff();
        
        e.releaseTime() => now;
        300::ms => now;
    }
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
        
        fun void runLeft(dur duration) {
            
            0.1 => s.gain;
            1::second => now;
            spork ~ buzz(127, 20, duration, 1::ms);
            while(Math.sin(t) < 0.99) {
                // pan the sound from left to right
                Math.sin(t) => p.pan;
                T / second * 2.5 +=> t;
                T => now;   
            }
            0.0 => s.gain; 
        }

        // function to sweep panning Right
        fun void runRight(dur duration) {
            0.1 => s.gain;
            1::second => now;
            0.99 => t;
            spork ~ buzz(20, 127, duration, 1::ms);
            while(Math.sin(t) > -0.99) {
                // pan the sound from left to right
                Math.sin(t) => p.pan;
                T / second * 2.5 +=> t;
                T => now;   
            }
            0.0 => s.gain;   
        }   
    }