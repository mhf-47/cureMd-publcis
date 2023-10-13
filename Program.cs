using System;
using System.Data;
using System.Data.SqlClient;

class Program
{
    public static void Main()
    {

        string ConnectionString = "Server=cmdlhrdb01; Database=5077_DB; Trusted_Connection=TRUE;";
        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            //8.1 Execute stored procedure to List all students
            using (SqlCommand command = new SqlCommand("ListAllStudents", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("StudentID: " + reader["StudentID"] + ",FirstName: " + reader["FirstName"] + ",LastName: " + reader["LastName"] + ",Age: " + reader["Age"] + ",CourseID: " + reader["CourseID"]);
                }
                connection.Close();
            }

            
            //8.2 Execute stored procedure to add a new student
            using (SqlCommand command = new SqlCommand("AddStudent", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@newFirstName", "Hamza");
                command.Parameters.AddWithValue("@newLastName", "Qadir");
                command.Parameters.AddWithValue("@newAge", 45);
                command.Parameters.AddWithValue("@newCourseID", 105);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            
            //8.3 Execute stored procedure to Update a student's Age
            using (SqlCommand command = new SqlCommand("UpdateStudentAge", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID", 3);
                command.Parameters.AddWithValue("@newAge", 34);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            
            //8.4 Execute stored procedure to Delete a Student
            using (SqlCommand command = new SqlCommand("DeleteStudent", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID", 2);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            //8.1 Execute stored procedure to List all students
            using (SqlCommand command = new SqlCommand("ListAllStudents", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("StudentID: " + reader["StudentID"] + ",FirstName: " + reader["FirstName"] + ",LastName: " + reader["LastName"] + ",Age: " + reader["Age"] + ",CourseID: " + reader["CourseID"]);
                }
                connection.Close();
            }

            Console.WriteLine("--------------------------------------------------------");

            //9.1 Execute stored procedure to List all students Not enrolled in any course
            using (SqlCommand command = new SqlCommand("StudentsNotEnrolled", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("FirstName: " + reader["FirstName"] + " ,LastName: " + reader["LastName"] + " ,Age: " + reader["Age"]);
                }
                connection.Close();
            }

            Console.WriteLine("--------------------------------------------------------");

            //9.2 Execute stored procedure to find the most popular course (the course with the most students enrolled).
            using (SqlCommand command = new SqlCommand("MostPopularCourse", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("CourseName: " + reader["CourseName"] + " ,CourseID: " + reader["CourseID"] + " ,StudentsInCourse: " + reader["StudentsInCourse"]);
                }
                connection.Close();
            }

            Console.WriteLine("--------------------------------------------------------");


            //9.3 Execute a stored procedure to List the students who are older than the average age of students
            using (SqlCommand command = new SqlCommand("OlderStudents", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("FirstName: " + reader["FirstName"] + " ,LastName: " + reader["LastName"] + " ,Age: " + reader["Age"]);
                }
                connection.Close();
            }

            Console.WriteLine("--------------------------------------------------------");

            //9.4 Execute a stored procedure to Find the total number of students and average age for each course.
            using (SqlCommand command = new SqlCommand("Course_Students_and_AvgAge", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("CourseName: " + reader["CourseName"] + " ,TotalStudents: " + reader["TotalStudents"] + " ,AverageAge: " + reader["AverageAge"]);
                }
                connection.Close();
            }

            Console.WriteLine("--------------------------------------------------------");

            //9.5 Execute a stored procedure to List the courses that have no students enrolled in them.
            using (SqlCommand command = new SqlCommand("CoursesWithNoStudents", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("CourseID: " + reader["CourseID"] + " ,CourseName: " + reader["CourseName"]);
                }
                connection.Close();
            }

            Console.WriteLine("--------------------------------------------------------");

            //9.6 Execute a stored procedure to List students who share courses with a specific student (choose one from your records).
            using (SqlCommand command = new SqlCommand("SharedCourseStudents", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("FirstName: " + reader["FirstName"] + " ,StudentID: " + reader["StudentID"] + " ,Age: " + reader["Age"] + " ,CourseID: " + reader["CourseID"] + " ,CourseName: " + reader["CourseName"]);
                }
                connection.Close();
            }

            Console.WriteLine("--------------------------------------------------------");

            //9.7 Execute a stored procedure to list the youngest and oldest student for each course
            using (SqlCommand command = new SqlCommand("OldAndYoung", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("CourseID: " + reader["CourseID"] + " ,Youngest_Student: " + reader["Youngest_Student"] + " ,Min_Age: " + reader["Min_Age"] + " ,Oldest_Student: " + reader["Oldest_Student"] + " ,Max_Age: " + reader["Max_Age"]);
                }
                connection.Close();
            }

            Console.WriteLine("--------------------------------------------------------");

            Console.WriteLine("Press enter to close...");
            Console.ReadLine();

        }

    }
}