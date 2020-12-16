//****************************************************************************************************************************
//Program name: "Final Program". This program makes a ball bounce around a 'table' and the user can click on said ball, once    *
//clicks >= 10, the game resets. Copyright (C) 2020  Spencer DeMera                                                         *  
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
//  Program name: Final Program
//  Programming language: C#
//  Date program began: 12-16-2020
//  Date of last update: 12-16-2020
//  Comments reorganized: 12-16-2020
//  Files in the program: main.cs, boardInterface.cs run.sh
//
//Purpose
//  Display a simple clicking ball game.
//  For learning purposes: Demonstrate use of hybrid programming concepts in a theoretical setting
//
//This file
//  File name: RicpchetInterface.cs
//  Language: C#
//  Optimal print specification: 7 point font, monospace, 136 columns, 8½x11 paper
//  Compile and Link: mcs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:RicochetInterface.dll -out:balls.exe main.cs
//
//Execution: ./final.exe

// ========== Begin code area ==========

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

public class RicochetInterface : Form {
    // ========== Declare UI Data ==========
    private const int formwidth = 1000;    //Preferred size: 1600;
    private const int formheight = 1260;    //Preferred size: 1200;
    private const int titleheight = 40;
    private const int graphicheight = 800;
    private const int lowerpanelheight = formheight - titleheight - graphicheight;

    // ========== Declare Ball Data ==========
    private double RedSpeed = 400.0;
    private double RedHypotenuseSquared = 0.0;
    private double RedHypotenuse = 0.0;
    private const double RedRadius = 22.0;
    private const double BlueRadius = 13;
    private double Distance = 0.0;
    private double RedPixPerSec;
    private double RedPixPerTic;
    private double RedDirectionX;
    private double RedDirectionY;
    private double RedDeltaX;
    private double RedDeltaY;
    private double RedCenterCoordX = 500.0;
    private double RedCenterCoordY = 400.0;
    private bool GameEnd = false;
    Random rand = new Random();
    private double mouseX;
    private double mouseY;
    private int attempts = 0;
    private int successes = 0;

    // ========== Timers ==========
    //Declare data about the motion clock:
    private static System.Timers.Timer MotionControlClock = new System.Timers.Timer();
    private const double MotionControlClockRate = 60;  //Units are Hz
    //Declare data about the refresh clock;
    private static System.Timers.Timer GraphicRefreshClock = new System.Timers.Timer();
    private const double GraphicRefreshClockRate = 60;  //Units are Hz = #refreshes per second

    // ========== Title Declarations ==========
    private Font style_of_message = new System.Drawing.Font("Arial",12,FontStyle.Bold);
    private String title = "Final Program by Spencer DeMera";
    private Brush writing_tool = new SolidBrush(System.Drawing.Color.Black);
    private Point title_location = new Point(formwidth - 630,10);

    // ========== Declare Buttons & Labels ==========
    //Declare buttons
    private Button pause_button = new Button();
    private Button start_button = new Button();
    private Button exit_button = new Button();
    private Point pause_location = new Point(50, 950);
    private Point start_location = new Point(50, 870);
    private Point exit_location = new Point(870, 950);
    //Declare inputareas and labels
    private Label RedxInputArea = new Label();
    private Label RedyInputArea = new Label();
    private Label RedX = new Label();
    private Label RedY = new Label();
    private Label RedBallLabel = new Label();
    private Label SuccessesNum = new Label();
    private Label AttemptsNum = new Label();
    private Label SuccessesInputArea = new Label();
    private Label AttemptsInputArea = new Label();
    
