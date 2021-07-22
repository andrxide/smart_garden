create database smartGarden;
use smartGarden;
go

create table users (
	u_ID int identity(1,1) not null,
	u_username varchar(25) not null,
	u_password varchar(25) not null,
	constraint pk_users primary key (u_ID asc),
	constraint ak_username unique (u_username)
);
go

create table gardens (
	g_ID varchar(8) not null,
	g_createdAt datetime not null default current_timestamp,
	g_name varchar(30) not null,
	g_description varchar(1000),
	constraint pk_gardens primary key(g_ID asc)
);
go

create table garden_user (
	gu_garden varchar(8) not null,
	gu_user int not null,
	constraint fk_gardenUser_garden foreign key(gu_garden) references gardens(g_ID),
	constraint fk_gardenUser_user foreign key(gu_user) references users(u_ID)
);
go

create table sensor_type (
	st_ID varchar(3) not null,
	st_description varchar(100) not null,
	st_unit varchar(30),
	st_icon varchar(30),
	constraint pk_sensorType primary key(st_ID)
)
go

create table sensors (
	s_ID varchar(5) not null,
	s_type varchar(3) not null,
	s_name varchar(30) not null,
	constraint pk_sensors primary key(s_ID),
	constraint fk_sensors_type foreign key (s_type) references sensor_type(st_ID)
);
go

create table garden_sensor (
	gs_sensor varchar(5) not null,
	gs_garden varchar(8) not null,
	gs_minValue decimal not null,
	gs_maxValue decimal not null,
	constraint fk_gardenSensor_sensor foreign key (gs_sensor) references sensors(s_ID),
	constraint fk_gardenSensor_garden foreign key (gs_garden) references gardens(g_ID)
);
go

create table readings (
	r_ID int identity(1,1) not null,
	r_sensor varchar(5) not null,
	r_garden varchar(8) not null,
	r_timestamp datetime not null default current_timestamp,
	r_value float(5)
);
go