// number of chickens (MUST match the Unity NUM_CHICKENS!)
2160 => int NUM_CHICKENS;
// beat (smallest division; dur between chickens)
400::ms => dur BEAT;
// update rate for pos
10::ms => dur POS_RATE;
// increment per step
POS_RATE/BEAT => float posInc;

// global (outgoing) variables for Unity animation
global int curentCell; // which chicken we are at
// will take on fractional values for smooth animation[0-NUM_CHICKENS)
global float playheadPos;

// global (incoming) data from Unity
global int editWhich;
1 => global float editRate;
1 => global float editGain;
global Event editHappened;

// a sequence (one of many ways to "sequence")
// this ones treats chickens as a step sequencer
float seqRate[NUM_CHICKENS];
float seqGain[NUM_CHICKENS];
// initialize
for( int i; i < NUM_CHICKENS; i++ )
{
    1 => seqGain[i];
    1 => seqRate[i];
}

// sound buffers
SndBuf bufs[NUM_CHICKENS];
// reverb
NRev reverb => dac;
// reverb mix
.1 => reverb.mix;

// connect them
for( int i; i < NUM_CHICKENS; i++ )
{
    // connect to dac
    bufs[i] => reverb;
    // load sound
    "special:dope" => bufs[i].read;
    // silence
    0 => bufs[i].gain;
}

// spork update
spork ~ playheadPosUpdate();
// spork edit listener
spork ~ listenForEdit();

// simple sequencer loop
while( true )
{
    // play current
    play( currentChicken,
          seqGain[currentChicken], seqRate[currentChicken] );
    // sync with discrete grid position
    currentChicken => playheadPos;
    // advance time by duration of one beat 
    BEAT => now;
    // increment to next chicken
    currentChicken++;
    // wrap
    if( currentChicken >= NUM_CHICKENS ) 0 => currentChicken;
}

fun void play( int which, float gain, float rate )
{
    // restart
    0 => bufs[which].pos;
    // set gain
    // gain => bufs[which].gain;
    gain => bufs[which].gain;
    // set rate
    rate => bufs[which].rate;
}

// updates the global playheadPos with fine granularity,
// for visualizing the playhead smoothly in Unity
fun void playheadPosUpdate()
{
    while( true )
    {
        // increment
        posInc +=> playheadPos;
        // advance time
        POS_RATE => now;
    }
}


// this listens for events from Unity to update the sequence
fun void listenForEdit()
{
    while( true )
    {
        // wait for event
        editHappened => now;
        // update the gain and rate
        editRate => seqRate[editWhich];
        editGain => seqGain[editWhich];
        
        <<< "EDIT rate:", editRate, "gain:", editGain >>>;
    }
}