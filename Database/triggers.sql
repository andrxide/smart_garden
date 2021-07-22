create trigger DIS_garden
on gardens
after insert
as declare @garden varchar(8); 
	
	select @garden = i.g_ID from inserted i;
	
	insert into garden_sensor(gs_sensor,gs_garden,gs_minValue,gs_maxValue) values
	('SRTMP',@garden,16,32),
	('SRHMD',@garden,50,60),
	('SRSHD',@garden,60,70),
	('SRPHS',@garden,4.0,5.0),
	('SRBGT',@garden,100,150);
go