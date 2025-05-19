
//Importing any external features into the program
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

//Declaration of necessary variables and data structures
int TotalFrames = 0; //Total amount of frames in the algorithm
int TotalPages = 0; //Total amount of pages in the algorithm
int PageFaults = 0; //Total amount of page faults in the algorithm
int PageHits = 0; //Total amount of page hits in the algorithm

<<<<<<< HEAD
int[] PageReference = {}; //List of the page reference for the algorithm
=======
String input; //Used for determining the input mode of the system
String pages; //Used for inputting the page reference should the user go for manual input mode
String ConvertedPages; //Used for the filtering of non-digit characters
bool ValidLength = false; //Checks if the manually inputted value is at least 20 digits

int[] PageReference; //List of the page reference for the algorithm
>>>>>>> parent of 93df240 (ver beta)
List<int> FrameList; //List used in determining whether the system has any array present
List<int> FIFOOrder; //Used for FIFO order once the system has no future references for optimal algorithm

//Credits
Console.WriteLine("Optimal Algorithm");
Console.WriteLine("Made by Justin Patrick David");
Console.WriteLine("Created for Case Study for Operating Systems (OS)");
Console.WriteLine("Submitted to Arielle Joy Barcelona");
Console.WriteLine("==================================================");

bool inputMode = true;
bool inputType = false;
bool inputFrame = true;
bool inputPage = true;

<<<<<<< HEAD
//Generation of the Page Reference
do
{
    String tempPage;
    Console.WriteLine("Please enter the number of pages in the program. Note that it should be at least 20 or above");
    tempPage = "" + Console.ReadLine();
    tempPage = Regex.Replace(tempPage, @"\D", "");
    try
    {
        TotalPages = Convert.ToInt32(tempPage);
    }
    catch
    {
        Console.WriteLine("Error! Please enter at least 20 or above for page reference");
        continue;
    }

    if (TotalPages >= 20)
    {
        PageReference = new int[TotalPages];
        Random random = new Random();

        for (int i = 0; i < TotalPages; i++)
        {
            PageReference[i] = random.Next(0, 10);
        }

        Console.Write("The Generated Page Reference is ");
        Console.WriteLine("[{0}]", string.Join(", ", PageReference));
        Console.WriteLine("==================================================");
        inputPage = false;
    }
    else
    {
        Console.WriteLine("Error! Please enter at least 20 or above for page reference");
    }

} while (inputPage);
=======
//Choose the mode whether the page reference is random or preset inputted
do
{
    input = "0";

    Console.WriteLine("Please Enter the Mode in which you are entering: ");
    Console.WriteLine("1: Manual Input");
    Console.WriteLine("2: Automatic Input");
    Console.WriteLine("3: Help");
    Console.WriteLine("==================================================");
    input = "" + Console.ReadLine();

    //Determines if the current mode is manual input or automatic input
    switch (input)
    {
        case "1":
            Console.WriteLine("You have selected manual input mode");
            Console.WriteLine("==================================================");
            inputType = true;
            inputMode = false;
            break;
        case "2":
            Console.WriteLine("You have selected automatic input mode");
            Console.WriteLine("==================================================");
            inputType = false;
            inputMode = false;
            break;
        case "3":
            Console.WriteLine("Manual Input - Allows the user to input their selected page reference in the console. Filtered using RegEx");
            Console.WriteLine("Automatic Input - The system will automatically add random values for the page reference");
            Console.WriteLine("==================================================");
            break;
        default:
            Console.WriteLine("Please only input from 1-3");
            Console.WriteLine("==================================================");
            break;
    }
} while (inputMode);

//Generation of the Page Reference
if (inputType)
{
    do
    {
        //Manual Input Mode
        Console.WriteLine("Please enter page reference as a string. it must include at least 20 digits. Note that non-digit characters will be ignored in the string.");
        pages = "" + Console.ReadLine();
        //For converting the inputted string into the total amount of pages
        ConvertedPages = Regex.Replace(pages, @"\D", "");
        TotalPages = ConvertedPages.Length;
        if (TotalPages >= 5)
        {
            ValidLength = true;
        }
        else
        {
            Console.WriteLine("Error. The length of the pages must be at least 20 digits");
        }
    } while (!ValidLength);

    //Application of the converted string into a page reference array
    PageReference = new int[TotalPages];
    for (int i = 0; i < TotalPages; i++)
    {
        PageReference[i] = ConvertedPages[i] - '0';
    }

}
else
{
    TotalPages = 20;
    PageReference = new int[20];
    Random random = new Random();

    Console.Write("The Generated Page Reference is ");
    for (int i = 0; i < TotalPages; i++)
    {
        PageReference[i] = random.Next(0, 10);
        if (i < (TotalPages - 1))
        {
            Console.Write(PageReference[i] + ", ");
        }
    }
    Console.WriteLine();
}
>>>>>>> parent of 93df240 (ver beta)

//Determining the amount of frames
do
{
    String tempFrame;
    Console.WriteLine("Please enter the number of frames in the program. Note that it can only be 3-5");
    tempFrame = "" + Console.ReadLine();
    tempFrame = Regex.Replace(tempFrame, @"\D", "");
    try
    {
        TotalFrames = Convert.ToInt32(tempFrame);
    }
    catch
    {
        Console.WriteLine("Error! Please enter only 3, 4, or 5");
        continue;
    }

    if (3 > TotalFrames || TotalFrames > 5)
    {
        Console.WriteLine("Error! Please enter only 3, 4, or 5");
    }
    else
    {
        Console.WriteLine("The algorithm will have " + TotalFrames + " frames");
        inputFrame = false;
    }

} while (inputFrame);

