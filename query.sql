-- Název projektu: DbManagementForShop
-- Jméno autora: Vilma Tomanová
-- Kontaktní údaje: vtomanova@gmail.com
create database shop;
use shop;

create table artifact(
id int primary key not null identity(1,1),
title varchar(40) not null,
usage varchar(50) not null,
dangerous bit not null
);

create table race(
id int primary key not null identity(1,1),
title varchar(20) not null
);

create table list(
id int primary key not null identity(1,1),
artifact_id int not null foreign key references artifact(id),
quality float not null check(quality > 0 and quality < 11),
price int not null check (price > 0)
);

create table customer(
id int primary key not null identity(1,1),
nickname varchar(20) not null,
race_id int not null foreign key references race(id)
);


create table order_status (
    id int primary key,
    name varchar(20) not null
);

create table commission(
id int primary key not null identity(1,1),
list_id int not null foreign key references list(id),
customer_id int not null foreign key references customer(id),
order_date datetime not null,
status_id int not null references order_status(id)
);

INSERT INTO artifact (title, usage, dangerous) VALUES
('Blade of the Ruined King', 'User or another person is healed', 0),
('Ring of Flame', 'User is granted power of flames', 1),
('Onyx Boots', 'When wearing them, user moves faster', 0),
('Arch of Guardians', 'For creating shiled, 5 meters', 0),
('Skull of the Dead', 'Creates explosion 5km', 1);

INSERT INTO race (title) VALUES
('Human'),
('Elf'),
('Dwarf'),
('Demon'),
('Troll');

INSERT INTO list (artifact_id, quality, price) VALUES
(1, 8.0, 800),
(2, 10.0, 1500),
(3, 5.0, 400),
(4, 7.0, 600),
(5, 2.0, 300);
go
create view get_commissions 
as 
select commission.id, customer.nickname, artifact.title as artifact, order_status.id as status_id
from commission 
inner join list on commission.list_id = list.id
inner join artifact on list.artifact_id = artifact.id
inner join customer on customer.id = commission.customer_id
inner join race on customer.race_id = race.id
inner join order_status on status_id = order_status.id

go
create view get_stats_per_order_customer as
select
    c.customer_id,
    count(c.id) AS number_of_orders,
    max(l.price) AS most_expensive,
    min(l.price) AS least_expensive,
    sum(l.price) AS the_amount_spent
from commission c
join list l ON c.list_id = l.id
group by c.customer_id;
go

go
create view get_pricelist
as 
select list.id, artifact.title, artifact.usage, artifact.dangerous, quality, price from list 
inner join artifact on list.artifact_id = artifact.id
go
