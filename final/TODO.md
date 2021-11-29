# Project **Operation**

TODO:

1. ChucK Integration:

   - Code a simple base track (pentatonic scale over a 4 bar sequence)
   - Add density and probability feature
   - Integrate smooth transitions between organ addition and subtraction

2. Add vitals monitor
   - Heart Rate
   - Brain Activity (EEG Trace)
   - Blood Pressure (Distortion?)
   - IF: vitals kill patient -> THEN: initiate kill function in ChucK and Unity
3. Add Epinephrine shots and Nurse's voice
   - Nurse's voice:
     - Consistent time intervals:
       - "Its a great day to save lives!"
     - When starting:
       - "Doctor, have you ever done this before?" ... "There is a first time for everything"
       - "Oh, this unfortunate soul"
     - When vitals are getting good (from empty)
       - "The patient is waking up!"
     - When the vitals are crashing (from full)
       - "Vitals are crashing. Do you want to administer EPI?"
4. Add Main Menu with two modes:
   - Start with empty body
   - Start with full body
5. Refactor project:
   - remove all extra files
   - sort files into folders
   - refactor scene hierarchy
   - refine .gitignore
6. Package application and files for download, publish final website
7. Refactor main index.html to present better UI to access all buttons

DONE:

- Watch drag-and-drop tutorial and implement the script to move organs between:
  - table
  - body
  - trash
- Integrate ChucK:
  - Install Chunity
  - Add global varaibles to communicate from Unity to ChucK
  - Add instrument classes for hear, lungs, and brain which are triggered by adding the respective organ to the body, and removed by draggin the respective organ to the trash (resets the organ to the table)
- Add all organs and exchange sprites for 3D assets

  - Add item slots for all organs
  - Get drag and drop to work with organs and trash
