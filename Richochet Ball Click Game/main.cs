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
//  File name: main.cs
//  Language: C#
//  Optimal print specification: 7 point font, monospace, 136 columns, 8½x11 paper
//  Compile and Link: mcs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:RicochetInterface.dll -out:balls.exe main.cs
//
//Execution: ./final.exe

// ========== Begin code area ==========

using System;
using System.Windows.Forms;

public class main {
  public static void Main() {  
    System.Console.WriteLine("The ricochet ball program will begin now.");

    RicochetInterface ricochet_application = new RicochetInterface();
    Application.Run(ricochet_application);

    System.Console.WriteLine("This ricochet ball program has ended.  Bye.");
  } // End of Main function
} // End of main class
