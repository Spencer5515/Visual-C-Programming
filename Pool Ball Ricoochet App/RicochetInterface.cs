//****************************************************************************************************************************
//Program name: "Ricochet". This program takes user commands from a set of buttons and displays an animated ball that bounces   *
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
//  Date program began: 10-30-2020
//  Date of last update: 10-
//  Comments reorganized: 10-
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
//  Compile and Link: mcs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:RicochetInterface.dll -out:Ricochet.exe main.cs
//
//Execution: ./Skateboard.exe

// ========== Begin code area ==========

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

public class RicochetInterface : Form {
   //Declare data about the UI:
   private const int formwidth = 1000;    //Preferred size: 1600;
   private const int formheight = 1260;    //Preferred size: 1200;
   private const int titleheight = 40;
   private const int graphicheight = 800;
   private const int lowerpanelheight = formheight - titleheight - graphicheight;

   //Declare data about the ball:
   private double speed = 0.0;
   private double v = 0.0;
   private double w = 0.0;
   private double hypotenuse_squared = 0.0;
   private double hypotenuse = 0.0;
   private const double ball_radius = 8.5;
   private double ball_linear_speed_pix_per_sec;
   private double ball_linear_speed_pix_per_tic;
   private double ball_direction_x;
   private double ball_direction_y;
   private double ball_delta_x;
   private double ball_delta_y;
   private const double ball_center_initial_coord_x = (double)formwidth*0.65;
   private const double ball_center_initial_coord_y = (double)graphicheight/2.0 + titleheight;
   private double ball_center_current_coord_x;
   private double ball_center_current_coord_y;
   private double ball_upper_left_current_coord_x;
   private double ball_upper_left_current_coord_y;

   //Declare data about the motion clock:
   private static System.Timers.Timer ball_motion_control_clock = new System.Timers.Timer();
   private const double ball_motion_control_clock_rate = 43.5;  //Units are Hz

   //Declare data about the refresh clock;
   private static System.Timers.Timer graphic_area_refresh_clock = new System.Timers.Timer();
   private const double graphic_refresh_rate = 23.3;  //Units are Hz = #refreshes per second

   //Declare data about title message
   private Font style_of_message = new System.Drawing.Font("Arial",10,FontStyle.Regular);
   private String title = "Ricochet Ball by Spencer DeMera";
   private Brush writing_tool = new SolidBrush(System.Drawing.Color.Black);
   private Point title_location = new Point(formwidth - 600,10);

   //Declare buttons
   private Button new_button = new Button();
   private Button start_button = new Button();
   private Button exit_button = new Button();
   private Panel controlpanel = new Panel();

   private Point new_location = new Point(50, 870);
   private Point start_location = new Point(50, 970);
   private Point exit_location = new Point(870, 970);

   //Declare inputareas and labels
   private TextBox speedInputArea = new TextBox();
   private TextBox directionInputArea = new TextBox();
   private TextBox xInputArea = new TextBox();
   private TextBox yInputArea = new TextBox();
   private Label speedInputLabel = new Label();
   private Label directionInputLabel = new Label();
   private Label CoordsInputLabel = new Label();
   private Label xInputLabel = new Label();
   private Label yInputLabel = new Label();
   
