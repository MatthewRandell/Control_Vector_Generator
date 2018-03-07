using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control_Vector_Generator
{
    class Program
    {
        
        static int PC = 0; // the variable that holds the Program counter for desplaying memory locations; 
        static int addressInc; // the variable that determines where instructions go in memory , set by the user
        static void Main(string[] args)
        {
            Console.SetWindowSize(110, 50);
            bool repeat = true;
            while (repeat)
            {


                string[] methods = new string[32]; // there are 32 possible options givin on the data sheet in class (31 plus brz) so the methods array can hold up to 32 methods 


                addressInc = getInc();
                getMethods(methods);
                string[] finalMethods = validate(methods); // after getMethods gets the method options from user and checks that there are no doubles, methods array is vaildated for spelling and availability, null positions are removed and the finalMethods array is set
                printIPC(finalMethods);
                printControlVector(finalMethods);

                Console.WriteLine();
                Console.WriteLine("Enter * to exit (any other input will restart this program)");
                if (Console.ReadLine().Equals("*")) repeat = false;

                else
                {
                    PC = 0;
                    Console.Clear();
                }
                
            }

        }

        /**getInc()
        Parameters: none
        Return: the increment value obtained from the users input
        Use: asks the user for the integer increment amount to be used in the program. If anything other than an
         integer is entered the user will be asked again until a valid number is enetered.
            */
        public static int getInc()
        {
            Console.WriteLine("Please enter the integer increment amount for PC on each complete instruction");
            string addressIncCheck = Console.ReadLine();
            int number;
            bool isNumber = Int32.TryParse(addressIncCheck, out number);
            while (!isNumber)
            {
                if (isNumber)
                {
                    // valid number
                    return number;
                }
                else
                {
                    //not a number
                    Console.WriteLine("That is not a number, please enter a valid integer");
                    addressIncCheck = Console.ReadLine();
                    isNumber = Int32.TryParse(addressIncCheck, out number);
                    if (isNumber)
                    {
                        // valid number
                        return number;
                    }
                }
            }
            return number;
        }
        
        /**getMethods(string[] m)
        Parameters: string[] m is a string array that will be populated with user input
        Return: void
        Use: the getMethods function prompts a user for a list of instructions to be used in a MIPS like machine. This function will not allow copies of functions already present in the array 
        the user will be asked to change or delete any doubles prior to continuing to fill the array. To signal that the user is finished entering instructions the sentinal value is *. 
        the program will automatically stop reading if the user tries to enter more than 32 unique instructions. 
         */
        public static void getMethods(string[] m)
        {
            string sentinal = "*"; // sentinal value to signal the end of input
            Console.WriteLine("please enter a command that will be available to the machine according to the IPC or \n enter * to finsih inputing instructions");
            bool loopOneFinished = false; // boolean to end the loop early

            for (int i = 0; !loopOneFinished && i < 32; ++i) //for loop to loop through each element in the array
            {
                
                string method = Console.ReadLine();

                if (method != sentinal)
                {
                    m[i] = method;
                    int doubleCounter = 0; //counts how many doubles are present
                    for (int k = 0; k < 32; k++)
                    {
                        while (m[i].Equals(m[k]) && i != k && m[k] != null)
                        {
                            doubleCounter++;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("sorry you have entered a command that is already present try again or enter * to finsih inputing instructions: ");
                            Console.ResetColor();
                            string moreInstructions = Console.ReadLine();
                            if (moreInstructions.Equals("*"))
                            {
                                m[k] = null;
                                loopOneFinished = true;
                            }
                            else
                            {
                                m[i] = moreInstructions;
                            }
                           
                        }

                    }
                }
                if (method == sentinal)
                {
                    loopOneFinished = true;
                }
                if (i == 31)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("if the spelling is correct there is no room for any more instructions");
                    Console.ResetColor();
                }
            }
        }

        /**validate(string[] m)
        Parameters: string[] m is a string array that should contain the users input instructions. Each element will be checked for spelling and availibility
        Return: a validated string array
        Use: the validate method takes in a string array that is filled with user instructions and checks that each input is a valid instruction. Any instruction that is invalid will be brought to the 
        attention of the user. they will have the option to change the invalid instruction or delete it. Any valid instructions are saved into a new string array which the method then returns. 
        */
        public static string[] validate(string[] m)
        {
            int validSize = 0; // used for initalizing the valid methods array later to limit the size to exactly the right amount.
            string[] MIPSMethods = { "add", "sub", "addi", "addu", "subu", "addiu",
                "mfc0", "mult", "multu", "div", "divu", "mfhi", "mflo","and","or","andi","ori",
                "sll","srl","lw","sw","lui","beq","bne","brz","slt","slti", "sltu",
                "sltiu", "j","jr","jal"}; // all available MIPS instructions by name according to the data sheet obtained in class(plus brz from example 1)

          
            for (int i = 0; i < 32; i++) //loop through m array
            {
                bool validMethod = false; // track if the method is a match to any valid matches
                int k = 0;
                while (k < 32 && !validMethod) // compare current m[i] with valid MIPSMethods untill we have no more valids or a match is found
                {
                    if (m[i] == null) {
                        validMethod = true;
                    }
                    
                    else if (m[i].Equals(MIPSMethods[k]))
                    {
                        validMethod = true;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(m[i] +" valid method");
                        Console.ResetColor();
                        validSize++;
                    }
                    k++;
                }
                if (!validMethod)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(m[i] + " is not a valid function please enter a valid function or delete to clear instruction. To see a full list of available instructions enter help");
                    Console.ResetColor();
                    string helpCheck = Console.ReadLine(); // check if the user needs a list of available options
                    if (helpCheck != "help") {
                        m[i] = helpCheck;
                    }
                    else
                    {
                        printArray(MIPSMethods);
                        Console.WriteLine("please select an instruction from the list above or type delete to remove");
                        Console.WriteLine();
                        m[i] = Console.ReadLine();
                    }

                    
                    
                    int x = 0;
                    while (x < 32 && !validMethod) // compare current m[i] with valid MIPSMethods untill we have no more valids or a match is found
                    {
                        if (m[i] == "help")
                        {

                        }
                        if (m[i] == "delete")
                        {
                            m[i] = null;
                        }
                        if (m[i] == null)
                        {
                            validMethod = true;
                        }

                        else if (m[i].Equals(MIPSMethods[x]))
                        {
                            int doubleCounter = 0; //counts how many doubles are present
                          
                            for (int z = 0; z < 32; z++)
                            {
                                while (m[i].Equals(m[z]) && i != z && m[z] != null)
                                {
                                    doubleCounter++;
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("sorry you have entered a double try again: ");
                                    Console.ResetColor();
                                    m[i] = Console.ReadLine();
                                }

                            }
                            validMethod = true;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(m[i] + " valid method");
                            Console.ResetColor();
                            validSize++;
                        }
                        x++;
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("you have entered " +validSize+ " valid instructions");
            Console.ResetColor();

            string[] validMethods = new string[validSize];

            int l = 0;
            for(int i = 0; i<32; i++)
            {
                if (m[i] != null)
                {
                    validMethods[l] = m[i];
                    l++;
                }
            }

            return validMethods;

        }

        /** printArray(string[] arr)
        Parameters: string[] arr is the string array to be printed to the console.
        Return: void
        Use: simple outout function to print a string array to the console
        */
        public static void printArray(string[] arr)
        {
            
            Console.WriteLine();
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(i + " " + arr[i]);
            }
            Console.WriteLine();
        }

        /** printArray(int[] arr)
        Parameters: int[] arr is the int array to be printed to the console.
        Return: void
        Use: simple outout function to print an int array to the console
        */
        public static void printArray(int[] arr)
        {
            
            Console.Write("");
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i]);
            }
            Console.WriteLine();
        }

        /** printIPC(string[] arr)
        Parameters: string[] arr is the string array of validated methods to use in the IPC representation for the execute stage
        Return: void
        Use: Prints the current IPC to the console. Note that the first 4 control vectors of our IPC's are the same and such are hard coded. IA(IR) will select to use OPR1,OPR2, and/or OPR3 depending on which
        instruction is in use. Only insturctions used in examples or the homework have detailed syntax in this program. Syntax can be found on the MIPS data sheet givin in class. 
        */
        public static void printIPC(string[] arr)
        {
            
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("The IPC currently in use is as follows");
            Console.WriteLine();
            Console.WriteLine("C0: PC <= PC + " + addressInc); //update stage
            Console.WriteLine("C1: IR <= M[PC]"); // fetch stage
            Console.WriteLine("C2: OP <= DC(IR)"); // decode stage
            Console.WriteLine("C3: OPR[1..3] <= IA(IR) // OPR1,OPR2 and/or OPR3 type dependant" ); //decode stage
            
            //execute stage
            for(int i = 0; i < arr.Length; i++)
            {
                if (arr[i].Equals("add")) Console.WriteLine("C" + (i + 4) + ": " + arr[i] + " OPR1 <= OPR2 + OPR3 "); 
                else if (arr[i].Equals("lw")) Console.WriteLine("C" + (i + 4) + ": " + arr[i] + " OPR1 <= M[OPR2 + OPR3] ");
                else if (arr[i].Equals("brz")) Console.WriteLine("C" + (i + 4) + ": " + arr[i] + " branch <= (OPR1 == 0) \n If branch then PC <= PC + " + addressInc + " + (" + addressInc + " * OPR2)");
                else if (arr[i].Equals("mult")) Console.WriteLine("C" + (i + 4) + ": " + arr[i] + " Hi,Lo <= OPR1 * OPR2");
                else if (arr[i].Equals("sw")) Console.WriteLine("C" + (i + 4) + ": " + arr[i] + " M[OPR2+OPR3] <= OPR1");
                else if (arr[i].Equals("beq")) Console.WriteLine("C" + (i + 4) + ": " + arr[i] + "branch <= (OPR1 == OPR2) \n If branch then PC <= PC + " + addressInc + " + (" + addressInc + " * OPR3)");
                else if (arr[i].Equals("bne")) Console.WriteLine("C" + (i + 4) + ": " + arr[i] + "branch <= (OPR1 != OPR2) \n If branch then PC <= PC + " + addressInc + " + (" + addressInc + " * OPR3)");
                else if (arr[i].Equals("j")) Console.WriteLine("C" + (i + 4) + ": " + arr[i] + "PC <= OPR1");
                else
                {
                    Console.WriteLine("C" + (i + 4) + ": " + arr[i] + " (see documentation for syntax)");
                }
            }
            Console.ResetColor();
        }

        /**printMicroProgram(string[,] arr)
        Parameters: string[,] arr is a 2D string array that contains the micro program derived from the control vectors
        Return: void
        Use: Prints the complete micro program of the givin machine based on the control vectors present.
            */
        public static void printMicroProgram(string[,] arr)
        {
            Console.WriteLine();
            Console.WriteLine("The Complete Micro Program");
            int rowLength = arr.GetLength(0);
            int colLength = arr.GetLength(1);

            for (int i = 0; i < rowLength; i++) //loop through each instruction
            {
           
                Console.WriteLine();
                Console.Write("M[" + PC + "] |");
                for (int j = 0; j < colLength; j++) // loop through each vector
                {
                    Console.Write(string.Format("{0}", arr[i, j]));
                }
                Console.WriteLine("|");
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write("--");
                }
                
                PC += addressInc;
            }
        }

        /**printMicroProgram(string[,] arr)
        Parameters: string[] arr is a string array that contains the validated methods entered by the user
        Return: void
        Use: creates an int array called vector based on the control signal number and prints that to the console. Then takes the contents of that vector and writes it to a 2D string array called microprogram. Repeats for each instruction.
        calls the printMicroProgram method on the microprogram array.
            */
        public static void printControlVector(string[] arr)
        {
            string[,] microprogram = new string[arr.Length, arr.Length + 4];

            int[] vector = new int[arr.Length + 4];
            int vectorTracker = 0;
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("The Present Available Control Vectors ");
            for (int z = 0; z < arr.Length; z++)
            {
                
                Console.WriteLine();
                Console.Write(arr[z] + ":");
                
                for (int x = 0; x < arr.Length + 4; x++)
                {

                    if (x < 4)
                    {
                        vector[x] = 1;
                    }
                    else if (x >= 4 && vectorTracker == (x - 4))
                    {
                        vector[x] = 1;

                    }
                    else
                    {
                        vector[x] = 0;
                    }
                }
                vectorTracker++;
                printArray(vector);
                for (int k = 0; k < vector.Length; k++)
                {

                    microprogram[z, k] = (vector[k].ToString());
                }

                
            }
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            printMicroProgram(microprogram);
            Console.ResetColor();

        }
        
    }
}
