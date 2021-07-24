CREATE PROCEDURE pa_getAllSensors
	@gardenId varchar(8)
AS
	select 
	s_ID,
	s_name,
	st_ID,
	st_description,
	st_unit,
	st_icon,
	gs_maxValue as maxValue,
	gs_minValue as minValue
	from garden_sensor join
	sensors on gs_sensor = s_ID join
	sensor_type on s_type = st_ID
	where gs_garden = @gardenId;
go

EXEC pa_getAllSensors 'SG21AA02'
go

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
			WHEN @datedif is null THEN 0
			WHEN @datedif > 60 THEN 0
			WHEN @datedif <= 60 THEN 1
		END as 
	g_online
	FROM gardens
	WHERE g_ID = @gardenId
GO

EXEC pa_getGarden 'SG21AA02'

CREATE PROCEDURE pa_getReadingsDaily
	@sensorId varchar(5),
	@gardenId varchar(8)
as
	select TOP 30
	avg(r_value) as r_value, Convert(date, r_timestamp) as r_timestamp
	from readings 
	where r_sensor = @sensorId and r_garden = @gardenId
	group by Convert(date, r_timestamp)
	order by Convert(date, r_timestamp) desc
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

exec pa_getLastReading 'SRTMP', 'SG21AA02'



SELECT * 
  FROM smartGarden.INFORMATION_SCHEMA.ROUTINES
 WHERE ROUTINE_TYPE = 'PROCEDURE' 
   AND LEFT(ROUTINE_NAME, 3) NOT IN ('sp_', 'xp_', 'ms_')






CREATE PROCEDURE pa_postReadings
	@gardenId varchar(8),
	@tmp decimal(18,2),
	@hmd decimal(18,2),
	@soil decimal(18,2),
	@ph decimal(18,2),
	@bgt decimal(18,2)
AS
	INSERT INTO readings (r_sensor, r_garden, r_value) VALUES
	('SRTMP',@gardenId,@tmp),
	('SRHMD',@gardenId,@hmd),
	('SRSHD',@gardenId,@soil),
	('SRPHS',@gardenId,@ph),
	('SRBGT',@gardenId,@bgt);
go

EXEC pa_postReadings 'SG21AA01',25.65,65,42,4.9,123
go