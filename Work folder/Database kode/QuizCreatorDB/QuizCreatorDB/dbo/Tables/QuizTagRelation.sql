--
-- Create Table    : 'QuizTagRelation'   
-- QuizId          :  (references Quiz.QuizId)
-- TagId           :  (references Tags.TagId)
--
CREATE TABLE QuizTagRelation (
    QuizId         INT NOT NULL,
    TagId          INT NOT NULL,
CONSTRAINT pk_QuizTagRelation PRIMARY KEY CLUSTERED (QuizId,TagId),
CONSTRAINT fk_QuizTagRelation FOREIGN KEY (QuizId)
    REFERENCES Quiz (QuizId)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
CONSTRAINT fk_QuizTagRelation2 FOREIGN KEY (TagId)
    REFERENCES Tag (TagId)
    ON DELETE NO ACTION
    ON UPDATE CASCADE)