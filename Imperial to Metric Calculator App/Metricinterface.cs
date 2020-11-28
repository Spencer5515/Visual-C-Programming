//****************************************************************************************************************************
//Program name: "Imperial to Metric Conversion Caclulator".  This programs accepts a non-negative integer from the user and     *
//then outputs the Metric equivalent of that integer.                                                           *
//Copyright (C) 2020  Spencer DeMera                                                                                        *
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License  *
//version 3 as published by the Free Software Foundation.                                                                    *
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied         *
//warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.     *
//A copy of the GNU General Public License v3 is available here:  <https://www.gnu.org/licenses/>.                           *
//****************************************************************************************************************************

// Author: Spencer DeMera
// Course ID: CPSC 223N-01
// Assignment Number: 1
// Due Date: 10/10/2020
// Purpose: Homework && Extra Credit

using System;
using System.Drawing;
using System.Windows.Forms;

public class Metricinterface: Form {
  private Label welcome = new Label();
  private Label author = new Label();
  private Label sequencemessage = new Label();
  private TextBox sequenceinputarea = new TextBox();
  private Label outputinfo = new Label();
  private Button computebutton = new Button();
  private Button clearbutton = new Button();
  private Button exitbutton = new Button();
  private Panel headerpanel = new Panel();
  private Panel displaypanel = new Panel();
  private Panel controlpanel = new Panel();
  private Size maxConverterinterfacesize = new Size(1024,800);
  private Size minConverterinterfacesize = new Size(1024,800);

 public Metricinterface() { //Constructor
    //Set the size of the user interface box.
    MaximumSize = maxConverterinterfacesize;
    MinimumSize = minConverterinterfacesize;
    //Initialize text strings
    Text = "Imperial to Metric Converter";
    welcome.Text = "Welcome to Imperial to Metric Converter";
    author.Text = "Author: Spencer DeMera";
    sequencemessage.Text = "Enter inches:";
    sequenceinputarea.Text = "Enter distance";
    outputinfo.Text = "The metric equivalent will be displayed here: ";
    computebutton.Text = "Compute";
    clearbutton.Text = "Clear";
    exitbutton.Text = "Exit";

    //Set sizes
    Size = new Size(400,240);
    welcome.Size = new Size(800,44);
    author.Size = new Size(320,34);
    sequencemessage.Size = new Size(400,36);
    sequenceinputarea.Size = new Size(200,30);
    outputinfo.Size = new Size(900,80);   //This label has a large height to accommodate 2 lines output text.
    computebutton.Size = new Size(120,60);
    clearbutton.Size = new Size(120,60);
    exitbutton.Size = new Size(120,60);
    headerpanel.Size = new Size(1024,200);
    displaypanel.Size = new Size(1024,400);
    controlpanel.Size = new Size(1024,200);

    //Set colors
    headerpanel.BackColor = Color.LightGreen;
    displaypanel.BackColor = Color.LightCoral;
    controlpanel.BackColor = Color.LightSkyBlue;
    computebutton.BackColor = Color.Aquamarine;
    clearbutton.BackColor = Color.Aquamarine;
    exitbutton.BackColor = Color.Aquamarine;

    //Set fonts
    welcome.Font = new Font("Arial",33,FontStyle.Bold);
    author.Font = new Font("Arial",26,FontStyle.Regular);
    sequencemessage.Font = new Font("Arial",26,FontStyle.Regular);
    sequenceinputarea.Font = new Font("Arial",19,FontStyle.Regular);
    outputinfo.Font = new Font("Arial",26,FontStyle.Regular);
    computebutton.Font = new Font("Liberation Serif",15,FontStyle.Regular);
    clearbutton.Font = new Font("Liberation Serif",15,FontStyle.Regular);
    exitbutton.Font = new Font("Liberation Serif",15,FontStyle.Regular);

    //Set locations
    headerpanel.Location = new Point(0,0);
    welcome.Location = new Point(125,26);
    author.Location = new Point(330,100);
    sequencemessage.Location = new Point(100,60);
    sequenceinputarea.Location = new Point(600,60);
    outputinfo.Location = new Point(100,200);
    computebutton.Location = new Point(200,50);
    clearbutton.Location = new Point(450,50);
    exitbutton.Location = new Point(720,50);
    headerpanel.Location = new Point(0,0);
    displaypanel.Location = new Point(0,200);
    controlpanel.Location = new Point(0,600);

    //Associate the Compute button with the Enter key of the keyboard
    AcceptButton = computebutton;

    //Add controls to the form
    Controls.Add(headerpanel);
    headerpanel.Controls.Add(welcome);
    headerpanel.Controls.Add(author);
    Controls.Add(displaypanel);
    displaypanel.Controls.Add(sequencemessage);
    displaypanel.Controls.Add(sequenceinputarea);
    displaypanel.Controls.Add(outputinfo);
    Controls.Add(controlpanel);
    controlpanel.Controls.Add(computebutton);
    controlpanel.Controls.Add(clearbutton);
    controlpanel.Controls.Add(exitbutton);

    //Register the event handler.  In this case each button has an event handler, but no other
    //controls have event handlers.
    computebutton.Click += new EventHandler(computeMetric);
    clearbutton.Click += new EventHandler(cleartext);
    exitbutton.Click += new EventHandler(stoprun);  //The '+' is required.

    //Open this user interface window in the center of the display.
    CenterToScreen();

   } //End of constructor Fibuserinterface

 //Method to execute when the compute button receives an event, namely: receives a mouse click
 protected void computeMetric(Object sender, EventArgs events) {
    double sequencenum;        // allows for int or double value input for "inches"
    string output;
    try {
        sequencenum = double.Parse(sequenceinputarea.Text); // intakes user input in box
        double Metricnum = Metricconvertlogic.computeMetricConvert(sequencenum); // sends to logic file
        if (Metricnum < 0)
              output = "The Metric conversion resulted in a negative output\n something's wrong.";
        else
              output = "The metric value is: " + Math.Round(Metricnum, 4) + " meters."; // prints rounded answer
    }//End of try
    catch(FormatException malformed_input) {
       Console.WriteLine("Non-integer input received. Please try again.\n{0}",malformed_input.Message);
       output = "Invalid input: no Metric conversion computed.";
    }//End of catch
    catch(OverflowException too_big) {
       Console.WriteLine("The value inputted is greater than the largest 32-bit integer.  Try again.\n{0}",too_big.Message);
       output = "The input number was too large for 32-bit integers.";
    }//End of catch
    outputinfo.Text = output; // prints final output
  }//End of computeMetric

 //Method to execute when the clear button receives an event, namely: receives a mouse click
 protected void cleartext(Object sender, EventArgs events) {
   sequenceinputarea.Text = ""; //Empty string
   outputinfo.Text = "The metric equivalent will be displayed here: ";
  } //End of cleartext

 //Method to execute when the exit button receives an event, namely: receives a mouse click
 protected void stoprun(Object sender, EventArgs events) {
   Close();
 } //End of stoprun
} //End of class Metricinterface