    // ========== Constructor ==========
    public RicochetInterface() {
        Text = "Ricochet Motion";
        System.Console.WriteLine("formwidth = {0}. formheight = {1}.",formwidth,formheight);
        //Set the initial size of this form
        Size = new Size(formwidth,formheight);
        //Set the background color of this form
        BackColor = Color.SlateGray;

        // ========== Sizes, Fonts, Locations ==========
        //Input Area Sizes
        RedxInputArea.Size = new Size(50, 40);
        RedyInputArea.Size = new Size(50, 40);
        RedBallLabel.Size = new Size(180, 30);
        RedX.Size = new Size(30, 30);
        RedY.Size = new Size(30, 30);
        SuccessesNum.Size = new Size(125, 30);
        AttemptsNum.Size = new Size(125, 30);
        SuccessesInputArea.Size = new Size(50, 40);
        AttemptsInputArea.Size = new Size(50, 40);

        //Input Area locations
        RedxInputArea.Location = new Point(230, 910);
        RedyInputArea.Location = new Point(320, 910);
        RedBallLabel.Location = new Point(200, 865);
        RedX.Location = new Point(200, 915);
        RedY.Location = new Point(290, 915);
        SuccessesNum.Location = new Point(500, 865);
        AttemptsNum.Location = new Point(645, 865);
        SuccessesInputArea.Location = new Point(540, 910);
        AttemptsInputArea.Location = new Point(685, 910);

        //Input Area text strings and fonts
        RedxInputArea.Text = "0";
        RedyInputArea.Text = "0";
        RedBallLabel.Text = "Red Ball Location";
        RedX.Text = "X = ";
        RedY.Text = "Y = ";
        SuccessesNum.Text = "Success #";
        AttemptsNum.Text = "Attempts #";
        SuccessesInputArea.Text = "0";
        AttemptsInputArea.Text = "0";

        RedBallLabel.Font = new Font("Arial", 10, FontStyle.Bold);
        RedxInputArea.Font = new Font("Arial", 10, FontStyle.Bold);
        RedyInputArea.Font = new Font("Arial", 10, FontStyle.Bold);
        RedX.Font = new Font("Arial", 10, FontStyle.Bold);
        RedY.Font = new Font("Arial", 10, FontStyle.Bold);
        SuccessesNum.Font = new Font("Arial", 10, FontStyle.Bold);
        AttemptsNum.Font = new Font("Arial", 10, FontStyle.Bold);
        SuccessesInputArea.Font = new Font("Arial", 10, FontStyle.Bold);
        AttemptsInputArea.Font = new Font("Arial", 10, FontStyle.Bold);
        
        // ========== Colors & Formatting ==========
        RedBallLabel.ForeColor = Color.White;
        RedBallLabel.BackColor = Color.RoyalBlue;
        RedBallLabel.TextAlign = ContentAlignment.MiddleCenter;

        RedxInputArea.ForeColor = Color.Black;
        RedxInputArea.BackColor = Color.White;
        RedxInputArea.TextAlign = ContentAlignment.MiddleCenter;
        RedyInputArea.ForeColor = Color.Black;
        RedyInputArea.BackColor = Color.White;
        RedyInputArea.TextAlign = ContentAlignment.MiddleCenter;

        RedX.ForeColor = Color.White;
        RedX.BackColor = Color.DimGray;
        RedX.TextAlign = ContentAlignment.MiddleCenter;
        RedY.ForeColor = Color.White;
        RedY.BackColor = Color.DimGray;
        RedY.TextAlign = ContentAlignment.MiddleCenter;

        SuccessesNum.ForeColor = Color.White;
        SuccessesNum.BackColor = Color.RoyalBlue;
        SuccessesNum.TextAlign = ContentAlignment.MiddleCenter;
        AttemptsNum.ForeColor = Color.White;
        AttemptsNum.BackColor = Color.RoyalBlue;
        AttemptsNum.TextAlign = ContentAlignment.MiddleCenter;
        SuccessesInputArea.ForeColor = Color.Black;
        SuccessesInputArea.BackColor = Color.White;
        SuccessesInputArea.TextAlign = ContentAlignment.MiddleCenter;
        AttemptsInputArea.ForeColor = Color.Black;
        AttemptsInputArea.BackColor = Color.White;
        AttemptsInputArea.TextAlign = ContentAlignment.MiddleCenter;

        // ========== Clock Formatting ==========
        //Set up the motion clock.  This clock controls the rate of update of the coordinates of the ball.
        MotionControlClock.Enabled = false;
        MotionControlClock.Elapsed += new ElapsedEventHandler(Update_ball_position);
        //Set up the refresh clock.
        GraphicRefreshClock.Enabled = false;  //Initially the clock controlling the rate of updating the display is stopped.
        GraphicRefreshClock.Elapsed += new ElapsedEventHandler(Update_display);  

        // ========== Button Initialization ==========
        //Set properties of the button (or maybe buttons)
        start_button.Text = "Start";
        start_button.Font = new Font("Arial", 10, FontStyle.Bold);
        start_button.Size = new Size(70,50);
        start_button.Location = start_location;
        start_button.ForeColor = Color.Black;
        start_button.BackColor = Color.DodgerBlue;
        start_button.TextAlign = ContentAlignment.MiddleCenter;
        Controls.Add(start_button);
        start_button.Click += new EventHandler(Start);
        // Set new Button
        pause_button.Text = "Pause";
        pause_button.Font = new Font("Arial", 10, FontStyle.Bold);
        pause_button.Size = new Size(70,50);
        pause_button.Location = pause_location;
        pause_button.ForeColor = Color.Black;
        pause_button.BackColor = Color.DodgerBlue;
        pause_button.TextAlign = ContentAlignment.MiddleCenter;
        Controls.Add(pause_button);
        pause_button.Click += new EventHandler(Pause);
        // Set exit Button
        exit_button.Text = "Quit";
        exit_button.Font = new Font("Arial", 10, FontStyle.Bold);
        exit_button.Size = new Size(70,50);
        exit_button.Location = exit_location;
        exit_button.ForeColor = Color.Black;
        exit_button.BackColor = Color.DodgerBlue;
        exit_button.TextAlign = ContentAlignment.MiddleCenter;
        Controls.Add(exit_button);
        exit_button.Click += new EventHandler(Exit);

        // ========== Set input areas ==========
        Controls.Add(RedxInputArea);
        Controls.Add(RedyInputArea);
        Controls.Add(RedBallLabel);
        Controls.Add(RedX);
        Controls.Add(RedY);
        Controls.Add(SuccessesNum);
        Controls.Add(AttemptsNum);
        Controls.Add(SuccessesInputArea);
        Controls.Add(AttemptsInputArea);

        RedxInputArea.Text = String.Format("{0:0}", RedCenterCoordX);
        RedyInputArea.Text = String.Format("{0:0}", RedCenterCoordY);

        //Open this user interface window in the center of the display.
        CenterToScreen();
    } //End of constructor

