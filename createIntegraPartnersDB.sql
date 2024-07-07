IF (SELECT name FROM master.sys.databases WHERE name = N'IntegraPartners') IS NULL 
    CREATE DATABASE IntegraPartners;
ELSE
  USE Master;
  DROP DATABASE IntegraPartners;
  CREATE DATABASE IntegraPartners;
  USE IntegraPartners;
GO

CREATE TABLE Users ( 
    user_id int IDENTITY(1,1) PRIMARY KEY,
    user_name varchar(255) NULL,
    first_name varchar(255) NULL,
    last_name varchar(255) NULL,
    email varchar(255) NOT NULL,
    user_status varchar(255) NULL,
    department varchar(255) NULL,
); 