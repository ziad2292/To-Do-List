CREATE TABLE Tasks(
	TaskID INT IDENTITY(1,1) PRIMARY KEY,
	TaskName VARCHAR(50) NOT NULL,
	StartDate DATE DEFAULT CURRENT_TIMESTAMP, 
	EndDate DATE,
	CompletionStatus VARCHAR(13) DEFAULT'Not Completed' NOT NULL,
	PriorityLevel INT DEFAULT(0) NOT NULL, 
	DescriptionText VARCHAR(500),
	Duration AS DATEDIFF(DAY, CURRENT_TIMESTAMP, EndDate),
	TaskType VARCHAR(20) NOT NULL,
	Reminder DATE,
	SubTasks INT DEFAULT(0) NOT NULL
);


CREATE TABLE subTask(
	subTaskID INT IDENTITY(1,1) PRIMARY KEY,
	subTaskName VARCHAR(25) NOT NULL,
	TaskID INT FOREIGN KEY REFERENCES Tasks(TaskID) ON DELETE CASCADE,
	CompletionStatus VARCHAR(13) DEFAULT'Not Completed' NOT NULL
);

CREATE TABLE Profile(
	UserName VARCHAR(20) DEFAULT 'Guest' PRIMARY KEY,
	PicturePath VARCHAR(200)
);

Insert INTO Profile Values('Guest', '');