    // ========== OnPaint ==========
    protected override void OnPaint(PaintEventArgs ee) {
        Graphics graph = ee.Graphics;

        graph.FillRectangle(Brushes.SeaGreen, 0, 40, formwidth, graphicheight);
        graph.FillRectangle(Brushes.CornflowerBlue, 0, 0, formwidth, titleheight);
        graph.FillRectangle(Brushes.DimGray, 0, 840, formwidth, lowerpanelheight);
        graph.DrawString(title, style_of_message, writing_tool, title_location);

        graph.FillEllipse(Brushes.Crimson, (float)RedCenterCoordX, (float)RedCenterCoordY,
                            (float)(2.0*RedRadius), (float)(2.0*RedRadius));

        base.OnPaint(ee);
    } // OnPaint function

    // ========== Start Button ==========
    protected void Start (Object sender, EventArgs events) {
        MotionControlClock.Enabled = true; // Start clock ticking.
        GraphicRefreshClock.Enabled = true; // Start clock ticking.

        // ========== Angle & X/Y Coord Calculations ==========
        double RedAngle = rand.NextDouble() * 2.0 * Math.PI;
            
        RedDirectionX = (double)Math.Cos(RedAngle);
        RedDirectionY = (double)Math.Sin(RedAngle);

        // ========== Red Ball ==========
        RedPixPerSec = RedSpeed;
        RedPixPerTic = RedPixPerSec / MotionControlClockRate;
        RedHypotenuseSquared = RedDirectionX * RedDirectionX + RedDirectionY * RedDirectionY;
        RedHypotenuse = System.Math.Sqrt(RedHypotenuseSquared);
        RedDeltaX = RedPixPerTic * RedDirectionX / RedHypotenuse;
        RedDeltaY = RedPixPerTic * RedDirectionY / RedHypotenuse;

        System.Console.WriteLine("RedDeltaX: " + RedDeltaX.ToString() + " | RedDelatY: " + RedDeltaY.ToString());
        
        Invalidate();
    } // start function

    // ========== Clock Functions ==========
    protected void Update_display(System.Object sender, ElapsedEventArgs evt) {
        Invalidate();  //This creates an artificial event so that the graphic area will repaint itself.
        //System.Console.WriteLine("The motion clock ticked and the time is {0}", evt.SignalTime);  //Debug statement; remove it later.
        if (!MotionControlClock.Enabled) {
            GraphicRefreshClock.Enabled = false;
            System.Console.WriteLine("The graphical area is no longer refreshing.  You may close the window.");
        } // if
    } // Update_display function

