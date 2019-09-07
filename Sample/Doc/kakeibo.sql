--drop table kakeibo_rireki;
--drop table kakeibo_zankin;

create table kakeibo_rireki
(
	hiduke date default getdate(),
	naiyou varchar(20),
	nyukin int,
	shukkin int,
	bikou varchar(200),
	insuser varchar(20),
	instime datetime default getdate(),
	upduser varchar(20),
	updtime datetime default getdate()
);

create table kakeibo_zankin
(
	zankin int,
	insuser varchar(20),
	instime datetime default getdate(),
	upduser varchar(20),
	updtime datetime default getdate(),
);

