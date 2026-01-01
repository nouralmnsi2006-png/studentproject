using System;

enum Grade
{
    Fail,
    Acceptable,
    Good,
    VeryGood,
    Excellent
}

class Student
{
    public int Id;
    public string Name;
    public double Exam1;
    public double Exam2;
    public double Average;
    public Grade Grade;
    public bool IsExcellent;
}

class Node
{
    public Student Data;
    public Node Next;
    public Node Prev;
}

class DoublyList
{
    public Node Head;
    public Node Tail;
    public int Count;

    public void Add(Student s)
    {
        Node n = new Node();
        n.Data = s;

        if (Head == null)
        {
            Head = Tail = n;
        }
        else
        {
            Tail.Next = n;
            n.Prev = Tail;
            Tail = n;
        }
        Count++;
    }
}

class Program
{
    static DoublyList students = new DoublyList();

    static void Main()
    {
        int choice;

        do
        {
            Console.WriteLine("\n--- Student System ---");
            Console.WriteLine("1. Add Student (max 5)");
            Console.WriteLine("2. Show Report");
            Console.WriteLine("0. Exit");
            Console.Write("Choose: ");
            choice = int.Parse(Console.ReadLine());

            if (choice == 1) AddStudent();
            else if (choice == 2) ShowReport();

        } while (choice != 0);
    }

    static void AddStudent()
    {
        if (students.Count >= 5)
        {
            Console.WriteLine("You cannot add more than 5 students.");
            return;
        }

        Student s = new Student();

        Console.Write("Enter ID: ");
        s.Id = int.Parse(Console.ReadLine());

        Console.Write("Enter Name: ");
        s.Name = Console.ReadLine();

        Console.Write("Enter Exam1: ");
        s.Exam1 = double.Parse(Console.ReadLine());

        Console.Write("Enter Exam2: ");
        s.Exam2 = double.Parse(Console.ReadLine());

        s.Average = (s.Exam1 + s.Exam2) / 2;

        if (s.Average < 50)
            s.Grade = Grade.Fail;
        else if (s.Average < 65)
            s.Grade = Grade.Acceptable;
        else if (s.Average < 75)
            s.Grade = Grade.Good;
        else if (s.Average < 85)
            s.Grade = Grade.VeryGood;
        else
            s.Grade = Grade.Excellent;

        s.IsExcellent = s.Average >= 85;

        students.Add(s);
        Console.WriteLine("Student added.");
    }

    static void ShowReport()
    {
        if (students.Head == null)
        {
            Console.WriteLine("No students.");
            return;
        }

        int success = 0, fail = 0;
        double max = students.Head.Data.Average;
        double min = students.Head.Data.Average;
        double sum = 0;

        Node temp = students.Head;

        Console.WriteLine("\n----- Report -----");

        while (temp != null)
        {
            Student s = temp.Data;

            Console.WriteLine($"ID:{s.Id} Name:{s.Name} Avg:{s.Average} Grade:{s.Grade}");
            Console.WriteLine("Excellent: " + (s.IsExcellent ? "Yes" : "No"));

            if (s.Grade == Grade.Fail) fail++;
            else success++;

            if (s.Average > max) max = s.Average;
            if (s.Average < min) min = s.Average;

            sum += s.Average;
            temp = temp.Next;
        }

        Console.WriteLine("-------------------");
        Console.WriteLine("Success: " + success);
        Console.WriteLine("Fail: " + fail);
        Console.WriteLine("Highest Avg: " + max);
        Console.WriteLine("Lowest Avg: " + min);
        Console.WriteLine("Class Avg: " + (sum / students.Count));
        Console.ReadKey();
    }
}