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
//  Programming language: C#
//  Date program began: 09-27-2020
//  Date of last update: 10-
//  Comments reorganized: 10-
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
//  Compile: mcs -t:library -r:System.Windows.Forms.dll -r:System.Drawing.dll -out:boardInterface.dll boardInterface.cs
//  Compile and Link: mcs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:boardInterface.dll -out:Skateboard.exe main.cs
//
//Execution: ./Skateboard.exe

//===== Begin code area ====================================================================================================================================================

using System;
using System.Windows.Forms;            //Needed for "Application.Run" near the end of Main function.

public class main {
    public static void Main() {
        System.Console.WriteLine("The graphics program will begin now.");
        BoardInterface Board_app = new BoardInterface();

        Application.Run(Board_app);
        System.Console.WriteLine("This graphics program has ended.  Bye.");       
      } //End of Main function
} //End of Simplemain class