//Timer for calculation time
var stopwatch = new Stopwatch();
stopwatch.Start();

//Initiates the lists declared at the start of the program
FIFOOrder = new List<int>();
FrameList = new List<int>();

int instance = 0;

//Used for the aesthetic of the table
Console.Write("F     | ");
for (int i = 0; i < TotalFrames; i++)
{
    Console.Write("Frame " + (i + 1) + " | ");
}
Console.WriteLine("Fault? |");
Console.Write("======|");
for (int i = 0; i < TotalFrames; i++)
{
    Console.Write("=========|");
}
Console.WriteLine("========|");

//Initiating the Optimal Algorithm
for (int i = 0; i < TotalPages; i++)
{
    bool fault = false; //Used in specifying whether a fault has occurred or not
    int num = PageReference[i];
    int FrameReplaced = -1;

    //Check if the frames can hold a page
    if (FrameList.Count < TotalFrames)
    {

        //Checks if any instance of the number is already in the frames 
        if (!FrameList.Contains(num))
        {
            FrameList.Add(num);
            FIFOOrder.Add(num);
            PageFaults++;
            FrameReplaced = num;
            fault = true;
        }
    }
    else
    {

        //Checks if any instance of the number is already in the frames 
        if (!FrameList.Contains(num))
        {
            List<int> FrameCheck = new List<int>(FrameList);
<<<<<<< HEAD
            int ReplacedValue; //Used in determining the value that will be used later, thus it is to be replaced
=======
            int ReplacedValue;
>>>>>>> parent of 93df240 (ver beta)

            for (int x = i + 1; x < TotalPages; x++)
            {
                int y = PageReference[x];
                if (FrameCheck.Contains(y))
                {
<<<<<<< HEAD
                    FrameCheck.Remove(y); //This works by creating a list and then removing the next instance of any given page until there is one left
                    FIFOOrder.Remove(y);
                    FIFOOrder.Add(y);
                    if (FrameCheck.Count == 1) //Replacement algorithm for the frames
=======
                    FrameCheck.Remove(y);
                    if (FrameCheck.Count == 1)
>>>>>>> parent of 93df240 (ver beta)
                    {
                        break;
                    }
                } 
            } if (FrameCheck.Count > 1) {

                int val = FIFOOrder.First();
                int index = FrameList.IndexOf(val);
                FrameList.Remove(val);
                FrameList.Insert(index, num);
                FIFOOrder.Add(num);
                FIFOOrder.Remove(val);
                PageFaults++;
                FrameReplaced = num;
                fault = true;
            }
            else if (FrameCheck.Count == 1)
            {
                ReplacedValue = FrameCheck.FirstOrDefault();
                int index = FrameList.IndexOf(ReplacedValue);
                FIFOOrder.Add(num);
                FIFOOrder.Remove(ReplacedValue);
                FrameList.Remove(ReplacedValue);
                FrameList.Insert(index, num);
                PageFaults++;
                FrameReplaced = num;
                fault = true;
            }
        }
    }

    if (!fault)
    {
        PageHits++;
    }

    instance++;
    Console.Write(instance);

    for (int j = instance.ToString().Length - 1; j <= 3; j++)
    {
        Console.Write(" ");
    }
    Console.Write(" |");

<<<<<<< HEAD
    //Used in the Display of each frame  
=======
    // Console.Write("fifo:"); foreach (int gg in FIFOOrder) {Console.Write(gg.ToString());} Console.WriteLine("");

    //Used in the Display of each frame
>>>>>>> parent of 93df240 (ver beta)
    for (int c = 0; c < TotalFrames; c++)
    {
        int x = -1;
        if (FrameList.Count > c)
        {
            x = FrameList[c];
        }

        if (x >= 0)
        {
            if (x == FrameReplaced)
            {
                Console.Write(" ---" + x + "--- |");
            }
            else
            {
                Console.Write("    " + x + "    |");
            }
        }
        else
        {
            Console.Write("         |");
        }
    }
<<<<<<< HEAD

    if (fault)
    {
        Console.WriteLine("   *   |");
=======
    Console.Write(" " + fault);
    if (fault)
    {
        Console.WriteLine("   |");
>>>>>>> parent of 93df240 (ver beta)
    }
    else
    {
        Console.WriteLine("  |");
    }
}

//Used in the display of the number of page faults
<<<<<<< HEAD
Console.WriteLine("==================================================");
Console.WriteLine("Total Amount of Page Faults: " + PageFaults);
Console.WriteLine("Total Amount of Hits: " + PageHits);
decimal HitRatio = (decimal)PageHits / (decimal)TotalPages * 100;
Console.WriteLine("Hit Ratio: {0}%", HitRatio);

//Displaying Time
stopwatch.Stop();
var time = stopwatch.ElapsedMilliseconds;
Console.WriteLine("Total Operation Time: " + time + "ms");
=======
Console.WriteLine("Total Amount of Page Faults: " + PageFaults);
>>>>>>> parent of 93df240 (ver beta)
