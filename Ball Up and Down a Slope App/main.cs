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
//  File name: main.cs
//  Language: C#
//  Optimal print specification: 7 point font, monospace, 136 columns, 8½x11 paper
//  Compile: mcs -t:library -r:System.Windows.Forms.dll -r:System.Drawing.dll -out:lineInterface.dll lineInterface.cs
//  Compile and Link: mcs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:lineInterface.dll -out:line.exe main.cs
//
//Execution: ./line.exe

//===== Begin code area ====================================================================================================================================================

using System;
using System.Windows.Forms; //Needed for "Application.Run" near the end of Main function.

public class main {
    public static void Main() {
        System.Console.WriteLine("The graphics program will begin now.");
        LineInterface Board_app = new LineInterface(); // calls lineInterface.cs

        Application.Run(Board_app); // runs the actual app
        System.Console.WriteLine("This graphics program has ended.  Bye.");       
      } //End of Main function
} //End of Simplemain class
