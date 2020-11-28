//****************************************************************************************************************************
//Program name: "Line". This program takes user commands from a set of buttons and displays an animated skateboarder         *
//skateing around an angled surface area.  Copyright (C) 2020  Spencer DeMera                                                *                                                                             *
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License  *
//version 3 as published by the Free Software Foundation.                                                                    *
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied         *
//warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.     *
//A copy of the GNU General Public License v3 is available here:  <https://www.gnu.org/licenses/>.                           *
//****************************************************************************************************************************

//Author information
//  Author name: Spencer DeMera
//  Author email: spencer.demera@csu.fullerton.edu
//
//Program information
//  Program name: Line
//  Programming languages: C#
//  Date program began: 09-27-2020
//  Date of last update: 10-19-2020
//  Comments reorganized: 10-19-2020
//  Files in the program: main.cs, lineInterface.cs run.sh
//
//Purpose
//  Display a stand-in skateboarder moving around a triangular path.
//  For learning purposes: Demonstrate use of hybrid programming concepts in a theoretical setting
//
//This file
//  File name: lineInterface.cs
//  Language: C#
//  Optimal print specification: 7 point font, monospace, 136 columns, 8½x11 paper
//  Compile: mcs -t:library -r:System.Windows.Forms.dll -r:System.Drawing.dll -out:lineInterface.dll lineInterface.cs
//  Compile and Link: mcs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:lineInterface.dll -out:line.exe main.cs
//
//Execution: ./line.exe

//===== Begin code area ====================================================================================================================================================

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

public class LineInterface : Form {

  // initializations
  private Label welcome = new Label();
  private Label timeDescription = new Label();
  private Button startbutton = new Button();
  private Button pausebutton = new Button();
  private Button timebutton = new Button();
  private Button exitbutton = new Button();
  private Panel headerpanel = new Panel();
  private Panel controlpanel = new Panel();
  private Size maxConverterinterfacesize = new Size(1420,1024); // window size
  private Size minConverterinterfacesize = new Size(1420,1024); // window size

  // Animation and Timer variables
  double animationCtr = 0.0; // ctr for animation
  int refreshCtr = 0; // ctr for refresh time
  double timeInSeconds = 0.0; // ctr for actual time passed
  bool timestarted = false; // flag for start button termainl print
  private const int speed = 90;
  private const double animationSpeed = 40.0; // 40 Hz 
  private const double refreshSpeed = 60.0; // 60 Hz
  private const double animationInterval = 1000.0 / animationSpeed; // sets animation interval
  private const double refreshInterval = 1000.0 / refreshSpeed; // sets refresh interval
  int animateSpeed = (int)System.Math.Round(animationInterval); // sets animation speed
  private const int radius = 10;
  private const int spx = 1155; // 1150
  private const int spy = 185; // 185
  private const int epx = 135; // 135
  private const int epy = 800; // 800
  private const double deltaX = 1.945931766; // delta X
  private const double deltaY = 1.129535106; // delta Y
  private double xPos = 0.0; // position of X
  private double yPos = 0.0; // position of Y
  bool firstStart = false; // true if program started for first time
  private Pen schaffer = new Pen(Color.Black,1); // pen variables
  private Pen bic = new Pen(Color.Black,(int)System.Math.Round(3.0)); // bic pen
  Point startPoint = new Point(1170, 200); // starting point
  Point endPoint = new Point(145, 800); // ending point
  private bool reverseFlag = false; // flag for if ball has reversed

   // Clock functions
   private static System.Timers.Timer animationClock = new System.Timers.Timer(); // creates new clock
   private static System.Timers.Timer refreshClock = new System.Timers.Timer(); // creates time keeping clock

