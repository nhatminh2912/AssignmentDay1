using AssignmentDay2;
using System;
class Program
{
    public static async Task Main(string[] args)
    {
        List<Member> members = new List<Member>();
        members.Add(new Member("Minh", "Hoang", true, DateOnly.Parse("December 29, 1997"), "093773788", "Ha Noi", 27, true));
        members.Add(new Member("Linh", "Nguyen", false, DateOnly.Parse("December 02, 2000"), "092222222", "Thai Binh", 24, true));
        members.Add(new Member("Xinh", "Tran", false, DateOnly.Parse("September 03, 2003"), "093333333", "Sai Gon", 21, true));
        members.Add(new Member("Thinh", "Mac", true, DateOnly.Parse("May 04, 2004"), "09444444", "Phu Tho", 20, false));
        members.Add(new Member("Dinh", "Truong", true, DateOnly.Parse("April 05, 2005"), "095555555", "Nam Dinh", 19, false));

        ListMember listMember = new ListMember(members);
        AsyncPrimeNumber asyncPrimeNumber = new AsyncPrimeNumber();

        Console.WriteLine("1.List members using LinQ \n 2.Get prime number for a range using asynchronous programming ");
        Console.WriteLine("Choose an option:");
        if (int.TryParse(Console.ReadLine(), out int option))
        {
            switch (option)
            {
                case 1:
                    bool show = true;
                    while (show == true)
                    {
                        show = listMember.ShowMenu();
                    }
                    return;
                case 2:
                    await asyncPrimeNumber.ShowMenu();
                    return;
            }
        }
    }


}
