
DROP DATABASE Project2

INSERT INTO Branches (LibraryId,Name) Values('1','LB1')

INSERT INTO Authors(Name) Values('Author1')
INSERT INTO Authors(Name) Values('Author2')
INSERT INTO Authors(Name) Values('Author3')
INSERT INTO Authors(Name) Values('BAuthor4')


INSERT INTO Books(Title,AuthorId,GenreId) Values('BK1','1','1')

INSERT INTO Genres(Name) Values ('Genre1')
INSERT INTO Genres(Name) Values ('Genre2')
INSERT INTO Genres(Name) Values ('Genre3')



use project2
select * from Authors
select * from Books
SELECT * FROM Genres
select * from Libraries
select * from Branches
select * from Stocks


DELETE FROM Stocks WHERE StockId<10000
DELETE FROM Books WHERE BookId<10000
DELETE FROM Authors WHERE AuthorId<10000
DELETE FROM Branches WHERE BranchId<10000
DELETE FROM Genres WHERE Id<10000


DBCC CHECKIDENT (Books, RESEED, 0)
DBCC CHECKIDENT (Authors, RESEED, 0)
DBCC CHECKIDENT (Stocks, RESEED, 0)
DBCC CHECKIDENT (Branches, RESEED, 0)
DBCC CHECKIDENT (Genres, RESEED, 0)
