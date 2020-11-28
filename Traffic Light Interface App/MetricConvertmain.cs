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
//using System.Drawing;
using System.Windows.Forms;

public class MetricConvertmain {
  static void Main(string[] args) {
    System.Console.WriteLine("Welcome to the Main method of the Metric Converter program.");

    Metricinterface Metapp = new Metricinterface(); // calls interface file
    Application.Run(Metapp);
    System.Console.WriteLine("Main method will now shutdown.");
   }//End of Main
}//End of Fibonaccimain
