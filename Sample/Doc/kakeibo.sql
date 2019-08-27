--drop table kakeibo_rireki;
--drop table kakeibo_zankin;

create table kakeibo_rireki
(
	hiduke date,
	naiyou varchar(20),
	nyukin int,
	shukkin int,
	bikou varchar(200),
	insuser varchar(20),
	instime date,
	upduser varchar(20),
	updtime date,
);

create table kakeibo_zankin
(
	zankin int,
	insuser varchar(20),
	instime date,
	upduser varchar(20),
	updtime date,
);

