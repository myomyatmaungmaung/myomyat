Create DATABASE AssWP

Create Table Register(
	UserID varchar(10) not null
	Constraint PK Primary Key (UserID),
	Username varchar(30) not null,
	email varchar(30) not null,
	password varchar(30) not null
)

Create Table Question(
	Qid varchar(20) primary key,
	UserId nvarchar(128) not null,
	SkinId varchar(20),
	QusText varchar(5000) not null,
	Phone varchar (20) null,
	constraint FK_QuesUserID foreign key(UserId) references dbo.AspNetUsers(Id),
	constraint FK_QuesSkinID foreign key(SkinId) references SkinType(SkinId)
)

Select * From Register
Select * From Question
Select * From SkinType
drop table Register
Create Table SkinType(
	SkinId varchar(20) primary key,
	Name varchar(20),
	Pic varchar(20),
	Detail varchar(1000),
)
INSERt dbo.AspNetRoles values ('A','Admin')
INSERt dbo.AspNetUserRoles values ('1eb2a8d1-9765-48cc-b9ea-c756d13ce2c2','A')

-----------------------------------------------------------------------
Create trigger trg_idgenerate_Question 
on dbo.Question
after insert as 
declare @row int 
set @row = (select COUNT(*) from dbo.Question )
declare @id varchar(20)
set @id =(select Qid from inserted)
declare @Gid varchar(20)
set @Gid = (select  CAST( (@row+1) as varchar(20)) )
set @Gid = 'Q-0'+ @Gid
update dbo.Question
set Qid=@Gid
where Qid=@id