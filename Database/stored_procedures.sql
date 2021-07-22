use smartGarden
go

select g_ID, g_createdAt, g_name
from garden_user 
join gardens on gu_garden = g_ID
where gu_user = 1
go



CREATE PROCEDURE pa_sensorType
	@id varchar(3)
as
	SELECT st_ID, st_description, st_unit, st_icon FROM sensor_type
	where st_ID = @id;
go

CREATE PROCEDURE pa_sensor
	@id varchar(5)
as
	select s_ID, s_type, s_name from sensors
	where s_ID = @id;
go

CREATE PROCEDURE pa_garden_sensor
	@gardenId varchar(8)
as
	SELECT gs_sensor, gs_garden, gs_minValue, gs_maxValue FROM garden_sensor
	WHERE gs_garden = @gardenId
go

execute pa_garden_sensor 'SG21AA01';
GO

CREATE PROCEDURE pa_getGardenSensor
	@gardenId varchar(8),
	@sensorId varchar(5)
as
	SELECT gs_sensor, gs_garden, gs_minValue, gs_maxValue FROM garden_sensor
	WHERE gs_garden = @gardenId AND
	gs_sensor = @sensorId
go

execute pa_getGardenSensor 'SG21AA01','SRTMP'
go
/*CREATE PROCEDURE pa_insertReadings
@jardin varcha(8)
	@ph float,
	@
as
	insert into readings (r_sensor, r_garden, r_timestamp, r_value) values
	('SRPHS',@jardin, CURRENT_TIMESTAMP, @ph),
	('SRTMP',@jardin, CURRENT_TIMESTAMP, @tmp),
	(),
	()
	()
go

execute pa_insertReadings '','','','','';*/

/*
	Consulta que regrese todas las lecturas de un sensor en especifico de cierto jardin,
	las lecturas deben estar agrupadas por dia y ordenadas desc por fecha,

*/

CREATE PROCEDURE pa_getReadingsDaily
	@sensorId varchar(5),
	@gardenId varchar(8)
as
	select avg(r_value) as r_value, Convert(date, r_timestamp) as r_timestamp
	from readings 
	where r_sensor = @sensorId and r_garden = @gardenId
	group by Convert(date, r_timestamp)
	order by Convert(date, r_timestamp) desc
go



/*select avg(r_value) as r_value, Convert(date, r_timestamp) as r_timestamp
from readings 
where r_sensor = 'SRTMP' and r_garden = 'SG21AA04'
group by Convert(date, r_timestamp)
order by Convert(date, r_timestamp) desc*/


execute pa_getReadingsDaily 'SRTMP','SG21AA04'
go

CREATE PROCEDURE pa_getReadingsHourly
	@sensorId varchar(5),
	@gardenId varchar(8)
as
	select 
	TOP 24
	avg(r_value) as r_value, 
	DATEADD(hour, DATEDIFF(hour, 0, r_timestamp), 0) as r_timestamp
	from readings 
	where r_sensor = @sensorId and r_garden = @gardenId
	group by DATEADD(hour, DATEDIFF(hour, 0, r_timestamp), 0)
	order by DATEADD(hour, DATEDIFF(hour, 0, r_timestamp), 0) desc
go

exec pa_getReadingsHourly 'SRTMP','SG21AA02'
go

/*select 
avg(r_value) as r_value, 
DATEADD(hour, DATEDIFF(hour, 0, r_timestamp), 0) as r_timestamp
from readings 
where r_sensor = 'SRTMP' and r_garden = 'SG21AA02'
group by DATEADD(hour, DATEDIFF(hour, 0, r_timestamp), 0)
order by DATEADD(hour, DATEDIFF(hour, 0, r_timestamp), 0) desc*/

CREATE PROCEDURE pa_getLastReading
	@sensorId varchar(5),
	@gardenId varchar(8)
as
	SELECT TOP 1 
	r_value, 
	r_timestamp
	from readings
	where r_sensor = @sensorId and r_garden = @gardenId
	order by r_timestamp desc;
go

exec pa_getLastReading 'SRTMP','SG21AA02'
go

CREATE PROCEDURE pa_postReadings
	@gardenId varchar(8),
	@tmp float(5),
	@hmd float(5),
	@soil float(5),
	@ph float(5),
	@bgt float(5)
AS
	INSERT INTO readings (r_sensor, r_garden, r_value) VALUES
	('SRTMP',@gardenId,@tmp),
	('SRHMD',@gardenId,@hmd),
	('SRSHD',@gardenId,@soil),
	('SRPHS',@gardenId,@ph),
	('SRBGT',@gardenId,@bgt);
go

execute pa_postReadings 'SG21AA01',25.65,65,42,4.9,123
GO

execute pa_getGardenSensor 'SG21AA01','SRTMP'
GO

CREATE PROCEDURE pa_getGarden
	@gardenId varchar(8)
AS
	DECLARE @datedif int;
	SET @datedif = DATEDIFF(SECOND,(select TOP 1 r_timestamp from readings where r_garden = @gardenId order by r_timestamp desc), CURRENT_TIMESTAMP);
	
	SELECT g_ID, 
	g_createdAt, 
	g_name, 
	g_description,
		CASE 
			WHEN @datedif > 60 THEN 0
			WHEN @datedif <= 60 THEN 1
		END as 
	g_online
	FROM gardens
	WHERE g_ID = @gardenId
GO

CREATE PROCEDURE pa_gardensUser
	@user_id int
as
	
	SELECT g_ID, 
	g_createdAt, 
	g_name, 
	g_description,
	CASE 
			WHEN DATEDIFF(SECOND,(select TOP 1 r_timestamp from readings where r_garden = g_id order by r_timestamp desc), CURRENT_TIMESTAMP) is null THEN 0
			WHEN DATEDIFF(SECOND,(select TOP 1 r_timestamp from readings where r_garden = g_id order by r_timestamp desc), CURRENT_TIMESTAMP) > 60 THEN 0
			WHEN DATEDIFF(SECOND,(select TOP 1 r_timestamp from readings where r_garden = g_id order by r_timestamp desc), CURRENT_TIMESTAMP) <= 60 THEN 1
		END as 
	g_online
	from garden_user
	join gardens on gu_garden = g_ID
	where gu_user = @user_id;
go

EXECUTE pa_gardensUser 1

select DATEDIFF(SECOND,(select TOP 1 r_timestamp from readings where r_garden = 'SG21AA01' order by r_timestamp desc), CURRENT_TIMESTAMP)