   public RicochetInterface() {
//    public RicochetInterface(double speed, double v, double w) {  //The constructor of this class
      //Set the title of this form.
      Text = "Ricochet Motion";
      System.Console.WriteLine("formwidth = {0}. formheight = {1}.",formwidth,formheight);
      //Set the initial size of this form
      Size = new Size(formwidth,formheight);
      //Set the background color of this form
      BackColor = Color.Green;

      //Input Area Sizes
      speedInputArea.Size = new Size(50, 50);
      directionInputArea.Size = new Size(50, 50);
      xInputArea.Size = new Size(50, 50);
      yInputArea.Size = new Size(40, 50);
      speedInputLabel.Size = new Size(180, 30);
      directionInputLabel.Size = new Size(180, 30);
      CoordsInputLabel.Size = new Size(200, 30);
      xInputLabel.Size = new Size(40, 30);
      yInputLabel.Size = new Size(40, 30);
      controlpanel.Size = new Size(1000, 420);

      //Input Area text strings and fonts
      speedInputArea.Text = "0";
      directionInputArea.Text = "0";
      xInputArea.Text = "0";
      yInputArea.Text = "0";
      speedInputLabel.Text = "Enter Speed (pixel/second)";
      directionInputLabel.Text = "Enter Direction (degrees)";
      CoordsInputLabel.Text = "Coordinates of center of ball";
      xInputLabel.Text = "X = ";
      yInputLabel.Text = "Y = ";

      speedInputLabel.Font = new Font("Arial", 10, FontStyle.Regular);
      directionInputLabel.Font = new Font("Arial", 10, FontStyle.Regular);
      CoordsInputLabel.Font = new Font("Arial", 10, FontStyle.Regular);
      xInputLabel.Font = new Font("Arial", 15, FontStyle.Regular);
      yInputLabel.Font = new Font("Arial", 15, FontStyle.Regular);

      //Input Area locations
      speedInputArea.Location = new Point(550, 870);
      directionInputArea.Location = new Point(870, 870);
      xInputArea.Location = new Point(600, 970);
      yInputArea.Location = new Point(700, 970);

      speedInputLabel.Location = new Point(350, 865);
      directionInputLabel.Location = new Point(670, 865);
      CoordsInputLabel.Location = new Point(600, 930);
      xInputLabel.Location = new Point(550, 960);
      yInputLabel.Location = new Point(650, 960);

      //Set up the motion clock.  This clock controls the rate of update of the coordinates of the ball.
      ball_motion_control_clock.Enabled = false;
      //Assign a handler to this clock.
      ball_motion_control_clock.Elapsed += new ElapsedEventHandler(Update_ball_position);

      //Set up the refresh clock.
      graphic_area_refresh_clock.Enabled = false;  //Initially the clock controlling the rate of updating the display is stopped.
      //Assign a handler to this clock.
      graphic_area_refresh_clock.Elapsed += new ElapsedEventHandler(Update_display);  

      // Set new Button
      new_button.Text = "New";
      new_button.Size = new Size(60,30);
      new_button.Location = new_location;
      new_button.BackColor = Color.DeepSkyBlue;
      Controls.Add(new_button);
      new_button.Click += new EventHandler(New);

      //Set properties of the button (or maybe buttons)
      start_button.Text = "Start";
      start_button.Size = new Size(60,30);
      start_button.Location = start_location;
      start_button.BackColor = Color.DeepSkyBlue;
      Controls.Add(start_button);
      start_button.Click += new EventHandler(All_systems_go);

      // Set exit Button
      exit_button.Text = "Quit";
      exit_button.Size = new Size(60,30);
      exit_button.Location = exit_location;
      exit_button.BackColor = Color.DeepSkyBlue;
      Controls.Add(exit_button);
      exit_button.Click += new EventHandler(Exit);

      //Set input areas
    //   Controls.Add(controlpanel);
      Controls.Add(speedInputArea);
      Controls.Add(speedInputLabel);
      Controls.Add(directionInputArea);
      Controls.Add(directionInputLabel);
      Controls.Add(CoordsInputLabel);
      Controls.Add(xInputArea);
      Controls.Add(xInputLabel);
      Controls.Add(yInputArea);
      Controls.Add(yInputLabel);

      //Open this user interface window in the center of the display.
      CenterToScreen();
   } //End of constructor

   protected override void OnPaint(PaintEventArgs ee) {
      Graphics graph = ee.Graphics;

      graph.FillRectangle(Brushes.LightSkyBlue, 0, 0, formwidth, titleheight);
      graph.FillRectangle(Brushes.LightCoral, 0, 840, formwidth, lowerpanelheight);
      graph.DrawString(title,style_of_message,writing_tool,title_location);

      ball_upper_left_current_coord_x = ball_center_current_coord_x - ball_radius;
      ball_upper_left_current_coord_y = ball_center_current_coord_y - ball_radius;

      graph.FillEllipse(Brushes.Crimson,(int)ball_upper_left_current_coord_x,(int)ball_upper_left_current_coord_y,
                        (float)(2.0*ball_radius),(float)(2.0*ball_radius));
      //The next statement calls the method with the same name located in the super class.
      base.OnPaint(ee);
   }

   protected void All_systems_go(Object sender,EventArgs events) {
    //The refreshclock is started.
    Start_graphic_clock(graphic_refresh_rate);
    //The motion clock is started.
    Start_ball_clock(ball_motion_control_clock_rate);

    double sdirection;

    try {
        speed = double.Parse(speedInputArea.Text);
        sdirection = double.Parse(directionInputArea.Text);
        v = (double)Math.Cos(sdirection);
        w = (double)Math.Sin(sdirection);
        ball_upper_left_current_coord_x = 400;
        ball_upper_left_current_coord_y = 400;
    } // try
    catch (FormatException malformed_input) {
        System.Console.WriteLine("Non-integer input received. Please try again.\n{0}",malformed_input.Message);
    } // catch
    //Save the two incoming parameters into local variables.
      ball_linear_speed_pix_per_sec = speed;
      ball_direction_x = v;
      ball_direction_y = w;

      //Compute fixed values needed for motion in a straight line; some trigonometry is required.
      //To understand why it works you should draw some right triangles, and the math will be more clear.
      ball_linear_speed_pix_per_tic = ball_linear_speed_pix_per_sec/ball_motion_control_clock_rate;
      hypotenuse_squared = ball_direction_x*ball_direction_x + ball_direction_y*ball_direction_y;
      hypotenuse = System.Math.Sqrt(hypotenuse_squared);
      ball_delta_x = ball_linear_speed_pix_per_tic * ball_direction_x / hypotenuse;
      ball_delta_y = ball_linear_speed_pix_per_tic * ball_direction_y / hypotenuse;

      //Set starting values for the ball
      ball_center_current_coord_x = ball_center_initial_coord_x;
      ball_center_current_coord_y = ball_center_initial_coord_y;
      System.Console.WriteLine("Initial coordinates: ball_center_current_coord_x = {0}. ball_center_current_coord_y = {1}.",
                               ball_center_current_coord_x,ball_center_current_coord_y);
      Invalidate();
   } // start function

