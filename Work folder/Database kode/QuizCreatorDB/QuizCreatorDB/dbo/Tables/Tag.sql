--
-- Create Table    : 'Tags'   
-- TagId           :  
-- Tag             :  
--
CREATE TABLE Tag (
    TagId          INT NOT NULL UNIQUE,
    Tag            NVARCHAR(75) NOT NULL UNIQUE,
CONSTRAINT pk_Tag PRIMARY KEY CLUSTERED (TagId))