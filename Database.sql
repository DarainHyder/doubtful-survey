-- Database.sql
-- Create database schema for Online Survey System

CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(100) UNIQUE NOT NULL,
    Password NVARCHAR(100) NOT NULL, -- stored as plain text per requirement
    Role NVARCHAR(50) NOT NULL, -- 'SurveyBuilder', 'Surveyor', 'SurveyAdministrator'
    IsAnonymous BIT NOT NULL DEFAULT 0
);

CREATE TABLE Surveys (
    SurveyID INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX),
    CreatedBy INT FOREIGN KEY REFERENCES Users(UserID),
    CreatedAt DATETIME DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1
);

CREATE TABLE Questions (
    QuestionID INT PRIMARY KEY IDENTITY(1,1),
    SurveyID INT FOREIGN KEY REFERENCES Surveys(SurveyID),
    QuestionText NVARCHAR(MAX) NOT NULL,
    QuestionType NVARCHAR(50) NOT NULL, -- 'MultipleChoice', 'TrueFalse'
    QuestionOrder INT NOT NULL
);

CREATE TABLE AnswerOptions (
    OptionID INT PRIMARY KEY IDENTITY(1,1),
    QuestionID INT FOREIGN KEY REFERENCES Questions(QuestionID),
    OptionText NVARCHAR(255) NOT NULL,
    OptionOrder INT NOT NULL
);

CREATE TABLE SurveyResponses (
    ResponseID INT PRIMARY KEY IDENTITY(1,1),
    SurveyID INT FOREIGN KEY REFERENCES Surveys(SurveyID),
    UserID INT FOREIGN KEY REFERENCES Users(UserID) NULL, -- NULL if anonymous
    SubmittedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE ResponseAnswers (
    AnswerID INT PRIMARY KEY IDENTITY(1,1),
    ResponseID INT FOREIGN KEY REFERENCES SurveyResponses(ResponseID),
    QuestionID INT FOREIGN KEY REFERENCES Questions(QuestionID),
    SelectedOptionID INT FOREIGN KEY REFERENCES AnswerOptions(OptionID)
);

-- Seed data
INSERT INTO Users (Username, Password, Role, IsAnonymous) VALUES ('builder1', 'Builder123', 'SurveyBuilder', 0);
INSERT INTO Users (Username, Password, Role, IsAnonymous) VALUES ('surveyor1', 'Surveyor123', 'Surveyor', 0);
INSERT INTO Users (Username, Password, Role, IsAnonymous) VALUES ('admin1', 'Admin123', 'SurveyAdministrator', 0);
