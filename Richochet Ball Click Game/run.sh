#!/bin/bash
#In the official documemtation the line above always has to be the first line of any script file.

# Author: Spencer DeMera
# Email: spencer.demera@csu.fullerton.edu
# Course ID: CPSC 223N-01
# Assignment Number: Final
# Due Date: 12/16/2020
# Purpose: Final Exam

#This is a bash shell script to be used for compiling, linking, and executing the C sharp files of this assignment.
#Execute this file by navigating the terminal window to the folder where this file resides, and then enter either of the commands below:
#  sh run.sh   OR   ./build.sh

#System requirements:
#  A Linux system with BASH shell (in a terminal window).
#  The mono compiler must be installed.  If not installed run the command "sudo apt install mono-complete" without quotes.
#  The source files and this script file must be in the same folder.
#  This file, run.sh, must have execute permission.  Go to the properties window of build.sh and put a check in the
#  permission to execute box.

echo First remove old binary files
rm *.dll
rm *.exe

echo "\n----- Begin Program -----\n"
echo View the list of source files
ls -l

echo "Compile RicochetInterface.cs to create the file: RicochetInterface.dll"
mcs -t:library -r:System.Windows.Forms.dll -r:System.Drawing.dll -out:RicochetInterface.dll RicochetInterface.cs

echo "Compile main.cs and link the previously created dll file(s) to create an executable file."
mcs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:RicochetInterface.dll -out:balls.exe main.cs

echo View the list of files in the current folder
ls -l

echo Run the Assignment 6 program.
./final.exe

echo "\n----- Program Finished -----\n"