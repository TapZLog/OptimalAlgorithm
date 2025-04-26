//Importing any external features into the program
using System.Globalization;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

//Declaration of necessary variables and data structures
int TotalFrames = 0; //Total amount of frames in the algorithm
int TotalPages; //Total amount of pages in the algorithm
int PageFaults = 0; //Total amount of page faults in the algorithm

String input; //Used for determining the input mode of the system
String pages; //Used for inputting the page reference should the user go for manual input mode
String ConvertedPages; //Used for the filtering of non-digit characters
bool ValidLength = false; //Checks if the manually inputted value is at least 20 digits

int[] PageReference; //List of the page reference for the algorithm
List<int> FrameList; //List used in determining whether the system has any array present
Queue<int> FIFOOrder; //Used in FIFO order once the system has no future refeences for optimal algorithm

//Credits
Console.WriteLine("Optimal Algorithm");
Console.WriteLine("Made by Justin Patrick David");
Console.WriteLine("Created for Case Study for Operating Systems (OS)");
Console.WriteLine("Submitted to Arielle Joy Barcelona");
Console.WriteLine("==================================================");

bool inputMode = true;
bool inputType = false;
bool inputFrame = true;

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
        if (TotalPages >= 20)
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
        PageReference[i] = -'0';
    }

}
else
{
    TotalPages = 20;
    PageReference = new int[20];
    Random random = new Random();

    for (int i = 0; i < TotalPages; i++)
    {
        PageReference[i] = random.Next(0, 10);
        Console.Write(PageReference[i]);
    }
}

//Determining the amount of frames
do
{
    String temp;
    Console.WriteLine("Please enter the amount of frames in the program. Note that it can only be 3-5");
    temp = "" + Console.ReadLine();
    temp = Regex.Replace(temp, @"\D", "");
    try
    {
        TotalFrames = Convert.ToInt32(temp);
    }
    catch
    {
        Console.WriteLine("Error! Please enter only 3, 4, or 5");
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

//Initiates the arrays declared at the start of the program
FIFOOrder = new Queue<int>();
FrameList = new List<int>();

int instance = 0;

//Temporarily changes each array to -1 as a way to remove any 0 that doesn't exist yet
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

    //Check if the frames can hold a page
    if (FrameList.Count < TotalFrames)
    {

        //Checks if any instance of the number is already in the frames 
        if (!FrameList.Contains(num))
        {
            FrameList.Add(num);
            PageFaults++;
            fault = true;
        }
    }
    else
    {

        //Checks if any instance of the number is already in the frames 
        if (FrameList.Contains(i))
        {
            List<int> FrameCheck = FrameList;
            int ReplacedValue;

            for (int x = i; x < TotalPages; x++)
            {
                int y = PageReference[x];
                if (FrameCheck.Contains(y))
                {
                    FrameCheck.Remove(y);
                }
                if (FrameCheck.Count == 1)
                {
                    ReplacedValue = FrameCheck.FirstOrDefault();
                    PageFaults++;
                    fault = true;
                    break;
                }
            }
        }
    }

    instance++;
    Console.Write(instance);

    for (int j = instance.ToString().Length - 1; j <= 3; j++)
    {
        Console.Write(" ");
    }
    Console.Write(" |");

    //Used in the Display of each frame
    foreach (int x in FrameList)
    {
        if (x >= 0)
        {
            Console.Write("    " + x + "    |");
        }
        else
        {
            Console.Write("         |");
        }
    }
    Console.Write(" " + fault);
    if (fault)
    {
        Console.WriteLine("   |");
    }
    else
    {
        Console.WriteLine("  |");
    }
}