   protected void Start_graphic_clock(double refresh_rate) {
       double actual_refresh_rate = 1.0;  //Minimum refresh rate is 1 Hz to avoid a potential division by a number close to zero
       double elapsed_time_between_tics;
       if (refresh_rate > actual_refresh_rate) {
           actual_refresh_rate = refresh_rate;
       } // if
       elapsed_time_between_tics = 1000.0/actual_refresh_rate;  //elapsedtimebetweentics has units milliseconds.
       graphic_area_refresh_clock.Interval = (int)System.Math.Round(elapsed_time_between_tics);
       graphic_area_refresh_clock.Enabled = true;  //Start clock ticking.
   }

   protected void Start_ball_clock(double update_rate) {
       double elapsed_time_between_ball_moves;

       if(update_rate < 1.0) {
           update_rate = 1.0;  //This program does not allow updates slower than 1 Hz.
       } // if
       elapsed_time_between_ball_moves = 1000.0/update_rate;  //1000.0ms = 1second.  
       //The variable elapsed_time_between_ball_moves has units "milliseconds".
       ball_motion_control_clock.Interval = (int)System.Math.Round(elapsed_time_between_ball_moves);
       ball_motion_control_clock.Enabled = true;   //Start clock ticking.
   }

   protected void Update_display(System.Object sender, ElapsedEventArgs evt) {
      Invalidate();  //This creates an artificial event so that the graphic area will repaint itself.
      //System.Console.WriteLine("The motion clock ticked and the time is {0}", evt.SignalTime);  //Debug statement; remove it later.
      if (!ball_motion_control_clock.Enabled) {
           graphic_area_refresh_clock.Enabled = false;
           System.Console.WriteLine("The graphical area is no longer refreshing.  You may close the window.");
      } // if
   }

   protected void Update_ball_position(System.Object sender, ElapsedEventArgs evt) {
      ball_center_current_coord_x += ball_delta_x;
      ball_center_current_coord_y -= ball_delta_y;  //The minus sign is due to the upside down nature of the C# system.
      //System.Console.WriteLine("The motion clock ticked and the time is {0}", evt.SignalTime);//Debug statement; remove later.

      //Determine if the ball has made a collision with the right wall.
      if ((int)System.Math.Round(ball_center_current_coord_x + ball_radius) >= (formwidth - ball_radius)) {
            ball_delta_x = -ball_delta_x; // deltaX == negative
      } // if
      //Determine if the ball has made a collision with the lower wall
      if ((int)System.Math.Round(ball_center_current_coord_y + ball_radius) >= ((titleheight + graphicheight) + ball_radius)) {
            ball_delta_y = -ball_delta_y; // deltaY == positive
      } // if
      //Determine if the ball has made a collision with the left wall
      if ((int)System.Math.Round(ball_center_current_coord_x + ball_radius) <= (1 + ball_radius)) {
            ball_delta_x = -ball_delta_x; // deltaX == positive
      } // if
      //Determine if the ball has made a collision with the upper wall
      if ((int)System.Math.Round(ball_center_current_coord_y + ball_radius) <= (titleheight + ball_radius)) {
          ball_delta_y = -ball_delta_y; // deltaY == negative
      } // if

      xInputArea.Text = ball_upper_left_current_coord_x.ToString();
      yInputArea.Text = ball_upper_left_current_coord_y.ToString();

      //The next statement checks to determine if the ball has traveled beyond the four boundaries.  The statement may be
      //removed after the ricochet feature has been implemented by a 223N student.
      if ((int)System.Math.Round(ball_center_current_coord_y - ball_radius) >= titleheight + graphicheight) {
            ball_motion_control_clock.Enabled = false;
            graphic_area_refresh_clock.Enabled = false;
            System.Console.WriteLine("The clock controlling the ball has stopped.");
      } // if
   }//End of method Update_ball_position

   protected void New(Object sender,EventArgs events) {
        speedInputArea.Text = "0";
        directionInputArea.Text = "0";
        xInputArea.Text = "0";
        yInputArea.Text = "0";

        ball_motion_control_clock.Enabled = false;   //Stop clock ticking.
        graphic_area_refresh_clock.Enabled = false;   //Stop clock ticking.
        System.Console.WriteLine("New instance created.");
   } // End of New function

   protected void Exit(Object sender,EventArgs events) {
        System.Console.WriteLine("You have chosen to exit.");
        Close(); 
   } // End of Exit function
}//End of class Ricochet_interface_form.cs