   public LineInterface() {   //The constructor of this class
     //Set the size of the user interface box.
     MaximumSize = maxConverterinterfacesize;
     MinimumSize = minConverterinterfacesize;
     //Initialize text strings
     Text = "Line";
     welcome.Text = "Line Animation by Spencer DeMera";
     startbutton.Text = "Start";
     pausebutton.Text = "Pause";
     timebutton.Text = "000.00";
     timeDescription.Text = "Elapsed Time (seconds): ";
     exitbutton.Text = "Exit";

     //Set sizes
     Size = new Size(400,240);
     welcome.Size = new Size(1024,44);
     startbutton.Size = new Size(80,45);
     pausebutton.Size = new Size(80,45);
     timebutton.Size = new Size(100, 45);
     timeDescription.Size = new Size(300, 45);
     exitbutton.Size = new Size(80,45);
     // Panel sizes
     headerpanel.Size = new Size(1820,75);
     controlpanel.Size = new Size(1820,75);

     //Set colors
     // Panel colors
     headerpanel.BackColor = Color.LightCoral;
     controlpanel.BackColor = Color.DarkGray;
     // Button colors
     startbutton.BackColor = Color.DodgerBlue;
     pausebutton.BackColor = Color.DodgerBlue;
     timebutton.BackColor = Color.DodgerBlue;
     exitbutton.BackColor = Color.DodgerBlue;

     //Set fonts
     welcome.Font = new Font("Comic Sans MS",17,FontStyle.Bold);
     startbutton.Font = new Font("Comic Sans MS",12,FontStyle.Bold);
     pausebutton.Font = new Font("Comic Sans MS",12,FontStyle.Bold);
     timebutton.Font = new Font("Comic Sans MS",15,FontStyle.Bold);
     timeDescription.Font = new Font("Comic Sans MS",15,FontStyle.Bold);
     exitbutton.Font = new Font("Comic Sans MS",12,FontStyle.Bold);

     //Set locations
     // Welcome text location
     welcome.Location = new Point(475,20);
     // Button Locations
     startbutton.Location = new Point(75,10);
     pausebutton.Location = new Point(275,10);
     timebutton.Location = new Point(1025,10);
     exitbutton.Location = new Point(1170,10);
     //Time text location
     timeDescription.Location = new Point(750, 20);
     // Panel Locations
     headerpanel.Location = new Point(0,0);
     controlpanel.Location = new Point(0,924);

     //Open this user interface window in the center of the display.
     CenterToScreen();

     // controls and clicks
      Controls.Add(headerpanel);
      headerpanel.Controls.Add(welcome);
      Controls.Add(controlpanel);
      controlpanel.Controls.Add(startbutton);
      controlpanel.Controls.Add(pausebutton);
      controlpanel.Controls.Add(timebutton);
      controlpanel.Controls.Add(timeDescription);
      controlpanel.Controls.Add(exitbutton);
      // click calls to functions
      startbutton.Click += new EventHandler(startLine);
      pausebutton.Click += new EventHandler(pauseclock);
      exitbutton.Click += new EventHandler(exitfromthisprogram);

      // Clock functions
      animationClock.Enabled = false;
      animationClock.Elapsed += new ElapsedEventHandler(timeUpdateBox);
      refreshClock.Enabled = false;
      refreshClock.Elapsed += new ElapsedEventHandler(refreshTime);
    
      // initialize ball at starting point
      xPos = spx;
      yPos = spy;    
   } //End of constructor

   protected override void OnPaint(PaintEventArgs ee) {
     Graphics graph = ee.Graphics; // graphics

     graph.FillRectangle(Brushes.Aquamarine, 0, 75, 1820, 850); // draws center rectangle display area

     graph.DrawLine(bic, startPoint, endPoint); // draws line for ball

     // draws the dynamic ellipse
     graph.FillEllipse(Brushes.Red, (float)System.Math.Round(xPos), (float)System.Math.Round(yPos), (3.0f * radius), (3.0f * radius));

     //The next statement looks like recursion, but it really is not recursion.
     //In fact, it calls the method with the same name located in the super class.
     base.OnPaint(ee);
   } // OnPaint

  protected void refreshTime(Object sender, ElapsedEventArgs eventArgs) {
    Invalidate(); // calls invalidate
  } // refreshTime

   protected void startLine(Object sender, EventArgs events) {
     if (timestarted == false) { // if time has started
        animationClock.Enabled = true; // disabled both clocks
        refreshClock.Enabled = true;
        firstStart = true; // flip bool flag
        timestarted = true; // flip bool flag
        startbutton.BackColor = Color.Gray; // change button color
        System.Console.WriteLine("Timers started."); 
        Invalidate(); // calls invalidate
     } // if
     else if (timestarted == true) {
       System.Console.WriteLine("Timer already started.");
     } // else if
   } // startlight

   protected void timeUpdateBox(Object sender, EventArgs events) {
    animationCtr += (double)animateSpeed / 240.0; // gets time in seconds
    timebutton.Text = String.Format("{0:000.00}", animationCtr); // prints time to panel

    // if ball has reached the bottom
    if (xPos <= 145 && yPos >= 800) {
      reverseFlag = true;
    } // if
    // when ball gets to top
    else if ((xPos >= 1170 && yPos <= 200) && reverseFlag == true) { 
      xPos = 1155;
      yPos = 185;
      Invalidate(); // calls invalidate
      
      // stops clocks and resets positioning
      animationClock.Enabled = false; // stops both clocks
      refreshClock.Enabled = false;
      timestarted = false; // flips bool flag
      firstStart = false; // flips bool flag
      startbutton.BackColor = Color.DodgerBlue; // resets button color
      animationCtr = 0.0; // negates buttonCtr
    } // else if

    // if its travelling down the line
    if (reverseFlag == false) {
      xPos -= deltaX;
      yPos += deltaY;
    } // if
    // else if its travelling up the line
    else if (reverseFlag == true) {
      xPos += deltaX;
      yPos -= deltaY;
    } // else if
   } // changespeed

   protected void pauseclock(Object sender, EventArgs events) {
     if (timestarted == true) { // if pause button has been clicked
        animationClock.Enabled = false; // stops both clocks
        refreshClock.Enabled = false; 
        timestarted = false; // flips bool flag
        startbutton.BackColor = Color.DodgerBlue; // change button color
        System.Console.WriteLine("Timers paused.");
        Invalidate(); // calls invalidate
     } // if
     else if (timestarted == false) {
        System.Console.WriteLine("Timers already paused.");
     } // else if
   } // pauseclock

   protected void exitfromthisprogram(Object sender,EventArgs events) {
     System.Console.WriteLine("This program will end execution...");
     Close();
   } // Exit message
} //End of class Simpleform
