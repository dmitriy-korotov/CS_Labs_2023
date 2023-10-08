using Lab1;

public class Programm
{
    public static void Main(string[] _args)
    {
        Console.WriteLine("\t\tTask1");
        Student student = new Student(new Person("Maria", "Likova", new DateTime(2005, 5, 14)), Education.Bachelor, 15);
        Console.WriteLine(student);



        Console.WriteLine("\t\tTask2");
        Console.WriteLine($"Specialist: {student[Education.Specialist]}");
        Console.WriteLine($"Bachelor: {student[Education.Bachelor]}");
        Console.WriteLine($"SecondEducation: {student[Education.SecondEducation]}");



        Console.WriteLine("\t\tTask3");
        student.Person = new Person("Ivan", "Ivanov", new DateTime(2001, 12, 1));
        student.EducationType = Education.Specialist;
        student.NumberOfGroup = 1;
        student.Exams = new Exam[] { new Exam("Math", 4, new DateTime(2023, 4, 23)), new Exam("Physic", 5, new DateTime(2023, 5, 16)), new Exam() };
        Console.WriteLine(student);



        Console.WriteLine("\t\tTask4");
        student.addExams(new Exam("Russian language", 3, new DateTime(2023, 2, 12)), new Exam("History", 2, new DateTime(2023, 5, 7)));
        Console.WriteLine(student);



        Console.WriteLine("\t\tTask5");
        Console.WriteLine("Input amount rows and amount cols (format: {rows};or,or-{cols}):\t");
        string? input = Console.ReadLine();
        while (input == null)
        {
            Console.WriteLine("Input amount rows and amount cols again (format: {rows};or,or-or:{cols}):\t");
            input = Console.ReadLine();
        }



        string[] splited_line = input.Split(';', ',', '-', ':');
        if (splited_line.Length != 2)
        {
            Console.WriteLine("Incorrected format.");
            return;
        }
        int amount_rows = 0;
        if (!int.TryParse(splited_line[0], out amount_rows))
        {
            Console.WriteLine("Rows is not integer type.");
            return;
        }
        int amount_cols = 0;
        if (!int.TryParse(splited_line[0], out amount_cols))
        {
            Console.WriteLine("Cols is not integer type.");
            return;
        }



        Exam[] array_1 = new Exam[amount_cols * amount_rows];
        Exam[][] array_2 = new Exam[amount_rows][];
        Exam[,] array_3 = new Exam[amount_rows, amount_cols];

        for (int i = 0; i < amount_rows; i++)
        {
            array_2[i] = new Exam[amount_cols];
            for (int j = 0; j < amount_cols; j++)
            {
                array_1[j + i * amount_cols] = new Exam();
                array_2[i][j] = new Exam();
                array_3[i, j] = new Exam();
            }
        }


        
        System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
        timer.Start();
        for (int i = 0; i < amount_rows; i++)
        {
            for (int j = 0; j < amount_cols; j++)
            {
                array_1[j + i * amount_cols].StartingDate = new DateTime();
            }
        }
        timer.Stop();
        timer.Restart();
        Console.WriteLine($"\nElapsed time for [] array: {timer.Elapsed}");

        timer.Start();
        for (int i = 0; i < amount_rows; i++)
        {
            for (int j = 0; j < amount_cols; j++)
            {
                array_2[i][j].StartingDate = new DateTime();
            }
        }
        timer.Stop();
        timer.Restart();
        Console.WriteLine($"Elapsed time for [][] array: {timer.Elapsed}");

        timer.Start();
        for (int i = 0; i < amount_rows; i++)
        {
            for (int j = 0; j < amount_cols; j++)
            {
                array_3[i, j].StartingDate = new DateTime();
            }
        }
        timer.Stop();
        timer.Restart();
        Console.WriteLine($"Elapsed time for [,] array: {timer.Elapsed}");
    }
}