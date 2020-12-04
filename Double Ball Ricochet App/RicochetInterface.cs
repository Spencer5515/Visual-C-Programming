//****************************************************************************************************************************
//Program name: "Ricochet". This program takes user commands from a set of buttons and displays two animated balls that bounce   *
//around a rectangular area.  Copyright (C) 2020  Spencer DeMera                                                         *  
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
//  Program name: Ricochet
//  Programming language: C#
//  Date program began: 11-30-2020
//  Date of last update: 12-7-2020
//  Comments reorganized: 12-7-2020
//  Files in the program: main.cs, boardInterface.cs run.sh
//
//Purpose
//  Display a stand-in skateboarder moving around a triangular path.
//  For learning purposes: Demonstrate use of hybrid programming concepts in a theoretical setting
//
//This file
//  File name: RicochetInterfaceusing System;
//  Language: C#
//  Optimal print specification: 7 point font, monospace, 136 columns, 8½x11 paper
//  Compile: mcs -t:library -r:System.Windows.Forms.dll -r:System.Drawing.dll -out:RicochetInterface.dll RicochetInterface.cs
//  Compile and Link: mcs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:RicochetInterface.dll -out:balls.exe main.cs
//
//Execution: ./balls.exe

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
    private double BlueSpeed = 800.0;
    private double RedHypotenuseSquared = 0.0;
    private double RedHypotenuse = 0.0;
    private double BlueHypotenuseSquared = 0.0;
    private double BlueHypotenuse = 0.0;
    private const double RedRadius = 22.0;
    private const double BlueRadius = 13;
    private double Distance = 0.0;
    private double RedPixPerSec;
    private double BluePixPerSec;
    private double RedPixPerTic;
    private double BluePixPerTic;
    private double RedDirectionX;
    private double RedDirectionY;
    private double BlueDirectionX;
    private double BlueDirectionY;
    private double RedDeltaX;
    private double RedDeltaY;
    private double BlueDeltaX;
    private double BlueDeltaY;
    private double RedCenterCoordX = 200.0;
    private double RedCenterCoordY = 300.0;
    private double BlueCenterCoordX = 500.0;
    private double BlueCenterCoordY = 700.0;
    private bool GameEnd = false;
    Random rand = new Random();

    // ========== Timers ==========
    //Declare data about the motion clock:
    private static System.Timers.Timer MotionControlClock = new System.Timers.Timer();
    private const double MotionControlClockRate = 60;  //Units are Hz
    //Declare data about the refresh clock;
    private static System.Timers.Timer GraphicRefreshClock = new System.Timers.Timer();
    private const double GraphicRefreshClockRate = 25;  //Units are Hz = #refreshes per second

    // ========== Title Declarations ==========
    private Font style_of_message = new System.Drawing.Font("Arial",12,FontStyle.Bold);
    private String title = "Double Ricochet Ball by Spencer DeMera";
    private Brush writing_tool = new SolidBrush(System.Drawing.Color.Black);
    private Point title_location = new Point(formwidth - 630,10);

    // ========== Declare Buttons & Labels ==========
    //Declare buttons
    private Button new_button = new Button();
    private Button start_button = new Button();
    private Button exit_button = new Button();
    private Point new_location = new Point(50, 870);
    private Point start_location = new Point(50, 950);
    private Point exit_location = new Point(870, 950);
    //Declare inputareas and labels
    private Label RedxInputArea = new Label();
    private Label RedyInputArea = new Label();
    private Label BluexInputArea = new Label();
    private Label BlueyInputArea = new Label();
    private Label RedX = new Label();
    private Label RedY = new Label();
    private Label BlueX = new Label();
    private Label BlueY = new Label();
    private Label RedBallLabel = new Label();
    private Label BlueBallLabel = new Label();
    
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
        BluexInputArea.Size = new Size(50, 40);
        BlueyInputArea.Size = new Size(50, 40);
        RedBallLabel.Size = new Size(180, 30);
        BlueBallLabel.Size = new Size(180, 30);
        RedX.Size = new Size(30, 30);
        RedY.Size = new Size(30, 30);
        BlueX.Size = new Size(30, 30);
        BlueY.Size = new Size(30, 30);

        //Input Area locations
        RedxInputArea.Location = new Point(285, 910);
        RedyInputArea.Location = new Point(370, 910);
        BluexInputArea.Location = new Point(605, 910);
        BlueyInputArea.Location = new Point(690, 910);
        RedBallLabel.Location = new Point(250, 865);
        BlueBallLabel.Location = new Point(570, 865);
        RedX.Location = new Point(255, 915);
        RedY.Location = new Point(340, 915);
        BlueX.Location = new Point(575, 915);
        BlueY.Location = new Point(660, 915);

        //Input Area text strings and fonts
        RedxInputArea.Text = "0";
        RedyInputArea.Text = "0";
        BluexInputArea.Text = "0";
        BlueyInputArea.Text = "0";
        RedBallLabel.Text = "Red Ball Location";
        BlueBallLabel.Text = "Blue Ball Location";
        RedX.Text = "X = ";
        RedY.Text = "Y = ";
        BlueX.Text = "X = ";
        BlueY.Text = "Y = ";

        RedBallLabel.Font = new Font("Arial", 10, FontStyle.Bold);
        BlueBallLabel.Font = new Font("Arial", 10, FontStyle.Bold);
        RedxInputArea.Font = new Font("Arial", 10, FontStyle.Bold);
        RedyInputArea.Font = new Font("Arial", 10, FontStyle.Bold);
        BluexInputArea.Font = new Font("Arial", 10, FontStyle.Bold);
        BlueyInputArea.Font = new Font("Arial", 10, FontStyle.Bold);
        RedX.Font = new Font("Arial", 10, FontStyle.Bold);
        RedY.Font = new Font("Arial", 10, FontStyle.Bold);
        BlueX.Font = new Font("Arial", 10, FontStyle.Bold);
        BlueY.Font = new Font("Arial", 10, FontStyle.Bold);
        
        // ========== Colors & Formatting ==========
        RedBallLabel.ForeColor = Color.Black;
        BlueBallLabel.ForeColor = Color.Black;
        RedBallLabel.BackColor = Color.DeepSkyBlue;
        BlueBallLabel.BackColor = Color.DeepSkyBlue;
        RedBallLabel.TextAlign = ContentAlignment.MiddleCenter;
        BlueBallLabel.TextAlign = ContentAlignment.MiddleCenter;

        RedxInputArea.ForeColor = Color.Black;
        RedxInputArea.BackColor = Color.White;
        RedxInputArea.TextAlign = ContentAlignment.MiddleCenter;
        RedyInputArea.ForeColor = Color.Black;
        RedyInputArea.BackColor = Color.White;
        RedyInputArea.TextAlign = ContentAlignment.MiddleCenter;
        
        BluexInputArea.ForeColor = Color.Black;
        BluexInputArea.BackColor = Color.White;
        BluexInputArea.TextAlign = ContentAlignment.MiddleCenter;
        BlueyInputArea.ForeColor = Color.Black;
        BlueyInputArea.BackColor = Color.White;
        BlueyInputArea.TextAlign = ContentAlignment.MiddleCenter;

        RedX.ForeColor = Color.White;
        RedX.BackColor = Color.SlateGray;
        RedX.TextAlign = ContentAlignment.MiddleCenter;
        RedY.ForeColor = Color.White;
        RedY.BackColor = Color.SlateGray;
        RedY.TextAlign = ContentAlignment.MiddleCenter;

        BlueX.ForeColor = Color.White;
        BlueX.BackColor = Color.SlateGray;
        BlueX.TextAlign = ContentAlignment.MiddleCenter;
        BlueY.ForeColor = Color.White;
        BlueY.BackColor = Color.SlateGray;
        BlueY.TextAlign = ContentAlignment.MiddleCenter;

        // ========== Clock Formatting ==========
        //Set up the motion clock.  This clock controls the rate of update of the coordinates of the ball.
        MotionControlClock.Enabled = false;
        MotionControlClock.Elapsed += new ElapsedEventHandler(Update_ball_position);
        //Set up the refresh clock.
        GraphicRefreshClock.Enabled = false;  //Initially the clock controlling the rate of updating the display is stopped.
        GraphicRefreshClock.Elapsed += new ElapsedEventHandler(Update_display);  

        // ========== Button Initialization ==========
        // Set new Button
        new_button.Text = "New";
        new_button.Font = new Font("Arial", 10, FontStyle.Bold);
        new_button.Size = new Size(70,50);
        new_button.Location = new_location;
        new_button.ForeColor = Color.White;
        new_button.BackColor = Color.SlateBlue;
        new_button.TextAlign = ContentAlignment.MiddleCenter;
        Controls.Add(new_button);
        new_button.Click += new EventHandler(New);
        //Set properties of the button (or maybe buttons)
        start_button.Text = "Start";
        start_button.Font = new Font("Arial", 10, FontStyle.Bold);
        start_button.Size = new Size(70,50);
        start_button.Location = start_location;
        start_button.ForeColor = Color.White;
        start_button.BackColor = Color.SlateBlue;
        start_button.TextAlign = ContentAlignment.MiddleCenter;
        Controls.Add(start_button);
        start_button.Click += new EventHandler(Start);
        // Set exit Button
        exit_button.Text = "Quit";
        exit_button.Font = new Font("Arial", 10, FontStyle.Bold);
        exit_button.Size = new Size(70,50);
        exit_button.Location = exit_location;
        exit_button.ForeColor = Color.White;
        exit_button.BackColor = Color.SlateBlue;
        exit_button.TextAlign = ContentAlignment.MiddleCenter;
        Controls.Add(exit_button);
        exit_button.Click += new EventHandler(Exit);

        // ========== Set input areas ==========
        //Controls.Add(controlpanel);
        Controls.Add(RedxInputArea);
        Controls.Add(RedyInputArea);
        Controls.Add(BluexInputArea);
        Controls.Add(BlueyInputArea);
        Controls.Add(RedBallLabel);
        Controls.Add(BlueBallLabel);
        Controls.Add(RedX);
        Controls.Add(RedY);
        Controls.Add(BlueX);
        Controls.Add(BlueY);

        RedxInputArea.Text = String.Format("{0:0}", RedCenterCoordX);
        RedyInputArea.Text = String.Format("{0:0}", RedCenterCoordY);
        BluexInputArea.Text = String.Format("{0:0}", BlueCenterCoordX);
        BlueyInputArea.Text = String.Format("{0:0}", BlueCenterCoordY);

        //Open this user interface window in the center of the display.
        CenterToScreen();
    } //End of constructor

    // ========== OnPaint ==========
    protected override void OnPaint(PaintEventArgs ee) {
        Graphics graph = ee.Graphics;

        graph.FillRectangle(Brushes.SeaGreen, 0, 40, formwidth, graphicheight);
        graph.FillRectangle(Brushes.LightCoral, 0, 0, formwidth, titleheight);
        graph.FillRectangle(Brushes.SlateGray, 0, 840, formwidth, lowerpanelheight);
        graph.DrawString(title, style_of_message, writing_tool, title_location);

        graph.FillEllipse(Brushes.Crimson, (float)RedCenterCoordX, (float)RedCenterCoordY,
                            (float)(2.0*RedRadius), (float)(2.0*RedRadius));
        graph.FillEllipse(Brushes.MediumBlue, (float)BlueCenterCoordX, (float)BlueCenterCoordY,
                            (float)(2.0*BlueRadius), (float)(2.0*BlueRadius));

        base.OnPaint(ee);
    } // OnPaint function

    // ========== Start Button ==========
    protected void Start (Object sender, EventArgs events) {
        MotionControlClock.Enabled = true; // Start clock ticking.
        GraphicRefreshClock.Enabled = true; // Start clock ticking.

        // ========== Angle & X/Y Coord Calculations ==========
        double RedAngle = rand.NextDouble() * 2.0 * Math.PI;
        double BlueAngle = rand.NextDouble() * 2.0 * Math.PI;
        
        RedDirectionX = (double)Math.Cos(RedAngle);
        RedDirectionY = (double)Math.Sin(RedAngle);
        BlueDirectionX = (double)Math.Cos(BlueAngle);
        BlueDirectionY = (double)Math.Sin(BlueAngle);

        // ========== Red Ball ==========
        RedPixPerSec = RedSpeed;
        RedPixPerTic = RedPixPerSec / MotionControlClockRate;
        RedHypotenuseSquared = RedDirectionX * RedDirectionX + RedDirectionY * RedDirectionY;
        RedHypotenuse = System.Math.Sqrt(RedHypotenuseSquared);
        RedDeltaX = RedPixPerTic * RedDirectionX / RedHypotenuse;
        RedDeltaY = RedPixPerTic * RedDirectionY / RedHypotenuse;

        // ========== Blue Ball ==========
        BluePixPerSec = BlueSpeed;
        BluePixPerTic = BluePixPerSec / MotionControlClockRate;
        BlueHypotenuseSquared = BlueDirectionX * BlueDirectionX + BlueDirectionY * BlueDirectionY;
        BlueHypotenuse = System.Math.Sqrt(BlueHypotenuseSquared);
        BlueDeltaX = BluePixPerTic * BlueDirectionX / BlueHypotenuse;
        BlueDeltaY = BluePixPerTic * BlueDirectionY / BlueHypotenuse;

        System.Console.WriteLine("RedDeltaX: " + RedDeltaX.ToString() + " | RedDelatY: " + RedDeltaY.ToString());
        System.Console.WriteLine("BlueDeltaX: " + BlueDeltaX.ToString() + " | BlueDeltaY: " + BlueDeltaY.ToString());

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

    // ========== Update Ball Positions ==========
    protected void Update_ball_position(System.Object sender, ElapsedEventArgs evt) {
        RedCenterCoordX += RedDeltaX;
        RedCenterCoordY -= RedDeltaY;
        BlueCenterCoordX += BlueDeltaX;
        BlueCenterCoordY -= BlueDeltaY;

        // ========== Red Ball ==========
        // Right Wall
        if ((int)System.Math.Round(RedCenterCoordX + RedRadius) >= (formwidth - (2.0 *RedRadius))) {
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

        // ========== Blue Ball ==========
        // Right Wall
        if ((int)System.Math.Round(BlueCenterCoordX + BlueRadius) >= (formwidth - (2.0 * BlueRadius))) {
            BlueDeltaX = -BlueDeltaX;
        } // if
        // Lower Wall
        if ((int)System.Math.Round(BlueCenterCoordY + BlueRadius) >= (graphicheight + (titleheight - BlueRadius))) {
            BlueDeltaY = -BlueDeltaY;
        } // if
        // Left Wall
        if ((int)System.Math.Round(BlueCenterCoordX + BlueRadius) <= (1 + BlueRadius)) {
            BlueDeltaX = -BlueDeltaX;
        } // if
        // Upper Wall
        if ((int)System.Math.Round(BlueCenterCoordY + BlueRadius) <= (titleheight + BlueRadius)) {
            BlueDeltaY = -BlueDeltaY;
        } // if

        // ========== Red - Blue Collisions ==========
        Distance = System.Math.Sqrt((RedCenterCoordX - BlueCenterCoordX) * (RedCenterCoordX - BlueCenterCoordX)
                + (RedCenterCoordY - BlueCenterCoordY) * (RedCenterCoordY - BlueCenterCoordY));

        if (Distance <= RedRadius + BlueRadius) {
            MotionControlClock.Enabled = false;   //Stop clock ticking
            GraphicRefreshClock.Enabled = false;   //Stop clock ticking
            GameEnd = true;
        } // if

        // ========== Display Coordinate Outputs ==========
        RedxInputArea.Text = String.Format("{0:0}", RedCenterCoordX);
        RedyInputArea.Text = String.Format("{0:0}", RedCenterCoordY);
        BluexInputArea.Text = String.Format("{0:0}", BlueCenterCoordX);
        BlueyInputArea.Text = String.Format("{0:0}", BlueCenterCoordY);

        Invalidate();
    } //End of method Update_ball_position

    // ========== New Button ==========
    protected void New (Object sender,EventArgs events) {
        // Created for testing but adds extra game functionality
        if (GameEnd == false) {
            // ========== Display Coordinate Outputs ==========
            RedxInputArea.Text = String.Format("{0:0}", RedCenterCoordX);
            RedyInputArea.Text = String.Format("{0:0}", RedCenterCoordY);
            BluexInputArea.Text = String.Format("{0:0}", BlueCenterCoordX);
            BlueyInputArea.Text = String.Format("{0:0}", BlueCenterCoordY);

            MotionControlClock.Enabled = false;   //Stop clock ticking
            GraphicRefreshClock.Enabled = false;   //Stop clock ticking
            System.Console.WriteLine("New Angles Calculated.");
        } // if
        // Designed functionality
        else if (GameEnd == true) {
            RedCenterCoordX = 200.0;
            RedCenterCoordY = 300.0;
            BlueCenterCoordX = 500.0;
            BlueCenterCoordY = 700.0;

            // ========== Display Coordinate Outputs ==========
            RedxInputArea.Text = String.Format("{0:0}", RedCenterCoordX);
            RedyInputArea.Text = String.Format("{0:0}", RedCenterCoordY);
            BluexInputArea.Text = String.Format("{0:0}", BlueCenterCoordX);
            BlueyInputArea.Text = String.Format("{0:0}", BlueCenterCoordY);

            MotionControlClock.Enabled = false;   //Stop clock ticking
            GraphicRefreshClock.Enabled = false;   //Stop clock ticking
            System.Console.WriteLine("New instance created.");
            GameEnd = false;
        } // else if
        Invalidate();
    } // End of New function

    // ========== Exit Button ==========
    protected void Exit (Object sender,EventArgs events) {
        System.Console.WriteLine("You have chosen to exit.");
        Close(); 
    } // End of Exit function
} // End of class RicochetInterface.cs
