//****************************************************************************************************************************
//Program name: "Skateboard". This program takes user commands from a set of buttons and displays an animated skateboarder   *
//skateing around a triangular area.  Copyright (C) 2020  Spencer DeMera                                                         *                                                                             *
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
//  Program name: Skateboard
//  Programming languages: C#
//  Date program began: 09-27-2020
//  Date of last update: 10-17-2020
//  Comments reorganized: 10-17-2020
//  Files in the program: main.cs, boardInterface.cs run.sh
//
//Purpose
//  Display a stand-in skateboarder moving around a triangular path.
//  For learning purposes: Demonstrate use of hybrid programming concepts in a theoretical setting
//
//This file
//  File name: boardInterface.cs
//  Language: C#
//  Optimal print specification: 7 point font, monospace, 136 columns, 8½x11 paper
//  Compile: mcs -t:library -r:System.Windows.Forms.dll -r:System.Drawing.dll -out:boardInterface.dll boardInterface.cs
//  Compile and Link: mcs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:boardInterface.dll -out:Skateboard.exe main.cs
//
//Execution: ./Skateboard.exe

//===== Begin code area ====================================================================================================================================================

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

public class BoardInterface : Form {

  // initializations
  private Label welcome = new Label();
  private Label timeDescription = new Label();
  private Button startbutton = new Button();
  private Button pausebutton = new Button();
  private Button timebutton = new Button();
  private Button exitbutton = new Button();
  private Panel headerpanel = new Panel();
  private Panel controlpanel = new Panel();
  private Size maxConverterinterfacesize = new Size(1820,1024);
  private Size minConverterinterfacesize = new Size(1820,1024);

  double animationCtr = 0.0; // ctr for animation
  int refreshCtr = 0; // ctr for refresh time
  double timeInSeconds = 0.0; // ctr for actual time passed
  bool timestarted = false; // flag for start button termainl print
  private const int speed = 90;
  private const double animationSpeed = 40.0; // 40 Hz 
  private const double refreshSpeed = 60.0; // 60 Hz
  private const double animationInterval = 1000.0 / animationSpeed;
  private const double refreshInterval = 1000.0 / refreshSpeed;
  int animateSpeed = (int)System.Math.Round(animationInterval);
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

   // Clock functions
   private static System.Timers.Timer animationClock = new System.Timers.Timer(); // creates new clock
   private static System.Timers.Timer refreshClock = new System.Timers.Timer(); // creates time keeping clock

   public BoardInterface() {   //The constructor of this class
     //Set the size of the user interface box.
     MaximumSize = maxConverterinterfacesize;
     MinimumSize = minConverterinterfacesize;
     //Initialize text strings
     Text = "Skateboard";
     welcome.Text = "Skateboard Animation by Spencer DeMera";
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
     welcome.Location = new Point(675,20);
     // Button Locations
     startbutton.Location = new Point(75,10);
     pausebutton.Location = new Point(275,10);
     timebutton.Location = new Point(1425,10);
     exitbutton.Location = new Point(1670,10);
     //Time text location
     timeDescription.Location = new Point(1150, 20);
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
      startbutton.Click += new EventHandler(startBoard);
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
      // Triangle Points
      Point A = new Point(1170, 200);
      Point B = new Point(145, 800);
      Point C = new Point(1520, 800);

     Graphics graph = ee.Graphics;
     Point[] points = {A, B, C};

     graph.FillRectangle(Brushes.SkyBlue, 0, 75, 1820, 850); // draws center rectangle display area
     graph.FillRectangle(Brushes.SeaGreen, 0, 780, 1820, 850);

     graph.FillPolygon(Brushes.SaddleBrown, points);

     graph.FillEllipse(Brushes.Red, 
                      (float)System.Math.Round(xPos), 
                      (float)System.Math.Round(yPos), 
                      (3.0f * radius), 
                      (3.0f * radius));

     //The next statement looks like recursion, but it really is not recursion.
     //In fact, it calls the method with the same name located in the super class.
     base.OnPaint(ee);
   } // OnPaint

  protected void refreshTime(Object sender, ElapsedEventArgs eventArgs) {
    Invalidate();
  } // refreshTime

   protected void startBoard(Object sender, EventArgs events) {
     if (timestarted == false) {
        animationClock.Enabled = true;
        refreshClock.Enabled = true;
        firstStart = true;
        timestarted = true;
        startbutton.BackColor = Color.Gray;
        System.Console.WriteLine("Timers started.");
        Invalidate();
     } // if
     else if (timestarted == true) {
       System.Console.WriteLine("Timer already started.");
     } // else if
   } // startlight

   protected void timeUpdateBox(Object sender, EventArgs events) {
    animationCtr += (double)animateSpeed / 240.0;
    xPos -= deltaX; 
    yPos += deltaY;

    timebutton.Text = String.Format("{0:000.00}", animationCtr);
    // } // if its not at the end
    if (xPos <= 145 && yPos >= 800) {
      xPos = 1155;
      yPos = 185;
      Invalidate();
      
      animationClock.Enabled = false;
      refreshClock.Enabled = false;
      timestarted = false;
      firstStart = false;
      startbutton.BackColor = Color.DodgerBlue;
      animationCtr = 0.0;
    } // else if its at the end
   } // changespeed

   protected void pauseclock(Object sender, EventArgs events) {
     if (timestarted == true) {
        animationClock.Enabled = false;
        refreshClock.Enabled = false;
        timestarted = false;
        startbutton.BackColor = Color.DodgerBlue;
        System.Console.WriteLine("Timers paused.");
        Invalidate();
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
