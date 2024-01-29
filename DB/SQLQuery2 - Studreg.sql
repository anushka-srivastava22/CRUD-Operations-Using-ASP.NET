CREATE TABLE city (
    cityid INT PRIMARY KEY,
    cityname VARCHAR(255),
    stateid INT,
    FOREIGN KEY (stateid) REFERENCES state(stateid)
);