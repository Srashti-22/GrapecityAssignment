use Blogging

create table UserDetails 
(
	Id				int	identity(1,1) not null Primary key,
	UserName		varchar(200) not null,
	UserPassword	varchar(10)  not null,
	UserEmail		varchar(50)  not null,
	UserContact		varchar(10)  not null,
)
create table Post
(
	Id			 int		  not null Primary Key,
	UserId		 int		   not null,
	Title		 varchar(50)   not null,
	Content		 nvarchar(max) not null,
	CreatedDate  datetime2     not null,
	ModifiedDate datetime2     not null,
	CONSTRAINT FK_Post FOREIGN KEY (UserId) REFERENCES UserDetails(Id) ON DELETE CASCADE
)
create table Comments
(
	Id			 int		   not null Primary Key,
	PostId		 int		   null,
	Content		 nvarchar(max) not null,
	CommentStatus varchar(20)  null,
	CreatedDate  datetime2     not null,
	ModifiedDate datetime2     not null,
	CONSTRAINT FK_Comments FOREIGN KEY (PostId) REFERENCES Post(Id) ON DELETE CASCADE
);

select * from  UserDetails
select * from  Post
select * from  Comments



