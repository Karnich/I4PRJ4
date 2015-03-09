--
-- Create Table    : 'Quiz'   
-- QuizId          :  
-- QuizName        :  
--
CREATE TABLE Quiz (
    QuizId         INT NOT NULL UNIQUE,
    QuizName       NVARCHAR(MAX) NOT NULL,
CONSTRAINT pk_Quiz PRIMARY KEY CLUSTERED (QuizId))