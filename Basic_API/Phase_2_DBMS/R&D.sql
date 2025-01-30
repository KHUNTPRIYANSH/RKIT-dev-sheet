Create database KPD_TPA_DB;
use KPD_TPA_DB;

create table auth_table(
id int primary key auto_increment,
username varchar(50) not null,
password varchar(50) not null,
role varchar(50) not null,
Dependencies text not null,
TokenExpiryInMinutes int not null
);