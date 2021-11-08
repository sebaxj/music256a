// 0 = empty
// 1 = full

/////////////////////////////////////////////////////////////////////////////////////////
// Global Variables
80 => int NUM_COLS;
48 => int NUM_ROWS;
Std.mtof(60) => float BASE_NOTE;

// beat (smallest division; dur between chickens)
1::ms => dur BEAT;

// global (outgoing) variables for Unity animation
0 => global int cur_COL; // which cell we are at

// global (incoming) data from Unity
global int edit_COL;
global int edit_ROW;
0 => global int reset;
global Event editHappened;

/////////////////////////////////////////////////////////////////////////////////////////

// set up matrix to model Unity grid matrix
int grid[NUM_COLS][NUM_ROWS];

// patch
SinOsc s => ADSR e => dac;

// set A, D, S, and R
e.set( 10::ms, 8::ms, .5, 500::ms );

// set gain
0 => s.gain;
        
// spork edit listener
spork ~ listenForEdit();
 
1 => int first;
-1 => int sec;


///////////////////////////
// seeding
//1 => grid[0][0];



///////////////////////////

// sequencer loop
while(true) {
    // find which col we are in
    // for each filled cell in col:
      // play sound corresponding to frequency
        // BASE_NOTE + filled row
    // <<< cur_COL >>>;
    for(0 => int cur_ROW; cur_ROW < NUM_ROWS; cur_ROW++) {
        if(grid[cur_COL][cur_ROW] == 1) {
            spork ~ playSound(cur_ROW);
        }
        // advance time by duration of one beat 
        BEAT => now;
    }
             
    // increment to next column           
    cur_COL++;
     
    // wrap
    if(cur_COL >= NUM_COLS) 0 => cur_COL;   
    
}

// function to play sound
fun void playSound(int frequency) {
    
    .5 => s.gain;
    Std.mtof(60 + frequency) => s.freq;
    
    e.keyOn();
    
    // advance time
    500::ms => now;
    
    // key off; start RELEASE
    e.keyOff();
    
    // allow the RELEASE to ramp down to 0
    e.releaseTime() => now;
    
    // silence
    0 => s.gain;
}

// to update the sequence               
fun void listenForEdit() {
    while( true ) {
        // wait for event
        editHappened => now;
        
        if(reset == 0) {
            // update grid with edit
            if(grid[edit_COL][edit_ROW] == 0) {
                1 => grid[edit_COL][edit_ROW];     
            } else {
                0 => grid[edit_COL][edit_ROW]; 
            }
        } else if(reset == 1) {
            for(0 => int cur_COL; cur_COL < NUM_COLS; cur_COL++) {
                for(0 => int cur_ROW; cur_ROW < NUM_ROWS; cur_ROW++) {
                    0 => grid[cur_COL][cur_ROW];
                }
            }
        }
        
    }
}