    // ========== OnMouseDown override ==========
    protected override void OnMouseDown(MouseEventArgs ee) {
        mouseX = ee.X;
        mouseY = ee.Y;

        if ( ( (int)System.Math.Round(mouseX) <= (int)System.Math.Round(RedCenterCoordX + (2.0 * RedRadius)) ) && ( (int)System.Math.Round(mouseX) >= (int)System.Math.Round(RedCenterCoordX - (2.0 * RedRadius)) ) 
            && ( (int)System.Math.Round(mouseY) <= (int)System.Math.Round(RedCenterCoordY + (2.0 * RedRadius)) ) && ( (int)System.Math.Round(mouseY) >= (int)System.Math.Round(RedCenterCoordY - (2.0 * RedRadius)) ) ) {
            successes++;
            attempts++;
            RedSpeed *= 1.25;

            // ========== Angle & X/Y Coord Calculations ==========
            double RedAngle = rand.NextDouble() * 2.0 * Math.PI;
                
            RedDirectionX = (double)Math.Cos(RedAngle);
            RedDirectionY = (double)Math.Sin(RedAngle);

            // ========== Red Ball ==========
            RedPixPerSec = RedSpeed;
            RedPixPerTic = RedPixPerSec / MotionControlClockRate;
            RedHypotenuseSquared = RedDirectionX * RedDirectionX + RedDirectionY * RedDirectionY;
            RedHypotenuse = System.Math.Sqrt(RedHypotenuseSquared);
            RedDeltaX = RedPixPerTic * RedDirectionX / RedHypotenuse;
            RedDeltaY = RedPixPerTic * RedDirectionY / RedHypotenuse;
        } // if
        else if (!( ( (int)System.Math.Round(mouseX) <= (int)System.Math.Round(RedCenterCoordX + (2.0 * RedRadius)) ) && ( (int)System.Math.Round(mouseX) >= (int)System.Math.Round(RedCenterCoordX - (2.0 * RedRadius)) ) 
            && ( (int)System.Math.Round(mouseY) <= (int)System.Math.Round(RedCenterCoordY + (2.0 * RedRadius)) ) && ( (int)System.Math.Round(mouseY) >= (int)System.Math.Round(RedCenterCoordY - (2.0 * RedRadius)) ))) {
            attempts++;
        } // else if

        // ========== Check if Attempts cap reached ==========
        if (attempts >= 10) {
            MotionControlClock.Enabled = false;   //Stop clock ticking
            GraphicRefreshClock.Enabled = false;   //Stop clock ticking
                
            // reset position of ball
            RedCenterCoordX = 500.0;
            RedCenterCoordY = 400.0;
            successes = 0;
            attempts = 0;
            RedSpeed = 400.0;
        } // if

        // ========== Display Coordinate Outputs ==========
        RedxInputArea.Text = String.Format("{0:0}", RedCenterCoordX);
        RedyInputArea.Text = String.Format("{0:0}", RedCenterCoordY);
        SuccessesInputArea.Text = String.Format("{0:0}", successes);
        AttemptsInputArea.Text = String.Format("{0:0}", attempts);

        base.OnMouseDown(ee);
        Invalidate();
    } // OnMouseDown function

    // ========== Update Ball Positions ==========
    protected void Update_ball_position(System.Object sender, ElapsedEventArgs evt) {
        RedCenterCoordX += RedDeltaX;
        RedCenterCoordY -= RedDeltaY;

        // ========== Red Ball ==========
        // Right Wall
        if ((int)System.Math.Round(RedCenterCoordX + RedRadius) >= (formwidth - (2.0 * RedRadius))) {
            RedDeltaX = -RedDeltaX;
        } // if
        // Lower Wall
        if ((int)System.Math.Round(RedCenterCoordY + RedRadius) >= (graphicheight + (titleheight - RedRadius))) {
            RedDeltaY = -RedDeltaY;
        } // if
        // Left Wall
        if ((int)System.Math.Round(RedCenterCoordX + RedRadius) <= (1 + RedRadius)) {
            RedDeltaX = -RedDeltaX;
        } // if
        // Upper Wall
        if ((int)System.Math.Round(RedCenterCoordY + RedRadius) <= (titleheight + RedRadius)) {
            RedDeltaY = -RedDeltaY;
        } // if 

        // ========== Display Coordinate Outputs ==========
        RedxInputArea.Text = String.Format("{0:0}", RedCenterCoordX);
        RedyInputArea.Text = String.Format("{0:0}", RedCenterCoordY);

        Invalidate();
    } //End of method Update_ball_position

    // ========== New Button ==========
    protected void Pause (Object sender,EventArgs events) {
        // Created for testing but adds extra game functionality
        MotionControlClock.Enabled = false;   //Stop clock ticking
        GraphicRefreshClock.Enabled = false;   //Stop clock ticking

        Invalidate();
    } // End of New function

    // ========== Exit Button ==========
    protected void Exit (Object sender,EventArgs events) {
        System.Console.WriteLine("You have chosen to exit.");
        Close(); 
    } // End of Exit function
} // End of class RicochetInterface.cs

