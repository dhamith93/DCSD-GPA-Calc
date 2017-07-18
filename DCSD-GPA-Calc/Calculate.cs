using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCSD_GPA_Calc
{
    class Calculate
    {
        private Dictionary<string, int> subjectsWithCredits = new Dictionary<string, int>();
        private Dictionary<string, double> gradePoints = new Dictionary<string, double>();

        public Calculate()
        {
            subjectsWithCredits.Add("Introduction to Computer Science", 3);
            subjectsWithCredits.Add("Introduction to Computer", 3);
            subjectsWithCredits.Add("Quantitative Techniques for Computing", 2);
            subjectsWithCredits.Add("Programming Fundamentals", 3);
            subjectsWithCredits.Add("Database Management Systems", 3);
            subjectsWithCredits.Add("Database Management System", 3);
            subjectsWithCredits.Add("Computer Technology", 3);
            subjectsWithCredits.Add("Business Information System", 2);
            subjectsWithCredits.Add("Business Information Systems", 2);
            subjectsWithCredits.Add("Object Oriented Programming C++", 3);
            subjectsWithCredits.Add("Object Oriented Programming  C++", 3);
            subjectsWithCredits.Add("Object Oriented Programming with C++", 3);
            subjectsWithCredits.Add("System Analysis and Design", 5);
            subjectsWithCredits.Add("C# Programming", 3);
            subjectsWithCredits.Add("C#.Net Programming", 3);
            subjectsWithCredits.Add("Programming in C#", 3);
            subjectsWithCredits.Add("Computer Architecture and Networking", 4);
            subjectsWithCredits.Add("System Software", 4);
            subjectsWithCredits.Add("Internet Technology", 5);
            subjectsWithCredits.Add("Programming in Java", 3);
            subjectsWithCredits.Add("Computer System Design Project", 10);

            gradePoints.Add("A+", 4);
            gradePoints.Add("A", 4);
            gradePoints.Add("A-", 3.7);
            gradePoints.Add("B+", 3.3);
            gradePoints.Add("B", 3);
            gradePoints.Add("B-", 2.7);
            gradePoints.Add("C+", 2.3);
            gradePoints.Add("C", 2.0);
            gradePoints.Add("C-", 1.7);
            gradePoints.Add("D+", 1.3);
            gradePoints.Add("D", 1);
            gradePoints.Add("E", 0);
            gradePoints.Add("X", 0);
        }  
        
        public double GPA(Dictionary<string, string> gradeInfo)
        {
            int totalCredits = 0;
            double totalGrades = 0;            

            foreach (var subject in subjectsWithCredits)
            {
                if (gradeInfo.ContainsKey(subject.Key))
                {
                    if (gradeInfo[subject.Key] != "X")
                    {
                        totalCredits += subject.Value;
                        totalGrades += (gradePoints[gradeInfo[subject.Key]] * subject.Value);
                    }
                }
            }
            
            if (totalGrades > 0) return (totalGrades / totalCredits);

            return 0;
        }

    }
}
