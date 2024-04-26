using System;
class Member
{

    public string firstName { get; set; }
    public string lastName { get; set; }
    public bool gender { get; set; }
    public DateOnly DOB { get; set; }
    public string phoneNumber { get; set; }
    public string birthPlace { get; set; }
    public int age { get; set; }
    public bool isGraduated { get; set; }

    public Member(string firstName,
                    string lastName,
                    bool gender,
                    DateOnly DOB,
                    string phoneNumber,
                    string birthPlace,
                    int age,
                    bool isGraduated)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.gender = gender;
        this.DOB = DOB;
        this.phoneNumber = phoneNumber;
        this.birthPlace = birthPlace;
        this.age = age;
        this.isGraduated = isGraduated;
    }
    public Member()
    {

    }

    override public string ToString()
    {
        string strGender;
        if (this.gender == true) strGender = "Male";
        else strGender = "Female";

        string strGraduated;
        if (this.isGraduated == true) strGraduated = "YES";
        else strGraduated = "NO";

        return $"{this.firstName}, {this.lastName}, {strGender}, {this.DOB}, {this.phoneNumber}, {this.birthPlace}, {this.age}, {strGraduated}";
    }

    public string fullNameToString()
    {
        return $"{this.lastName} {this.firstName}";
    }
}