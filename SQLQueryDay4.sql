--Task 1: Create a Database

Create Database [5077_DB];

USE [5077_DB];

--Task 2: Create Tables
CREATE TABLE Courses (
	CourseID int NOT NULL IDENTITY (101,1),
	CourseName varchar (100),
	PRIMARY KEY (CourseID)
    )

CREATE TABLE Students (
	StudentID int NOT NULL IDENTITY (1,1),
	FirstName varchar (50),
	LastName varchar (50),
	Age int,
	CourseID int,
	PRIMARY KEY (StudentID),
	FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
	)

--1.List the names of students who are not enrolled in any course.
Select Students.FirstName, Students.LastName, Students.Age
From Students
Where Students.CourseID IS NULL


--Task 3: Insert Data
Insert into Courses(CourseName) 
Values ('Mechanics'), 
('Dynamics'),
('English'),
('Maths'),
('Urdu')
Select*From Courses

Insert into Students(FirstName, LastName, Age, CourseID) 
Values 
('Ahmed', 'Ali', 20, 102),
('Qureshi', 'Ali', 23, 101),
('Fahad', 'Ali', 22, 104),
('Noman', 'Ali', 19, 104),
('Noufil', 'Ali', 28, 104),
('Haider', 'Ali', 27, 102),
('Babar', 'Ali', 25, NULL),
('Haris', 'Ali', 23, 101),
('Shadab', 'Ali', 24, 103),
('Abdullah', 'Ali', 22, 105)
Select*From Students

SELECT Students.StudentID, Courses.CourseName, Students.FirstName, Students.Age
FROM Students
JOIN Courses ON Students.CourseID = Courses.CourseID;

--Task 4: Update and Delete Records
UPDATE Students 
SET Age = 33 
WHERE StudentID = 9

DELETE FROM Students
WHERE StudentID = 10;

--Task 5: Queries and Filters

SELECT * FROM Students
WHERE Age >= 20;

Select Students.StudentID, Students.FirstName, Students.Age, Students.CourseID, Courses.CourseName From Students
JOIN Courses ON Students.CourseID = Courses.CourseID
WHERE Students.CourseID = 104;

--Task 6: Aggregation Functions
Select COUNT(StudentID) --Finds the no. of unique entries (PK) to return the no. of students 
From Students

Select AVG(Age)
From Students

--Task 7: Stored Procedures

--7.1 Create a stored procedure to add a new student. 
CREATE PROCEDURE AddStudent
    
	@newFirstName VARCHAR(50),
	@newLastName VARCHAR(50),
    @newAge INT,
    @newCourseID INT
AS
BEGIN
    INSERT INTO Students (FirstName, LastName, Age, CourseID)
    VALUES (@newFirstName, @newLastName, @newAge, @newCourseID);
END;
GO

--7.2 Create a stored procedure to update a student's age.
CREATE PROCEDURE UpdateStudentAge
    @ID INT,
    @NewAge INT
AS
BEGIN
    UPDATE Students
    SET Age = @NewAge
    WHERE StudentID = @ID;
END;
GO

--7.3 Create a stored procedure to delete a student.
CREATE PROCEDURE DeleteStudent
    @ID INT    
AS
BEGIN
    DELETE FROM Students 
    WHERE StudentID = @ID;
END;
GO

----7.4 Create a stored procedure to list all students.
CREATE PROCEDURE ListAllStudents
AS
BEGIN
    SELECT*FROM Students 
END;
GO


--Task 9: Advanced Reports in Console Application

--9.1 Create a stored procedure to List the names of students who are not enrolled in any course
CREATE PROCEDURE StudentsNotEnrolled
AS
BEGIN
	Select Students.FirstName, Students.LastName, Students.Age
	From Students
	Where Students.CourseID IS NULL
END;
GO

--9.2 Create a stored procedure to find the most popular course (the course with the most students enrolled).
CREATE PROCEDURE MostPopularCourse
AS
BEGIN
	SELECT TOP 1 Courses.CourseName, Courses.CourseID, COUNT(Students.StudentID) AS StudentsInCourse
	FROM Students
	Right JOIN Courses ON Students.CourseID = Courses.CourseID
	WHERE Students.CourseID IS NOT NULL
	GROUP BY Courses.CourseName, Courses.CourseID
	ORDER BY StudentsInCourse DESC;
END;
GO

--9.3 Create a stored procedure to List the students who are older than the average age of students
CREATE PROCEDURE OlderStudents
AS
BEGIN
	SELECT Students.FirstName, Students.LastName, Students.Age
	FROM Students
	WHERE Students.Age > (Select AVG(Age) From Students);
END;
GO


--9.4. Create a stored procedure to Find the total number of students and average age for each course.
CREATE PROCEDURE Course_Students_and_AvgAge
AS
BEGIN
	SELECT Courses.CourseName, COUNT(Students.StudentID) AS TotalStudents, AVG(Students.Age) AS AverageAge
	FROM Students
	Right JOIN Courses ON Students.CourseID = Courses.CourseID
	GROUP BY Courses.CourseName;
END;
GO

--9.5. Create a stored procedure to List the courses that have no students enrolled in them.
CREATE PROCEDURE CoursesWithNoStudents
AS
BEGIN
	SELECT CourseID, CourseName 
	From Courses
	Where CourseID NOT IN (Select CourseID From Students WHERE CourseID is NOT NULL);
END;
GO

--9.6. Create a stored procedure to List students who share courses with a specific student (choose one from your records).
CREATE PROCEDURE SharedCourseStudents
AS
BEGIN
	Select Students.FirstName, Students.StudentID, Students.Age, Students.CourseID, Courses.CourseName
	From Students
	RIGHT JOIN Courses ON Students.CourseID = Courses.CourseID	
	Where Students.CourseID =
	(
	Select Courses.CourseID								
	From Students
	JOIN Courses ON Students.CourseID = Courses.CourseID		
	Where Students.StudentID = 3							
	)
END;
GO

--9.7 Create a stored procedure to list the youngest and oldest student for each course
CREATE PROCEDURE OldAndYoung
AS
BEGIN
	Select 
	Courses.CourseID, MIN(FirstName) as Youngest_Student, MIN(AGE) as Min_Age, MAX(FirstName) as Oldest_Student, MAX(Age) as Max_Age
	From Courses
	Left Join Students on Courses.CourseID = Students.CourseID
	Group by Courses.CourseID
END;
GO

	












