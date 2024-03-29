﻿--
-- Create Table    : 'Questions'   
-- QuestionId      :  
-- Question        :  
-- QuizId          :  (references Quiz.QuizId)
--
CREATE TABLE Question (
    QuestionId     INT IDENTITY(1,1) NOT NULL UNIQUE,
    Question       NVARCHAR(MAX) NOT NULL,
    QuizId         INT NOT NULL,
CONSTRAINT pk_Question PRIMARY KEY CLUSTERED (QuestionId),
CONSTRAINT fk_Question FOREIGN KEY (QuizId)
    REFERENCES Quiz (QuizId)
    ON DELETE CASCADE
    ON UPDATE CASCADE)