-- DROP TABLE vaultkeeps;
-- DROP TABLE vaults;
-- DROP TABLE keeps;


-- Initial Tables Setup-----------------------------------------------------------------------
-- CREATE TABLE profiles(
--     id VARCHAR(255) NOT NULL,
--     name VARCHAR(255),
--     email VARCHAR(255) NOT NULL,
--     picture VARCHAR(255),
--     PRIMARY KEY (id)
-- );

-- CREATE TABLE keeps(
--     id INT NOT NULL AUTO_INCREMENT,
--     creatorId VARCHAR(255),
--     name VARCHAR(255),
--     description VARCHAR(255),
--     img VARCHAR(255),
--     views INT,
--     shares INT,
--     keeps INT,

--     PRIMARY KEY (id),

--     FOREIGN KEY (creatorId)
--     REFERENCES profiles (id)
--     ON DELETE CASCADE
-- );

-- CREATE TABLE vaults(
--     id INT NOT NULL AUTO_INCREMENT,
--     creatorId VARCHAR(255),
--     name VARCHAR(255),
--     description VARCHAR(255),
--     isPrivate TINYINT,

--     PRIMARY KEY (id),

--     FOREIGN KEY (creatorId)
--     REFERENCES profiles (id)
--     ON DELETE CASCADE
-- );

-- CREATE TABLE vaultkeeps(
--     id INT NOT NULL AUTO_INCREMENT,
--     creatorId VARCHAR(255),
--     vaultId INT,
--     keepId INT,
    
--     PRIMARY KEY (id),

--     FOREIGN KEY (creatorId)
--     REFERENCES profiles (id)
--     ON DELETE CASCADE,
    
--     FOREIGN KEY (vaultId)
--     REFERENCES vaults (id)
--     ON DELETE CASCADE,

--     FOREIGN KEY (keepId)
--     REFERENCES keeps (id)
--     ON DELETE CASCADE
-- );