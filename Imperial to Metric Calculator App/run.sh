#!/bin/bash

#Author: Spencer DeMera
#Course: CPSC 223N-01
#Semester: Fall 2020
#Assignment: 1
#Due: October 10, 2020.

echo First remove old binary files
rm *.dll
rm *.exe

echo View the list of source files
ls -l

echo Compile Metricconvertlogic.cs to create the file: Metricconvertlogic.dll
mcs -target:library -out:Metricconvertlogic.dll Metricconvertlogic.cs

echo Compile Metricinterface.cs to create the file: Metricinterface.dll
mcs -target:library -r:System.Drawing.dll -r:System.Windows.Forms.dll -r:Metricconvertlogic.dll -out:Metricinterface.dll Metricinterface.cs

echo Compile MetricConvertmain.cs and link the two previously created dll files to create an executable file. 
mcs -r:System -r:System.Windows.Forms -r:Metricinterface.dll -out:MetricConvert.exe MetricConvertmain.cs

echo View the list of files in the current folder
ls -l

echo Run the Assignment 1 program.
./MetricConvert.exe

echo The script has terminated.
