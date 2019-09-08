--drop table kakeibo_rireki;
--drop table kakeibo_zankin;

create table kakeibo_rireki
(
	seq int IDENTITY(1,1) not null primary key, -- yyyyMMddHHmmss + seq6Œ…
	hiduke date default getdate() not null,
	naiyou varchar(255),
	nyukin int,
	shukkin int,
	zankin int,
	bikou varchar(255),
	insuser varchar(255),
	instime datetime default getdate(),
	upduser varchar(255),
	updtime datetime default getdate()
);

create table kakeibo_zankin
(
	zankin int,
	insuser varchar(255),
	instime datetime default getdate(),
	upduser varchar(255),
	updtime datetime default getdate(),
);

