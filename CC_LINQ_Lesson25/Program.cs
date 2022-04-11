List<Student> students = new List<Student>();
students.Add(new Student(41782,"Yaki",21, 100));
students.Add(new Student(4178222,"Shimi",33, 88));
students.Add(new Student(12341782,"David",52, 50));
students.Add(new Student(5234,"Ron",33, 74));

SelectMethod(students);

OrderAndThenMethod(students);

WhereMethod(students);

#region Select Methods

static void SelectMethod(List<Student> students)
{
    // long long way
    List<double> selectGrades = new List<double>();
    foreach (Student std in students)
    {
        selectGrades.Add(std.Grade);
    }

    //long way
    List<double> selectGrades2 = students.Select<Student, double>((Student std) => { return std.Grade; }).ToList();

    //shotest way
    List<double> selectGrades3 = students.Select(std => std.Grade).ToList();
    var selectGrades3_1 = students.Select(std => std.Grade);

    //special way
    List<double> selectGrades4 = students.Select(GetGradeDiv2).ToList();
    List<double> selectGrades5 = students.Select(GetGrade).ToList();
}

static double GetGrade(Student std)
{
    return std.Grade;
}

static double GetGradeDiv2(Student std)
{
    return std.Grade / 2;
}

#endregion

#region Order By And Then By Methods

static void OrderAndThenMethod(List<Student> students)
{
    Func<Student, int> myFunc = (std) => std.Age;
    List<Student> ordersByAge = students.OrderBy(myFunc).ToList();
    List<Student> ordersByAgeAndThenById = students.OrderBy(MyOrderByAgeMethod).ThenByDescending(std => std.Id).ToList();

    Console.WriteLine("ordersByAgeAndThenById With Lambda:");
    ordersByAgeAndThenById.ForEach(student => Console.WriteLine(student));

    Action<Student> myAct = (std) => Console.WriteLine($"From Action: {std}");
    Console.WriteLine();
    Console.WriteLine("ordersByAgeAndThenById With Action:");
    ordersByAgeAndThenById.ForEach(myAct);

    Console.WriteLine();
    Console.WriteLine("ordersByAgeAndThenById With Method:");
    ordersByAgeAndThenById.ForEach(MyForEachMethod);

}

static int MyOrderByAgeMethod(Student std)
{
    return std.Age;
}

static void MyForEachMethod(Student std)
{
    Console.WriteLine($"From Method: {std}");
}

#endregion

#region Where Method

static void WhereMethod(List<Student> students)
{
    List<Student> onlyHigherThan80OrAgeHigherFrom50 = students.Where(std => std.Grade > 85 || std.Age > 50).ToList();
    List<Student> whereByMethod = students.Where(OnlyWhereStartWithA).ToList();
    List<Student> whereByMethod2ByMyWhereMethod = MyWhereMethod(students, OnlyWhereStartWithA);
    //myStudents2 = myStudents2.Where(x => x.CourseId == 4);
    //myStudents2 = myStudents2.Where(x => x.Grade > 80 && x.CourseId == 4);
}

static bool OnlyWhereStartWithA(Student myStd)
{
    return myStd.Name.StartsWith("A");
}

static List<Student> MyWhereMethod(List<Student> students, Func<Student, bool> checkMethod)
{
    List<Student> result =  new List<Student>();
    foreach (Student curStd in students)
    {
        if (checkMethod.Invoke(curStd))
            result.Add(curStd);
    }
    return result;
}

#endregion