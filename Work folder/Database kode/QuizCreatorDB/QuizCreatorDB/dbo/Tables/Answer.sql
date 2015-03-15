--
-- Create Table    : 'Answers'   
-- AnswerId        :  
-- Answer          :  
-- Correct         :  
-- QuestionId      :  (references Questions.QuestionId)
--
CREATE TABLE Answer (
    AnswerId       INT IDENTITY(1,1) NOT NULL UNIQUE,
    Answer         NVARCHAR(MAX) NOT NULL,
    Correct        TINYINT NOT NULL,
    QuestionId     INT NOT NULL,
CONSTRAINT pk_Answer PRIMARY KEY CLUSTERED (AnswerId),
CONSTRAINT fk_Answer FOREIGN KEY (QuestionId)
    REFERENCES Question (QuestionId)
    ON DELETE CASCADE
    ON UPDATE CASCADE)