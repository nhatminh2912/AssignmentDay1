using System;
using System.Collections;
using System.Collections.Generic;
class Program
{
    private static void Main(string[] args)
    {
        List<Member> members = new List<Member>();
        members.Add(new Member("Minh", "Hoang", true, DateOnly.Parse("December 29, 1997"), "093773788", "Ha Noi", 27, true));
        members.Add(new Member("Linh", "Nguyen", false, DateOnly.Parse("December 02, 2000"), "092222222", "Thai Binh", 24, true));
        members.Add(new Member("Xinh", "Tran", false, DateOnly.Parse("September 03, 2003"), "093333333", "Sai Gon", 21, true));
        members.Add(new Member("Thinh", "Mac", true, DateOnly.Parse("May 04, 2004"), "09444444", "Phu Tho", 20, false));
        members.Add(new Member("Dinh", "Truong", true, DateOnly.Parse("April 05, 2005"), "095555555", "Nam Dinh", 19, false));

        bool show = true;
        while (show == true)
        {
            show = MainMenu(members);
        }


    }

    public static bool MainMenu(List<Member> members)
    {
        Console.WriteLine("\n Choose an option:");
        Console.WriteLine("1. Return a list of members who is Male");
        Console.WriteLine("2. Return the oldest one based on “Age”");
        Console.WriteLine("3. Return a new list that contains Full Name only");
        Console.WriteLine("4. Return 3 lists: List of members who has birth year is 2000"
                                            + "List of members who has birth year greater than 2000"
                                            + "List of members who has birth year less than 2000");
        Console.WriteLine("5. Return the first person who was born in Ha Noi");
        Console.WriteLine("6. Clear console");
        Console.WriteLine("7. Exit");
        Console.Write("Select an option:");
        switch (Console.ReadLine())
        {
            case "1":
                Q1(members);
                return true;
            case "2":
                Q2(members);
                return true;
            case "3":
                Q3(members);
                return true;
            case "4":
                Q4(members);
                return true;
            case "5":
                Q5(members);
                return true;
            case "6":
            try{
                Console.Clear();
            }catch (Exception ex) {

            }
                return true;
            case "7":
                return false;
            default:
                return true;
        }
    }

    public static void Q1(List<Member> members)
    {
        // 1. MALE ONLY
        Console.WriteLine("LIST OF MALE MEMBERS");
        for (int i = 0; i < members.Count(); i++)
        {
            if (members[i].gender == true)
            {
                Console.WriteLine(members[i].ToString());
            }
        }
    }

    public static void Q2(List<Member> members)
    {
        // 2. OLDEST MEMBER
        Console.WriteLine("OLDEST MEMBER");
        Member oldestMember = members[0];
        for (int i = 1; i < members.Count(); i++)
        {
            if (members[i].age > oldestMember.age) oldestMember = members[i];
        }
        Console.WriteLine(oldestMember.ToString());
    }

    public static void Q3(List<Member> members)
    {
        // 3. FULL NAME LIST
        Console.WriteLine("FULL NAME LIST");
        foreach (Member member in members)
        {
            Console.WriteLine(member.fullNameToString());
        }
    }

    public static void Q4(List<Member> members)
    {
        // 4. 3 Lists
        Console.WriteLine("1. List of members who has birth year is ___");
        Console.WriteLine("2. List of members who has birth year greater than ___");
        Console.WriteLine("3. List of members who has birth year less than ___");
        Console.Write("Choose an option:");
        switch(Console.ReadLine())
        {
            case "1":
            Console.Write("Enter year:");
            if(int.TryParse(Console.ReadLine(),out int year1)){
                Q4f(members, 1, year1);
            }
            return;

            case "2":
            Console.Write("Enter year:");
            if(int.TryParse(Console.ReadLine(),out int year2)){
                Q4f(members, 2, year2);
            }
            return;

            case "3":
            Console.Write("Enter year:");
            if(int.TryParse(Console.ReadLine(),out int year3)){
                Q4f(members, 3, year3);
            }
            return;
        }
    }

    public static void Q4f(List<Member> members, int func, int year)
    {
        switch (func)
        {
            case 1:
                for (int i = 0; i < members.Count(); i++)
                {
                    if (members[i].DOB.Year == year) Console.WriteLine(members[i].ToString());
                }
            return;
            case 2:
                for (int i = 0; i < members.Count(); i++)
                {
                    if (members[i].DOB.Year > year) Console.WriteLine(members[i].ToString());
                }
            return;
            case 3:
                for (int i = 0; i < members.Count(); i++)
                {
                    if (members[i].DOB.Year < year) Console.WriteLine(members[i].ToString());
                }
            return;
        }
    }

    public static void Q5(List<Member> members)
    {
        // 5. First person born in Ha Noi
        Console.WriteLine("First person was born in Ha Noi");
        while (true)
        {
            for (int i = 0; i < members.Count(); i++)
            {
                if (members[i].birthPlace == "Ha Noi")
                {
                    Console.WriteLine(members[i].ToString());
                    return;
                }
            }
        }
    }
}