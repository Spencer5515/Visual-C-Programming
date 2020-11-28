//****************************************************************************************************************************
//Program name: "Street Light Program".  This programs accepts a non-negative integer from the user and                      *
//then outputs the Metric equivalent of that integer.                                                                        *
//Copyright (C) 2020  Spencer DeMera                                                                                         *
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License  *
//version 3 as published by the Free Software Foundation.                                                                    *
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied         *
//warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.     *
//A copy of the GNU General Public License v3 is available here:  <https://www.gnu.org/licenses/>.                           *
//****************************************************************************************************************************

// Author: Spencer DeMera
// Email: spencer.demera@csu.fullerton.edu
// Course ID: CPSC 223N-01
// Assignment Number: 2
// Due Date: 10/14/2020
// Purpose: Homework && Extra Credit

using System;
using System.Windows.Forms;            //Needed for "Application.Run" near the end of Main function.

public class Simplemain {
    public static void Main() {
        System.Console.WriteLine("The graphics program will begin now.");
        LightInterface Light_app = new LightInterface();

        Application.Run(Light_app);
        System.Console.WriteLine("This graphics program has ended.  Bye.");       
      } //End of Main function
} //End of Simplemain class
