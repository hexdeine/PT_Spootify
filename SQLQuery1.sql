CREATE DATABASE SpootifyDB;
CREATE TABLE tblPlaylist(SongNo INT IDENTITY, SongName VARCHAR(100), Artist VARCHAR(50), Album VARCHAR(50));
SELECT * FROM tblPlaylist;
DROP TABLE tblPlaylist;