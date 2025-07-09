create database Newmes

use Newmes

CREATE TABLE Candidates (
    CandidateId INT PRIMARY KEY IDENTITY,
    FullName NVARCHAR(100),
    Email NVARCHAR(100),
    Mobile NVARCHAR(20),
    Password NVARCHAR(100)
);

select * from Candidates

