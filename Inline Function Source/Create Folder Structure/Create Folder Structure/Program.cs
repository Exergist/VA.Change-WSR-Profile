// C# inline function for creating a folder structure based on inputted path data

namespace Create_Folder_Structure
{
    class Program
    {
        static void Main(string[] args)
        {
            // Specify full path of desired folder structure
            string folderPath = "YourFilePathHere";
            ///string folderPath = VA.GetText("VARestartPath");
        
            // Create the desired folder structure
            System.IO.Directory.CreateDirectory(folderPath);
        }
    }
}

// References:
  // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-create-a-file-or-folder
