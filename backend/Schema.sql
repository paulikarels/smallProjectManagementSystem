CREATE TABLE Projects(
	ProjectID INT IDENTITY (1, 1),
	ProjectName VARCHAR (255),
	ProjectDescription VARCHAR (max),
	StartDate DATE,
	EndDate DATE,
	UserID INT,
	PRIMARY KEY (ProjectID),
	FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE
)


CREATE TABLE Users(
	UserID INT IDENTITY (1, 1),
	Username VARCHAR(255) UNIQUE,
	Password VARCHAR(255),
	PRIMARY KEY (UserID)
)

CREATE TABLE Tasks(
	TaskID INT IDENTITY (1, 1),
	TaskName VARCHAR (255),
	TaskDescription VARCHAR (max),
	DueDate DATE,
	ProjectID INT,
	UserID INT,
	Status varchar(10),
	PRIMARY KEY (TaskID),
	FOREIGN KEY (UserID) REFERENCES Users(UserID) 
	FOREIGN KEY (ProjectID) REFERENCES Projects(ProjectID) ON DELETE CASCADE
)