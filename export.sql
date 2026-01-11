-- ========================================
-- Project name: DbManagementForShop
-- Author name: Vilma Tomanová
-- Email: vtomanova33@gmail.com
-- ========================================

USE master;
GO

-- =====================
-- CREATE DATABASE
-- =====================
IF DB_ID('shop') IS NULL
BEGIN
    CREATE DATABASE shop;
END
GO

USE shop;
GO

-- =====================
-- DATABASE SETTINGS
-- =====================
ALTER DATABASE shop SET COMPATIBILITY_LEVEL = 160;
ALTER DATABASE shop SET ANSI_NULL_DEFAULT OFF;
ALTER DATABASE shop SET ANSI_NULLS OFF;
ALTER DATABASE shop SET ANSI_PADDING OFF;
ALTER DATABASE shop SET ANSI_WARNINGS OFF;
ALTER DATABASE shop SET ARITHABORT OFF;
ALTER DATABASE shop SET AUTO_CLOSE ON;
ALTER DATABASE shop SET AUTO_SHRINK OFF;
ALTER DATABASE shop SET AUTO_UPDATE_STATISTICS ON;
ALTER DATABASE shop SET CURSOR_CLOSE_ON_COMMIT OFF;
ALTER DATABASE shop SET CURSOR_DEFAULT GLOBAL;
ALTER DATABASE shop SET CONCAT_NULL_YIELDS_NULL OFF;
ALTER DATABASE shop SET NUMERIC_ROUNDABORT OFF;
ALTER DATABASE shop SET QUOTED_IDENTIFIER OFF;
ALTER DATABASE shop SET RECURSIVE_TRIGGERS OFF;
ALTER DATABASE shop SET ENABLE_BROKER;
ALTER DATABASE shop SET AUTO_UPDATE_STATISTICS_ASYNC OFF;
ALTER DATABASE shop SET DATE_CORRELATION_OPTIMIZATION OFF;
ALTER DATABASE shop SET TRUSTWORTHY OFF;
ALTER DATABASE shop SET ALLOW_SNAPSHOT_ISOLATION OFF;
ALTER DATABASE shop SET PARAMETERIZATION SIMPLE;
ALTER DATABASE shop SET READ_COMMITTED_SNAPSHOT OFF;
ALTER DATABASE shop SET HONOR_BROKER_PRIORITY OFF;
ALTER DATABASE shop SET RECOVERY SIMPLE;
ALTER DATABASE shop SET MULTI_USER;
ALTER DATABASE shop SET PAGE_VERIFY CHECKSUM;
ALTER DATABASE shop SET DB_CHAINING OFF;
ALTER DATABASE shop SET TARGET_RECOVERY_TIME = 60 SECONDS;
ALTER DATABASE shop SET DELAYED_DURABILITY = DISABLED;
ALTER DATABASE shop SET ACCELERATED_DATABASE_RECOVERY = OFF;
GO

-- =====================
-- TABLES
-- =====================

IF OBJECT_ID('artifact','U') IS NULL
CREATE TABLE artifact (
    id INT IDENTITY(1,1) PRIMARY KEY,
    title VARCHAR(40) NOT NULL,
    usage VARCHAR(50) NOT NULL,
    dangerous BIT NOT NULL
);
GO

IF OBJECT_ID('race','U') IS NULL
CREATE TABLE race (
    id INT IDENTITY(1,1) PRIMARY KEY,
    title VARCHAR(20) NOT NULL
);
GO

IF OBJECT_ID('order_status','U') IS NULL
CREATE TABLE order_status (
    id INT PRIMARY KEY,
    name VARCHAR(20) NOT NULL
);
GO

IF OBJECT_ID('customer','U') IS NULL
CREATE TABLE customer (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nickname VARCHAR(20) NOT NULL,
    race_id INT NOT NULL
);
GO

IF OBJECT_ID('list','U') IS NULL
CREATE TABLE list (
    id INT IDENTITY(1,1) PRIMARY KEY,
    artifact_id INT NOT NULL,
    quality FLOAT NOT NULL,
    price INT NOT NULL,
    CONSTRAINT CK_list_price CHECK (price > 0),
    CONSTRAINT CK_list_quality CHECK (quality > 0 AND quality < 11)
);
GO

IF OBJECT_ID('commission','U') IS NULL
CREATE TABLE commission (
    id INT IDENTITY(1,1) PRIMARY KEY,
    list_id INT NOT NULL,
    customer_id INT NOT NULL,
    order_date DATETIME NOT NULL,
    status_id INT NOT NULL
);
GO

-- =====================
-- FOREIGN KEYS
-- =====================

ALTER TABLE customer
ADD CONSTRAINT FK_customer_race
FOREIGN KEY (race_id) REFERENCES race(id);
GO

ALTER TABLE list
ADD CONSTRAINT FK_list_artifact
FOREIGN KEY (artifact_id) REFERENCES artifact(id);
GO

ALTER TABLE commission
ADD CONSTRAINT FK_commission_customer
FOREIGN KEY (customer_id) REFERENCES customer(id);
GO

ALTER TABLE commission
ADD CONSTRAINT FK_commission_list
FOREIGN KEY (list_id) REFERENCES list(id);
GO

ALTER TABLE commission
ADD CONSTRAINT FK_commission_status
FOREIGN KEY (status_id) REFERENCES order_status(id);
GO


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

INSERT INTO order_status (id, name)
VALUES 
    (1, 'Created'),
    (2, 'Canceled'),
    (3, 'Paid');


-- =====================
-- VIEWS
-- =====================
GO
CREATE OR ALTER VIEW get_pricelist AS
SELECT
    l.id,
    a.title,
    a.usage,
    a.dangerous,
    l.quality,
    l.price
FROM list l
JOIN artifact a ON l.artifact_id = a.id;
GO

CREATE OR ALTER VIEW get_commissions AS
SELECT
    c.id,
    cu.nickname,
    a.title AS artifact,
    os.id AS status_id
FROM commission c
JOIN list l ON c.list_id = l.id
JOIN artifact a ON l.artifact_id = a.id
JOIN customer cu ON cu.id = c.customer_id
JOIN race r ON cu.race_id = r.id
JOIN order_status os ON c.status_id = os.id;
GO

CREATE OR ALTER VIEW get_stats_per_order_customer AS
SELECT
    c.customer_id,
    COUNT(c.id) AS number_of_orders,
    MAX(l.price) AS most_expensive,
    MIN(l.price) AS least_expensive,
    SUM(l.price) AS the_amount_spent
FROM commission c
JOIN list l ON c.list_id = l.id
GROUP BY c.customer_id;
GO
