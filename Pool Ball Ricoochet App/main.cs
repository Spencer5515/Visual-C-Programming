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
//  Date of last update: 11-10-2020
//  Comments reorganized: 11-10-2020
//  Files in the program: main.cs, boardInterface.cs run.sh
//
//Purpose
//  Display a stand-in skateboarder moving around a triangular path.
//  For learning purposes: Demonstrate use of hybrid programming concepts in a theoretical setting
//
//This file
//  File name: main.cs
//  Language: C#
//  Optimal print specification: 7 point font, monospace, 136 columns, 8½x11 paper
//  Compile and Link: mcs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:RicochetInterface.dll -out:Ricochet.exe main.cs
//
//Execution: ./Ricochet.exe

// ========== Begin code area ==========

using System;
using System.Windows.Forms;            //Needed for "Application.Run" near the end of Main function.

public class main {
    public static void Main() {
        // System.Console.WriteLine("The graphics program will begin now.");
        // RicochetInterface Ricochet_app = new RicochetInterface();

        // Application.Run(Ricochet_app);
        // System.Console.WriteLine("This graphics program has ended.  Bye.");   

        System.Console.WriteLine("The ricochet ball program will begin now.");
        //Declare the basic quantities needed for a straight line motion application: speed and direction.
        // double speed = 81.9; //C# units, namely: pixels/sec.
        //Direction may be specified in either of two ways: (a) an angle number, or (b) a vector u⃗ = <v,w>
        //In this program we use the second choice.
        // double v = 37.2;
        // double w = 21.8;
        // RicochetInterface ricochet_application = new RicochetInterface(speed,v,w);
        RicochetInterface ricochet_application = new RicochetInterface();
        Application.Run(ricochet_application);
        System.Console.WriteLine("This ricochet ball program has ended.  Bye.");
      } //End of Main function
} //End of main class
