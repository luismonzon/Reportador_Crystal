create table LSA_USUARIOS (
username varchar(50) not null,
password  VARBINARY(250) not null,
Constraint pk_LSA_USUARIOS_username primary Key (username)
);

create table LSA_DEPTOS(
id_departamento int identity(1,1) not null,
nombre_departamento varchar(100) not null,
Constraint pk_LSA_DEPTOS_id_dep primary Key (id_departamento)
);

create table LSA_REPORTES(
id_reporte int identity (1,1) not null,
nombre_reporte varchar(100) null,
path_reporte varchar(100) not null,
Constraint pk_LSA_REPORTES_id_rep primary Key (id_reporte)  
);


create table LSA_USER_DEP(
username varchar(50) not null,
id_departamento int not null,
Constraint pk_LSA_USER_DEP primary Key (username,id_departamento),
Constraint fk_LSA_USER_DEP_username Foreign Key (username) references LSA_USUARIOS(username),
Constraint fk_LSA_USER_DEP_id_dep Foreign Key (id_departamento) references LSA_DEPTOS(id_departamento)
);


create table LSA_REPORT_DEP(
id_reporte int not null,
id_departamento int not null,
Constraint pk_LSA_REPORT_DEP primary Key (id_reporte,id_departamento),
Constraint fk_LSA_REPORT_DEP_id_rep Foreign Key (id_reporte) references LSA_REPORTES(id_reporte),
Constraint fk_LSA_REPORT_DEP_id_dep Foreign Key (id_departamento) references LSA_DEPTOS(id_departamento)
);



drop table LSA_REPORT_DEP
drop table LSA_USER_DEP
Drop table LSA_REPORTES
drop table LSA_DEPTOS
drop table LSA_USUARIOS


insert into LSA_USUARIOS (username,password) (select name, password_hash FROM sys.sql_logins);
insert into LSA_DEPTOS (nombre_departamento) values ('Contabilidad'),('Costos'),('CxP'),('CxC'),('Produccion'),( 'Ventas'),( 'Inventario');


insert into LSA_REPORT_DEP (id_reporte,id_departamento) values (1,1),(2,1);



select X.*, Z.nombre_reporte, Z.path_reporte from (
	select A.id_departamento, A.nombre_departamento, B.id_reporte  from LSA_DEPTOS A Left Join LSA_REPORT_DEP B on A.id_departamento=B.id_departamento )X  Left Join LSA_REPORTES Z on X.id_reporte=Z.id_reporte



