use smartGarden;
go

-- inserciones bases
insert into sensor_type (st_ID, st_description, st_mark, st_icon) values
('TMP','temperature sensor (DHT11)','°C','thermometer'),
('HMD','air humidity sensor (DHT11)','%','tint'),
('SHD','soil humidity sensor (Capacitive Soil Moisture Sensor)','%','seeding'),
('PHS','pH sensor','','tachometer-alt'),
('BGT','Brightness sensor (Photoresistor)','cd','sun');
go

insert into sensors (s_ID, s_type, s_name) values
('SRTMP','TMP','Temperature'),
('SRHMD','HMD','Air Humidity'),
('SRSHD','SHD','Soil Humidity'),
('SRPHS','PHS','pH'),
('SRBGT','BGT','Brightness');
go

-- inserciones de prueba
------ USERS
insert into users (u_username, u_password) values
('test5','asdfghjklqwertyuiopzxcvbnmdfdffffffffffffffffffffff');
insert into users (u_username, u_password) values
('test2','abc123');
insert into users (u_username, u_password) values
('test3','xyz');
------ GARDENS
insert into gardens (g_ID,g_name) values
('SG21AA01','Garden 1');
insert into garden_user (gu_garden,gu_user) values
('SG21AA01',1);

insert into gardens (g_ID,g_name) values
('SG21AA02','Garden 2');
insert into garden_user (gu_garden,gu_user) values
('SG21AA02',1);

insert into gardens (g_ID,g_name) values
('SG21AA03','Garden 3');
insert into garden_user (gu_garden,gu_user) values
('SG21AA03',1);

insert into gardens (g_ID,g_name) values
('SG21AA04','Garden a');
insert into garden_user (gu_garden,gu_user) values
('SG21AA04',3);
go

------ READINGS
insert into readings (r_sensor,r_garden,r_value) values
('SRTMP','SG21AA04', 22.5),
('SRTMP','SG21AA04', 22.8),
('SRTMP','SG21AA04', 23.1),
('SRTMP','SG21AA04', 23.4),
('SRTMP','SG21AA04', 25.9),
('SRTMP','SG21AA03', 25.9),
('SRTMP','SG21AA03', 27.6),
('SRTMP','SG21AA03', 26.4),
('SRTMP','SG21AA03', 22.1),
('SRTMP','SG21AA03', 20.8),
('SRPHS','SG21AA04', 5.6),
('SRPHS','SG21AA04', 5.4),
('SRPHS','SG21AA04', 5.0),
('SRPHS','SG21AA04', 5.1),
('SRPHS','SG21AA04', 5.9);
go

insert into readings (r_sensor,r_garden,r_timestamp,r_value) values
('SRTMP','SG21AA04',DATEADD(day, -10, CURRENT_TIMESTAMP),25.5),
('SRTMP','SG21AA04',DATEADD(day, -9, CURRENT_TIMESTAMP),28.6),
('SRTMP','SG21AA04',DATEADD(day, -8, CURRENT_TIMESTAMP),24.9),
('SRTMP','SG21AA04',DATEADD(day, -7, CURRENT_TIMESTAMP),23.7),
('SRTMP','SG21AA04',DATEADD(day, -6, CURRENT_TIMESTAMP),22.1),
('SRTMP','SG21AA04',DATEADD(day, -5, CURRENT_TIMESTAMP),27.9),
('SRTMP','SG21AA04',DATEADD(day, -4, CURRENT_TIMESTAMP),29.6),
('SRTMP','SG21AA04',DATEADD(day, -3, CURRENT_TIMESTAMP),21.8),
('SRTMP','SG21AA04',DATEADD(day, -2, CURRENT_TIMESTAMP),25.3),
('SRTMP','SG21AA04',DATEADD(day, -1, CURRENT_TIMESTAMP),19.5);