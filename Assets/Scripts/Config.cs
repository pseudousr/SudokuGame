using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

public static class Config // Made the class static since it only contains static methods
{
    private static string path;

    static Config()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
        path = Application.persistentDataPath;
        #else
        path = Directory.GetCurrentDirectory();
        #endif

        path = Path.Combine(path, "board_data.ini");
    }

    public static void DeleteDataFile()
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public static void SaveBoardData(SudokuData.SudokuBoardData board_data, string level, 
                                    int board_index, int error_number, Dictionary<string, List<string>> grid_notes)
    {
        using (StreamWriter writer = new StreamWriter(path)) // Use "using" statement to automatically close the StreamWriter
        {
            writer.WriteLine("#time:" + Clock.GetCurrentTime());
            writer.WriteLine("#level:" + level);
            writer.WriteLine("#errors:" + error_number);
            writer.WriteLine("#board_index:" + board_index);

            writer.WriteLine("#unsolved:" + string.Join(",", board_data.unsolved_data));
            writer.WriteLine("#solved:" + string.Join(",", board_data.solved_data));

            foreach (var square in grid_notes)
            {
                string square_string = "#" + square.Key + ":";

                foreach (var note in square.Value)
                {
                    if (!string.IsNullOrWhiteSpace(note))
                    {
                        square_string += note + " ";
                    }
                }

                if (!string.IsNullOrEmpty(square_string))
                {
                    writer.WriteLine(square_string);
                }
            }
        }
    }

    public static Dictionary<int, List<int>> GetGridNotes()
    {
        Dictionary<int, List<int>> grid_notes = new Dictionary<int, List<int>>();

        if (File.Exists(path))
        {
            string line;
            using (StreamReader file = new StreamReader(path))
            {
                while ((line = file.ReadLine()) != null)
                {
                    string[] word = line.Split(':');
                    if (word[0] == "#square_note")
                    {
                        int square_index = -1;
                        List<int> notes = new List<int>();
                        int.TryParse(word[1], out square_index);
                        string[] substring = Regex.Split(word[2], ",");
                
                        foreach (var note in substring)
                        {
                            int note_number = -1;
                            int.TryParse(note, out note_number);
                            if (note_number > 0)
                            {
                                notes.Add(note_number);
                            }
                        }
                        grid_notes.Add(square_index, notes);
                    }
                }
            }
        }

        return grid_notes;
    }

    
    public static string ReadBoardLevel()
    {
        string line;
        string level = "";
        StreamReader file = new StreamReader(path);

        while ((line = file.ReadLine()) != null)
        {
            string[] word = line.Split(':');
            if (word[0] == "#level")
            {
                level = word[1];
            }
        }
        file.Close();

        return level;
    }

    public static SudokuData.SudokuBoardData ReadGridData()
    {
        string line;
        StreamReader file = new StreamReader(path);

        int[] unsolved_data = new int[81];
        int[] solved_data = new int[81];

        int unsolved_index = 0;
        int solved_index = 0;

        while ((line = file.ReadLine()) != null)
        {
            string[] word = line.Split(':');
            if (word[0] == "#unsolved")
            {
                string[] substrings = Regex.Split(word[1], ",");

                foreach (var value in substrings)
                {
                    int square_number = -1;
                    if (int.TryParse(value, out square_number))
                    {
                        unsolved_data[unsolved_index] = square_number;
                        unsolved_index++;
                    }

                }
            }
            if (word[0] == "#solved")
            {
                string[] substrings = Regex.Split(word[1], ",");

                foreach (var value in substrings)
                {
                    int square_number = -1;
                    if (int.TryParse(value, out square_number))
                    {
                        solved_data[solved_index] = square_number;
                        solved_index++;
                    }
                }
            }
        }
        file.Close();
        return new SudokuData.SudokuBoardData(unsolved_data, solved_data);
    }


    public static int ReadGameBoardLevel()
    {
        int level = -1;
        string line;
        StreamReader file = new StreamReader(path);

        while ((line = file.ReadLine()) != null)
        {
            string[] word = line.Split(':');
            if (word[0] == "#board_index")
            {
                int.TryParse(word[1], out level);
            }
        }
        file.Close();
        
        return level;
    }

    public static float ReadGameTime()
    {
        float time = -1.0f;
        string line;
        
        StreamReader file = new StreamReader(path);

        while ((line = file.ReadLine()) != null)
        {
            string[] word = line.Split(':');
            if (word[0] == "#time")
            {
                float.TryParse(word[1], out time);
            }
        }
        file.Close();
        return time;
    }

    public static int ErrorNumber()
    {
        int errors = 0;
        string line;

        StreamReader file = new StreamReader(path);

        while ((line = file.ReadLine()) != null)
        {
            string[] word = line.Split(':');
            if (word[0] == "#errors")
            {
                int.TryParse(word[1], out errors);
            }
        }
        file.Close();
        return errors;
    }

    public static bool GameDataFileExist()
    {
        return File.Exists(path);
    }
}
