# Control_Vector_Generator

System Requirements
This program is developed in C# using Microsoft Visual Studio 2015. The .exe should run on any windows operating system. 
This file can not be read by Linux or Mac devices without additional third party software.

Running The Program
To run this program simply download the file, locate it in your computers file explorer and double click the .exe. 
This will open a terminal window and start requesting user input. The program will ask for an integer value to use as the programs 
increment amount for updating the program counter(PC). 
Input an integer value and press enter. If a non-integer is entered the user will be prompted to input a valid value. 
Next the computer will ask for a list of instructions you want to make available to the machine. The available instructions are the 31 
instructions from the data sheet for MIPS. Enter your desired instructions one at a time pressing enter after each. 
You can enter the * character to finish inputing instructions. If you enter an instruction that you have already selected the program 
will prompt you to try again. If you do not want to input any more instructions enter *. After the program reads * or reachs 
32 unique values it will then validate the givin instructions for spelling and availability. If an instruction is not valid the program 
will ask you if you want to change or delete that instruction. Enter the correct instruction or type delete to remove the instruction. 
Once all validation errors are fixed the program will output: 
1) the givin IPC (displayed in blue)
2) the present available control vectors(displayed in pink)
3) the complete micro program(displayed in purple)
The program will then ask if you want to restart or exit. To exit type * and to restart enter any other value(or leave the input empty 
and press enter).      

Note regarding the IPC: only a select few instructions will print their entire syntax within the IPC. These instructions are 
add,lw,sw,mult,brz,bne,beq, and j. 
