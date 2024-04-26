using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentDay2
{
    internal class ListMember
    {
        List<Member> members;
        public ListMember(List<Member> members)
        {
            this.members = members;
        }

        public bool ShowMenu()
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
                    GetMaleMembers(members);
                    return true;
                case "2":
                    GetOldestMember(members);
                    return true;
                case "3":
                    GetFullNameList(members);
                    return true;
                case "4":
                    GetBirthYear(members);
                    return true;
                case "5":
                    GetFirstPersonBornInHaNoi(members);
                    return true;
                case "6":
                    try
                    {
                        Console.Clear();
                    }
                    catch (Exception ex)
                    {

                    }
                    return true;
                case "7":
                    return false;
                default:
                    return true;
            }
        }

        public static void GetMaleMembers(List<Member> members)
        {
            // 1. MALE ONLY
            Console.WriteLine("LIST OF MALE MEMBERS");
            IEnumerable<Member> malemMembers = from member in members
                                               where member.gender == true
                                               select member;

            foreach (var i in malemMembers)
            {
                Console.WriteLine(i.ToString());
            }
        }

        public static void GetOldestMember(List<Member> members)
        {
            // 2. OLDEST MEMBER
            Console.WriteLine("OLDEST MEMBER");
            IEnumerable<Member> oldestMember = from member in members
                                               where member.age == members.Max(x => x.age)
                                               select member;
            foreach (var i in oldestMember)
            {
                Console.WriteLine(i.ToString());
            }
        }

        public static void GetFullNameList(List<Member> members)
        {
            // 3. FULL NAME LIST
            Console.WriteLine("FULL NAME LIST");
            IEnumerable<Member> allMembers = from member in members
                                             select member;
            foreach (var i in allMembers)
            {
                Console.WriteLine(i.fullNameToString());
            }
        }

        public static void GetBirthYear(List<Member> members)
        {
            // 4. 3 Lists
            Console.WriteLine("1. List of members who has birth year is ___");
            Console.WriteLine("2. List of members who has birth year greater than ___");
            Console.WriteLine("3. List of members who has birth year less than ___");
            Console.Write("Choose an option:");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.Write("Enter year:");
                    if (int.TryParse(Console.ReadLine(), out int year1))
                    {
                        BaseOnFunction(members, 1, year1);
                    }
                    return;

                case "2":
                    Console.Write("Enter year:");
                    if (int.TryParse(Console.ReadLine(), out int year2))
                    {
                        BaseOnFunction(members, 2, year2);
                    }
                    return;

                case "3":
                    Console.Write("Enter year:");
                    if (int.TryParse(Console.ReadLine(), out int year3))
                    {
                        BaseOnFunction(members, 3, year3);
                    }
                    return;
            }
        }

        public static void BaseOnFunction(List<Member> members, int func, int year)
        {
            switch (func)
            {
                // Equal
                case 1:
                    IEnumerable<Member> equalBirthDate = from member in members
                                                         where member.DOB.Year == year
                                                         select member;
                    foreach (var i in equalBirthDate)
                    {
                        Console.WriteLine(i.ToString());
                    }
                    return;
                // Greater
                case 2:
                    IEnumerable<Member> greaterBirthDate = from member in members
                                                           where member.DOB.Year > year
                                                           select member;
                    foreach (var i in greaterBirthDate)
                    {
                        Console.WriteLine(i.ToString());
                    }
                    return;
                // Lesser
                case 3:
                    IEnumerable<Member> lesserBirthDate = from member in members
                                                          where member.DOB.Year < year
                                                          select member;
                    foreach (var i in lesserBirthDate)
                    {
                        Console.WriteLine(i.ToString());
                    }
                    return;
            }
        }

        public static void GetFirstPersonBornInHaNoi(List<Member> members)
        {
            // 5. First person born in Ha Noi
            Console.WriteLine("First person was born in Ha Noi");
            while (true)
            {
                IEnumerable<Member> personBornInHaNoi = from member in members
                                                        where member.birthPlace == "Ha Noi"
                                                        select member;
                Member firstPerson = personBornInHaNoi.FirstOrDefault();
                if (firstPerson != null)
                {
                    Console.WriteLine(firstPerson.ToString());
                }
                else { Console.WriteLine("No one born in Ha Noi"); }
                break;
            }
        }
    }
}
