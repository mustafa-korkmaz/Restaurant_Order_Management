-- phpMyAdmin SQL Dump
-- version 3.5.2
-- http://www.phpmyadmin.net
--
-- Anamakine: localhost
-- Üretim Zamanı: 09 Şub 2014, 00:07:10
-- Sunucu sürümü: 5.5.25a
-- PHP Sürümü: 5.4.4

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Veritabanı: `cafecasta`
--

DELIMITER $$
--
-- Yordamlar
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `addCustomer`(IN `cName` TEXT, IN `cTel` VARCHAR(20), IN `cMail` VARCHAR(50), IN `cAddress` TEXT, IN `cNote` TEXT, OUT `id` INT)
BEGIN

DECLARE m_date datetime;
DECLARE reg_date datetime;

SET m_date = now();
SET reg_date = now();

INSERT INTO customer_details(customer_name,customer_tel,customer_mail,customer_address,customer_note,register_date,modified_date,is_deleted) VALUES(cName,cTel,cMail,cAddress,cNote,reg_date,m_date,0);

SET id = (SELECT LAST_INSERT_ID());
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `addGoods`(IN `name` VARCHAR(50), IN `unit` VARCHAR(4))
    NO SQL
BEGIN
DECLARE m_date datetime;
DECLARE reg_date datetime;

SET m_date = now();
SET reg_date = now();

INSERT INTO goods_details(goods_name,unit,create_date,modified_date,is_deleted) VALUES(name,unit,reg_date,m_date,0);
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `addMenuItem`(IN `product_id` INT, IN `order_id` INT, IN `price` DOUBLE, IN `amount` INT, IN `porsion` DOUBLE, IN `product_desc` TEXT)
    NO SQL
BEGIN

INSERT INTO product_to_order
(product_id,order_id,product_price,amount,porsion,product_desc) 
VALUES(product_id,order_id,price,amount,porsion,product_desc);

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `addOption`(IN `name` VARCHAR(100))
    NO SQL
BEGIN

DECLARE m_date datetime;

SET m_date = now();

INSERT INTO option_details(option_name,create_date,modified_date,is_deleted) VALUES(name,m_date,m_date,0);
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `addOptionToProduct`(IN `product_id` INT, IN `option_id` INT)
    NO SQL
BEGIN

INSERT INTO options_to_product(product_id,option_id,is_deleted) VALUES(product_id,option_id,0);

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `addPrinter`(IN `ip` VARCHAR(30), IN `_desc` TEXT, IN `pType` INT)
    NO SQL
BEGIN


INSERT INTO printer_details(printer_ip,type,printer_desc,is_deleted) VALUES(ip,pType,_desc,0);
	
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `addTable`(IN `name` VARCHAR(20), IN `category` INT, IN `status` INT, IN `display_order` INT)
    NO SQL
BEGIN

DECLARE m_date datetime;

SET m_date = now();

INSERT INTO table_details(table_name,table_category,table_status,create_date,modified_date,display_order,is_deleted) VALUES(name,category,status,m_date,m_date,display_order,0);
	
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `addTableCategory`(IN `name` VARCHAR(20), IN `status` INT, IN `display_order` INT)
    NO SQL
BEGIN

DECLARE m_date datetime;

SET m_date = now();

INSERT INTO table_categories(tcategory_name,tcategory_status,create_date,modified_date,display_order,is_deleted) VALUES(name,status,m_date,m_date,display_order,0);
	
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `changeTable`(IN `acc_id` INT, IN `ex_table_id` INT, IN `new_table_id` INT)
    NO SQL
BEGIN
update account_details set account_owner=new_table_id
where account_status=1 and account_id=acc_id and account_owner=ex_table_id;

update table_details set table_status=4 where table_id= new_table_id;
update table_details set table_status=3 where table_id= ex_table_id;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `customerPayment`(IN `customer_id` INT,IN `sell_id` INT, IN `total_price` DOUBLE, IN `payment` DOUBLE, IN `type` INT, IN `payment_desc` TEXT)
BEGIN

DECLARE m_date datetime;
SET m_date = now();

INSERT INTO `customer_payments` (customer_id,sell_id,total_price,payment,type,payment_desc,payment_date) VALUES (customer_id,sell_id,total_price,payment,type,payment_desc,m_date);	

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `doPayment`(IN `sell_id` INT, IN `payment_type` INT, IN `payment_price` DOUBLE, IN `bank_id` INT, IN `instalment` INT, IN `rate` DOUBLE(11,3))
BEGIN

DECLARE payment_id int(11);
DECLARE m_date datetime;
SET m_date = now();


INSERT INTO payment_details(sell_id,payment_type,payment_price,modified_date) VALUES (sell_id,payment_type,payment_price,m_date);
SET payment_id = (SELECT LAST_INSERT_ID());
if payment_type = 1 then
	INSERT INTO payment_to_bank(payment_id,bank_id,modified_date) VALUES (payment_id,bank_id,m_date);	
	UPDATE bank_details bd SET bd.total = bd.total + payment_price, modified_date = m_date WHERE bd.bank_id = bank_id;
	insert into bank_logs (bank_id,amount,instalment,rate,type,modified_date) values (bank_id,payment_price,instalment,rate, payment_type,m_date);
end if;

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `editCustomer`(IN `id` INT, IN `cName` TEXT, IN `cTel` VARCHAR(20), IN `cMail` VARCHAR(50), IN `cAddress` TEXT, IN `cNote` TEXT)
BEGIN

DECLARE m_date datetime;

SET m_date = now();

UPDATE customer_details SET  customer_name=cName ,customer_tel=cTel,customer_mail=cMail,customer_address=cAddress,customer_note=cNote,modified_date=m_date where customer_id=id;
	
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `editGoods`(IN `name` VARCHAR(50), IN `unit` VARCHAR(4), IN `id` INT)
    NO SQL
BEGIN

DECLARE m_date datetime;

SET m_date = now();

UPDATE goods_details SET  goods_name=name,unit=unit ,modified_date=m_date where goods_id=id;
	
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `editPrinter`(IN `id` INT, IN `ip` VARCHAR(30), IN `_desc` TEXT, IN `pType` INT)
    NO SQL
BEGIN

update printer_details set printer_ip=ip,type=pType,printer_desc=_desc
where printer_id=id;	
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `editProduct`(IN `pCode` VARCHAR(20), IN `pImg` TEXT, IN `pCat` INT(11), IN `pName` VARCHAR(30), IN `pDesc` TEXT, IN `pId` INT(11), IN `pPrice` DOUBLE, IN `isOnMenu` INT(11), IN `pUnit` INT, IN `min_size` INT, IN `max_size` INT, IN `pPrice_bucuk` DOUBLE, IN `pPrice_double` DOUBLE)
BEGIN

DECLARE m_date datetime;
SET m_date = now();

	update product_details set product_code_manual=pCode,product_img_path=pImg,product_cat=pCat,product_name=pName,product_desc=pDesc,isOnMenu=isOnMenu,modified_date=m_date, unit=pUnit where product_id=pId;
	update product_size_limits set min_size=min_size,max_size=max_size where product_id=pId;
	update price_to_product set modified_date=m_date, product_price=pPrice where (porsion=1 and product_id=pId);
	
	  IF pPrice_bucuk > 0 THEN	          
              update price_to_product set modified_date=m_date,product_price=pPrice_bucuk where (porsion=1.5 and product_id=pId);
        END IF;
		  
	  IF pPrice_double > 0 THEN	          
              update price_to_product set modified_date=m_date,product_price=pPrice_double where (porsion=2 and product_id=pId);
        END IF;
	
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `editSuppliersPayment`(IN `payment_id` INT,IN `type` INT, IN `payment_price` DOUBLE, IN `payment_desc` TEXT)
BEGIN

DECLARE m_date datetime;
SET m_date = now();

update `suppliers_payment` sp set sp.payment_price=payment_price, sp.type=type,
sp.payment_desc=payment_desc,sp.modified_date=m_date where sp.payment_id=payment_id;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `editTable`(IN `id` INT, IN `name` VARCHAR(20), IN `category` INT, IN `status` INT, IN `is_deleted` INT, IN `display_order` INT)
    NO SQL
BEGIN

DECLARE m_date datetime;

SET m_date = now();

update table_details set table_name=name, table_category=category, table_status=status,is_deleted=is_deleted,display_order=display_order, modified_date=m_date
where table_id=id;	
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `editTableCategory`(IN `id` INT, IN `name` VARCHAR(20), IN `status` INT, IN `is_deleted` INT, IN `display_order` INT)
    NO SQL
BEGIN

DECLARE m_date datetime;

SET m_date = now();

update table_categories set tcategory_name=name,tcategory_status=status,modified_date=m_date,display_order=display_order,is_deleted=is_deleted
where tcategory_id=id;	
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `logEx`(IN msg TEXT, IN source VARCHAR(100))
BEGIN
DECLARE cdate datetime;
SET cdate = now();
INSERT INTO exception_logs(message,source,date) VALUES (msg,source,cdate);

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `migo`(IN `goods_id` INT, IN `product_count` DOUBLE, IN `buy_price` DOUBLE, IN `kdv` DOUBLE, IN `buy_id` INT, IN `barcode` TEXT)
BEGIN
DECLARE m_date datetime;
DECLARE list_id int(11);
DECLARE i int(11);

SET m_date = now();



INSERT INTO buy_list(buy_id,goods_id,product_count,product_size,product_color,modified_date) VALUES(buy_id,goods_id,product_count,0,0,m_date);
SET list_id = (SELECT LAST_INSERT_ID());

INSERT INTO buy_price(list_id,buy_price,kdv,currency,modified_date) VALUES (list_id,buy_price,kdv,'TL',m_date);

SELECT	@i:=COUNT(*) FROM stock_details sd WHERE sd.goods_id = goods_id;
	IF @i = 0 THEN
		INSERT INTO stock_details(barcode,goods_id,product_color,product_size,product_code,product_count,modified_date) VALUES (barcode,goods_id,0,0, 'pcode',product_count,m_date);
         ELSE
        	UPDATE stock_details sd SET  sd.product_count = (sd.product_count + product_count),sd.modified_date = m_date WHERE sd.goods_id = goods_id;
       	
        END IF;
     
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `newBuyDetail`(IN `buy_desc` TEXT, IN `suppliers_id` INT, OUT `buy_id` INT)
BEGIN
DECLARE m_date datetime;
SET m_date = now();
INSERT INTO buy_details(buy_desc,suppliers_id,modified_date) VALUES (buy_desc,suppliers_id,m_date); 
SET buy_id = (SELECT LAST_INSERT_ID());
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `newOrder`(IN `owner_id` INT, IN `staff_id` INT, IN `order_type` INT, OUT `order_id` INT, IN `order_desc` TEXT, IN `a_id` INT)
    NO SQL
    DETERMINISTIC
BEGIN

DECLARE m_date datetime;
DECLARE ac_id int (11);

SET m_date = now();

INSERT INTO order_details(staff_id,order_type,order_status,order_desc,create_date,modified_date,is_deleted) VALUES(staff_id,order_type,1,order_desc,m_date,m_date,0);

SET order_id = (SELECT LAST_INSERT_ID());

if a_id=0 then
SET @ac_id = (SELECT account_id from account_details where
(account_type=order_type and account_status=1 and account_owner=owner_id) order by account_id desc limit 1);
else set @ac_id=a_id;
end if;

INSERT INTO orders_to_accounts (account_id,order_id,create_date) values (@ac_id,order_id,m_date);
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `newSellDetail`(IN `sell_desc` TEXT, OUT `sell_id` INT, IN `account_id` INT, IN `staff_id` INT)
BEGIN

DECLARE m_date datetime;
SET m_date = now();

INSERT INTO sell_details (account_id,staff_id,sell_desc,modified_date) VALUES(account_id,staff_id,sell_desc,m_date);
SET sell_id = (SELECT LAST_INSERT_ID());

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `newSuppliersPayment`(IN `suppliers_id` INT, IN `type` INT, IN `payment_price` DOUBLE, IN `process_id` INT, IN `payment_desc` TEXT, IN `unit` VARCHAR(3))
BEGIN
DECLARE m_date datetime;
DECLARE p_desc text;
SET m_date = now();

IF type = 1 THEN
	SELECT @p_desc := pr.return_desc FROM product_return pr WHERE pr.id = process_id;
	SET payment_desc = @p_desc;
        END IF;

IF type = 2 THEN
	SELECT @p_desc := bd.buy_desc FROM buy_details bd WHERE bd.buy_id = process_id;
	SET payment_desc = @p_desc;
        END IF;

INSERT INTO suppliers_payment(suppliers_id,unit,type,process_id,payment_price,payment_desc,modified_date) VALUES (suppliers_id,unit,type,process_id,payment_price,payment_desc,m_date);

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `openNewCheck`(IN `owner_id` INT, IN `type` INT, IN `staff_id` INT, OUT `a_id` INT)
    NO SQL
BEGIN

DECLARE m_date datetime;

SET m_date = now();

INSERT INTO account_details(account_type,staff_id,account_owner,account_status,create_date,modified_date) VALUES(type,staff_id,owner_id,1,m_date,m_date);
SET a_id= (SELECT LAST_INSERT_ID());

IF type=1 THEN
UPDATE table_details SET table_status=4 where table_id=owner_id;
END IF;

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `productReturn`(IN `goods_id` INT, IN `product_count` DOUBLE, IN `product_price` DOUBLE, IN `suppliers_id` INT, IN `return_desc` TEXT)
BEGIN

DECLARE m_date datetime;
DECLARE process_id int;
SET m_date = now();
INSERT INTO product_return(product_id,product_count,product_size,product_color,product_price,suppliers_id,return_desc,modified_date) VALUES (goods_id,product_count,0,0,product_price,suppliers_id,return_desc,m_date);
SET process_id = (SELECT LAST_INSERT_ID());
INSERT INTO suppliers_payment(suppliers_id,type,process_id,payment_price,payment_desc,modified_date) VALUES(suppliers_id,1,process_id,product_price,return_desc,m_date);
UPDATE stock_details sd SET sd.product_count = (sd.product_count - product_count), sd.modified_date=m_date WHERE sd.goods_id = goods_id;

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `saveProduct`(IN `pCode` VARCHAR(20), IN `pCat` INT(11), IN `pName` VARCHAR(30), IN `pDesc` TEXT, IN `pImg` TEXT, OUT `pId` INT(11), IN `pPrice` DOUBLE, IN `pUnit` INT, IN `min_size` INT, IN `max_size` INT, IN `pPrice_double` DOUBLE, IN `pPrice_bucuk` DOUBLE)
BEGIN

DECLARE m_date datetime;
DECLARE new_img_path text;
SET m_date = now();

	
	INSERT INTO product_details(product_code_manual,product_cat,product_name,product_desc,product_img_path,product_isDeleted,isOnMenu,modified_date,unit) VALUES (pCode,pCat,pName,pDesc,pImg,0,1,m_date,pUnit);
    SET pId = (SELECT LAST_INSERT_ID());
	INSERT INTO product_size_limits(product_id,min_size,max_size) values (pId,min_size,max_size);
	
	INSERT INTO price_to_product (product_id,product_price,porsion,create_date,modified_date) VALUES (pId,pPrice,1,m_date,m_date);       	          
    INSERT INTO price_to_product (product_id,product_price,porsion,create_date,modified_date) VALUES (pId,pPrice_bucuk,1.5,m_date,m_date);
   	INSERT INTO price_to_product (product_id,product_price,porsion,create_date,modified_date) VALUES (pId,pPrice_double,2,m_date,m_date);
		
		 IF pImg <> '-1' THEN	          
                SET new_img_path = CONCAT_WS('.',pId,pImg);
                UPDATE `product_details` SET `product_img_path`= new_img_path WHERE product_id = pId;
       		 END IF;
			
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sellProduct`(IN `sell_id` INT, IN `product_id` INT, IN `product_amount` DOUBLE, IN `product_size` DOUBLE, IN `product_color` INT, IN `product_price` DOUBLE, IN `product_code` TEXT)
BEGIN

DECLARE m_date datetime;
SET m_date = now();


INSERT INTO sell_list(sell_id,product_id,product_amount,product_size,product_color,product_price,modified_date) VALUES (sell_id,product_id,product_amount,product_size,product_color,product_price,m_date);
UPDATE stock_details sd SET sd.product_count = sd.product_count-product_amount, modified_date = m_date WHERE sd.product_code = product_code;

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sellReturn`(IN `sell_id` INT, IN `product_id` INT, IN `product_amount` DOUBLE, IN `product_size` DOUBLE, IN `product_color` INT, IN `product_price` DOUBLE, IN `product_code` TEXT, IN `return_desc` TEXT)
BEGIN

DECLARE m_date datetime;

SET m_date = now();

INSERT INTO sell_return(sell_id,product_id,product_amount,product_size,product_color,product_price,return_desc,modified_date) VALUES (sell_id,product_id,product_amount,product_size,product_color,product_price,return_desc,m_date);
UPDATE sell_list sl SET sl.product_amount = sl.product_amount - product_amount WHERE sl.product_id = product_id AND sl.product_size = product_size AND sl.product_color = product_color AND sl.sell_id=sell_id;
UPDATE stock_details sd SET sd.product_count = sd.product_count + product_amount, sd.modified_date = m_date WHERE sd.product_code = product_code;

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `setStatusAfterSale`(IN `o_id` INT, IN `a_id` INT, IN `ow_id` INT)
    NO SQL
BEGIN
DECLARE m_date datetime;

SET m_date = now();
update account_details set account_status=6, modified_date=m_date where account_id=a_id;
update order_details set order_status=6, modified_date=m_date where order_id=o_id;

if ow_id>0 THEN
update table_details set table_status=3, modified_date=m_date where table_id=ow_id;
end if;

END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `account_details`
--

CREATE TABLE IF NOT EXISTS `account_details` (
  `account_id` int(11) NOT NULL AUTO_INCREMENT,
  `account_type` int(11) NOT NULL,
  `staff_id` int(11) NOT NULL,
  `account_owner` int(11) NOT NULL,
  `account_status` int(11) NOT NULL,
  `create_date` datetime NOT NULL,
  `modified_date` datetime NOT NULL,
  PRIMARY KEY (`account_id`),
  KEY `account_status` (`account_status`),
  KEY `staff_id` (`staff_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=52 ;

--
-- Tablo döküm verisi `account_details`
--

INSERT INTO `account_details` (`account_id`, `account_type`, `staff_id`, `account_owner`, `account_status`, `create_date`, `modified_date`) VALUES
(1, 1, 14, 24, 6, '2013-08-12 21:54:16', '2013-08-12 21:57:33'),
(2, 1, 14, 25, 6, '2013-08-13 12:38:44', '2013-08-23 14:41:56'),
(3, 1, 14, 38, 6, '2013-08-14 19:26:42', '2013-08-23 14:42:00'),
(4, 1, 14, 39, 6, '2013-08-14 19:28:09', '2013-08-23 14:42:02'),
(5, 1, 14, 26, 6, '2013-08-14 19:28:44', '2013-08-23 14:41:54'),
(6, 2, 12, 1, 6, '2013-08-21 02:42:07', '2013-08-23 15:50:21'),
(7, 1, 12, 29, 6, '2013-08-21 02:50:04', '2013-08-23 14:42:07'),
(8, 1, 12, 27, 6, '2013-08-21 11:27:28', '2013-08-23 14:41:52'),
(9, 1, 12, 36, 6, '2013-08-21 11:32:38', '2013-08-23 14:41:49'),
(10, 1, 12, 35, 1, '2013-08-21 11:33:03', '2013-08-21 11:33:03'),
(11, 2, 12, 1, 6, '2013-08-21 11:34:04', '2013-08-23 15:50:23'),
(12, 2, 12, 1, 6, '2013-08-21 11:34:35', '2013-08-23 15:50:26'),
(13, 2, 12, 1, 6, '2013-08-21 11:35:02', '2013-08-23 15:50:25'),
(14, 1, 12, 24, 6, '2013-08-21 11:39:15', '2013-08-23 14:41:58'),
(15, 1, 12, 28, 6, '2013-08-21 11:40:03', '2013-08-23 14:41:45'),
(16, 1, 12, 34, 6, '2013-08-21 11:40:32', '2013-08-23 14:41:47'),
(17, 1, 8, 24, 1, '2013-08-22 02:47:14', '2013-08-22 02:47:14'),
(18, 1, 6, 29, 1, '2013-08-22 02:50:25', '2013-08-22 02:50:25'),
(26, 1, 12, 40, 6, '2013-08-23 14:41:20', '2013-08-23 14:42:04'),
(30, 1, 8, 28, 7, '2013-08-23 14:48:13', '2013-08-23 14:48:13'),
(38, 1, 8, 34, 6, '2013-08-23 14:51:46', '2013-08-24 01:31:56'),
(43, 1, 5, 24, 1, '2013-08-23 14:52:29', '2013-08-23 14:52:29'),
(44, 1, 6, 28, 1, '2013-08-23 14:52:47', '2013-08-23 14:52:47'),
(45, 1, 6, 36, 7, '2013-08-23 14:52:59', '2013-08-23 14:52:59'),
(46, 1, 5, 26, 6, '2013-08-23 14:53:18', '2013-08-24 02:12:03'),
(47, 1, 6, 39, 7, '2013-08-23 14:54:11', '2013-08-23 14:54:11'),
(48, 1, 6, 29, 6, '2013-08-23 14:54:31', '2013-08-24 02:09:11'),
(49, 1, 8, 31, 7, '2013-08-23 16:04:13', '2013-08-23 16:04:13'),
(50, 1, 12, 34, 7, '2013-08-24 02:07:02', '2013-08-24 02:07:02'),
(51, 2, 12, 1, 1, '2013-08-29 12:48:26', '2013-08-29 12:48:26');

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `bank_details`
--

CREATE TABLE IF NOT EXISTS `bank_details` (
  `bank_id` int(11) NOT NULL AUTO_INCREMENT,
  `bank_name` varchar(20) COLLATE utf8_turkish_ci NOT NULL,
  `total` double NOT NULL,
  `bank_isDeleted` tinyint(1) NOT NULL,
  `modified_date` datetime NOT NULL,
  PRIMARY KEY (`bank_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=2 ;

--
-- Tablo döküm verisi `bank_details`
--

INSERT INTO `bank_details` (`bank_id`, `bank_name`, `total`, `bank_isDeleted`, `modified_date`) VALUES
(1, 'GARANTI', 0, 0, '2013-08-12 19:23:34');

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `bank_instalments`
--

CREATE TABLE IF NOT EXISTS `bank_instalments` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `bank_id` int(11) NOT NULL,
  `instalment` text NOT NULL,
  `payment_day` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `bank_id` (`bank_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- Tablo döküm verisi `bank_instalments`
--

INSERT INTO `bank_instalments` (`id`, `bank_id`, `instalment`, `payment_day`) VALUES
(1, 1, '0:0', 0);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `bank_logs`
--

CREATE TABLE IF NOT EXISTS `bank_logs` (
  `log_id` int(11) NOT NULL AUTO_INCREMENT,
  `bank_id` int(11) NOT NULL,
  `amount` double NOT NULL,
  `instalment` int(11) NOT NULL,
  `rate` double(11,3) NOT NULL,
  `type` int(11) NOT NULL,
  `description` text COLLATE utf8_turkish_ci NOT NULL,
  `modified_date` datetime NOT NULL,
  PRIMARY KEY (`log_id`),
  KEY `bank_id` (`bank_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `buy_details`
--

CREATE TABLE IF NOT EXISTS `buy_details` (
  `buy_id` int(11) NOT NULL AUTO_INCREMENT,
  `buy_desc` text COLLATE utf8_turkish_ci,
  `suppliers_id` int(11) DEFAULT NULL,
  `modified_date` datetime NOT NULL,
  PRIMARY KEY (`buy_id`),
  KEY `suppliers_id` (`suppliers_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `buy_list`
--

CREATE TABLE IF NOT EXISTS `buy_list` (
  `list_id` int(11) NOT NULL AUTO_INCREMENT,
  `buy_id` int(11) DEFAULT NULL,
  `goods_id` int(11) DEFAULT NULL,
  `product_count` double(11,4) NOT NULL,
  `product_size` double NOT NULL,
  `product_color` int(11) NOT NULL,
  `modified_date` datetime NOT NULL,
  PRIMARY KEY (`list_id`),
  KEY `buy_id` (`buy_id`),
  KEY `product_id` (`goods_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `buy_price`
--

CREATE TABLE IF NOT EXISTS `buy_price` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `list_id` int(11) DEFAULT NULL,
  `buy_price` double DEFAULT NULL,
  `kdv` double DEFAULT NULL,
  `currency` varchar(6) COLLATE utf8_turkish_ci NOT NULL,
  `modified_date` datetime NOT NULL,
  PRIMARY KEY (`id`),
  KEY `list_id` (`list_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `category_details`
--

CREATE TABLE IF NOT EXISTS `category_details` (
  `cat_id` int(11) NOT NULL AUTO_INCREMENT,
  `cat_name` varchar(20) COLLATE utf8_turkish_ci NOT NULL,
  `cat_desc` text COLLATE utf8_turkish_ci,
  `parent_id` int(11) DEFAULT NULL,
  `display_order` int(11) NOT NULL,
  `is_deleted` tinyint(4) NOT NULL,
  `create_date` datetime DEFAULT NULL,
  `modified_date` datetime NOT NULL,
  PRIMARY KEY (`cat_id`),
  KEY `parent_id` (`parent_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=13 ;

--
-- Tablo döküm verisi `category_details`
--

INSERT INTO `category_details` (`cat_id`, `cat_name`, `cat_desc`, `parent_id`, `display_order`, `is_deleted`, `create_date`, `modified_date`) VALUES
(1, 'PIZZA', NULL, 0, 8, 0, NULL, '2013-08-20 17:02:43'),
(2, 'SALATA', NULL, 0, 6, 0, NULL, '2013-08-20 17:02:24'),
(3, 'BURGER', NULL, 0, 1, 0, NULL, '2013-08-20 17:01:37'),
(4, 'TOST', NULL, 0, 0, 0, NULL, '2013-08-12 19:46:41'),
(5, 'MENÜ', NULL, 0, 7, 0, NULL, '2013-08-20 17:02:34'),
(6, 'MAKARNA', NULL, 0, 5, 0, NULL, '2013-08-20 17:02:15'),
(7, 'TAVUK', NULL, 0, 4, 0, NULL, '2013-08-20 17:02:07'),
(8, 'ET', NULL, 0, 0, 1, NULL, '2013-08-12 19:48:01'),
(9, 'IÇECEK', NULL, 0, 2, 0, NULL, '2013-08-20 17:01:50'),
(10, 'TATLI', NULL, 0, 9, 0, NULL, '2013-08-20 17:02:53'),
(11, 'KAHVALTI', NULL, 0, 10, 0, NULL, '2013-08-20 16:36:53'),
(12, 'EXTRA', NULL, 0, 11, 0, NULL, '2013-08-20 16:41:57');

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `color_details`
--

CREATE TABLE IF NOT EXISTS `color_details` (
  `color_id` int(11) NOT NULL AUTO_INCREMENT,
  `color_name` varchar(20) CHARACTER SET utf8 COLLATE utf8_turkish_ci NOT NULL,
  `is_deleted` tinyint(1) NOT NULL,
  PRIMARY KEY (`color_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

--
-- Tablo döküm verisi `color_details`
--

INSERT INTO `color_details` (`color_id`, `color_name`, `is_deleted`) VALUES
(-999, 'no-color', 0);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `customer_details`
--

CREATE TABLE IF NOT EXISTS `customer_details` (
  `customer_id` int(11) NOT NULL AUTO_INCREMENT,
  `customer_name` text CHARACTER SET utf8 COLLATE utf8_turkish_ci NOT NULL,
  `customer_tel` varchar(20) CHARACTER SET utf8 COLLATE utf8_turkish_ci NOT NULL,
  `customer_mail` varchar(50) CHARACTER SET utf8 COLLATE utf8_turkish_ci NOT NULL,
  `customer_address` text CHARACTER SET utf8 COLLATE utf8_turkish_ci NOT NULL,
  `customer_note` text,
  `register_date` datetime NOT NULL,
  `modified_date` datetime NOT NULL,
  `is_deleted` tinyint(1) NOT NULL,
  `display_order` int(11) NOT NULL,
  PRIMARY KEY (`customer_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=3 ;

--
-- Tablo döküm verisi `customer_details`
--

INSERT INTO `customer_details` (`customer_id`, `customer_name`, `customer_tel`, `customer_mail`, `customer_address`, `customer_note`, `register_date`, `modified_date`, `is_deleted`, `display_order`) VALUES
(1, 'DIGER', '', '', '', NULL, '2013-08-12 00:00:00', '2013-08-12 00:00:00', 0, -999),
(2, 'MUSTAFA KORKMAZ', '5414428846', '', 'TAHIR ÜN CAD. GARANTI BANKASI KARSISI. BOYALI IS HANI. KAT: 3 DAIRE:1', 'TURSU BIBER ISTER', '2013-08-12 21:37:25', '2013-08-12 21:37:25', 0, 0);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `customer_payments`
--

CREATE TABLE IF NOT EXISTS `customer_payments` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `customer_id` int(11) NOT NULL,
  `sell_id` int(11) NOT NULL,
  `total_price` double NOT NULL,
  `payment` double NOT NULL,
  `type` int(11) NOT NULL COMMENT '0=payment; 1=shopping; 2=gift; 3=sell_return',
  `payment_desc` text,
  `payment_date` datetime NOT NULL,
  PRIMARY KEY (`id`),
  KEY `customer_id` (`customer_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;

--
-- Tablo döküm verisi `customer_payments`
--

INSERT INTO `customer_payments` (`id`, `customer_id`, `sell_id`, `total_price`, `payment`, `type`, `payment_desc`, `payment_date`) VALUES
(1, 1, 13, 12.5, 12.5, 1, '', '2013-08-23 15:50:21'),
(2, 1, 14, 5, 5, 1, '', '2013-08-23 15:50:23'),
(3, 1, 15, 4.5, 4.5, 1, '', '2013-08-23 15:50:24'),
(4, 1, 16, 4.5, 4.5, 1, '', '2013-08-23 15:50:26');

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `event_details`
--

CREATE TABLE IF NOT EXISTS `event_details` (
  `event_id` int(11) NOT NULL AUTO_INCREMENT,
  `event_name` text COLLATE utf8_turkish_ci NOT NULL,
  `event_desc` text COLLATE utf8_turkish_ci NOT NULL,
  PRIMARY KEY (`event_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `event_logs`
--

CREATE TABLE IF NOT EXISTS `event_logs` (
  `log_id` int(11) NOT NULL AUTO_INCREMENT,
  `event_id` int(11) NOT NULL,
  `event_type` int(11) NOT NULL,
  `event_actor` int(11) NOT NULL,
  `create_date` datetime NOT NULL,
  PRIMARY KEY (`log_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `exception_logs`
--

CREATE TABLE IF NOT EXISTS `exception_logs` (
  `log_id` int(11) NOT NULL AUTO_INCREMENT,
  `message` text COLLATE utf8_turkish_ci NOT NULL,
  `source` text COLLATE utf8_turkish_ci NOT NULL,
  `date` datetime NOT NULL,
  PRIMARY KEY (`log_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=2 ;

--
-- Tablo döküm verisi `exception_logs`
--

INSERT INTO `exception_logs` (`log_id`, `message`, `source`, `date`) VALUES
(1, 'GDI+ içinde genel bir hata olustu.', 'StockProgram.Products.ucEditProduct: SaveImage()', '2013-08-20 20:53:44');

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `expense_details`
--

CREATE TABLE IF NOT EXISTS `expense_details` (
  `payment_id` int(11) NOT NULL AUTO_INCREMENT,
  `payment_cat` int(11) NOT NULL,
  `payment_price` double NOT NULL,
  `payment_desc` text CHARACTER SET utf8 COLLATE utf8_turkish_ci,
  `modified_date` datetime NOT NULL,
  PRIMARY KEY (`payment_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `goods_details`
--

CREATE TABLE IF NOT EXISTS `goods_details` (
  `goods_id` int(11) NOT NULL AUTO_INCREMENT,
  `goods_name` varchar(20) CHARACTER SET utf8 COLLATE utf8_turkish_ci NOT NULL,
  `unit` varchar(4) NOT NULL,
  `create_date` datetime NOT NULL,
  `modified_date` datetime NOT NULL,
  `is_deleted` tinyint(4) NOT NULL,
  PRIMARY KEY (`goods_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=3 ;

--
-- Tablo döküm verisi `goods_details`
--

INSERT INTO `goods_details` (`goods_id`, `goods_name`, `unit`, `create_date`, `modified_date`, `is_deleted`) VALUES
(1, 'HAMBURGER KÖFTESI', 'ADET', '2013-08-12 19:49:14', '2013-08-12 19:49:14', 0),
(2, 'PATATES', 'KG', '2013-08-12 19:49:31', '2013-08-12 19:49:31', 0);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `options_to_orders`
--

CREATE TABLE IF NOT EXISTS `options_to_orders` (
  `option_id` int(11) NOT NULL,
  `log_id` int(11) NOT NULL,
  KEY `option_id` (`option_id`),
  KEY `log_id` (`log_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `options_to_product`
--

CREATE TABLE IF NOT EXISTS `options_to_product` (
  `option_id` int(11) NOT NULL,
  `product_id` int(11) NOT NULL,
  `is_deleted` tinyint(1) NOT NULL,
  KEY `option_id` (`option_id`),
  KEY `option_id_2` (`option_id`),
  KEY `product_id` (`product_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Tablo döküm verisi `options_to_product`
--

INSERT INTO `options_to_product` (`option_id`, `product_id`, `is_deleted`) VALUES
(20, 61, 0),
(19, 61, 0),
(3, 3, 0),
(21, 3, 0),
(15, 56, 0),
(15, 57, 0),
(16, 67, 0),
(17, 67, 0),
(16, 38, 0),
(17, 38, 0),
(16, 71, 0),
(17, 71, 0),
(16, 73, 0),
(17, 73, 0),
(1, 6, 0),
(2, 6, 0),
(18, 6, 0),
(5, 6, 0),
(21, 6, 0),
(4, 6, 0),
(3, 6, 0),
(23, 6, 0),
(22, 6, 0),
(25, 6, 0),
(24, 6, 0),
(1, 52, 0),
(2, 52, 0),
(18, 52, 0),
(5, 52, 0),
(21, 52, 0),
(4, 52, 0),
(3, 52, 0),
(23, 52, 0),
(22, 52, 0),
(24, 52, 0),
(25, 52, 0),
(1, 51, 0),
(2, 51, 0),
(18, 51, 0),
(5, 51, 0),
(21, 51, 0),
(4, 51, 0),
(3, 51, 0),
(23, 51, 0),
(22, 51, 0),
(24, 51, 0),
(25, 51, 0),
(1, 2, 0),
(2, 2, 0),
(18, 2, 0),
(5, 2, 0),
(21, 2, 0),
(4, 2, 0),
(3, 2, 0),
(23, 2, 0),
(22, 2, 0),
(24, 2, 0),
(25, 2, 0),
(1, 45, 0),
(2, 45, 0),
(18, 45, 0),
(5, 45, 0),
(21, 45, 0),
(4, 45, 0),
(3, 45, 0),
(23, 45, 0),
(22, 45, 0),
(24, 45, 0),
(25, 45, 0),
(1, 46, 0),
(2, 46, 0),
(18, 46, 0),
(5, 46, 0),
(21, 46, 0),
(4, 46, 0),
(3, 46, 0),
(23, 46, 0),
(22, 46, 0),
(25, 46, 0),
(24, 46, 0),
(1, 4, 0),
(2, 4, 0),
(18, 4, 0),
(5, 4, 0),
(21, 4, 0),
(4, 4, 0),
(3, 4, 0),
(23, 4, 0),
(22, 4, 0),
(24, 4, 0),
(25, 4, 0),
(1, 44, 0),
(2, 44, 0),
(18, 44, 0),
(5, 44, 0),
(21, 44, 0),
(4, 44, 0),
(3, 44, 0),
(23, 44, 0),
(22, 44, 0),
(25, 44, 0),
(24, 44, 0),
(1, 43, 0),
(2, 43, 0),
(18, 43, 0),
(5, 43, 0),
(21, 43, 0),
(4, 43, 0),
(3, 43, 0),
(23, 43, 0),
(22, 43, 0),
(25, 43, 0),
(24, 43, 0),
(1, 7, 0),
(2, 7, 0),
(18, 7, 0),
(5, 7, 0),
(21, 7, 0),
(4, 7, 0),
(3, 7, 0),
(23, 7, 0),
(22, 7, 0),
(25, 7, 0),
(24, 7, 0),
(1, 8, 0),
(2, 8, 0),
(18, 8, 0),
(5, 8, 0),
(21, 8, 0),
(4, 8, 0),
(3, 8, 0),
(23, 8, 0),
(22, 8, 0),
(25, 8, 0),
(24, 8, 0),
(1, 17, 0),
(2, 17, 0),
(18, 17, 0),
(5, 17, 0),
(21, 17, 0),
(4, 17, 0),
(3, 17, 0),
(23, 17, 0),
(22, 17, 0),
(24, 17, 0),
(25, 17, 0),
(24, 48, 0),
(25, 48, 0),
(5, 48, 0),
(7, 48, 0),
(1, 14, 0),
(2, 14, 0),
(18, 14, 0),
(5, 14, 0),
(21, 14, 0),
(4, 14, 0),
(3, 14, 0),
(23, 14, 0),
(22, 14, 0),
(25, 14, 0),
(24, 14, 0),
(1, 19, 0),
(2, 19, 0),
(18, 19, 0),
(5, 19, 0),
(21, 19, 0),
(4, 19, 0),
(3, 19, 0),
(23, 19, 0),
(22, 19, 0),
(24, 19, 0),
(25, 19, 0),
(1, 15, 0),
(2, 15, 0),
(18, 15, 0),
(21, 15, 0),
(3, 15, 0),
(23, 15, 0),
(22, 15, 0),
(16, 68, 0),
(17, 68, 0),
(29, 68, 0),
(30, 68, 0),
(10, 68, 0),
(16, 69, 0),
(17, 69, 0),
(27, 69, 0),
(9, 69, 0),
(29, 69, 0),
(11, 69, 0),
(10, 69, 0),
(26, 69, 0),
(2, 55, 0),
(1, 55, 0),
(18, 55, 0),
(25, 55, 0),
(5, 55, 0),
(21, 55, 0),
(24, 55, 0),
(4, 55, 0),
(3, 55, 0),
(23, 55, 0),
(22, 55, 0),
(16, 72, 0),
(17, 72, 0),
(1, 42, 0),
(2, 42, 0),
(18, 42, 0),
(25, 42, 0),
(5, 42, 0),
(24, 42, 0),
(4, 42, 0),
(21, 42, 0),
(3, 42, 0),
(23, 42, 0),
(22, 42, 0),
(4, 41, 0),
(24, 41, 0),
(3, 41, 0),
(23, 41, 0),
(22, 41, 0),
(18, 41, 0),
(5, 41, 0),
(25, 41, 0),
(2, 41, 0),
(1, 41, 0),
(16, 74, 0),
(17, 74, 0),
(8, 74, 0),
(27, 74, 0),
(12, 74, 0),
(28, 74, 0),
(11, 74, 0),
(29, 74, 0),
(1, 22, 0),
(2, 22, 0),
(18, 22, 0),
(25, 22, 0),
(5, 22, 0),
(21, 22, 0),
(24, 22, 0),
(4, 22, 0),
(3, 22, 0),
(23, 22, 0),
(22, 22, 0),
(1, 23, 0),
(2, 23, 0),
(18, 23, 0),
(25, 23, 0),
(5, 23, 0),
(21, 23, 0),
(24, 23, 0),
(4, 23, 0),
(22, 23, 0),
(23, 23, 0),
(3, 23, 0),
(16, 39, 0),
(17, 39, 0),
(1, 28, 0),
(2, 28, 0),
(18, 28, 0),
(25, 28, 0),
(5, 28, 0),
(24, 28, 0),
(21, 28, 0),
(4, 28, 0),
(3, 28, 0),
(23, 28, 0),
(22, 28, 0),
(22, 53, 0),
(23, 53, 0),
(3, 53, 0),
(4, 53, 0),
(24, 53, 0),
(21, 53, 0),
(18, 53, 0),
(2, 53, 0),
(1, 53, 0),
(25, 53, 0),
(13, 53, 0),
(6, 33, 0),
(18, 33, 0),
(14, 33, 0),
(13, 33, 0),
(1, 27, 0),
(2, 27, 0),
(6, 27, 0),
(18, 27, 0),
(14, 27, 0),
(13, 27, 0),
(7, 27, 0),
(31, 27, 0),
(32, 27, 0),
(33, 27, 0),
(1, 1, 0),
(2, 1, 0),
(6, 1, 0),
(18, 1, 0),
(14, 1, 0),
(33, 1, 0),
(13, 1, 0),
(7, 1, 0),
(32, 1, 0),
(31, 1, 0),
(1, 16, 0),
(2, 16, 0),
(6, 16, 0),
(18, 16, 0),
(14, 16, 0),
(33, 16, 0),
(13, 16, 0),
(7, 16, 0),
(32, 16, 0),
(31, 16, 0),
(8, 70, 0),
(12, 70, 0),
(29, 70, 0),
(28, 70, 0),
(26, 70, 0),
(36, 70, 0),
(35, 70, 0),
(34, 70, 0);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `option_details`
--

CREATE TABLE IF NOT EXISTS `option_details` (
  `option_id` int(11) NOT NULL AUTO_INCREMENT,
  `option_name` varchar(50) NOT NULL,
  `create_date` datetime NOT NULL,
  `modified_date` datetime NOT NULL,
  `is_deleted` tinyint(4) NOT NULL,
  PRIMARY KEY (`option_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=37 ;

--
-- Tablo döküm verisi `option_details`
--

INSERT INTO `option_details` (`option_id`, `option_name`, `create_date`, `modified_date`, `is_deleted`) VALUES
(1, 'ACI BOL', '2013-08-21 01:23:00', '2013-08-21 01:23:00', 0),
(2, 'ACI YOK', '2013-08-21 01:23:03', '2013-08-21 01:23:03', 0),
(3, 'SOGAN YOK', '2013-08-21 01:23:10', '2013-08-21 01:23:10', 0),
(4, 'MAYONEZ YOK', '2013-08-21 01:23:15', '2013-08-21 01:23:15', 0),
(5, 'KETÇAP YOK', '2013-08-21 01:23:18', '2013-08-21 01:23:18', 0),
(6, 'BIBER YOK', '2013-08-21 01:23:41', '2013-08-21 01:23:41', 0),
(7, 'MANTAR YOK', '2013-08-21 01:23:43', '2013-08-21 01:23:43', 0),
(8, 'ÇILEKLI', '2013-08-21 01:23:51', '2013-08-21 01:23:51', 0),
(9, 'KAYISILI', '2013-08-21 01:23:54', '2013-08-21 01:23:54', 0),
(10, 'SEFTALILI', '2013-08-21 01:23:57', '2013-08-21 01:23:57', 0),
(11, 'PORTAKALLI', '2013-08-21 01:24:01', '2013-08-21 01:24:01', 0),
(12, 'KARPUZLU', '2013-08-21 01:24:04', '2013-08-21 01:24:04', 0),
(13, 'KEKIK YOK', '2013-08-21 01:24:21', '2013-08-21 01:24:21', 0),
(14, 'FESLEGEN YOK', '2013-08-21 01:24:28', '2013-08-21 01:24:28', 0),
(15, 'AÇIK', '2013-08-21 01:24:42', '2013-08-21 01:24:42', 0),
(16, 'BUZLU ', '2013-08-21 01:24:50', '2013-08-21 01:24:50', 0),
(17, 'BUZSUZ', '2013-08-21 01:24:55', '2013-08-21 01:24:55', 0),
(18, 'DOMATES YOK', '2013-08-21 01:25:06', '2013-08-21 01:25:06', 0),
(19, 'IHLAMUR', '2013-08-21 01:27:41', '2013-08-21 01:27:41', 0),
(20, 'ADAÇAYI', '2013-08-21 01:27:53', '2013-08-21 01:27:53', 0),
(21, 'MARUL YOK', '2013-08-21 01:30:10', '2013-08-21 01:30:10', 0),
(22, 'TURSU YOK', '2013-08-21 01:43:10', '2013-08-21 01:43:10', 0),
(23, 'TURSU BOL', '2013-08-21 01:43:14', '2013-08-21 01:43:14', 0),
(24, 'MAYONEZ BOL', '2013-08-21 01:49:05', '2013-08-21 01:49:05', 0),
(25, 'KETÇAP BOL', '2013-08-21 01:49:09', '2013-08-21 01:49:09', 0),
(26, 'VISNELI', '2013-08-21 01:53:36', '2013-08-21 01:53:36', 0),
(27, 'KARISIK ', '2013-08-21 01:53:43', '2013-08-21 01:53:43', 0),
(28, 'NARLI', '2013-08-21 01:53:46', '2013-08-21 01:53:46', 0),
(29, 'LIMON', '2013-08-21 01:54:18', '2013-08-21 01:54:18', 0),
(30, 'MANGO', '2013-08-21 01:54:21', '2013-08-21 01:54:21', 0),
(31, 'SOSIS YOK', '2013-08-21 11:07:31', '2013-08-21 11:07:31', 0),
(32, 'SALAM YOK ', '2013-08-21 11:07:34', '2013-08-21 11:07:34', 0),
(33, 'KASAR YOK', '2013-08-21 11:07:36', '2013-08-21 11:07:36', 0),
(34, 'MANDALINA', '2013-08-21 19:35:42', '2013-08-21 19:35:42', 0),
(35, 'SADE', '2013-08-21 19:35:45', '2013-08-21 19:35:45', 0),
(36, 'ELMA', '2013-08-21 19:36:56', '2013-08-21 19:36:56', 0);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `orders_in`
--

CREATE TABLE IF NOT EXISTS `orders_in` (
  `order_id` int(11) NOT NULL,
  `table_id` int(11) NOT NULL,
  `staff_id` int(11) NOT NULL,
  KEY `order_id` (`order_id`),
  KEY `table_id` (`table_id`),
  KEY `staff_id` (`staff_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `orders_out`
--

CREATE TABLE IF NOT EXISTS `orders_out` (
  `order_id` int(11) NOT NULL,
  `customer_id` int(11) NOT NULL,
  `staff_id` int(11) NOT NULL,
  KEY `order_id` (`order_id`),
  KEY `customer_id` (`customer_id`),
  KEY `staff_id` (`staff_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `orders_to_accounts`
--

CREATE TABLE IF NOT EXISTS `orders_to_accounts` (
  `log_id` int(11) NOT NULL AUTO_INCREMENT,
  `account_id` int(11) NOT NULL,
  `order_id` int(11) NOT NULL,
  `create_date` datetime NOT NULL,
  PRIMARY KEY (`log_id`),
  KEY `account_id` (`account_id`),
  KEY `order_id` (`order_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=74 ;

--
-- Tablo döküm verisi `orders_to_accounts`
--

INSERT INTO `orders_to_accounts` (`log_id`, `account_id`, `order_id`, `create_date`) VALUES
(1, 1, 1, '2013-08-12 21:54:16'),
(2, 2, 2, '2013-08-13 12:38:44'),
(3, 3, 3, '2013-08-14 19:26:42'),
(4, 4, 4, '2013-08-14 19:28:09'),
(5, 5, 5, '2013-08-14 19:28:44'),
(6, 6, 6, '2013-08-21 02:42:07'),
(7, 7, 7, '2013-08-21 02:50:04'),
(8, 2, 8, '2013-08-21 11:13:59'),
(9, 2, 9, '2013-08-21 11:18:08'),
(10, 2, 10, '2013-08-21 11:22:37'),
(11, 2, 11, '2013-08-21 11:23:44'),
(12, 2, 12, '2013-08-21 11:27:10'),
(13, 8, 13, '2013-08-21 11:27:28'),
(14, 2, 14, '2013-08-21 11:28:35'),
(15, 2, 15, '2013-08-21 11:30:42'),
(16, 2, 16, '2013-08-21 11:31:40'),
(17, 8, 17, '2013-08-21 11:32:21'),
(18, 9, 18, '2013-08-21 11:32:39'),
(19, 10, 19, '2013-08-21 11:33:04'),
(20, 11, 20, '2013-08-21 11:34:04'),
(21, 12, 21, '2013-08-21 11:34:35'),
(22, 13, 22, '2013-08-21 11:35:02'),
(23, 14, 23, '2013-08-21 11:39:15'),
(24, 15, 24, '2013-08-21 11:40:03'),
(25, 16, 25, '2013-08-21 11:40:32'),
(26, 18, 26, '2013-08-22 02:51:00'),
(27, 15, 27, '2013-08-23 14:09:23'),
(28, 26, 28, '2013-08-23 14:41:20'),
(29, 46, 29, '2013-08-23 14:53:38'),
(30, 44, 32, '2013-08-23 14:58:56'),
(31, 48, 33, '2013-08-23 14:59:24'),
(32, 38, 34, '2013-08-23 15:00:09'),
(33, 38, 35, '2013-08-23 15:30:20'),
(34, 48, 36, '2013-08-23 16:09:39'),
(35, 44, 37, '2013-08-23 16:10:43'),
(36, 44, 38, '2013-08-23 16:11:34'),
(37, 44, 39, '2013-08-23 16:12:00'),
(38, 50, 40, '2013-08-24 02:07:02'),
(39, 44, 41, '2013-08-24 02:07:36'),
(40, 46, 42, '2013-08-24 02:09:23'),
(41, 44, 43, '2013-08-27 14:17:57'),
(42, 44, 44, '2013-08-27 14:18:26'),
(43, 44, 45, '2013-08-27 14:22:50'),
(44, 44, 46, '2013-08-27 14:23:53'),
(45, 44, 47, '2013-08-27 14:24:15'),
(46, 44, 48, '2013-08-27 14:29:01'),
(47, 44, 49, '2013-08-27 14:34:44'),
(48, 44, 50, '2013-08-27 14:36:51'),
(49, 44, 51, '2013-08-27 14:39:39'),
(50, 44, 52, '2013-08-27 14:40:37'),
(51, 44, 53, '2013-08-27 14:44:46'),
(52, 44, 54, '2013-08-27 14:54:18'),
(53, 44, 55, '2013-08-27 15:25:44'),
(54, 44, 56, '2013-08-27 15:26:01'),
(55, 44, 57, '2013-08-27 15:28:18'),
(56, 44, 58, '2013-08-27 15:39:52'),
(57, 44, 59, '2013-08-27 15:41:54'),
(58, 44, 60, '2013-08-27 16:17:37'),
(59, 44, 61, '2013-08-27 16:22:42'),
(60, 44, 62, '2013-08-27 16:28:04'),
(61, 44, 63, '2013-08-27 16:35:17'),
(62, 44, 64, '2013-08-27 16:47:05'),
(63, 44, 65, '2013-08-27 17:09:18'),
(64, 44, 66, '2013-08-27 17:18:44'),
(65, 44, 67, '2013-08-27 17:45:27'),
(66, 44, 68, '2013-08-27 17:49:01'),
(67, 44, 69, '2013-08-27 17:49:08'),
(68, 44, 70, '2013-08-27 17:56:46'),
(69, 44, 71, '2013-08-27 18:21:17'),
(70, 44, 72, '2013-08-27 18:21:50'),
(71, 44, 73, '2013-08-27 18:25:44'),
(72, 44, 74, '2013-08-27 18:28:11'),
(73, 51, 75, '2013-08-29 12:48:26');

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `order_details`
--

CREATE TABLE IF NOT EXISTS `order_details` (
  `staff_id` int(11) NOT NULL,
  `order_id` int(11) NOT NULL AUTO_INCREMENT,
  `order_type` int(11) NOT NULL,
  `order_status` int(11) NOT NULL,
  `order_desc` text CHARACTER SET utf8 COLLATE utf8_turkish_ci NOT NULL,
  `is_deleted` tinyint(1) DEFAULT NULL,
  `create_date` datetime NOT NULL,
  `modified_date` datetime NOT NULL,
  PRIMARY KEY (`order_id`),
  KEY `order_status` (`order_status`),
  KEY `staff_id` (`staff_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=76 ;

--
-- Tablo döküm verisi `order_details`
--

INSERT INTO `order_details` (`staff_id`, `order_id`, `order_type`, `order_status`, `order_desc`, `is_deleted`, `create_date`, `modified_date`) VALUES
(14, 1, 1, 6, '', 0, '2013-08-12 21:54:16', '2013-08-12 21:57:33'),
(14, 2, 1, 6, '', 0, '2013-08-13 12:38:44', '2013-08-23 14:41:56'),
(14, 3, 1, 6, '', 0, '2013-08-14 19:26:42', '2013-08-23 14:42:00'),
(14, 4, 1, 6, '', 0, '2013-08-14 19:28:09', '2013-08-23 14:42:02'),
(14, 5, 1, 6, '', 0, '2013-08-14 19:28:44', '2013-08-23 14:41:54'),
(12, 6, 2, 6, '', 0, '2013-08-21 02:42:07', '2013-08-23 15:50:21'),
(12, 7, 1, 6, '', 0, '2013-08-21 02:50:04', '2013-08-23 14:42:07'),
(12, 8, 1, 1, '', 0, '2013-08-21 11:13:59', '2013-08-21 11:13:59'),
(12, 9, 1, 1, '', 0, '2013-08-21 11:18:08', '2013-08-21 11:18:08'),
(12, 10, 1, 1, '', 0, '2013-08-21 11:22:37', '2013-08-21 11:22:37'),
(12, 11, 1, 1, '', 0, '2013-08-21 11:23:44', '2013-08-21 11:23:44'),
(12, 12, 1, 1, '', 0, '2013-08-21 11:27:10', '2013-08-21 11:27:10'),
(12, 13, 1, 6, '', 0, '2013-08-21 11:27:28', '2013-08-23 14:41:52'),
(12, 14, 1, 1, '', 0, '2013-08-21 11:28:35', '2013-08-21 11:28:35'),
(12, 15, 1, 1, '', 0, '2013-08-21 11:30:42', '2013-08-21 11:30:42'),
(12, 16, 1, 1, '', 0, '2013-08-21 11:31:40', '2013-08-21 11:31:40'),
(12, 17, 1, 1, '', 0, '2013-08-21 11:32:21', '2013-08-21 11:32:21'),
(12, 18, 1, 6, '', 0, '2013-08-21 11:32:39', '2013-08-23 14:41:49'),
(12, 19, 1, 1, '', 0, '2013-08-21 11:33:04', '2013-08-21 11:33:04'),
(12, 20, 2, 6, '', 0, '2013-08-21 11:34:04', '2013-08-23 15:50:23'),
(12, 21, 2, 6, '', 0, '2013-08-21 11:34:35', '2013-08-23 15:50:26'),
(12, 22, 2, 6, '', 0, '2013-08-21 11:35:02', '2013-08-23 15:50:25'),
(12, 23, 1, 6, '', 0, '2013-08-21 11:39:15', '2013-08-23 14:41:58'),
(12, 24, 1, 6, '', 0, '2013-08-21 11:40:03', '2013-08-23 14:41:45'),
(12, 25, 1, 6, '', 0, '2013-08-21 11:40:32', '2013-08-23 14:41:47'),
(12, 26, 1, 1, '', 0, '2013-08-22 02:51:00', '2013-08-22 02:51:00'),
(6, 27, 1, 1, 'none', 0, '2013-08-23 14:09:23', '2013-08-23 14:09:23'),
(12, 28, 1, 6, '', 0, '2013-08-23 14:41:20', '2013-08-23 14:42:04'),
(5, 29, 1, 1, 'none', 0, '2013-08-23 14:53:38', '2013-08-23 14:53:38'),
(6, 32, 1, 1, 'none', 0, '2013-08-23 14:58:56', '2013-08-23 14:58:56'),
(6, 33, 1, 6, 'none', 0, '2013-08-23 14:59:24', '2013-08-24 02:09:11'),
(6, 34, 1, 6, 'none', 0, '2013-08-23 15:00:09', '2013-08-24 01:31:56'),
(12, 35, 1, 1, '', 0, '2013-08-23 15:30:20', '2013-08-23 15:30:20'),
(6, 36, 1, 1, 'none', 0, '2013-08-23 16:09:39', '2013-08-23 16:09:39'),
(6, 37, 1, 1, 'none', 0, '2013-08-23 16:10:43', '2013-08-23 16:10:43'),
(8, 38, 1, 1, 'none', 0, '2013-08-23 16:11:34', '2013-08-23 16:11:34'),
(6, 39, 1, 1, 'none', 0, '2013-08-23 16:12:00', '2013-08-23 16:12:00'),
(12, 40, 1, 1, '', 0, '2013-08-24 02:07:02', '2013-08-24 02:07:02'),
(12, 41, 1, 1, '', 0, '2013-08-24 02:07:36', '2013-08-24 02:07:36'),
(12, 42, 1, 6, '', 0, '2013-08-24 02:09:23', '2013-08-24 02:12:03'),
(8, 43, 1, 1, 'none', 0, '2013-08-27 14:17:57', '2013-08-27 14:17:57'),
(6, 44, 1, 1, 'none', 0, '2013-08-27 14:18:26', '2013-08-27 14:18:26'),
(5, 45, 1, 1, 'none', 0, '2013-08-27 14:22:50', '2013-08-27 14:22:50'),
(8, 46, 1, 1, 'none', 0, '2013-08-27 14:23:53', '2013-08-27 14:23:53'),
(8, 47, 1, 1, 'none', 0, '2013-08-27 14:24:15', '2013-08-27 14:24:15'),
(8, 48, 1, 1, 'none', 0, '2013-08-27 14:29:01', '2013-08-27 14:29:01'),
(5, 49, 1, 1, 'none', 0, '2013-08-27 14:34:44', '2013-08-27 14:34:44'),
(5, 50, 1, 1, 'none', 0, '2013-08-27 14:36:51', '2013-08-27 14:36:51'),
(5, 51, 1, 1, 'none', 0, '2013-08-27 14:39:39', '2013-08-27 14:39:39'),
(5, 52, 1, 1, 'none', 0, '2013-08-27 14:40:37', '2013-08-27 14:40:37'),
(5, 53, 1, 1, 'none', 0, '2013-08-27 14:44:46', '2013-08-27 14:44:46'),
(8, 54, 1, 1, 'none', 0, '2013-08-27 14:54:18', '2013-08-27 14:54:18'),
(8, 55, 1, 1, 'none', 0, '2013-08-27 15:25:44', '2013-08-27 15:25:44'),
(8, 56, 1, 1, 'none', 0, '2013-08-27 15:26:01', '2013-08-27 15:26:01'),
(6, 57, 1, 1, 'none', 0, '2013-08-27 15:28:18', '2013-08-27 15:28:18'),
(6, 58, 1, 1, 'none', 0, '2013-08-27 15:39:52', '2013-08-27 15:39:52'),
(6, 59, 1, 1, 'none', 0, '2013-08-27 15:41:54', '2013-08-27 15:41:54'),
(5, 60, 1, 1, 'none', 0, '2013-08-27 16:17:37', '2013-08-27 16:17:37'),
(5, 61, 1, 1, 'none', 0, '2013-08-27 16:22:42', '2013-08-27 16:22:42'),
(5, 62, 1, 1, 'none', 0, '2013-08-27 16:28:04', '2013-08-27 16:28:04'),
(5, 63, 1, 1, 'none', 0, '2013-08-27 16:35:17', '2013-08-27 16:35:17'),
(5, 64, 1, 1, 'none', 0, '2013-08-27 16:47:05', '2013-08-27 16:47:05'),
(6, 65, 1, 1, 'none', 0, '2013-08-27 17:09:18', '2013-08-27 17:09:18'),
(6, 66, 1, 1, 'none', 0, '2013-08-27 17:18:44', '2013-08-27 17:18:44'),
(6, 67, 1, 1, 'none', 0, '2013-08-27 17:45:27', '2013-08-27 17:45:27'),
(6, 68, 1, 1, 'none', 0, '2013-08-27 17:49:01', '2013-08-27 17:49:01'),
(6, 69, 1, 1, 'none', 0, '2013-08-27 17:49:08', '2013-08-27 17:49:08'),
(6, 70, 1, 1, 'none', 0, '2013-08-27 17:56:46', '2013-08-27 17:56:46'),
(6, 71, 1, 1, 'none', 0, '2013-08-27 18:21:17', '2013-08-27 18:21:17'),
(6, 72, 1, 1, 'none', 0, '2013-08-27 18:21:50', '2013-08-27 18:21:50'),
(5, 73, 1, 1, 'none', 0, '2013-08-27 18:25:44', '2013-08-27 18:25:44'),
(6, 74, 1, 1, 'none', 0, '2013-08-27 18:28:11', '2013-08-27 18:28:11'),
(12, 75, 2, 1, '', 0, '2013-08-29 12:48:26', '2013-08-29 12:48:26');

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `payment_details`
--

CREATE TABLE IF NOT EXISTS `payment_details` (
  `payment_id` int(11) NOT NULL AUTO_INCREMENT,
  `sell_id` int(11) DEFAULT NULL,
  `payment_type` int(11) NOT NULL,
  `payment_price` double NOT NULL,
  `modified_date` datetime NOT NULL,
  PRIMARY KEY (`payment_id`),
  KEY `sell_id` (`sell_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=20 ;

--
-- Tablo döküm verisi `payment_details`
--

INSERT INTO `payment_details` (`payment_id`, `sell_id`, `payment_type`, `payment_price`, `modified_date`) VALUES
(1, 1, 0, 6.5, '2013-08-12 21:57:33'),
(2, 2, 0, 8.5, '2013-08-23 14:41:45'),
(3, 3, 0, 5.5, '2013-08-23 14:41:47'),
(4, 4, 0, 4.5, '2013-08-23 14:41:49'),
(5, 5, 0, 6.5, '2013-08-23 14:41:52'),
(6, 6, 0, 15.5, '2013-08-23 14:41:54'),
(7, 7, 0, 34, '2013-08-23 14:41:56'),
(8, 8, 0, 3.5, '2013-08-23 14:41:58'),
(9, 9, 0, 24.5, '2013-08-23 14:42:00'),
(10, 10, 0, 22.5, '2013-08-23 14:42:02'),
(11, 11, 0, 9.5, '2013-08-23 14:42:04'),
(12, 12, 0, 6.5, '2013-08-23 14:42:07'),
(13, 13, 0, 12.5, '2013-08-23 15:50:21'),
(14, 14, 0, 5, '2013-08-23 15:50:23'),
(15, 15, 0, 4.5, '2013-08-23 15:50:24'),
(16, 16, 0, 4.5, '2013-08-23 15:50:26'),
(17, 17, 0, 106.5, '2013-08-24 01:31:56'),
(18, 18, 0, 9, '2013-08-24 02:09:11'),
(19, 19, 0, 25.5, '2013-08-24 02:12:03');

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `payment_to_bank`
--

CREATE TABLE IF NOT EXISTS `payment_to_bank` (
  `payment_id` int(11) DEFAULT NULL,
  `bank_id` int(11) DEFAULT NULL,
  `modified_date` datetime NOT NULL,
  KEY `payment_id` (`payment_id`),
  KEY `bank_id` (`bank_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `price_to_product`
--

CREATE TABLE IF NOT EXISTS `price_to_product` (
  `log_id` int(11) NOT NULL AUTO_INCREMENT,
  `product_id` int(11) NOT NULL,
  `product_price` double NOT NULL,
  `porsion` double NOT NULL,
  `create_date` datetime NOT NULL,
  `modified_date` datetime NOT NULL,
  PRIMARY KEY (`log_id`),
  KEY `product_id` (`product_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=289 ;

--
-- Tablo döküm verisi `price_to_product`
--

INSERT INTO `price_to_product` (`log_id`, `product_id`, `product_price`, `porsion`, `create_date`, `modified_date`) VALUES
(1, 1, 10, 1, '2013-08-12 19:51:18', '2013-08-20 17:34:34'),
(2, 1, 0, 1.5, '2013-08-12 19:51:18', '2013-08-12 19:51:18'),
(3, 1, 0, 2, '2013-08-12 19:51:18', '2013-08-12 19:51:18'),
(4, 2, 4.5, 1, '2013-08-12 19:59:19', '2013-08-12 20:10:05'),
(5, 2, 0, 1.5, '2013-08-12 19:59:19', '2013-08-12 19:59:19'),
(6, 2, 0, 2, '2013-08-12 19:59:19', '2013-08-12 19:59:19'),
(7, 3, 4.5, 1, '2013-08-12 20:11:52', '2013-08-12 21:28:38'),
(8, 3, 0, 1.5, '2013-08-12 20:11:52', '2013-08-12 20:11:52'),
(9, 3, 0, 2, '2013-08-12 20:11:52', '2013-08-12 20:11:52'),
(10, 4, 4.5, 1, '2013-08-12 20:15:13', '2013-08-20 17:08:39'),
(11, 4, 0, 1.5, '2013-08-12 20:15:13', '2013-08-12 20:15:13'),
(12, 4, 0, 2, '2013-08-12 20:15:13', '2013-08-12 20:15:13'),
(13, 5, 5, 1, '2013-08-12 20:17:01', '2013-08-20 20:51:00'),
(14, 5, 0, 1.5, '2013-08-12 20:17:01', '2013-08-12 20:17:01'),
(15, 5, 0, 2, '2013-08-12 20:17:01', '2013-08-12 20:17:01'),
(16, 6, 5, 1, '2013-08-12 20:18:56', '2013-08-20 17:42:19'),
(17, 6, 0, 1.5, '2013-08-12 20:18:56', '2013-08-12 20:18:56'),
(18, 6, 0, 2, '2013-08-12 20:18:56', '2013-08-12 20:18:56'),
(19, 7, 7, 1, '2013-08-12 20:21:51', '2013-08-12 21:31:00'),
(20, 7, 0, 1.5, '2013-08-12 20:21:51', '2013-08-12 20:21:51'),
(21, 7, 0, 2, '2013-08-12 20:21:51', '2013-08-12 20:21:51'),
(22, 8, 13, 1, '2013-08-12 20:25:19', '2013-08-12 20:25:19'),
(23, 8, 0, 1.5, '2013-08-12 20:25:19', '2013-08-12 20:25:19'),
(24, 8, 0, 2, '2013-08-12 20:25:19', '2013-08-12 20:25:19'),
(25, 9, 6, 1, '2013-08-12 20:26:42', '2013-08-20 17:55:58'),
(26, 9, 0, 1.5, '2013-08-12 20:26:42', '2013-08-12 20:26:42'),
(27, 9, 0, 2, '2013-08-12 20:26:42', '2013-08-12 20:26:42'),
(28, 10, 8, 1, '2013-08-12 20:30:53', '2013-08-20 17:36:45'),
(29, 10, 0, 1.5, '2013-08-12 20:30:53', '2013-08-12 20:30:53'),
(30, 10, 0, 2, '2013-08-12 20:30:53', '2013-08-12 20:30:53'),
(31, 11, 6, 1, '2013-08-12 20:34:17', '2013-08-20 17:33:41'),
(32, 11, 0, 1.5, '2013-08-12 20:34:17', '2013-08-12 21:33:02'),
(33, 11, 0, 2, '2013-08-12 20:34:17', '2013-08-12 20:34:17'),
(34, 12, 6.5, 1, '2013-08-12 20:36:32', '2013-08-20 17:40:33'),
(35, 12, 0, 1.5, '2013-08-12 20:36:32', '2013-08-12 20:36:32'),
(36, 12, 0, 2, '2013-08-12 20:36:32', '2013-08-12 20:36:32'),
(37, 13, 3, 1, '2013-08-12 20:40:38', '2013-08-20 17:51:20'),
(38, 13, 0, 1.5, '2013-08-12 20:40:38', '2013-08-12 20:40:38'),
(39, 13, 0, 2, '2013-08-12 20:40:38', '2013-08-12 20:40:38'),
(40, 14, 4.5, 1, '2013-08-12 20:44:41', '2013-08-12 20:44:41'),
(41, 14, 0, 1.5, '2013-08-12 20:44:41', '2013-08-12 20:44:41'),
(42, 14, 0, 2, '2013-08-12 20:44:41', '2013-08-12 20:44:41'),
(43, 15, 7, 1, '2013-08-12 20:45:27', '2013-08-20 17:35:27'),
(44, 15, 0, 1.5, '2013-08-12 20:45:27', '2013-08-12 20:45:27'),
(45, 15, 0, 2, '2013-08-12 20:45:27', '2013-08-12 20:45:27'),
(46, 16, 8, 1, '2013-08-12 20:46:45', '2013-08-12 21:29:44'),
(47, 16, 0, 1.5, '2013-08-12 20:46:45', '2013-08-12 20:46:45'),
(48, 16, 0, 2, '2013-08-12 20:46:45', '2013-08-12 20:46:45'),
(49, 17, 5, 1, '2013-08-12 20:48:57', '2013-08-12 21:30:35'),
(50, 17, 0, 1.5, '2013-08-12 20:48:57', '2013-08-12 20:48:57'),
(51, 17, 0, 2, '2013-08-12 20:48:57', '2013-08-12 20:48:57'),
(52, 18, 6, 1, '2013-08-12 20:51:10', '2013-08-20 17:33:52'),
(53, 18, 0, 1.5, '2013-08-12 20:51:10', '2013-08-12 20:51:10'),
(54, 18, 0, 2, '2013-08-12 20:51:10', '2013-08-12 20:51:10'),
(55, 19, 3, 1, '2013-08-12 20:52:37', '2013-08-20 17:45:37'),
(56, 19, 0, 1.5, '2013-08-12 20:52:37', '2013-08-12 20:52:37'),
(57, 19, 0, 2, '2013-08-12 20:52:37', '2013-08-12 20:52:37'),
(58, 20, 5, 1, '2013-08-12 20:53:54', '2013-08-20 17:32:01'),
(59, 20, 0, 1.5, '2013-08-12 20:53:54', '2013-08-12 20:53:54'),
(60, 20, 0, 2, '2013-08-12 20:53:54', '2013-08-12 20:53:54'),
(61, 21, 7, 1, '2013-08-12 20:55:14', '2013-08-12 21:31:31'),
(62, 21, 0, 1.5, '2013-08-12 20:55:14', '2013-08-12 20:55:14'),
(63, 21, 0, 2, '2013-08-12 20:55:14', '2013-08-12 20:55:14'),
(64, 22, 4.5, 1, '2013-08-12 20:57:11', '2013-08-12 21:30:28'),
(65, 22, 0, 1.5, '2013-08-12 20:57:11', '2013-08-12 20:57:11'),
(66, 22, 0, 2, '2013-08-12 20:57:11', '2013-08-12 20:57:11'),
(67, 23, 3.5, 1, '2013-08-12 20:58:34', '2013-08-20 17:48:00'),
(68, 23, 0, 1.5, '2013-08-12 20:58:34', '2013-08-12 20:58:34'),
(69, 23, 0, 2, '2013-08-12 20:58:34', '2013-08-12 20:58:34'),
(70, 24, 2.5, 1, '2013-08-12 20:59:57', '2013-08-20 17:51:10'),
(71, 24, 0, 1.5, '2013-08-12 20:59:57', '2013-08-12 20:59:57'),
(72, 24, 0, 2, '2013-08-12 20:59:57', '2013-08-12 20:59:57'),
(73, 25, 4, 1, '2013-08-12 21:01:20', '2013-08-20 17:50:59'),
(74, 25, 0, 1.5, '2013-08-12 21:01:20', '2013-08-12 21:01:20'),
(75, 25, 0, 2, '2013-08-12 21:01:20', '2013-08-12 21:01:20'),
(76, 26, 7, 1, '2013-08-12 21:02:26', '2013-08-20 17:33:20'),
(77, 26, 0, 1.5, '2013-08-12 21:02:26', '2013-08-12 21:02:26'),
(78, 26, 0, 2, '2013-08-12 21:02:26', '2013-08-12 21:02:26'),
(79, 27, 13, 1, '2013-08-12 21:05:30', '2013-08-20 17:34:27'),
(80, 27, 0, 1.5, '2013-08-12 21:05:30', '2013-08-12 21:05:30'),
(81, 27, 0, 2, '2013-08-12 21:05:30', '2013-08-12 21:05:30'),
(82, 28, 3.5, 1, '2013-08-12 21:07:11', '2013-08-20 17:49:42'),
(83, 28, 0, 1.5, '2013-08-12 21:07:11', '2013-08-12 21:07:11'),
(84, 28, 0, 2, '2013-08-12 21:07:11', '2013-08-12 21:07:11'),
(85, 29, 6, 1, '2013-08-12 21:08:18', '2013-08-20 17:37:59'),
(86, 29, 0, 1.5, '2013-08-12 21:08:18', '2013-08-12 21:08:18'),
(87, 29, 0, 2, '2013-08-12 21:08:18', '2013-08-12 21:08:18'),
(88, 30, 6.5, 1, '2013-08-12 21:09:11', '2013-08-20 17:38:43'),
(89, 30, 0, 1.5, '2013-08-12 21:09:11', '2013-08-12 21:09:11'),
(90, 30, 0, 2, '2013-08-12 21:09:11', '2013-08-12 21:09:11'),
(91, 31, 6, 1, '2013-08-12 21:10:43', '2013-08-20 17:32:12'),
(92, 31, 0, 1.5, '2013-08-12 21:10:43', '2013-08-12 21:10:43'),
(93, 31, 0, 2, '2013-08-12 21:10:43', '2013-08-12 21:10:43'),
(94, 32, 5, 1, '2013-08-12 21:11:53', '2013-08-12 21:11:53'),
(95, 32, 0, 1.5, '2013-08-12 21:11:53', '2013-08-12 21:11:53'),
(96, 32, 0, 2, '2013-08-12 21:11:53', '2013-08-12 21:11:53'),
(97, 33, 6.5, 1, '2013-08-12 21:13:17', '2013-08-20 17:32:25'),
(98, 33, 0, 1.5, '2013-08-12 21:13:17', '2013-08-12 21:13:17'),
(99, 33, 0, 2, '2013-08-12 21:13:17', '2013-08-12 21:13:17'),
(100, 34, 3.5, 1, '2013-08-12 21:14:37', '2013-08-20 17:48:23'),
(101, 34, 0, 1.5, '2013-08-12 21:14:37', '2013-08-12 21:14:37'),
(102, 34, 0, 2, '2013-08-12 21:14:37', '2013-08-12 21:14:37'),
(103, 35, 4, 1, '2013-08-12 21:16:34', '2013-08-20 17:51:32'),
(104, 35, 0, 1.5, '2013-08-12 21:16:34', '2013-08-12 21:16:34'),
(105, 35, 0, 2, '2013-08-12 21:16:34', '2013-08-12 21:16:34'),
(106, 36, 5, 1, '2013-08-12 21:18:31', '2013-08-20 17:56:07'),
(107, 36, 0, 1.5, '2013-08-12 21:18:31', '2013-08-12 21:18:31'),
(108, 36, 0, 2, '2013-08-12 21:18:31', '2013-08-12 21:18:31'),
(109, 37, 2, 1, '2013-08-12 21:19:51', '2013-08-12 21:19:51'),
(110, 37, 0, 1.5, '2013-08-12 21:19:51', '2013-08-12 21:19:51'),
(111, 37, 0, 2, '2013-08-12 21:19:51', '2013-08-12 21:19:51'),
(112, 38, 2, 1, '2013-08-12 21:21:23', '2013-08-12 21:21:23'),
(113, 38, 0, 1.5, '2013-08-12 21:21:23', '2013-08-12 21:21:23'),
(114, 38, 0, 2, '2013-08-12 21:21:23', '2013-08-12 21:21:23'),
(115, 39, 1, 1, '2013-08-12 21:22:33', '2013-08-20 18:01:44'),
(116, 39, 0, 1.5, '2013-08-12 21:22:33', '2013-08-12 21:22:33'),
(117, 39, 0, 2, '2013-08-12 21:22:33', '2013-08-12 21:22:33'),
(118, 40, 6, 1, '2013-08-17 15:01:56', '2013-08-17 15:12:37'),
(119, 40, 8, 1.5, '2013-08-17 15:01:56', '2013-08-17 15:12:37'),
(120, 40, 9, 2, '2013-08-17 15:01:56', '2013-08-17 15:12:37'),
(121, 41, 6.5, 1, '2013-08-20 17:11:55', '2013-08-20 17:16:02'),
(122, 41, 0, 1.5, '2013-08-20 17:11:55', '2013-08-20 17:11:55'),
(123, 41, 0, 2, '2013-08-20 17:11:55', '2013-08-20 17:11:55'),
(124, 42, 6, 1, '2013-08-20 17:17:48', '2013-08-20 17:17:48'),
(125, 42, 0, 1.5, '2013-08-20 17:17:48', '2013-08-20 17:17:48'),
(126, 42, 0, 2, '2013-08-20 17:17:48', '2013-08-20 17:17:48'),
(127, 43, 9, 1, '2013-08-20 17:19:34', '2013-08-20 17:19:34'),
(128, 43, 0, 1.5, '2013-08-20 17:19:34', '2013-08-20 17:19:34'),
(129, 43, 0, 2, '2013-08-20 17:19:34', '2013-08-20 17:19:34'),
(130, 44, 8.5, 1, '2013-08-20 17:20:03', '2013-08-20 17:20:15'),
(131, 44, 0, 1.5, '2013-08-20 17:20:03', '2013-08-20 17:20:03'),
(132, 44, 0, 2, '2013-08-20 17:20:03', '2013-08-20 17:20:03'),
(133, 45, 6, 1, '2013-08-20 17:21:45', '2013-08-20 17:21:45'),
(134, 45, 0, 1.5, '2013-08-20 17:21:45', '2013-08-20 17:21:45'),
(135, 45, 0, 2, '2013-08-20 17:21:45', '2013-08-20 17:21:45'),
(136, 46, 6.5, 1, '2013-08-20 17:22:57', '2013-08-20 17:22:57'),
(137, 46, 0, 1.5, '2013-08-20 17:22:57', '2013-08-20 17:22:57'),
(138, 46, 0, 2, '2013-08-20 17:22:57', '2013-08-20 17:22:57'),
(139, 47, 2.5, 1, '2013-08-20 17:24:03', '2013-08-20 17:24:03'),
(140, 47, 0, 1.5, '2013-08-20 17:24:03', '2013-08-20 17:24:03'),
(141, 47, 0, 2, '2013-08-20 17:24:03', '2013-08-20 17:24:03'),
(142, 48, 4, 1, '2013-08-20 17:26:14', '2013-08-20 17:26:14'),
(143, 48, 0, 1.5, '2013-08-20 17:26:14', '2013-08-20 17:26:14'),
(144, 48, 0, 2, '2013-08-20 17:26:14', '2013-08-20 17:26:14'),
(145, 49, 4, 1, '2013-08-20 17:28:40', '2013-08-20 17:57:32'),
(146, 49, 0, 1.5, '2013-08-20 17:28:40', '2013-08-20 17:28:40'),
(147, 49, 0, 2, '2013-08-20 17:28:40', '2013-08-20 17:28:40'),
(148, 50, 16, 1, '2013-08-20 17:41:26', '2013-08-20 17:41:26'),
(149, 50, 0, 1.5, '2013-08-20 17:41:26', '2013-08-20 17:41:26'),
(150, 50, 0, 2, '2013-08-20 17:41:26', '2013-08-20 17:41:26'),
(151, 51, 6.5, 1, '2013-08-20 17:42:54', '2013-08-20 17:43:55'),
(152, 51, 0, 1.5, '2013-08-20 17:42:54', '2013-08-20 17:42:54'),
(153, 51, 0, 2, '2013-08-20 17:42:54', '2013-08-20 17:42:54'),
(154, 52, 6, 1, '2013-08-20 17:43:22', '2013-08-20 17:43:49'),
(155, 52, 0, 1.5, '2013-08-20 17:43:22', '2013-08-20 17:43:22'),
(156, 52, 0, 2, '2013-08-20 17:43:22', '2013-08-20 17:43:22'),
(157, 53, 3, 1, '2013-08-20 17:46:14', '2013-08-20 17:46:14'),
(158, 53, 0, 1.5, '2013-08-20 17:46:14', '2013-08-20 17:46:14'),
(159, 53, 0, 2, '2013-08-20 17:46:14', '2013-08-20 17:46:14'),
(160, 54, 3.5, 1, '2013-08-20 17:46:50', '2013-08-20 17:47:30'),
(161, 54, 0, 1.5, '2013-08-20 17:46:50', '2013-08-20 17:46:50'),
(162, 54, 0, 2, '2013-08-20 17:46:50', '2013-08-20 17:46:50'),
(163, 55, 3.5, 1, '2013-08-20 17:47:23', '2013-08-20 17:47:23'),
(164, 55, 0, 1.5, '2013-08-20 17:47:23', '2013-08-20 17:47:23'),
(165, 55, 0, 2, '2013-08-20 17:47:23', '2013-08-20 17:47:23'),
(166, 56, 1, 1, '2013-08-20 18:03:17', '2013-08-20 20:44:00'),
(167, 56, 0, 1.5, '2013-08-20 18:03:17', '2013-08-20 18:03:17'),
(168, 56, 0, 2, '2013-08-20 18:03:17', '2013-08-20 18:03:17'),
(169, 57, 1.5, 1, '2013-08-20 18:03:30', '2013-08-20 20:45:07'),
(170, 57, 0, 1.5, '2013-08-20 18:03:30', '2013-08-20 18:03:30'),
(171, 57, 0, 2, '2013-08-20 18:03:30', '2013-08-20 18:03:30'),
(172, 58, 3, 1, '2013-08-20 18:03:53', '2013-08-20 20:43:02'),
(173, 58, 0, 1.5, '2013-08-20 18:03:53', '2013-08-20 18:03:53'),
(174, 58, 0, 2, '2013-08-20 18:03:53', '2013-08-20 18:03:53'),
(175, 59, 4, 1, '2013-08-20 18:04:04', '2013-08-20 20:46:08'),
(176, 59, 0, 1.5, '2013-08-20 18:04:04', '2013-08-20 18:04:04'),
(177, 59, 0, 2, '2013-08-20 18:04:04', '2013-08-20 18:04:04'),
(178, 60, 4, 1, '2013-08-20 18:04:18', '2013-08-20 20:39:41'),
(179, 60, 0, 1.5, '2013-08-20 18:04:18', '2013-08-20 18:04:18'),
(180, 60, 0, 2, '2013-08-20 18:04:18', '2013-08-20 18:04:18'),
(181, 61, 2, 1, '2013-08-20 18:05:58', '2013-08-20 20:05:24'),
(182, 61, 0, 1.5, '2013-08-20 18:05:58', '2013-08-20 18:05:58'),
(183, 61, 0, 2, '2013-08-20 18:05:58', '2013-08-20 18:05:58'),
(184, 62, 3.5, 1, '2013-08-20 18:06:12', '2013-08-20 20:38:01'),
(185, 62, 0, 1.5, '2013-08-20 18:06:12', '2013-08-20 18:06:12'),
(186, 62, 0, 2, '2013-08-20 18:06:12', '2013-08-20 18:06:12'),
(187, 63, 2.5, 1, '2013-08-20 18:06:30', '2013-08-20 18:11:09'),
(188, 63, 0, 1.5, '2013-08-20 18:06:30', '2013-08-20 18:06:30'),
(189, 63, 0, 2, '2013-08-20 18:06:30', '2013-08-20 18:06:30'),
(190, 64, 2.5, 1, '2013-08-20 18:06:41', '2013-08-20 18:10:57'),
(191, 64, 0, 1.5, '2013-08-20 18:06:41', '2013-08-20 18:06:41'),
(192, 64, 0, 2, '2013-08-20 18:06:41', '2013-08-20 18:06:41'),
(193, 65, 2.5, 1, '2013-08-20 18:11:32', '2013-08-20 18:11:41'),
(194, 65, 0, 1.5, '2013-08-20 18:11:32', '2013-08-20 18:11:32'),
(195, 65, 0, 2, '2013-08-20 18:11:32', '2013-08-20 18:11:32'),
(196, 66, 3, 1, '2013-08-20 19:59:59', '2013-08-20 20:18:18'),
(197, 66, 0, 1.5, '2013-08-20 19:59:59', '2013-08-20 19:59:59'),
(198, 66, 0, 2, '2013-08-20 19:59:59', '2013-08-20 19:59:59'),
(199, 67, 1.5, 1, '2013-08-20 20:00:45', '2013-08-20 20:31:09'),
(200, 67, 0, 1.5, '2013-08-20 20:00:45', '2013-08-20 20:00:45'),
(201, 67, 0, 2, '2013-08-20 20:00:45', '2013-08-20 20:00:45'),
(202, 68, 2, 1, '2013-08-20 20:00:58', '2013-08-20 20:35:48'),
(203, 68, 0, 1.5, '2013-08-20 20:00:58', '2013-08-20 20:00:58'),
(204, 68, 0, 2, '2013-08-20 20:00:58', '2013-08-20 20:00:58'),
(205, 69, 2, 1, '2013-08-20 20:01:24', '2013-08-21 01:02:13'),
(206, 69, 0, 1.5, '2013-08-20 20:01:24', '2013-08-20 20:01:24'),
(207, 69, 0, 2, '2013-08-20 20:01:24', '2013-08-20 20:01:24'),
(208, 70, 1.5, 1, '2013-08-20 20:01:36', '2013-08-20 20:33:02'),
(209, 70, 0, 1.5, '2013-08-20 20:01:36', '2013-08-20 20:01:36'),
(210, 70, 0, 2, '2013-08-20 20:01:36', '2013-08-20 20:01:36'),
(211, 71, 3, 1, '2013-08-20 20:01:47', '2013-08-20 20:22:58'),
(212, 71, 0, 1.5, '2013-08-20 20:01:47', '2013-08-20 20:01:47'),
(213, 71, 0, 2, '2013-08-20 20:01:47', '2013-08-20 20:01:47'),
(214, 72, 3, 1, '2013-08-20 20:01:56', '2013-08-21 00:59:39'),
(215, 72, 0, 1.5, '2013-08-20 20:01:56', '2013-08-20 20:01:56'),
(216, 72, 0, 2, '2013-08-20 20:01:56', '2013-08-20 20:01:56'),
(217, 73, 1.5, 1, '2013-08-20 20:02:17', '2013-08-20 20:28:20'),
(218, 73, 0, 1.5, '2013-08-20 20:02:17', '2013-08-20 20:02:17'),
(219, 73, 0, 2, '2013-08-20 20:02:17', '2013-08-20 20:02:17'),
(220, 74, 2, 1, '2013-08-20 20:02:45', '2013-08-20 20:20:32'),
(221, 74, 0, 1.5, '2013-08-20 20:02:45', '2013-08-20 20:02:45'),
(222, 74, 0, 2, '2013-08-20 20:02:45', '2013-08-20 20:02:45'),
(223, 75, 5, 1, '2013-08-20 20:47:24', '2013-08-21 00:24:30'),
(224, 75, 0, 1.5, '2013-08-20 20:47:24', '2013-08-20 20:47:24'),
(225, 75, 0, 2, '2013-08-20 20:47:24', '2013-08-20 20:47:24'),
(226, 76, 5, 1, '2013-08-20 20:47:37', '2013-08-21 00:45:10'),
(227, 76, 0, 1.5, '2013-08-20 20:47:37', '2013-08-20 20:47:37'),
(228, 76, 0, 2, '2013-08-20 20:47:37', '2013-08-20 20:47:37'),
(229, 77, 5, 1, '2013-08-20 20:48:08', '2013-08-21 00:33:15'),
(230, 77, 0, 1.5, '2013-08-20 20:48:08', '2013-08-20 20:48:08'),
(231, 77, 0, 2, '2013-08-20 20:48:08', '2013-08-20 20:48:08'),
(232, 78, 5, 1, '2013-08-20 20:48:22', '2013-08-21 00:40:24'),
(233, 78, 0, 1.5, '2013-08-20 20:48:22', '2013-08-20 20:48:22'),
(234, 78, 0, 2, '2013-08-20 20:48:22', '2013-08-20 20:48:22'),
(235, 79, 5, 1, '2013-08-20 20:48:38', '2013-08-21 00:42:09'),
(236, 79, 0, 1.5, '2013-08-20 20:48:38', '2013-08-20 20:48:38'),
(237, 79, 0, 2, '2013-08-20 20:48:38', '2013-08-20 20:48:38'),
(238, 80, 5, 1, '2013-08-20 20:48:49', '2013-08-21 00:39:04'),
(239, 80, 0, 1.5, '2013-08-20 20:48:49', '2013-08-20 20:48:49'),
(240, 80, 0, 2, '2013-08-20 20:48:49', '2013-08-20 20:48:49'),
(241, 81, 5, 1, '2013-08-20 20:48:59', '2013-08-21 00:37:55'),
(242, 81, 0, 1.5, '2013-08-20 20:48:59', '2013-08-20 20:48:59'),
(243, 81, 0, 2, '2013-08-20 20:48:59', '2013-08-20 20:48:59'),
(244, 82, 5, 1, '2013-08-20 20:49:21', '2013-08-21 00:36:04'),
(245, 82, 0, 1.5, '2013-08-20 20:49:21', '2013-08-20 20:49:21'),
(246, 82, 0, 2, '2013-08-20 20:49:21', '2013-08-20 20:49:21'),
(247, 83, 5, 1, '2013-08-20 20:49:33', '2013-08-21 00:21:03'),
(248, 83, 0, 1.5, '2013-08-20 20:49:33', '2013-08-20 20:49:33'),
(249, 83, 0, 2, '2013-08-20 20:49:33', '2013-08-20 20:49:33'),
(250, 84, 5, 1, '2013-08-20 20:49:43', '2013-08-20 20:49:43'),
(251, 84, 0, 1.5, '2013-08-20 20:49:43', '2013-08-20 20:49:43'),
(252, 84, 0, 2, '2013-08-20 20:49:43', '2013-08-20 20:49:43'),
(253, 85, 5, 1, '2013-08-20 20:49:54', '2013-08-21 00:07:04'),
(254, 85, 0, 1.5, '2013-08-20 20:49:54', '2013-08-20 20:49:54'),
(255, 85, 0, 2, '2013-08-20 20:49:54', '2013-08-20 20:49:54'),
(256, 86, 3, 1, '2013-08-20 23:59:38', '2013-08-21 00:02:51'),
(257, 86, 0, 1.5, '2013-08-20 23:59:38', '2013-08-20 23:59:38'),
(258, 86, 0, 2, '2013-08-20 23:59:38', '2013-08-20 23:59:38'),
(259, 87, 4, 1, '2013-08-21 00:08:44', '2013-08-21 00:09:15'),
(260, 87, 0, 1.5, '2013-08-21 00:08:44', '2013-08-21 00:08:44'),
(261, 87, 0, 2, '2013-08-21 00:08:44', '2013-08-21 00:08:44'),
(262, 88, 34, 1, '2013-08-21 00:45:44', '2013-08-21 00:45:44'),
(263, 88, 0, 1.5, '2013-08-21 00:45:44', '2013-08-21 00:45:44'),
(264, 88, 0, 2, '2013-08-21 00:45:44', '2013-08-21 00:45:44'),
(265, 89, 4, 1, '2013-08-21 00:46:00', '2013-08-21 00:46:00'),
(266, 89, 0, 1.5, '2013-08-21 00:46:00', '2013-08-21 00:46:00'),
(267, 89, 0, 2, '2013-08-21 00:46:00', '2013-08-21 00:46:00'),
(268, 90, 3, 1, '2013-08-21 00:47:12', '2013-08-21 00:47:12'),
(269, 90, 0, 1.5, '2013-08-21 00:47:12', '2013-08-21 00:47:12'),
(270, 90, 0, 2, '2013-08-21 00:47:12', '2013-08-21 00:47:12'),
(271, 91, 6, 1, '2013-08-21 00:48:26', '2013-08-21 00:49:15'),
(272, 91, 0, 1.5, '2013-08-21 00:48:26', '2013-08-21 00:48:26'),
(273, 91, 0, 2, '2013-08-21 00:48:26', '2013-08-21 00:48:26'),
(274, 92, 3, 1, '2013-08-21 19:41:19', '2013-08-21 19:41:19'),
(275, 92, 0, 1.5, '2013-08-21 19:41:19', '2013-08-21 19:41:19'),
(276, 92, 0, 2, '2013-08-21 19:41:19', '2013-08-21 19:41:19'),
(277, 93, 3, 1, '2013-08-21 19:42:17', '2013-08-21 19:42:17'),
(278, 93, 0, 1.5, '2013-08-21 19:42:17', '2013-08-21 19:42:17'),
(279, 93, 0, 2, '2013-08-21 19:42:17', '2013-08-21 19:42:17'),
(280, 94, 1, 1, '2013-08-21 19:45:56', '2013-08-21 19:45:56'),
(281, 94, 0, 1.5, '2013-08-21 19:45:56', '2013-08-21 19:45:56'),
(282, 94, 0, 2, '2013-08-21 19:45:56', '2013-08-21 19:45:56'),
(283, 95, 1.5, 1, '2013-08-21 19:46:11', '2013-08-21 19:46:54'),
(284, 95, 0, 1.5, '2013-08-21 19:46:11', '2013-08-21 19:46:11'),
(285, 95, 0, 2, '2013-08-21 19:46:11', '2013-08-21 19:46:11'),
(286, 96, 1.5, 1, '2013-08-21 19:46:30', '2013-08-21 19:46:30'),
(287, 96, 0, 1.5, '2013-08-21 19:46:30', '2013-08-21 19:46:30'),
(288, 96, 0, 2, '2013-08-21 19:46:30', '2013-08-21 19:46:30');

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `printer_details`
--

CREATE TABLE IF NOT EXISTS `printer_details` (
  `printer_id` int(11) NOT NULL AUTO_INCREMENT,
  `printer_ip` text CHARACTER SET utf8 COLLATE utf8_turkish_ci NOT NULL,
  `type` int(11) NOT NULL,
  `printer_desc` text CHARACTER SET utf8 COLLATE utf8_turkish_ci NOT NULL,
  `is_deleted` tinyint(4) NOT NULL,
  PRIMARY KEY (`printer_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- Tablo döküm verisi `printer_details`
--

INSERT INTO `printer_details` (`printer_id`, `printer_ip`, `type`, `printer_desc`, `is_deleted`) VALUES
(1, '192.168.2.100', 0, 'MUTFAK1', 0);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `process_details`
--

CREATE TABLE IF NOT EXISTS `process_details` (
  `process_id` int(11) NOT NULL AUTO_INCREMENT,
  `process_name` varchar(20) CHARACTER SET utf8 COLLATE utf8_turkish_ci NOT NULL,
  `process_desc` text CHARACTER SET utf8 COLLATE utf8_turkish_ci NOT NULL,
  `parent_id` int(11) NOT NULL,
  `display_order` int(11) NOT NULL,
  `modified_date` datetime NOT NULL,
  PRIMARY KEY (`process_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- Tablo döküm verisi `process_details`
--

INSERT INTO `process_details` (`process_id`, `process_name`, `process_desc`, `parent_id`, `display_order`, `modified_date`) VALUES
(1, 'PERSONEL MAASI', '', 0, 0, '2013-08-12 19:48:54');

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `product_details`
--

CREATE TABLE IF NOT EXISTS `product_details` (
  `product_id` int(11) NOT NULL AUTO_INCREMENT,
  `product_code_manual` varchar(15) COLLATE utf8_turkish_ci DEFAULT NULL,
  `product_price` double NOT NULL,
  `product_cat` int(11) DEFAULT NULL,
  `product_color` int(11) DEFAULT NULL,
  `product_name` varchar(30) COLLATE utf8_turkish_ci NOT NULL,
  `product_desc` text COLLATE utf8_turkish_ci,
  `product_img_path` text COLLATE utf8_turkish_ci,
  `product_isDeleted` tinyint(1) NOT NULL,
  `isOnMenu` tinyint(1) NOT NULL,
  `modified_date` datetime NOT NULL,
  `unit` int(11) NOT NULL,
  `unit_amount` double(11,4) NOT NULL,
  `currency` int(11) NOT NULL,
  PRIMARY KEY (`product_id`),
  KEY `product_cat` (`product_cat`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=97 ;

--
-- Tablo döküm verisi `product_details`
--

INSERT INTO `product_details` (`product_id`, `product_code_manual`, `product_price`, `product_cat`, `product_color`, `product_name`, `product_desc`, `product_img_path`, `product_isDeleted`, `isOnMenu`, `modified_date`, `unit`, `unit_amount`, `currency`) VALUES
(1, 'YEMEK', 0, 1, NULL, 'AKDENIZ', '', '_____1.jpg', 0, 1, '2013-08-20 17:34:34', 0, 0.0000, 0),
(2, 'YEMEK', 0, 3, NULL, 'CENGIZ', '', '_2.jpg', 0, 1, '2013-08-12 20:10:05', 0, 0.0000, 0),
(3, 'YEMEK', 0, 2, NULL, 'BUGDAYLI ANADOLU USULÜ', '', '_3.jpg', 0, 1, '2013-08-12 21:28:38', 0, 0.0000, 0),
(4, 'YEMEK', 0, 3, NULL, 'CHEESEBURGER', 'CHEESEBURGER+ CIPS', '_4.png', 0, 1, '2013-08-20 17:08:39', 0, 0.0000, 0),
(5, 'YEMEK', 0, 10, NULL, 'FRAMBUAZLI CHEESECAKE', '', '_5.jpg', 0, 1, '2013-08-20 20:51:00', 0, 0.0000, 0),
(6, 'YEMEK', 0, 4, NULL, 'AYVALIK', '', '__6.jpg', 0, 1, '2013-08-20 17:42:19', 0, 0.0000, 0),
(7, 'YEMEK', 0, 3, NULL, 'DUBLE BIG', '', '_7.png', 0, 1, '2013-08-12 21:31:00', 0, 0.0000, 0),
(8, 'YEMEK', 0, 5, NULL, 'DUBLE MENÜ', '2 KARSIYAKA', '8.jpg', 0, 1, '2013-08-12 20:25:19', 0, 0.0000, 0),
(9, 'YEMEK', 0, 5, NULL, 'DOYURAN MENÜ', '', '_9.jpg', 0, 0, '2013-08-20 17:55:58', 0, 0.0000, 0),
(10, 'YEMEK', 0, 7, NULL, 'SOYA SOSLU TAVUK', '', '_10.jpg', 0, 1, '2013-08-20 17:36:45', 0, 0.0000, 0),
(11, 'YEMEK', 0, 6, NULL, 'TAVUKLU PENNE', '', '_____11.jpg', 0, 1, '2013-08-20 17:33:41', 0, 0.0000, 0),
(12, 'YEMEK', 0, 11, NULL, 'KAHVALTI TABAGI', '', '_12.jpg', 0, 1, '2013-08-20 17:40:33', 0, 0.0000, 0),
(13, 'YEMEK', 0, 4, NULL, 'KARISIK', '', '__13.png', 0, 0, '2013-08-20 17:51:20', 0, 0.0000, 0),
(14, 'YEMEK', 0, 4, NULL, 'KARSIYAKA KARISIK', '', '14.jpg', 0, 1, '2013-08-12 20:44:41', 0, 0.0000, 0),
(15, 'YEMEK', 0, 7, NULL, 'KÖRI SOSLU TAVUK', '', '_15.JPG', 0, 1, '2013-08-20 17:35:27', 0, 0.0000, 0),
(16, 'YEMEK', 0, 1, NULL, 'MARGHARITA', '', '__16.jpg', 0, 1, '2013-08-12 21:29:44', 0, 0.0000, 0),
(17, 'YEMEK', 0, 3, NULL, 'MEXICAN', '', '_17.jpg', 0, 1, '2013-08-12 21:30:35', 0, 0.0000, 0),
(18, 'YEMEK', 0, 6, NULL, 'PESTO FETTUCINI', '', '__18.jpg', 0, 1, '2013-08-20 17:33:52', 0, 0.0000, 0),
(19, 'YEMEK', 0, 4, NULL, 'KASARLI', '', '__19.jpg', 0, 1, '2013-08-20 17:45:37', 0, 0.0000, 0),
(20, 'YEMEK', 0, 2, NULL, 'ROKKA', '', '__20.jpg', 0, 1, '2013-08-20 17:32:01', 0, 0.0000, 0),
(21, 'YEMEK', 0, 6, NULL, 'SEBZELI TORTELLONI', '', '_21.JPG', 0, 1, '2013-08-12 21:31:31', 0, 0.0000, 0),
(22, 'YEMEK', 0, 3, NULL, 'SNITZEL', '', '_22.jpg', 0, 1, '2013-08-12 21:30:28', 0, 0.0000, 0),
(23, 'YEMEK', 0, 4, NULL, 'SOSIS + KASAR', '', '__23.png', 0, 1, '2013-08-20 17:48:00', 0, 0.0000, 0),
(24, 'YEMEK', 0, 4, NULL, 'SOGUK ANATOLIAN SANDWICH', '', '_24.png', 0, 0, '2013-08-20 17:51:10', 0, 0.0000, 0),
(25, 'YEMEK', 0, 4, NULL, 'SOGUK TUNA SANDWICH', '', '_25.jpg', 0, 0, '2013-08-20 17:50:59', 0, 0.0000, 0),
(26, 'YEMEK', 0, 6, NULL, 'SPAGHETTI BOLONEZ', '', '__26.jpg', 0, 1, '2013-08-20 17:33:20', 0, 0.0000, 0),
(27, 'YEMEK', 0, 1, NULL, 'SPECIAL PIZZA', '', '_27.jpg', 0, 1, '2013-08-20 17:34:27', 0, 0.0000, 0),
(28, 'YEMEK', 0, 4, NULL, 'SUCUK + KASAR', '', '___28.jpg', 0, 1, '2013-08-20 17:49:42', 0, 0.0000, 0),
(29, 'YEMEK', 0, 7, NULL, 'SUSAMLI TAVUK SIS', '', '_29.jpg', 0, 0, '2013-08-20 17:37:59', 0, 0.0000, 0),
(30, 'YEMEK', 0, 7, NULL, 'TAVUK SINITZEL', '', '_30.jpg', 0, 1, '2013-08-20 17:38:43', 0, 0.0000, 0),
(31, 'YEMEK', 0, 2, NULL, 'TAVUKLU SEZAR', '', '__31.jpg', 0, 1, '2013-08-20 17:32:12', 0, 0.0000, 0),
(32, 'YEMEK', 0, 10, NULL, 'TIRAMISU', '', '32.png', 0, 1, '2013-08-12 21:11:53', 0, 0.0000, 0),
(33, 'YEMEK', 0, 2, NULL, 'TON', '', '__33.jpg', 0, 1, '2013-08-20 17:32:25', 0, 0.0000, 0),
(34, 'YEMEK', 0, 4, NULL, 'WEMBLEY SANDWICH', '', '_34.jpg', 0, 0, '2013-08-20 17:48:23', 0, 0.0000, 0),
(35, 'YEMEK', 0, 5, NULL, 'ÇOCUK MENÜSÜ KARISIK', '', '_35.png', 0, 0, '2013-08-20 17:51:32', 0, 0.0000, 0),
(36, 'YEMEK', 0, 5, NULL, 'ÖGRENCI MENÜSÜ', '( CENGIZ BURGER+PATATES CIPSI+AYRAN)', '_36.png', 0, 0, '2013-08-20 17:56:07', 0, 0.0000, 0),
(37, 'YEMEK', 0, 9, NULL, 'COLA', '', '37.png', 0, 1, '2013-08-12 21:19:51', 0, 0.0000, 0),
(38, 'YEMEK', 0, 9, NULL, 'FANTA', '', '38.png', 0, 1, '2013-08-12 21:21:23', 0, 0.0000, 0),
(39, 'YEMEK', 0, 9, NULL, 'SU', '', '_39.png', 0, 1, '2013-08-20 18:01:44', 0, 0.0000, 0),
(40, 'YEMEK', 0, 3, NULL, 'DENEME', 'TANIM', '____40.jpg', 1, 1, '2013-08-17 15:12:37', 0, 0.0000, 0),
(41, 'YEMEK', 0, 5, NULL, 'SINITZEL + COLA', '', '_41.jpg', 0, 1, '2013-08-20 17:16:02', 0, 0.0000, 0),
(42, 'YEMEK', 0, 5, NULL, 'SINITZEL + AYRAN', '', '42.jpg', 0, 1, '2013-08-20 17:17:48', 0, 0.0000, 0),
(43, 'YEMEK', 0, 5, NULL, 'DUBLE + COLA', '', '43.png', 0, 1, '2013-08-20 17:19:34', 0, 0.0000, 0),
(44, 'YEMEK', 0, 5, NULL, 'DUBLE + AYRAN', '', '_44.png', 0, 1, '2013-08-20 17:20:15', 0, 0.0000, 0),
(45, 'YEMEK', 0, 5, NULL, 'CENGIZ + AYRAN', '', '45.jpg', 0, 1, '2013-08-20 17:21:45', 0, 0.0000, 0),
(46, 'YEMEK', 0, 5, NULL, 'CENGIZ + COLA', '', '46.jpg', 0, 1, '2013-08-20 17:22:57', 0, 0.0000, 0),
(47, 'YEMEK', 0, 12, NULL, 'SOGAN HALKASI', '', '47.gif', 0, 1, '2013-08-20 17:24:03', 0, 0.0000, 0),
(48, 'YEMEK', 0, 12, NULL, 'PATETES CIPSI', '', '48.jpg', 0, 1, '2013-08-20 17:26:14', 0, 0.0000, 0),
(49, 'YEMEK', 0, 12, NULL, 'TAVUK PARÇACIKLARI', '', '__49.jpg', 0, 1, '2013-08-20 17:57:32', 0, 0.0000, 0),
(50, 'YEMEK', 0, 11, NULL, 'SERPME (2 KISILIK)', '', '50.jpg', 0, 1, '2013-08-20 17:41:26', 0, 0.0000, 0),
(51, 'YEMEK', 0, 5, NULL, 'AYVALIK + COLA', '', '_51.jpg', 0, 1, '2013-08-20 17:43:55', 0, 0.0000, 0),
(52, 'YEMEK', 0, 5, NULL, 'AYVALIK + AYRAN', '', '_52.jpg', 0, 1, '2013-08-20 17:43:49', 0, 0.0000, 0),
(53, 'YEMEK', 0, 4, NULL, 'SUCUKLU', '', '53.jpg', 0, 1, '2013-08-20 17:46:14', 0, 0.0000, 0),
(54, 'YEMEK', 0, 4, NULL, 'SOSIS + KASAR', '', '_54.jpg', 1, 1, '2013-08-20 17:47:30', 0, 0.0000, 0),
(55, 'YEMEK', 0, 4, NULL, 'SALAM +  KASAR', '', '55.jpg', 0, 1, '2013-08-20 17:47:23', 0, 0.0000, 0),
(56, 'YEMEK', 0, 9, NULL, 'ÇAY (BARDAK)', '', '_56.jpg', 0, 1, '2013-08-20 20:44:00', 0, 0.0000, 0),
(57, 'YEMEK', 0, 9, NULL, 'ÇAY (FINCAN)', '', '_57.jpg', 0, 1, '2013-08-20 20:45:07', 0, 0.0000, 0),
(58, 'YEMEK', 0, 9, NULL, 'NESCAFE CLASSIC', '', '_58.jpg', 0, 1, '2013-08-20 20:43:02', 0, 0.0000, 0),
(59, 'YEMEK', 0, 9, NULL, 'NESCAFE SÜTLÜ', '', '_59.jpg', 0, 1, '2013-08-20 20:46:08', 0, 0.0000, 0),
(60, 'YEMEK', 0, 9, NULL, 'LATTE', '', '_60.jpg', 0, 1, '2013-08-20 20:39:41', 0, 0.0000, 0),
(61, 'YEMEK', 0, 9, NULL, 'BITKI ÇAYLARI', '', '_61.png', 0, 1, '2013-08-20 20:05:24', 0, 0.0000, 0),
(62, 'YEMEK', 0, 9, NULL, 'CAPPUCCINO', '', '_62.jpg', 0, 1, '2013-08-20 20:38:01', 0, 0.0000, 0),
(63, 'YEMEK', 0, 9, NULL, 'TÜRK KAHVESI SADE', '', '_63.png', 0, 1, '2013-08-20 18:11:09', 0, 0.0000, 0),
(64, 'YEMEK', 0, 9, NULL, 'TÜRK KAHVESI ORTA', '', '_64.png', 0, 1, '2013-08-20 18:10:57', 0, 0.0000, 0),
(65, 'YEMEK', 0, 9, NULL, 'TÜRK KAHVESI SEKERLI', '', '_65.png', 0, 1, '2013-08-20 18:11:41', 0, 0.0000, 0),
(66, 'YEMEK', 0, 9, NULL, 'SAHLEP', '', '66.jpg', 0, 1, '2013-08-20 20:18:18', 0, 0.0000, 0),
(67, 'YEMEK', 0, 9, NULL, 'GAZOZ', '', '67.png', 0, 1, '2013-08-20 20:31:09', 0, 0.0000, 0),
(68, 'YEMEK', 0, 9, NULL, 'ICE TEA', '', '_68.png', 0, 1, '2013-08-20 20:35:48', 0, 0.0000, 0),
(69, 'YEMEK', 0, 9, NULL, 'MEYVE SUYU', '', '_69.jpg', 0, 1, '2013-08-21 01:02:13', 0, 0.0000, 0),
(70, 'YEMEK', 0, 9, NULL, 'SODA', '', '70.png', 0, 1, '2013-08-20 20:33:02', 0, 0.0000, 0),
(71, 'YEMEK', 0, 9, NULL, 'LIMONATA', '', '71.jpg', 0, 1, '2013-08-20 20:22:58', 0, 0.0000, 0),
(72, 'YEMEK', 0, 9, NULL, 'SIKMA PORTAKAL', '', '_72.jpg', 0, 1, '2013-08-21 00:59:39', 0, 0.0000, 0),
(73, 'YEMEK', 0, 9, NULL, 'NANELI AYRAN', '', '_73.jpg', 0, 1, '2013-08-20 20:28:20', 0, 0.0000, 0),
(74, 'YEMEK', 0, 9, NULL, 'SIRMA SODA BÜYÜK', '', '_74.png', 0, 1, '2013-08-20 20:20:32', 0, 0.0000, 0),
(75, 'YEMEK', 0, 10, NULL, 'BEYAZ ÇIKOLATALI PASTA', '', '75.jpg', 0, 1, '2013-08-21 00:24:30', 0, 0.0000, 0),
(76, 'YEMEK', 0, 10, NULL, 'BITTER ÇIKOLATALI PASTA', '', '_76.png', 0, 1, '2013-08-21 00:45:10', 0, 0.0000, 0),
(77, 'YEMEK', 0, 10, NULL, 'FRAMBUAZLI PASTA', '', '_77.jpg', 0, 1, '2013-08-21 00:33:15', 0, 0.0000, 0),
(78, 'YEMEK', 0, 10, NULL, 'VISNE - BITTER ÇIK. PASTA', '', '_78.jpg', 0, 1, '2013-08-21 00:40:24', 0, 0.0000, 0),
(79, 'YEMEK', 0, 10, NULL, 'PROFITEROLLÜ PASTA', '', '_79.jpeg', 0, 1, '2013-08-21 00:42:09', 0, 0.0000, 0),
(80, 'YEMEK', 0, 10, NULL, 'BEYAZ PROF. PASTA', '', '_80.jpg', 0, 1, '2013-08-21 00:39:04', 0, 0.0000, 0),
(81, 'YEMEK', 0, 10, NULL, 'FINDIK KROKANLI PASTA', '', '_81.jpg', 0, 1, '2013-08-21 00:37:55', 0, 0.0000, 0),
(82, 'YEMEK', 0, 10, NULL, 'LIMONLU CHEESECAKE', '', '_82.jpg', 0, 1, '2013-08-21 00:36:04', 0, 0.0000, 0),
(83, 'YEMEK', 0, 10, NULL, 'ÇIKOLATALI CHEESECAKE', '', '__83.jpg', 0, 1, '2013-08-21 00:21:03', 0, 0.0000, 0),
(84, 'YEMEK', 0, 10, NULL, 'FRAMBUAZLI CHEESECAKE', '', '-1', 1, 1, '2013-08-20 20:49:43', 0, 0.0000, 0),
(85, 'YEMEK', 0, 10, NULL, 'MONO PASTA', '', '_85.jpg', 0, 1, '2013-08-21 00:07:04', 0, 0.0000, 0),
(86, 'YEMEK', 0, 7, NULL, 'TEST', '', '_86.jpg', 1, 1, '2013-08-21 00:02:51', 0, 0.0000, 0),
(87, 'YEMEK', 0, 7, NULL, 'TEST', '', '_87.jpg', 1, 1, '2013-08-21 00:09:15', 0, 0.0000, 0),
(88, 'YEMEK', 0, 4, NULL, 'EST', '', '-1', 1, 1, '2013-08-21 00:45:44', 0, 0.0000, 0),
(89, 'YEMEK', 0, 6, NULL, 'TEST2', '', '-1', 1, 1, '2013-08-21 00:46:00', 0, 0.0000, 0),
(90, 'YEMEK', 0, 6, NULL, 'TEST', '', '-1', 1, 1, '2013-08-21 00:47:12', 0, 0.0000, 0),
(91, 'YEMEK', 0, 10, NULL, 'TEST', '', '_91.jpg', 1, 1, '2013-08-21 00:49:15', 0, 0.0000, 0),
(92, 'YEMEK', 0, 9, NULL, 'TÜRK KAHVESI DAMLA SAKIZLI', '', '92.png', 0, 1, '2013-08-21 19:41:19', 0, 0.0000, 0),
(93, 'YEMEK', 0, 9, NULL, 'TÜRK KAHVESI ÇIKOLATALI', '', '93.png', 0, 1, '2013-08-21 19:42:17', 0, 0.0000, 0),
(94, 'YEMEK', 0, 9, NULL, 'AYRAN (KÜÇÜK)', '', '94.png', 0, 1, '2013-08-21 19:45:56', 0, 0.0000, 0),
(95, 'YEMEK', 0, 9, NULL, 'AYRAN (BÜYÜK)', '', '_95.png', 0, 1, '2013-08-21 19:46:54', 0, 0.0000, 0),
(96, 'YEMEK', 0, 9, NULL, 'AYRAN (SISE)', '', '96.png', 0, 1, '2013-08-21 19:46:30', 0, 0.0000, 0);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `product_return`
--

CREATE TABLE IF NOT EXISTS `product_return` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `product_id` int(11) DEFAULT NULL COMMENT 'artik goods_id',
  `product_count` double(11,4) NOT NULL,
  `product_size` double NOT NULL,
  `product_color` int(11) NOT NULL,
  `product_price` double NOT NULL,
  `suppliers_id` int(11) NOT NULL,
  `return_desc` text COLLATE utf8_turkish_ci,
  `modified_date` datetime NOT NULL,
  PRIMARY KEY (`id`),
  KEY `product_id` (`product_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `product_size_limits`
--

CREATE TABLE IF NOT EXISTS `product_size_limits` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `product_id` int(11) NOT NULL,
  `min_size` int(11) NOT NULL,
  `max_size` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `product_id` (`product_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=97 ;

--
-- Tablo döküm verisi `product_size_limits`
--

INSERT INTO `product_size_limits` (`id`, `product_id`, `min_size`, `max_size`) VALUES
(1, 1, 0, 0),
(2, 2, 0, 0),
(3, 3, 0, 0),
(4, 4, 0, 0),
(5, 5, 0, 0),
(6, 6, 0, 0),
(7, 7, 0, 0),
(8, 8, 0, 0),
(9, 9, 0, 0),
(10, 10, 0, 0),
(11, 11, 0, 0),
(12, 12, 0, 0),
(13, 13, 0, 0),
(14, 14, 0, 0),
(15, 15, 0, 0),
(16, 16, 0, 0),
(17, 17, 0, 0),
(18, 18, 0, 0),
(19, 19, 0, 0),
(20, 20, 0, 0),
(21, 21, 0, 0),
(22, 22, 0, 0),
(23, 23, 0, 0),
(24, 24, 0, 0),
(25, 25, 0, 0),
(26, 26, 0, 0),
(27, 27, 0, 0),
(28, 28, 0, 0),
(29, 29, 0, 0),
(30, 30, 0, 0),
(31, 31, 0, 0),
(32, 32, 0, 0),
(33, 33, 0, 0),
(34, 34, 0, 0),
(35, 35, 0, 0),
(36, 36, 0, 0),
(37, 37, 0, 0),
(38, 38, 0, 0),
(39, 39, 0, 0),
(40, 40, 0, 0),
(41, 41, 0, 0),
(42, 42, 0, 0),
(43, 43, 0, 0),
(44, 44, 0, 0),
(45, 45, 0, 0),
(46, 46, 0, 0),
(47, 47, 0, 0),
(48, 48, 0, 0),
(49, 49, 0, 0),
(50, 50, 0, 0),
(51, 51, 0, 0),
(52, 52, 0, 0),
(53, 53, 0, 0),
(54, 54, 0, 0),
(55, 55, 0, 0),
(56, 56, 0, 0),
(57, 57, 0, 0),
(58, 58, 0, 0),
(59, 59, 0, 0),
(60, 60, 0, 0),
(61, 61, 0, 0),
(62, 62, 0, 0),
(63, 63, 0, 0),
(64, 64, 0, 0),
(65, 65, 0, 0),
(66, 66, 0, 0),
(67, 67, 0, 0),
(68, 68, 0, 0),
(69, 69, 0, 0),
(70, 70, 0, 0),
(71, 71, 0, 0),
(72, 72, 0, 0),
(73, 73, 0, 0),
(74, 74, 0, 0),
(75, 75, 0, 0),
(76, 76, 0, 0),
(77, 77, 0, 0),
(78, 78, 0, 0),
(79, 79, 0, 0),
(80, 80, 0, 0),
(81, 81, 0, 0),
(82, 82, 0, 0),
(83, 83, 0, 0),
(84, 84, 0, 0),
(85, 85, 0, 0),
(86, 86, 0, 0),
(87, 87, 0, 0),
(88, 88, 0, 0),
(89, 89, 0, 0),
(90, 90, 0, 0),
(91, 91, 0, 0),
(92, 92, 0, 0),
(93, 93, 0, 0),
(94, 94, 0, 0),
(95, 95, 0, 0),
(96, 96, 0, 0);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `product_to_color`
--

CREATE TABLE IF NOT EXISTS `product_to_color` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `product_id` int(11) NOT NULL,
  `color_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `product_id` (`product_id`),
  KEY `color_id` (`color_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `product_to_order`
--

CREATE TABLE IF NOT EXISTS `product_to_order` (
  `product_id` int(11) NOT NULL,
  `order_id` int(11) NOT NULL,
  `log_id` int(11) NOT NULL AUTO_INCREMENT,
  `product_price` double NOT NULL,
  `amount` int(11) NOT NULL,
  `porsion` double NOT NULL,
  `product_desc` text CHARACTER SET utf8 COLLATE utf8_turkish_ci,
  PRIMARY KEY (`log_id`),
  KEY `order_id` (`order_id`),
  KEY `product_id` (`product_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=163 ;

--
-- Tablo döküm verisi `product_to_order`
--

INSERT INTO `product_to_order` (`product_id`, `order_id`, `log_id`, `product_price`, `amount`, `porsion`, `product_desc`) VALUES
(2, 1, 1, 4.5, 1, 1, ''),
(37, 1, 2, 2, 1, 1, ''),
(2, 2, 3, 4.5, 1, 1, ''),
(37, 2, 4, 2, 1, 1, ''),
(2, 3, 5, 4.5, 1, 1, ''),
(4, 3, 6, 4, 1, 1, ''),
(7, 3, 7, 7, 1, 1, ''),
(17, 3, 8, 5, 1, 1, ''),
(38, 3, 9, 2, 1, 1, ''),
(37, 3, 10, 2, 1, 1, ''),
(7, 4, 11, 7, 1, 1, ''),
(4, 4, 12, 4, 1, 1, ''),
(2, 4, 13, 4.5, 1, 1, ''),
(7, 4, 14, 7, 1, 1, ''),
(2, 5, 15, 4.5, 1, 1, ''),
(4, 5, 16, 4, 1, 1, ''),
(7, 5, 17, 7, 1, 1, ''),
(6, 6, 18, 5, 1, 1, ''),
(14, 6, 19, 4.5, 1, 1, ''),
(19, 6, 20, 3, 1, 1, ''),
(19, 7, 21, 3, 1, 1, ''),
(55, 7, 22, 3.5, 1, 1, ''),
(28, 8, 23, 3.5, 1, 1, ''),
(53, 9, 24, 3, 1, 1, ''),
(53, 10, 25, 3, 1, 1, ''),
(14, 11, 26, 4.5, 1, 1, ''),
(28, 12, 27, 3.5, 1, 1, ''),
(28, 13, 28, 3.5, 1, 1, ''),
(28, 14, 29, 3.5, 1, 1, ''),
(53, 15, 30, 3, 1, 1, ''),
(28, 16, 31, 3.5, 1, 1, ''),
(53, 17, 32, 3, 1, 1, ''),
(14, 18, 33, 4.5, 1, 1, ''),
(53, 19, 34, 3, 1, 1, ''),
(6, 20, 35, 5, 1, 1, ''),
(14, 21, 36, 4.5, 1, 1, ''),
(14, 22, 37, 4.5, 1, 1, ''),
(28, 23, 38, 3.5, 1, 1, ''),
(14, 24, 39, 4.5, 1, 1, ''),
(61, 25, 40, 2, 1, 1, ''),
(62, 25, 41, 3.5, 1, 1, ''),
(37, 27, 42, 2, 1, 1, ''),
(38, 27, 43, 2, 1, 1, ''),
(6, 28, 44, 5, 1, 1, ''),
(14, 28, 45, 4.5, 1, 1, ''),
(37, 32, 46, 2, 1, 1, ''),
(38, 32, 47, 2, 1, 1, ''),
(37, 33, 48, 2, 1, 1, ''),
(38, 33, 49, 2, 1, 1, ''),
(37, 34, 50, 2, 1, 1, ''),
(38, 34, 51, 2, 1, 1, ''),
(14, 35, 52, 4.5, 1, 1, ''),
(14, 35, 53, 4.5, 1, 1, ''),
(14, 35, 54, 4.5, 1, 1, ''),
(14, 35, 55, 4.5, 1, 1, ''),
(14, 35, 56, 4.5, 1, 1, ''),
(14, 35, 57, 4.5, 1, 1, ''),
(14, 35, 58, 4.5, 1, 1, ''),
(14, 35, 59, 4.5, 1, 1, ''),
(14, 35, 60, 4.5, 1, 1, ''),
(14, 35, 61, 4.5, 1, 1, ''),
(14, 35, 62, 4.5, 1, 1, ''),
(14, 35, 63, 4.5, 1, 1, ''),
(14, 35, 64, 4.5, 1, 1, ''),
(14, 35, 65, 4.5, 1, 1, ''),
(14, 35, 66, 4.5, 1, 1, ''),
(28, 35, 67, 3.5, 1, 1, ''),
(28, 35, 68, 3.5, 1, 1, ''),
(28, 35, 69, 3.5, 1, 1, ''),
(28, 35, 70, 3.5, 1, 1, ''),
(28, 35, 71, 3.5, 1, 1, ''),
(28, 35, 72, 3.5, 1, 1, ''),
(28, 35, 73, 3.5, 1, 1, ''),
(28, 35, 74, 3.5, 1, 1, ''),
(28, 35, 75, 3.5, 1, 1, ''),
(28, 35, 76, 3.5, 1, 1, ''),
(6, 36, 77, 5, 1, 1, ''),
(37, 37, 78, 2, 2, 1, ''),
(38, 37, 79, 2, 3, 1, ''),
(39, 37, 80, 1, 1, 1, ''),
(56, 37, 81, 1, 2, 1, ''),
(14, 38, 82, 4.5, 2, 1, ''),
(19, 38, 83, 3, 2, 1, ''),
(19, 38, 84, 3, 2, 1, ''),
(6, 39, 85, 5, 1, 1, ''),
(14, 39, 86, 4.5, 1, 1, ''),
(55, 40, 87, 3.5, 1, 1, ''),
(94, 41, 88, 1, 1, 1, ''),
(95, 41, 89, 1.5, 1, 1, ''),
(6, 42, 90, 5, 1, 1, ''),
(14, 42, 91, 4.5, 1, 1, ''),
(19, 42, 92, 3, 1, 1, ''),
(19, 42, 93, 3, 1, 1, ''),
(53, 42, 94, 3, 1, 1, ''),
(28, 42, 95, 3.5, 1, 1, ''),
(28, 42, 96, 3.5, 1, 1, ''),
(6, 43, 97, 5, 1, 1, ''),
(14, 43, 98, 4.5, 1, 1, ''),
(6, 44, 99, 5, 1, 1, ''),
(14, 44, 100, 4.5, 1, 1, ''),
(6, 45, 101, 5, 1, 1, ''),
(14, 45, 102, 4.5, 1, 1, ''),
(19, 46, 103, 3, 1, 1, ''),
(23, 46, 104, 3.5, 1, 1, ''),
(19, 47, 105, 3, 1, 1, ''),
(23, 47, 106, 3.5, 1, 1, ''),
(19, 48, 107, 3, 1, 1, ''),
(23, 48, 108, 3.5, 1, 1, ''),
(6, 49, 109, 5, 1, 1, ''),
(14, 49, 110, 4.5, 1, 1, ''),
(6, 50, 111, 5, 1, 1, ''),
(14, 50, 112, 4.5, 1, 1, ''),
(6, 51, 113, 5, 1, 1, ''),
(14, 51, 114, 4.5, 1, 1, ''),
(6, 52, 115, 5, 1, 1, ''),
(14, 52, 116, 4.5, 1, 1, ''),
(6, 53, 117, 5, 1, 1, ''),
(14, 53, 118, 4.5, 1, 1, ''),
(6, 54, 119, 5, 1, 1, ''),
(14, 54, 120, 4.5, 1, 1, ''),
(6, 55, 121, 5, 1, 1, ''),
(14, 55, 122, 4.5, 1, 1, ''),
(6, 56, 123, 5, 1, 1, ''),
(14, 56, 124, 4.5, 1, 1, ''),
(6, 57, 125, 5, 1, 1, ''),
(14, 57, 126, 4.5, 1, 1, ''),
(6, 58, 127, 5, 1, 1, ''),
(14, 58, 128, 4.5, 1, 1, ''),
(6, 59, 129, 5, 1, 1, ''),
(14, 59, 130, 4.5, 1, 1, ''),
(6, 60, 131, 5, 1, 1, ''),
(14, 60, 132, 4.5, 1, 1, ''),
(6, 61, 133, 5, 1, 1, ''),
(14, 61, 134, 4.5, 1, 1, ''),
(6, 62, 135, 5, 1, 1, ''),
(14, 62, 136, 4.5, 1, 1, ''),
(6, 63, 137, 5, 1, 1, ''),
(6, 64, 138, 5, 1, 1, ''),
(14, 64, 139, 4.5, 1, 1, ''),
(6, 65, 140, 5, 1, 1, ''),
(14, 65, 141, 4.5, 1, 1, ''),
(6, 66, 142, 5, 1, 1, ''),
(14, 66, 143, 4.5, 1, 1, ''),
(6, 67, 144, 5, 1, 1, ''),
(14, 67, 145, 4.5, 1, 1, ''),
(6, 68, 146, 5, 1, 1, ''),
(14, 68, 147, 4.5, 1, 1, ''),
(6, 69, 148, 5, 1, 1, ''),
(14, 69, 149, 4.5, 1, 1, ''),
(6, 70, 150, 5, 1, 1, ''),
(14, 70, 151, 4.5, 1, 1, ''),
(6, 71, 152, 5, 1, 1, ''),
(14, 71, 153, 4.5, 1, 1, ''),
(6, 72, 154, 5, 1, 1, ''),
(14, 72, 155, 4.5, 1, 1, ''),
(6, 73, 156, 5, 1, 1, ''),
(14, 73, 157, 4.5, 1, 1, ''),
(6, 74, 158, 5, 1, 1, ''),
(14, 74, 159, 4.5, 1, 1, ''),
(14, 75, 160, 4.5, 1, 1, ''),
(19, 75, 161, 3, 1, 1, ''),
(55, 75, 162, 3.5, 1, 1, '');

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `sell_details`
--

CREATE TABLE IF NOT EXISTS `sell_details` (
  `sell_id` int(11) NOT NULL AUTO_INCREMENT,
  `account_id` int(11) NOT NULL,
  `staff_id` int(11) NOT NULL,
  `sell_desc` text COLLATE utf8_turkish_ci,
  `modified_date` datetime NOT NULL,
  PRIMARY KEY (`sell_id`),
  KEY `customer_id` (`account_id`),
  KEY `customer_id_2` (`account_id`),
  KEY `staff_id` (`staff_id`),
  KEY `customer_id_3` (`account_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=20 ;

--
-- Tablo döküm verisi `sell_details`
--

INSERT INTO `sell_details` (`sell_id`, `account_id`, `staff_id`, `sell_desc`, `modified_date`) VALUES
(1, 1, 14, '', '2013-08-12 21:57:33'),
(2, 15, 12, '', '2013-08-23 14:41:45'),
(3, 16, 12, '', '2013-08-23 14:41:47'),
(4, 9, 12, '', '2013-08-23 14:41:49'),
(5, 8, 12, '', '2013-08-23 14:41:52'),
(6, 5, 12, '', '2013-08-23 14:41:54'),
(7, 2, 12, '', '2013-08-23 14:41:56'),
(8, 14, 12, '', '2013-08-23 14:41:58'),
(9, 3, 12, '', '2013-08-23 14:42:00'),
(10, 4, 12, '', '2013-08-23 14:42:02'),
(11, 26, 12, '', '2013-08-23 14:42:04'),
(12, 7, 12, '', '2013-08-23 14:42:07'),
(13, 6, 12, '', '2013-08-23 15:50:20'),
(14, 11, 12, '', '2013-08-23 15:50:23'),
(15, 13, 12, '', '2013-08-23 15:50:24'),
(16, 12, 12, '', '2013-08-23 15:50:26'),
(17, 38, 12, '', '2013-08-24 01:31:55'),
(18, 48, 12, '', '2013-08-24 02:09:11'),
(19, 46, 12, '', '2013-08-24 02:12:03');

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `sell_list`
--

CREATE TABLE IF NOT EXISTS `sell_list` (
  `list_id` int(11) NOT NULL AUTO_INCREMENT,
  `sell_id` int(11) DEFAULT NULL,
  `product_id` int(11) DEFAULT NULL,
  `product_amount` double(11,4) NOT NULL,
  `product_size` double NOT NULL,
  `product_color` int(11) NOT NULL,
  `product_price` double NOT NULL,
  `modified_date` datetime NOT NULL,
  PRIMARY KEY (`list_id`),
  KEY `sell_id` (`sell_id`),
  KEY `product_id` (`product_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=82 ;

--
-- Tablo döküm verisi `sell_list`
--

INSERT INTO `sell_list` (`list_id`, `sell_id`, `product_id`, `product_amount`, `product_size`, `product_color`, `product_price`, `modified_date`) VALUES
(1, 1, 2, 1.0000, 1, 0, 4.5, '2013-08-12 21:57:33'),
(2, 1, 37, 1.0000, 1, 0, 2, '2013-08-12 21:57:33'),
(3, 2, 14, 1.0000, 1, 0, 4.5, '2013-08-23 14:41:45'),
(4, 2, 37, 1.0000, 1, 0, 2, '2013-08-23 14:41:45'),
(5, 2, 38, 1.0000, 1, 0, 2, '2013-08-23 14:41:45'),
(6, 3, 61, 1.0000, 1, 0, 2, '2013-08-23 14:41:47'),
(7, 3, 62, 1.0000, 1, 0, 3.5, '2013-08-23 14:41:47'),
(8, 4, 14, 1.0000, 1, 0, 4.5, '2013-08-23 14:41:49'),
(9, 5, 28, 1.0000, 1, 0, 3.5, '2013-08-23 14:41:52'),
(10, 5, 53, 1.0000, 1, 0, 3, '2013-08-23 14:41:52'),
(11, 6, 2, 1.0000, 1, 0, 4.5, '2013-08-23 14:41:54'),
(12, 6, 4, 1.0000, 1, 0, 4, '2013-08-23 14:41:54'),
(13, 6, 7, 1.0000, 1, 0, 7, '2013-08-23 14:41:54'),
(14, 7, 2, 1.0000, 1, 0, 4.5, '2013-08-23 14:41:56'),
(15, 7, 37, 1.0000, 1, 0, 2, '2013-08-23 14:41:56'),
(16, 7, 28, 1.0000, 1, 0, 3.5, '2013-08-23 14:41:56'),
(17, 7, 53, 1.0000, 1, 0, 3, '2013-08-23 14:41:56'),
(18, 7, 53, 1.0000, 1, 0, 3, '2013-08-23 14:41:56'),
(19, 7, 14, 1.0000, 1, 0, 4.5, '2013-08-23 14:41:56'),
(20, 7, 28, 1.0000, 1, 0, 3.5, '2013-08-23 14:41:56'),
(21, 7, 28, 1.0000, 1, 0, 3.5, '2013-08-23 14:41:56'),
(22, 7, 53, 1.0000, 1, 0, 3, '2013-08-23 14:41:56'),
(23, 7, 28, 1.0000, 1, 0, 3.5, '2013-08-23 14:41:56'),
(24, 8, 28, 1.0000, 1, 0, 3.5, '2013-08-23 14:41:58'),
(25, 9, 2, 1.0000, 1, 0, 4.5, '2013-08-23 14:42:00'),
(26, 9, 4, 1.0000, 1, 0, 4, '2013-08-23 14:42:00'),
(27, 9, 7, 1.0000, 1, 0, 7, '2013-08-23 14:42:00'),
(28, 9, 17, 1.0000, 1, 0, 5, '2013-08-23 14:42:00'),
(29, 9, 38, 1.0000, 1, 0, 2, '2013-08-23 14:42:00'),
(30, 9, 37, 1.0000, 1, 0, 2, '2013-08-23 14:42:00'),
(31, 10, 7, 1.0000, 1, 0, 7, '2013-08-23 14:42:02'),
(32, 10, 4, 1.0000, 1, 0, 4, '2013-08-23 14:42:02'),
(33, 10, 2, 1.0000, 1, 0, 4.5, '2013-08-23 14:42:02'),
(34, 10, 7, 1.0000, 1, 0, 7, '2013-08-23 14:42:02'),
(35, 11, 6, 1.0000, 1, 0, 5, '2013-08-23 14:42:04'),
(36, 11, 14, 1.0000, 1, 0, 4.5, '2013-08-23 14:42:04'),
(37, 12, 19, 1.0000, 1, 0, 3, '2013-08-23 14:42:07'),
(38, 12, 55, 1.0000, 1, 0, 3.5, '2013-08-23 14:42:07'),
(39, 13, 6, 1.0000, 1, 0, 5, '2013-08-23 15:50:21'),
(40, 13, 14, 1.0000, 1, 0, 4.5, '2013-08-23 15:50:21'),
(41, 13, 19, 1.0000, 1, 0, 3, '2013-08-23 15:50:21'),
(42, 14, 6, 1.0000, 1, 0, 5, '2013-08-23 15:50:23'),
(43, 15, 14, 1.0000, 1, 0, 4.5, '2013-08-23 15:50:24'),
(44, 16, 14, 1.0000, 1, 0, 4.5, '2013-08-23 15:50:26'),
(45, 17, 37, 1.0000, 1, 0, 2, '2013-08-24 01:31:55'),
(46, 17, 38, 1.0000, 1, 0, 2, '2013-08-24 01:31:55'),
(47, 17, 14, 1.0000, 1, 0, 4.5, '2013-08-24 01:31:55'),
(48, 17, 14, 1.0000, 1, 0, 4.5, '2013-08-24 01:31:55'),
(49, 17, 14, 1.0000, 1, 0, 4.5, '2013-08-24 01:31:55'),
(50, 17, 14, 1.0000, 1, 0, 4.5, '2013-08-24 01:31:55'),
(51, 17, 14, 1.0000, 1, 0, 4.5, '2013-08-24 01:31:55'),
(52, 17, 14, 1.0000, 1, 0, 4.5, '2013-08-24 01:31:55'),
(53, 17, 14, 1.0000, 1, 0, 4.5, '2013-08-24 01:31:55'),
(54, 17, 14, 1.0000, 1, 0, 4.5, '2013-08-24 01:31:55'),
(55, 17, 14, 1.0000, 1, 0, 4.5, '2013-08-24 01:31:55'),
(56, 17, 14, 1.0000, 1, 0, 4.5, '2013-08-24 01:31:55'),
(57, 17, 14, 1.0000, 1, 0, 4.5, '2013-08-24 01:31:55'),
(58, 17, 14, 1.0000, 1, 0, 4.5, '2013-08-24 01:31:55'),
(59, 17, 14, 1.0000, 1, 0, 4.5, '2013-08-24 01:31:55'),
(60, 17, 14, 1.0000, 1, 0, 4.5, '2013-08-24 01:31:55'),
(61, 17, 14, 1.0000, 1, 0, 4.5, '2013-08-24 01:31:55'),
(62, 17, 28, 1.0000, 1, 0, 3.5, '2013-08-24 01:31:55'),
(63, 17, 28, 1.0000, 1, 0, 3.5, '2013-08-24 01:31:55'),
(64, 17, 28, 1.0000, 1, 0, 3.5, '2013-08-24 01:31:55'),
(65, 17, 28, 1.0000, 1, 0, 3.5, '2013-08-24 01:31:55'),
(66, 17, 28, 1.0000, 1, 0, 3.5, '2013-08-24 01:31:55'),
(67, 17, 28, 1.0000, 1, 0, 3.5, '2013-08-24 01:31:55'),
(68, 17, 28, 1.0000, 1, 0, 3.5, '2013-08-24 01:31:55'),
(69, 17, 28, 1.0000, 1, 0, 3.5, '2013-08-24 01:31:56'),
(70, 17, 28, 1.0000, 1, 0, 3.5, '2013-08-24 01:31:56'),
(71, 17, 28, 1.0000, 1, 0, 3.5, '2013-08-24 01:31:56'),
(72, 18, 37, 1.0000, 1, 0, 2, '2013-08-24 02:09:11'),
(73, 18, 38, 1.0000, 1, 0, 2, '2013-08-24 02:09:11'),
(74, 18, 6, 1.0000, 1, 0, 5, '2013-08-24 02:09:11'),
(75, 19, 6, 1.0000, 1, 0, 5, '2013-08-24 02:12:03'),
(76, 19, 14, 1.0000, 1, 0, 4.5, '2013-08-24 02:12:03'),
(77, 19, 19, 1.0000, 1, 0, 3, '2013-08-24 02:12:03'),
(78, 19, 19, 1.0000, 1, 0, 3, '2013-08-24 02:12:03'),
(79, 19, 53, 1.0000, 1, 0, 3, '2013-08-24 02:12:03'),
(80, 19, 28, 1.0000, 1, 0, 3.5, '2013-08-24 02:12:03'),
(81, 19, 28, 1.0000, 1, 0, 3.5, '2013-08-24 02:12:03');

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `sell_return`
--

CREATE TABLE IF NOT EXISTS `sell_return` (
  `list_id` int(11) NOT NULL AUTO_INCREMENT,
  `sell_id` int(11) NOT NULL,
  `product_id` int(11) NOT NULL,
  `product_amount` double(11,4) NOT NULL,
  `product_size` double NOT NULL,
  `product_color` int(11) NOT NULL,
  `product_price` double NOT NULL,
  `return_desc` text CHARACTER SET utf8 COLLATE utf8_turkish_ci,
  `modified_date` datetime NOT NULL,
  PRIMARY KEY (`list_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `settings`
--

CREATE TABLE IF NOT EXISTS `settings` (
  `table_count_per_line` int(11) NOT NULL,
  `table_width` int(11) NOT NULL,
  `table_height` int(11) NOT NULL,
  `tables_refresh_time` int(11) NOT NULL,
  `menu_item_width` int(11) NOT NULL,
  `menu_item_height` int(11) NOT NULL,
  `menu_item_name_panel_height` int(11) NOT NULL,
  `menu_item_count_per_line` int(11) NOT NULL,
  `main_form_img_path` text
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Tablo döküm verisi `settings`
--

INSERT INTO `settings` (`table_count_per_line`, `table_width`, `table_height`, `tables_refresh_time`, `menu_item_width`, `menu_item_height`, `menu_item_name_panel_height`, `menu_item_count_per_line`, `main_form_img_path`) VALUES
(5, 180, 160, 10, 205, 120, 35, 4, 'logo.png');

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `staff`
--

CREATE TABLE IF NOT EXISTS `staff` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(50) NOT NULL,
  `password` varchar(20) NOT NULL,
  `type` int(11) NOT NULL,
  `name` varchar(50) NOT NULL,
  `is_deleted` tinyint(4) NOT NULL,
  `display_order` int(11) NOT NULL,
  `modified_date` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=17 ;

--
-- Tablo döküm verisi `staff`
--

INSERT INTO `staff` (`id`, `username`, `password`, `type`, `name`, `is_deleted`, `display_order`, `modified_date`) VALUES
(5, 'mahmud', '1234', 2, 'MAHMUD RAHMI KIRMACI', 0, 0, '2013-06-14 12:27:45'),
(6, 'mustafa', '1234', 2, 'MUSTAFA KORKMAZ', 0, 0, '2013-06-14 12:28:00'),
(8, 'turkan', '1234', 2, 'TÜRKAN SOYDAN', 0, 0, '2013-06-19 12:45:02'),
(11, 'kasa', '1234', 1, 'KASA', 0, 0, '2013-07-17 00:40:00'),
(12, 'cengiz', '1234', 0, 'CENGIZ CAFE', 0, 0, '2012-12-26 08:27:35'),
(13, 'mustafa', '1234', 0, 'MUSTAFA KORKMAZ', 0, 0, '2012-12-27 15:17:00'),
(14, 'ibrahim', '1234', 0, 'TAYLAN SABIRCAN', 0, 0, '2013-07-24 00:00:00'),
(15, 'deneme', '112344', 1, 'DENEME', 1, 0, '2013-07-22 15:01:29'),
(16, 'deneme2', '234234', 2, 'DENEME KULLANICISI 2', 1, 0, '2013-07-22 15:05:20');

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `status_details`
--

CREATE TABLE IF NOT EXISTS `status_details` (
  `status_id` int(11) NOT NULL,
  `status_name` varchar(20) CHARACTER SET utf8 COLLATE utf8_turkish_ci NOT NULL,
  `create_date` datetime NOT NULL,
  `modified_date` datetime NOT NULL,
  `is_deleted` tinyint(4) NOT NULL,
  PRIMARY KEY (`status_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Tablo döküm verisi `status_details`
--

INSERT INTO `status_details` (`status_id`, `status_name`, `create_date`, `modified_date`, `is_deleted`) VALUES
(1, 'ACIK', '2013-06-26 00:00:00', '2013-06-26 00:00:00', 0),
(2, 'KAPALI', '2013-06-26 00:00:00', '2013-06-26 00:00:00', 0),
(3, 'UYGUN', '2013-06-26 00:00:00', '2013-06-26 00:00:00', 0),
(4, 'DOLU', '2013-06-26 00:00:00', '2013-06-26 00:00:00', 0),
(5, 'REZERVE', '2013-06-26 00:00:00', '2013-06-26 00:00:00', 0),
(6, 'ALINDI', '2013-07-03 00:00:00', '2013-07-03 00:00:00', 0),
(7, 'KAPANDI', '2013-07-03 00:00:00', '2013-07-03 00:00:00', 0);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `stock_details`
--

CREATE TABLE IF NOT EXISTS `stock_details` (
  `stock_id` int(11) NOT NULL AUTO_INCREMENT,
  `barcode` text COLLATE utf8_turkish_ci COMMENT 'barcode',
  `product_code` text CHARACTER SET latin1 NOT NULL,
  `goods_id` int(11) NOT NULL,
  `product_size` double NOT NULL,
  `product_color` int(11) NOT NULL,
  `product_count` double(11,4) NOT NULL,
  `modified_date` datetime NOT NULL,
  PRIMARY KEY (`stock_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `suppliers_details`
--

CREATE TABLE IF NOT EXISTS `suppliers_details` (
  `suppliers_id` int(11) NOT NULL AUTO_INCREMENT,
  `suppliers_name` varchar(20) COLLATE utf8_turkish_ci NOT NULL,
  `suppliers_desc` text COLLATE utf8_turkish_ci,
  `suppliers_address` text COLLATE utf8_turkish_ci,
  `suppliers_tel` varchar(15) COLLATE utf8_turkish_ci DEFAULT NULL,
  `suppliers_mail` varchar(50) COLLATE utf8_turkish_ci DEFAULT NULL,
  `suppliers_isDeleted` tinyint(1) NOT NULL,
  `modified_date` datetime NOT NULL,
  PRIMARY KEY (`suppliers_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=2 ;

--
-- Tablo döküm verisi `suppliers_details`
--

INSERT INTO `suppliers_details` (`suppliers_id`, `suppliers_name`, `suppliers_desc`, `suppliers_address`, `suppliers_tel`, `suppliers_mail`, `suppliers_isDeleted`, `modified_date`) VALUES
(1, 'DIGER', '', '', '', '', 0, '2013-08-12 19:23:12');

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `suppliers_payment`
--

CREATE TABLE IF NOT EXISTS `suppliers_payment` (
  `payment_id` int(11) NOT NULL AUTO_INCREMENT,
  `suppliers_id` int(11) DEFAULT NULL,
  `unit` varchar(3) COLLATE utf8_turkish_ci NOT NULL,
  `type` int(11) NOT NULL,
  `process_id` int(11) DEFAULT NULL,
  `payment_price` double NOT NULL,
  `payment_desc` text COLLATE utf8_turkish_ci,
  `modified_date` datetime NOT NULL,
  PRIMARY KEY (`payment_id`),
  KEY `suppliers_id` (`suppliers_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `tablet_to_table`
--

CREATE TABLE IF NOT EXISTS `tablet_to_table` (
  `tablet_id` int(11) NOT NULL,
  `masa_id` int(11) NOT NULL,
  KEY `tablet_id` (`tablet_id`,`masa_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `table_categories`
--

CREATE TABLE IF NOT EXISTS `table_categories` (
  `tcategory_id` int(11) NOT NULL AUTO_INCREMENT,
  `tcategory_name` varchar(20) CHARACTER SET utf8 COLLATE utf8_turkish_ci NOT NULL,
  `tcategory_status` int(11) NOT NULL,
  `create_date` datetime NOT NULL,
  `modified_date` datetime NOT NULL,
  `display_order` int(11) NOT NULL,
  `is_deleted` tinyint(4) NOT NULL,
  PRIMARY KEY (`tcategory_id`),
  KEY `tcategory_status` (`tcategory_status`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;

--
-- Tablo döküm verisi `table_categories`
--

INSERT INTO `table_categories` (`tcategory_id`, `tcategory_name`, `tcategory_status`, `create_date`, `modified_date`, `display_order`, `is_deleted`) VALUES
(1, 'SALON', 1, '2013-06-26 00:00:00', '2013-08-10 16:14:06', 1, 0),
(2, 'BAHÇE ARKA', 1, '2013-06-26 00:00:00', '2013-08-21 19:34:52', 3, 0),
(3, 'ÜST KAT', 3, '2013-08-12 14:39:13', '2013-08-21 19:23:12', 2, 0),
(4, 'BAHÇE ÖN', 3, '2013-08-21 19:22:47', '2013-08-21 19:22:47', 0, 0);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `table_details`
--

CREATE TABLE IF NOT EXISTS `table_details` (
  `table_id` int(11) NOT NULL AUTO_INCREMENT,
  `table_name` varchar(20) CHARACTER SET utf8 COLLATE utf8_turkish_ci NOT NULL,
  `table_category` int(11) NOT NULL,
  `table_status` int(11) NOT NULL,
  `create_date` datetime NOT NULL,
  `modified_date` datetime NOT NULL,
  `display_order` int(11) NOT NULL,
  `is_deleted` tinyint(4) NOT NULL,
  PRIMARY KEY (`table_id`),
  KEY `table_status` (`table_status`),
  KEY `table_category` (`table_category`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=46 ;

--
-- Tablo döküm verisi `table_details`
--

INSERT INTO `table_details` (`table_id`, `table_name`, `table_category`, `table_status`, `create_date`, `modified_date`, `display_order`, `is_deleted`) VALUES
(24, '01', 2, 4, '2013-07-31 14:51:00', '2013-08-23 14:41:58', 0, 0),
(25, '02', 2, 3, '2013-07-31 14:51:09', '2013-08-23 14:41:56', 1, 0),
(26, '03', 2, 3, '2013-07-31 14:51:20', '2013-08-24 02:12:03', 2, 0),
(27, '04', 2, 3, '2013-07-31 14:51:28', '2013-08-23 14:41:52', 3, 0),
(28, '01', 4, 4, '2013-07-31 14:51:36', '2013-08-23 14:41:45', 1, 0),
(29, '01', 1, 3, '2013-07-31 14:51:57', '2013-08-24 02:09:11', 1, 0),
(30, '07', 1, 3, '2013-07-31 14:56:37', '2013-08-21 19:32:44', 7, 0),
(31, '08', 1, 3, '2013-07-31 14:56:44', '2013-08-21 19:32:54', 8, 0),
(32, '09', 1, 3, '2013-07-31 14:56:51', '2013-08-21 19:32:59', 9, 0),
(33, '10', 1, 3, '2013-07-31 14:56:59', '2013-08-21 19:33:05', 10, 0),
(34, '02', 4, 3, '2013-08-12 14:38:36', '2013-08-24 01:31:56', 2, 0),
(35, '12', 2, 4, '2013-08-12 14:38:43', '2013-08-12 18:14:41', 12, 1),
(36, '03', 4, 3, '2013-08-12 14:38:49', '2013-08-23 14:41:49', 3, 0),
(37, '02', 1, 3, '2013-08-12 14:38:58', '2013-08-21 19:32:33', 2, 0),
(38, '01', 3, 3, '2013-08-12 14:39:29', '2013-08-23 14:42:00', 1, 0),
(39, '02', 3, 3, '2013-08-12 14:39:35', '2013-08-23 14:42:02', 2, 0),
(40, '03', 3, 3, '2013-08-12 14:39:41', '2013-08-23 14:42:04', 3, 0),
(41, '04', 3, 3, '2013-08-12 14:39:51', '2013-08-21 19:31:36', 4, 0),
(42, '04', 1, 3, '2013-08-12 14:40:05', '2013-08-21 19:33:25', 4, 0),
(43, '03', 1, 3, '2013-08-12 14:40:13', '2013-08-21 19:33:15', 3, 0),
(44, '05', 1, 3, '2013-08-21 19:33:37', '2013-08-21 19:33:37', 5, 0),
(45, '06', 1, 3, '2013-08-21 19:33:43', '2013-08-21 19:33:43', 6, 0);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `user_details`
--

CREATE TABLE IF NOT EXISTS `user_details` (
  `user_id` int(11) NOT NULL AUTO_INCREMENT,
  `user_nick` varchar(20) COLLATE utf8_turkish_ci NOT NULL,
  `user_password` varchar(32) COLLATE utf8_turkish_ci NOT NULL,
  `user_name` varchar(50) COLLATE utf8_turkish_ci NOT NULL,
  `user_auth` int(11) NOT NULL,
  `user_mail` varchar(50) COLLATE utf8_turkish_ci NOT NULL,
  `user_isDeleted` tinyint(1) NOT NULL,
  `modified_date` datetime NOT NULL,
  PRIMARY KEY (`user_id`),
  UNIQUE KEY `user_nick` (`user_nick`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Görünüm yapısı durumu `v_accounts_master`
--
CREATE TABLE IF NOT EXISTS `v_accounts_master` (
`account_id` int(11)
,`owner_id` int(11)
,`account_type` int(11)
,`account_status` int(11)
,`status_name` varchar(20)
,`owner_name` text
,`table_category_name` varchar(20)
,`account_staff_name` varchar(50)
,`total_product_amount` decimal(54,0)
,`total_order_price` double
,`account_create_date` datetime
,`order_create_date` datetime
,`order_modified_date` datetime
);
-- --------------------------------------------------------

--
-- Görünüm yapısı durumu `v_bank_details`
--
CREATE TABLE IF NOT EXISTS `v_bank_details` (
`bank_name` varchar(20)
,`id` int(11)
,`bank_id` int(11)
,`instalment` text
,`payment_day` int(11)
);
-- --------------------------------------------------------

--
-- Görünüm yapısı durumu `v_bank_logs`
--
CREATE TABLE IF NOT EXISTS `v_bank_logs` (
`toplam_borc` double
,`bank_id` int(11)
,`instalment` int(11)
,`rate` double(11,3)
,`payment_date` datetime
,`desc` text
,`log_id` int(11)
,`type` varchar(18)
,`payment_price` double
,`modified_date` datetime
);
-- --------------------------------------------------------

--
-- Görünüm yapısı durumu `v_customers_master`
--
CREATE TABLE IF NOT EXISTS `v_customers_master` (
`name` text
,`tel` varchar(20)
,`address` text
,`customer_id` int(11)
,`is_deleted` tinyint(1)
,`payment_price` double
,`last_modified_date` varchar(20)
);
-- --------------------------------------------------------

--
-- Görünüm yapısı durumu `v_customer_payment_details`
--
CREATE TABLE IF NOT EXISTS `v_customer_payment_details` (
`name` text
,`customer_id` int(11)
,`total_price` double
,`payment_id` int(11)
,`payment_price` double
,`owed` double
,`bakiye` double
,`payment_desc` text
,`type` varchar(16)
,`type_id` int(11)
,`payment_date` datetime
);
-- --------------------------------------------------------

--
-- Görünüm yapısı durumu `v_expense_details`
--
CREATE TABLE IF NOT EXISTS `v_expense_details` (
`payment_id` int(11)
,`payment_cat` bigint(20)
,`process_name` varchar(20)
,`payment_price` double
,`type` bigint(11)
,`display_order` bigint(20)
,`payment_desc` text
,`modified_date` datetime
);
-- --------------------------------------------------------

--
-- Görünüm yapısı durumu `v_goods_amount`
--
CREATE TABLE IF NOT EXISTS `v_goods_amount` (
`goods_id` int(11)
,`goods_name` varchar(20)
,`unit` varchar(4)
,`create_date` datetime
,`modified_date` datetime
,`is_deleted` tinyint(4)
,`last_modified_date` datetime
,`count` double(21,4)
);
-- --------------------------------------------------------

--
-- Görünüm yapısı durumu `v_goods_stocks`
--
CREATE TABLE IF NOT EXISTS `v_goods_stocks` (
`goods_name` varchar(20)
,`goods_id` int(11)
,`unit` varchar(4)
,`is_deleted` tinyint(4)
,`product_count` double(11,4)
,`modified_date` datetime
);
-- --------------------------------------------------------

--
-- Görünüm yapısı durumu `v_goods_view`
--
CREATE TABLE IF NOT EXISTS `v_goods_view` (
`last_buy_price` double
,`total_return_amount` double(21,4)
,`return_price` double
,`goods_id` int(11)
,`cost` double
,`kdv` double
,`total_buy_amount` double(21,4)
);
-- --------------------------------------------------------

--
-- Görünüm yapısı durumu `v_goods_view_helper`
--
CREATE TABLE IF NOT EXISTS `v_goods_view_helper` (
`goods_id` int(11)
,`total_return` double(21,4)
,`return_price` double
);
-- --------------------------------------------------------

--
-- Görünüm yapısı durumu `v_last_buy_price`
--
CREATE TABLE IF NOT EXISTS `v_last_buy_price` (
`list_id` int(11)
,`goods_id` int(11)
,`last_buy_price` double
,`currency` varchar(6)
,`modified_date` datetime
);
-- --------------------------------------------------------

--
-- Görünüm yapısı durumu `v_last_buy_price_helper`
--
CREATE TABLE IF NOT EXISTS `v_last_buy_price_helper` (
`list_id` int(11)
,`goods_id` int(11)
);
-- --------------------------------------------------------

--
-- Görünüm yapısı durumu `v_latest_orders`
--
CREATE TABLE IF NOT EXISTS `v_latest_orders` (
`order_id` int(11)
,`account_id` int(11)
,`account_type` int(11)
,`account_owner` int(11)
,`account_status` int(11)
,`create_date` datetime
);
-- --------------------------------------------------------

--
-- Görünüm yapısı durumu `v_options_to_product`
--
CREATE TABLE IF NOT EXISTS `v_options_to_product` (
`product_id` int(11)
,`product_name` varchar(30)
,`option_id` int(11)
,`option_name` varchar(50)
,`is_deleted` tinyint(1)
);
-- --------------------------------------------------------

--
-- Görünüm yapısı durumu `v_orders_master`
--
CREATE TABLE IF NOT EXISTS `v_orders_master` (
`order_id` int(11)
,`order_desc` text
,`order_type` int(11)
,`order_status` int(11)
,`status_name` varchar(20)
,`account_create_date` datetime
,`order_create_date` datetime
,`order_modified_date` datetime
,`account_id` int(11)
,`account_staff_id` int(11)
,`order_staff_id` int(11)
,`order_staff_name` varchar(50)
,`account_staff_name` varchar(50)
,`owner_id` int(11)
,`account_type` int(11)
,`account_status` int(11)
,`customer_name` text
,`table_name` varchar(20)
,`table_category_name` varchar(20)
,`order_amount` decimal(32,0)
,`order_price` double
,`is_deleted` tinyint(1)
);
-- --------------------------------------------------------

--
-- Görünüm yapısı durumu `v_orders_to_accounts`
--
CREATE TABLE IF NOT EXISTS `v_orders_to_accounts` (
`account_id` int(11)
,`order_id` int(11)
,`is_order_deleted` tinyint(1)
);
-- --------------------------------------------------------

--
-- Görünüm yapısı durumu `v_payment_details`
--
CREATE TABLE IF NOT EXISTS `v_payment_details` (
`sell_id` int(11)
,`pesin` double
,`pos` double
,`veresiye` double
,`hediye` double
);
-- --------------------------------------------------------

--
-- Görünüm yapısı durumu `v_products`
--
CREATE TABLE IF NOT EXISTS `v_products` (
`product_id` int(11)
,`unit` int(11)
,`unit_text` varchar(3)
,`is_deleted` tinyint(1)
,`isOnMenu` tinyint(1)
,`product_isDeleted` varchar(5)
,`product_code_manual` varchar(15)
,`product_img_path` text
,`product_cat` int(11)
,`product_name` varchar(30)
,`product_price` double
,`product_desc` text
,`modified_date` datetime
,`cat_name` varchar(20)
,`min_size` int(11)
,`max_size` int(11)
);
-- --------------------------------------------------------

--
-- Görünüm yapısı durumu `v_product_return`
--
CREATE TABLE IF NOT EXISTS `v_product_return` (
`goods_id` int(11)
,`product_name` varchar(20)
,`product_count` double(11,4)
,`unit` varchar(4)
,`modified_date` datetime
,`last_buy_price` double
);
-- --------------------------------------------------------

--
-- Görünüm yapısı durumu `v_product_return_count`
--
CREATE TABLE IF NOT EXISTS `v_product_return_count` (
`product_id` int(11)
,`product_return_count` double(21,4)
);
-- --------------------------------------------------------

--
-- Görünüm yapısı durumu `v_product_to_color`
--
CREATE TABLE IF NOT EXISTS `v_product_to_color` (
`id` int(11)
,`product_id` int(11)
,`color_id` int(11)
,`product_name` varchar(30)
,`color_name` varchar(20)
);
-- --------------------------------------------------------

--
-- Görünüm yapısı durumu `v_product_to_order`
--
CREATE TABLE IF NOT EXISTS `v_product_to_order` (
`order_id` int(11)
,`log_id` int(11)
,`order_type` int(11)
,`order_status` int(11)
,`staff_id` int(11)
,`order_staff_name` varchar(50)
,`product_id` int(11)
,`product_name` varchar(30)
,`product_desc` text
,`product_price` double
,`porsion` double
,`amount` int(11)
,`total_price` double
,`is_deleted` tinyint(1)
,`account_id` int(11)
,`account_type` int(11)
,`account_status` int(11)
);
-- --------------------------------------------------------

--
-- Görünüm yapısı durumu `v_product_view`
--
CREATE TABLE IF NOT EXISTS `v_product_view` (
`product_id` int(11)
,`total_sale_amount` double(21,4)
,`total_sale_price` double
,`total_return_amount` double(21,4)
,`total_return_price` double
,`product_price` double
,`product_name` varchar(30)
);
-- --------------------------------------------------------

--
-- Görünüm yapısı durumu `v_product_view_helper`
--
CREATE TABLE IF NOT EXISTS `v_product_view_helper` (
`product_name` varchar(30)
,`product_price` double
,`product_id` int(11)
,`total_return` double(21,4)
,`total_return_price` double
);
-- --------------------------------------------------------

--
-- Görünüm yapısı durumu `v_sell_lists`
--
CREATE TABLE IF NOT EXISTS `v_sell_lists` (
`sell_id` int(11)
,`product_id` int(11)
,`product_name` varchar(30)
,`product_amount` double(11,4)
,`product_price` double
,`modified_date` datetime
,`sell_desc` text
,`sell_staff_id` int(11)
,`sell_staff_name` varchar(50)
,`account_id` int(11)
,`account_owner` int(11)
,`account_owner_name` text
,`account_type` int(11)
,`account_staff_id` int(11)
,`account_staff_name` varchar(50)
,`account_status` int(11)
);
-- --------------------------------------------------------

--
-- Görünüm yapısı durumu `v_sell_lists_master`
--
CREATE TABLE IF NOT EXISTS `v_sell_lists_master` (
`sell_id` int(11)
,`account_owner_name` text
,`account_staff_id` int(11)
,`sell_staff_name` varchar(50)
,`account_staff_name` varchar(50)
,`total_sale_amount` double(21,4)
,`sell_desc` text
,`sell_income` double
,`modified_date` datetime
);
-- --------------------------------------------------------

--
-- Görünüm yapısı durumu `v_sell_via_pos_details`
--
CREATE TABLE IF NOT EXISTS `v_sell_via_pos_details` (
`payment_id` int(11)
,`bank_id` int(11)
,`modified_date` datetime
,`bank_name` varchar(20)
,`sell_id` int(11)
,`payment_type` int(11)
,`payment_price` double
);
-- --------------------------------------------------------

--
-- Görünüm yapısı durumu `v_supplier_payment_details`
--
CREATE TABLE IF NOT EXISTS `v_supplier_payment_details` (
`process_id` int(11)
,`p_type` int(11)
,`goods_name` varchar(20)
,`product_count` varbinary(21)
,`unit` varchar(4)
,`total_weight` double
,`toplam_borc` double
,`suppliers_id` int(11)
,`payment_id` int(11)
,`type` varchar(12)
,`payment_price` double
,`modified_date` datetime
,`payment_desc` text
);
-- --------------------------------------------------------

--
-- Görünüm yapısı durumu `v_supplier_payment_type2_details`
--
CREATE TABLE IF NOT EXISTS `v_supplier_payment_type2_details` (
`payment_id` int(11)
,`suppliers_id` int(11)
,`type` int(11)
,`process_id` int(11)
,`payment_price` double
,`payment_desc` text
,`modified_date` datetime
,`list_id` int(11)
,`goods_id` int(11)
,`goods_name` varchar(20)
,`unit` varchar(4)
,`g_id` int(11)
,`product_count` double(21,4)
,`buy_price` double
,`kdv` double
);
-- --------------------------------------------------------

--
-- Görünüm yapısı durumu `v_tables_total_price`
--
CREATE TABLE IF NOT EXISTS `v_tables_total_price` (
`SUM(pto.product_price*pto.amount)` double
,`account_owner` int(11)
,`account_id` int(11)
,`order_id` int(11)
);
-- --------------------------------------------------------

--
-- Görünüm yapısı durumu `v_table_categories`
--
CREATE TABLE IF NOT EXISTS `v_table_categories` (
`tcategory_id` int(11)
,`tcategory_name` varchar(20)
,`tcategory_status` int(11)
,`create_date` datetime
,`modified_date` datetime
,`display_order` int(11)
,`is_deleted` tinyint(4)
,`status_name` varchar(20)
);
-- --------------------------------------------------------

--
-- Görünüm yapısı durumu `v_table_details`
--
CREATE TABLE IF NOT EXISTS `v_table_details` (
`table_id` int(11)
,`table_name` varchar(20)
,`table_category` int(11)
,`table_category_name` varchar(20)
,`table_category_display_order` int(11)
,`table_status` int(11)
,`create_date` datetime
,`modified_date` datetime
,`display_order` int(11)
,`is_deleted` tinyint(4)
,`status_name` varchar(20)
);
-- --------------------------------------------------------

--
-- Görünüm yapısı durumu `v_table_orders`
--
CREATE TABLE IF NOT EXISTS `v_table_orders` (
`account_id` int(11)
,`account_type` int(11)
,`account_owner` int(11)
,`account_status` int(11)
,`order_id` int(11)
,`product_id` int(11)
,`product_price` double
,`amount` int(11)
,`porsion` double
);
-- --------------------------------------------------------

--
-- Görünüm yapısı `v_accounts_master`
--
DROP TABLE IF EXISTS `v_accounts_master`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_accounts_master` AS select `v_orders_master`.`account_id` AS `account_id`,`v_orders_master`.`owner_id` AS `owner_id`,`v_orders_master`.`account_type` AS `account_type`,`v_orders_master`.`account_status` AS `account_status`,`v_orders_master`.`status_name` AS `status_name`,if((`v_orders_master`.`account_type` = 1),`v_orders_master`.`table_name`,if((`v_orders_master`.`account_type` = 2),`v_orders_master`.`customer_name`,'')) AS `owner_name`,`v_orders_master`.`table_category_name` AS `table_category_name`,`v_orders_master`.`account_staff_name` AS `account_staff_name`,sum(`v_orders_master`.`order_amount`) AS `total_product_amount`,sum(`v_orders_master`.`order_price`) AS `total_order_price`,`v_orders_master`.`account_create_date` AS `account_create_date`,`v_orders_master`.`order_create_date` AS `order_create_date`,max(`v_orders_master`.`order_modified_date`) AS `order_modified_date` from `v_orders_master` where (`v_orders_master`.`is_deleted` = 0) group by `v_orders_master`.`account_id`;

-- --------------------------------------------------------

--
-- Görünüm yapısı `v_bank_details`
--
DROP TABLE IF EXISTS `v_bank_details`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_bank_details` AS select `bd`.`bank_name` AS `bank_name`,`bi`.`id` AS `id`,`bi`.`bank_id` AS `bank_id`,`bi`.`instalment` AS `instalment`,`bi`.`payment_day` AS `payment_day` from (`bank_details` `bd` left join `bank_instalments` `bi` on((`bd`.`bank_id` = `bi`.`bank_id`)));

-- --------------------------------------------------------

--
-- Görünüm yapısı `v_bank_logs`
--
DROP TABLE IF EXISTS `v_bank_logs`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_bank_logs` AS select if((`bl`.`type` = 0),-(`bl`.`amount`),if((`bl`.`type` = 1),`bl`.`amount`,if((`bl`.`type` = 2),`bl`.`amount`,0))) AS `toplam_borc`,`bl`.`bank_id` AS `bank_id`,`bl`.`instalment` AS `instalment`,`bl`.`rate` AS `rate`,(`bl`.`modified_date` + interval `bi`.`payment_day` day) AS `payment_date`,`bl`.`description` AS `desc`,`bl`.`log_id` AS `log_id`,if((`bl`.`type` = 0),'Para Çekme',if((`bl`.`type` = 1),'POS Satis',if((`bl`.`type` = 2),'Elden Para Yatirma',''))) AS `type`,`bl`.`amount` AS `payment_price`,`bl`.`modified_date` AS `modified_date` from (`bank_logs` `bl` join `bank_instalments` `bi` on((`bl`.`bank_id` = `bi`.`bank_id`)));

-- --------------------------------------------------------

--
-- Görünüm yapısı `v_customers_master`
--
DROP TABLE IF EXISTS `v_customers_master`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_customers_master` AS select `cd`.`customer_name` AS `name`,`cd`.`customer_tel` AS `tel`,`cd`.`customer_address` AS `address`,`cd`.`customer_id` AS `customer_id`,`cd`.`is_deleted` AS `is_deleted`,ifnull(sum(`v_c`.`bakiye`),0) AS `payment_price`,ifnull(max(date_format(`v_c`.`payment_date`,'%d.%m.%Y')),'ALISVERIS YAPILMAMIS') AS `last_modified_date` from (`customer_details` `cd` left join `v_customer_payment_details` `v_c` on((`v_c`.`customer_id` = `cd`.`customer_id`))) group by `cd`.`customer_id`;

-- --------------------------------------------------------

--
-- Görünüm yapısı `v_customer_payment_details`
--
DROP TABLE IF EXISTS `v_customer_payment_details`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_customer_payment_details` AS select `cd`.`customer_name` AS `name`,`cp`.`customer_id` AS `customer_id`,`cp`.`total_price` AS `total_price`,`cp`.`id` AS `payment_id`,`cp`.`payment` AS `payment_price`,if((`cp`.`total_price` = 0),0,(`cp`.`total_price` - `cp`.`payment`)) AS `owed`,(`cp`.`total_price` - `cp`.`payment`) AS `bakiye`,`cp`.`payment_desc` AS `payment_desc`,if((`cp`.`type` = 1),'ALISVERIS',if((`cp`.`type` = 0),'ELDEN ÖDEME',if((`cp`.`type` = 2),'HEDIYE',if((`cp`.`type` = 3),'ÜRÜN IADE',if((`cp`.`type` = 4),'ELDEN BORC KAYDI',''))))) AS `type`,`cp`.`type` AS `type_id`,`cp`.`payment_date` AS `payment_date` from (`customer_payments` `cp` left join `customer_details` `cd` on((`cp`.`customer_id` = `cd`.`customer_id`)));

-- --------------------------------------------------------

--
-- Görünüm yapısı `v_expense_details`
--
DROP TABLE IF EXISTS `v_expense_details`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_expense_details` AS select `ed`.`payment_id` AS `payment_id`,`ed`.`payment_cat` AS `payment_cat`,`pd`.`process_name` AS `process_name`,`ed`.`payment_price` AS `payment_price`,4 AS `type`,`pd`.`display_order` AS `display_order`,`ed`.`payment_desc` AS `payment_desc`,`ed`.`modified_date` AS `modified_date` from (`expense_details` `ed` join `process_details` `pd` on((`ed`.`payment_cat` = `pd`.`process_id`))) union select `sp`.`payment_id` AS `payment_id`,-(1) AS `payment_cat`,if((`sp`.`type` = 0),'MALZEME ALIM TUTARI','') AS `process_name`,`sp`.`payment_price` AS `payment_price`,`sp`.`type` AS `type`,1 AS `display_order`,`sp`.`payment_desc` AS `payment_desc`,`sp`.`modified_date` AS `modified_date` from `suppliers_payment` `sp` where (`sp`.`type` = 0);

-- --------------------------------------------------------

--
-- Görünüm yapısı `v_goods_amount`
--
DROP TABLE IF EXISTS `v_goods_amount`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_goods_amount` AS select `g`.`goods_id` AS `goods_id`,`g`.`goods_name` AS `goods_name`,`g`.`unit` AS `unit`,`g`.`create_date` AS `create_date`,`g`.`modified_date` AS `modified_date`,`g`.`is_deleted` AS `is_deleted`,max(`v`.`modified_date`) AS `last_modified_date`,ifnull(sum(`v`.`product_count`),0) AS `count` from (`goods_details` `g` left join `v_goods_stocks` `v` on((`v`.`goods_id` = `g`.`goods_id`))) group by `g`.`goods_id`;

-- --------------------------------------------------------

--
-- Görünüm yapısı `v_goods_stocks`
--
DROP TABLE IF EXISTS `v_goods_stocks`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_goods_stocks` AS select `g`.`goods_name` AS `goods_name`,`g`.`goods_id` AS `goods_id`,`g`.`unit` AS `unit`,`g`.`is_deleted` AS `is_deleted`,`sd`.`product_count` AS `product_count`,`sd`.`modified_date` AS `modified_date` from (`stock_details` `sd` join `goods_details` `g` on((`g`.`goods_id` = `sd`.`goods_id`)));

-- --------------------------------------------------------

--
-- Görünüm yapısı `v_goods_view`
--
DROP TABLE IF EXISTS `v_goods_view`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_goods_view` AS select `lbp`.`last_buy_price` AS `last_buy_price`,ifnull(`gvh`.`total_return`,0) AS `total_return_amount`,ifnull(`gvh`.`return_price`,0) AS `return_price`,`bl`.`goods_id` AS `goods_id`,sum((`bp`.`buy_price` * `bl`.`product_count`)) AS `cost`,sum((`bp`.`kdv` * `bl`.`product_count`)) AS `kdv`,sum(`bl`.`product_count`) AS `total_buy_amount` from (((`buy_list` `bl` join `buy_price` `bp` on((`bl`.`list_id` = `bp`.`list_id`))) join `v_last_buy_price` `lbp` on((`lbp`.`goods_id` = `bl`.`goods_id`))) left join `v_goods_view_helper` `gvh` on((`gvh`.`goods_id` = `bl`.`goods_id`))) group by `bl`.`goods_id`;

-- --------------------------------------------------------

--
-- Görünüm yapısı `v_goods_view_helper`
--
DROP TABLE IF EXISTS `v_goods_view_helper`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_goods_view_helper` AS select `gd`.`goods_id` AS `goods_id`,sum(`pr`.`product_count`) AS `total_return`,sum(`pr`.`product_price`) AS `return_price` from (`product_return` `pr` join `goods_details` `gd` on((`pr`.`product_id` = `gd`.`goods_id`))) group by `pr`.`product_id`;

-- --------------------------------------------------------

--
-- Görünüm yapısı `v_last_buy_price`
--
DROP TABLE IF EXISTS `v_last_buy_price`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_last_buy_price` AS select `helper`.`list_id` AS `list_id`,`helper`.`goods_id` AS `goods_id`,(`bp`.`buy_price` + `bp`.`kdv`) AS `last_buy_price`,`bp`.`currency` AS `currency`,`bp`.`modified_date` AS `modified_date` from (`v_last_buy_price_helper` `helper` left join `buy_price` `bp` on((`helper`.`list_id` = `bp`.`list_id`)));

-- --------------------------------------------------------

--
-- Görünüm yapısı `v_last_buy_price_helper`
--
DROP TABLE IF EXISTS `v_last_buy_price_helper`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_last_buy_price_helper` AS select max(`bl`.`list_id`) AS `list_id`,`bl`.`goods_id` AS `goods_id` from `buy_list` `bl` group by `bl`.`goods_id`;

-- --------------------------------------------------------

--
-- Görünüm yapısı `v_latest_orders`
--
DROP TABLE IF EXISTS `v_latest_orders`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_latest_orders` AS select `o2a`.`order_id` AS `order_id`,`ac`.`account_id` AS `account_id`,`ac`.`account_type` AS `account_type`,`ac`.`account_owner` AS `account_owner`,`ac`.`account_status` AS `account_status`,`ac`.`create_date` AS `create_date` from (`orders_to_accounts` `o2a` left join `account_details` `ac` on((`ac`.`account_id` = `o2a`.`account_id`)));

-- --------------------------------------------------------

--
-- Görünüm yapısı `v_options_to_product`
--
DROP TABLE IF EXISTS `v_options_to_product`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_options_to_product` AS select `pd`.`product_id` AS `product_id`,`pd`.`product_name` AS `product_name`,`od`.`option_id` AS `option_id`,`od`.`option_name` AS `option_name`,`o2p`.`is_deleted` AS `is_deleted` from ((`options_to_product` `o2p` join `product_details` `pd` on((`pd`.`product_id` = `o2p`.`product_id`))) join `option_details` `od` on((`od`.`option_id` = `o2p`.`option_id`)));

-- --------------------------------------------------------

--
-- Görünüm yapısı `v_orders_master`
--
DROP TABLE IF EXISTS `v_orders_master`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_orders_master` AS select `od`.`order_id` AS `order_id`,`od`.`order_desc` AS `order_desc`,`ad`.`account_type` AS `order_type`,`od`.`order_status` AS `order_status`,`sd`.`status_name` AS `status_name`,`ad`.`create_date` AS `account_create_date`,`od`.`create_date` AS `order_create_date`,`od`.`modified_date` AS `order_modified_date`,`ad`.`account_id` AS `account_id`,`ad`.`staff_id` AS `account_staff_id`,`od`.`staff_id` AS `order_staff_id`,`st`.`name` AS `order_staff_name`,`s`.`name` AS `account_staff_name`,`ad`.`account_owner` AS `owner_id`,`ad`.`account_type` AS `account_type`,`ad`.`account_status` AS `account_status`,if((`ad`.`account_type` = 2),`cd`.`customer_name`,'') AS `customer_name`,if((`ad`.`account_type` = 1),`td`.`table_name`,'') AS `table_name`,if((`ad`.`account_type` = 1),`td`.`table_category_name`,'') AS `table_category_name`,sum(`p2o`.`amount`) AS `order_amount`,sum((`p2o`.`product_price` * `p2o`.`amount`)) AS `order_price`,`od`.`is_deleted` AS `is_deleted` from ((((((((`product_to_order` `p2o` left join `order_details` `od` on((`od`.`order_id` = `p2o`.`order_id`))) left join `orders_to_accounts` `o2a` on((`o2a`.`order_id` = `od`.`order_id`))) left join `account_details` `ad` on((`o2a`.`account_id` = `ad`.`account_id`))) left join `status_details` `sd` on((`sd`.`status_id` = `ad`.`account_status`))) left join `customer_details` `cd` on((`cd`.`customer_id` = `ad`.`account_owner`))) left join `v_table_details` `td` on((`td`.`table_id` = `ad`.`account_owner`))) left join `staff` `s` on((`s`.`id` = `ad`.`staff_id`))) left join `staff` `st` on((`st`.`id` = `od`.`staff_id`))) group by `p2o`.`order_id`;

-- --------------------------------------------------------

--
-- Görünüm yapısı `v_orders_to_accounts`
--
DROP TABLE IF EXISTS `v_orders_to_accounts`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_orders_to_accounts` AS select `o2a`.`account_id` AS `account_id`,`o2a`.`order_id` AS `order_id`,`od`.`is_deleted` AS `is_order_deleted` from (`orders_to_accounts` `o2a` left join `order_details` `od` on((`o2a`.`order_id` = `od`.`order_id`)));

-- --------------------------------------------------------

--
-- Görünüm yapısı `v_payment_details`
--
DROP TABLE IF EXISTS `v_payment_details`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_payment_details` AS select `pd`.`sell_id` AS `sell_id`,sum(if((`pd`.`payment_type` = 0),`pd`.`payment_price`,0)) AS `pesin`,sum(if((`pd`.`payment_type` = 1),`pd`.`payment_price`,0)) AS `pos`,sum(if((`pd`.`payment_type` = 2),`pd`.`payment_price`,0)) AS `veresiye`,sum(if((`pd`.`payment_type` = 3),`pd`.`payment_price`,0)) AS `hediye` from `payment_details` `pd` group by `pd`.`sell_id` order by `pd`.`sell_id` desc;

-- --------------------------------------------------------

--
-- Görünüm yapısı `v_products`
--
DROP TABLE IF EXISTS `v_products`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_products` AS select `p`.`product_id` AS `product_id`,`p`.`unit` AS `unit`,if((`p`.`unit` = 0),'TL',if((`p`.`unit` = 1),'USD','')) AS `unit_text`,`p`.`product_isDeleted` AS `is_deleted`,`p`.`isOnMenu` AS `isOnMenu`,if((`p`.`product_isDeleted` = 0),'Evet','Hayir') AS `product_isDeleted`,`p`.`product_code_manual` AS `product_code_manual`,`p`.`product_img_path` AS `product_img_path`,`p`.`product_cat` AS `product_cat`,`p`.`product_name` AS `product_name`,`ptp`.`product_price` AS `product_price`,`p`.`product_desc` AS `product_desc`,`p`.`modified_date` AS `modified_date`,`c`.`cat_name` AS `cat_name`,`psl`.`min_size` AS `min_size`,`psl`.`max_size` AS `max_size` from (((`product_details` `p` join `category_details` `c` on((`c`.`cat_id` = `p`.`product_cat`))) join `product_size_limits` `psl` on((`p`.`product_id` = `psl`.`product_id`))) left join `price_to_product` `ptp` on(((`p`.`product_id` = `ptp`.`product_id`) and (`ptp`.`porsion` = 1))));

-- --------------------------------------------------------

--
-- Görünüm yapısı `v_product_return`
--
DROP TABLE IF EXISTS `v_product_return`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_product_return` AS select `sd`.`goods_id` AS `goods_id`,`sd`.`goods_name` AS `product_name`,`sd`.`product_count` AS `product_count`,`sd`.`unit` AS `unit`,`sd`.`modified_date` AS `modified_date`,`lbp`.`last_buy_price` AS `last_buy_price` from (`v_goods_stocks` `sd` join `v_last_buy_price` `lbp` on((`lbp`.`goods_id` = `sd`.`goods_id`)));

-- --------------------------------------------------------

--
-- Görünüm yapısı `v_product_return_count`
--
DROP TABLE IF EXISTS `v_product_return_count`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_product_return_count` AS select `product_return`.`product_id` AS `product_id`,sum(`product_return`.`product_count`) AS `product_return_count` from `product_return` group by `product_return`.`product_id`;

-- --------------------------------------------------------

--
-- Görünüm yapısı `v_product_to_color`
--
DROP TABLE IF EXISTS `v_product_to_color`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_product_to_color` AS select `p2c`.`id` AS `id`,`p2c`.`product_id` AS `product_id`,`p2c`.`color_id` AS `color_id`,`pd`.`product_name` AS `product_name`,`cd`.`color_name` AS `color_name` from ((`product_to_color` `p2c` join `product_details` `pd` on((`pd`.`product_id` = `p2c`.`product_id`))) join `color_details` `cd` on((`cd`.`color_id` = `p2c`.`color_id`)));

-- --------------------------------------------------------

--
-- Görünüm yapısı `v_product_to_order`
--
DROP TABLE IF EXISTS `v_product_to_order`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_product_to_order` AS select `p2o`.`order_id` AS `order_id`,`p2o`.`log_id` AS `log_id`,`od`.`order_type` AS `order_type`,`od`.`order_status` AS `order_status`,`od`.`staff_id` AS `staff_id`,`s`.`name` AS `order_staff_name`,`pd`.`product_id` AS `product_id`,`pd`.`product_name` AS `product_name`,`p2o`.`product_desc` AS `product_desc`,`p2o`.`product_price` AS `product_price`,`p2o`.`porsion` AS `porsion`,`p2o`.`amount` AS `amount`,(`p2o`.`product_price` * `p2o`.`amount`) AS `total_price`,`od`.`is_deleted` AS `is_deleted`,`o2a`.`account_id` AS `account_id`,`ad`.`account_type` AS `account_type`,`ad`.`account_status` AS `account_status` from (((((`product_to_order` `p2o` join `product_details` `pd` on((`p2o`.`product_id` = `pd`.`product_id`))) left join `order_details` `od` on((`od`.`order_id` = `p2o`.`order_id`))) left join `orders_to_accounts` `o2a` on((`o2a`.`order_id` = `od`.`order_id`))) join `account_details` `ad` on((`o2a`.`account_id` = `ad`.`account_id`))) left join `staff` `s` on((`s`.`id` = `od`.`staff_id`)));

-- --------------------------------------------------------

--
-- Görünüm yapısı `v_product_view`
--
DROP TABLE IF EXISTS `v_product_view`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_product_view` AS select `sl`.`product_id` AS `product_id`,sum(`sl`.`product_amount`) AS `total_sale_amount`,sum((`sl`.`product_amount` * `sl`.`product_price`)) AS `total_sale_price`,ifnull(`vh`.`total_return`,0) AS `total_return_amount`,ifnull(`vh`.`total_return_price`,0) AS `total_return_price`,`p2p`.`product_price` AS `product_price`,`pd`.`product_name` AS `product_name` from (((`sell_list` `sl` left join `v_product_view_helper` `vh` on((`vh`.`product_id` = `sl`.`product_id`))) join `product_details` `pd` on((`pd`.`product_id` = `sl`.`product_id`))) join `price_to_product` `p2p` on(((`pd`.`product_id` = `p2p`.`product_id`) and (`p2p`.`porsion` = 1)))) group by `sl`.`product_id`;

-- --------------------------------------------------------

--
-- Görünüm yapısı `v_product_view_helper`
--
DROP TABLE IF EXISTS `v_product_view_helper`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_product_view_helper` AS select `pd`.`product_name` AS `product_name`,`pd`.`product_price` AS `product_price`,`pd`.`product_id` AS `product_id`,sum(`sr`.`product_amount`) AS `total_return`,sum(`sr`.`product_price`) AS `total_return_price` from (`sell_return` `sr` join `product_details` `pd` on((`sr`.`product_id` = `pd`.`product_id`))) group by `sr`.`product_id`;

-- --------------------------------------------------------

--
-- Görünüm yapısı `v_sell_lists`
--
DROP TABLE IF EXISTS `v_sell_lists`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_sell_lists` AS select `sl`.`sell_id` AS `sell_id`,`sl`.`product_id` AS `product_id`,`pd`.`product_name` AS `product_name`,`sl`.`product_amount` AS `product_amount`,`sl`.`product_price` AS `product_price`,`sl`.`modified_date` AS `modified_date`,`sd`.`sell_desc` AS `sell_desc`,`sd`.`staff_id` AS `sell_staff_id`,`s`.`name` AS `sell_staff_name`,`sd`.`account_id` AS `account_id`,`ad`.`account_owner` AS `account_owner`,if((`ad`.`account_type` = 1),'RESTORAN',`cd`.`customer_name`) AS `account_owner_name`,`ad`.`account_type` AS `account_type`,`ad`.`staff_id` AS `account_staff_id`,`st`.`name` AS `account_staff_name`,`ad`.`account_status` AS `account_status` from ((((((`sell_details` `sd` left join `sell_list` `sl` on((`sd`.`sell_id` = `sl`.`sell_id`))) left join `account_details` `ad` on((`ad`.`account_id` = `sd`.`account_id`))) left join `customer_details` `cd` on((`ad`.`account_owner` = `cd`.`customer_id`))) left join `staff` `s` on((`s`.`id` = `sd`.`staff_id`))) left join `staff` `st` on((`st`.`id` = `ad`.`staff_id`))) left join `product_details` `pd` on((`pd`.`product_id` = `sl`.`product_id`)));

-- --------------------------------------------------------

--
-- Görünüm yapısı `v_sell_lists_master`
--
DROP TABLE IF EXISTS `v_sell_lists_master`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_sell_lists_master` AS select `v_sl`.`sell_id` AS `sell_id`,`v_sl`.`account_owner_name` AS `account_owner_name`,`v_sl`.`account_staff_id` AS `account_staff_id`,`v_sl`.`sell_staff_name` AS `sell_staff_name`,`v_sl`.`account_staff_name` AS `account_staff_name`,sum(`v_sl`.`product_amount`) AS `total_sale_amount`,`v_sl`.`sell_desc` AS `sell_desc`,sum((`v_sl`.`product_amount` * `v_sl`.`product_price`)) AS `sell_income`,`v_sl`.`modified_date` AS `modified_date` from `v_sell_lists` `v_sl` group by `v_sl`.`sell_id`;

-- --------------------------------------------------------

--
-- Görünüm yapısı `v_sell_via_pos_details`
--
DROP TABLE IF EXISTS `v_sell_via_pos_details`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_sell_via_pos_details` AS select `p2b`.`payment_id` AS `payment_id`,`p2b`.`bank_id` AS `bank_id`,`p2b`.`modified_date` AS `modified_date`,`bd`.`bank_name` AS `bank_name`,`pd`.`sell_id` AS `sell_id`,`pd`.`payment_type` AS `payment_type`,`pd`.`payment_price` AS `payment_price` from ((`payment_to_bank` `p2b` left join `payment_details` `pd` on((`p2b`.`payment_id` = `pd`.`payment_id`))) left join `bank_details` `bd` on((`p2b`.`bank_id` = `bd`.`bank_id`)));

-- --------------------------------------------------------

--
-- Görünüm yapısı `v_supplier_payment_details`
--
DROP TABLE IF EXISTS `v_supplier_payment_details`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_supplier_payment_details` AS select `sp`.`process_id` AS `process_id`,`sp`.`type` AS `p_type`,if((`sp`.`type` = 1),`gd`.`goods_name`,'') AS `goods_name`,if((`sp`.`type` = 2),sum(`bl`.`product_count`),if((`sp`.`type` = 1),`pr`.`product_count`,'')) AS `product_count`,`gd`.`unit` AS `unit`,(if((`sp`.`type` = 2),`bl`.`product_count`,if((`sp`.`type` = 1),`pr`.`product_count`,'')) * -(1)) AS `total_weight`,if((`sp`.`type` = 2),`sp`.`payment_price`,if((`sp`.`type` = 3),`sp`.`payment_price`,-(`sp`.`payment_price`))) AS `toplam_borc`,`sp`.`suppliers_id` AS `suppliers_id`,`sp`.`payment_id` AS `payment_id`,if((`sp`.`type` = 0),'Elden Ödeme',if((`sp`.`type` = 1),'Ürün iade',if((`sp`.`type` = 2),'Malzeme alim',if((`sp`.`type` = 3),'Borç Kayit','')))) AS `type`,`sp`.`payment_price` AS `payment_price`,`sp`.`modified_date` AS `modified_date`,`sp`.`payment_desc` AS `payment_desc` from (((`suppliers_payment` `sp` left join `product_return` `pr` on((`sp`.`process_id` = `pr`.`id`))) left join `buy_list` `bl` on((`sp`.`process_id` = `bl`.`buy_id`))) left join `goods_details` `gd` on((`pr`.`product_id` = `gd`.`goods_id`))) group by `sp`.`payment_id`;

-- --------------------------------------------------------

--
-- Görünüm yapısı `v_supplier_payment_type2_details`
--
DROP TABLE IF EXISTS `v_supplier_payment_type2_details`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_supplier_payment_type2_details` AS select `sp`.`payment_id` AS `payment_id`,`sp`.`suppliers_id` AS `suppliers_id`,`sp`.`type` AS `type`,`sp`.`process_id` AS `process_id`,`sp`.`payment_price` AS `payment_price`,`sp`.`payment_desc` AS `payment_desc`,`sp`.`modified_date` AS `modified_date`,`bl`.`list_id` AS `list_id`,`bl`.`goods_id` AS `goods_id`,`gd`.`goods_name` AS `goods_name`,`gd`.`unit` AS `unit`,`gd`.`goods_id` AS `g_id`,sum(`bl`.`product_count`) AS `product_count`,`bp`.`buy_price` AS `buy_price`,`bp`.`kdv` AS `kdv` from (((`suppliers_payment` `sp` left join `buy_list` `bl` on((`sp`.`process_id` = `bl`.`buy_id`))) left join `buy_price` `bp` on((`bl`.`list_id` = `bp`.`list_id`))) left join `goods_details` `gd` on((`gd`.`goods_id` = `bl`.`goods_id`))) group by `sp`.`payment_id`;

-- --------------------------------------------------------

--
-- Görünüm yapısı `v_tables_total_price`
--
DROP TABLE IF EXISTS `v_tables_total_price`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_tables_total_price` AS select sum((`pto`.`product_price` * `pto`.`amount`)) AS `SUM(pto.product_price*pto.amount)`,`ad`.`account_owner` AS `account_owner`,`ad`.`account_id` AS `account_id`,`ota`.`order_id` AS `order_id` from ((`orders_to_accounts` `ota` join `account_details` `ad` on((`ota`.`account_id` = `ad`.`account_id`))) join `product_to_order` `pto` on((`pto`.`order_id` = `ota`.`order_id`))) where ((`ad`.`account_type` = 1) and (`ad`.`account_status` = 1)) group by `pto`.`order_id`;

-- --------------------------------------------------------

--
-- Görünüm yapısı `v_table_categories`
--
DROP TABLE IF EXISTS `v_table_categories`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_table_categories` AS select `tc`.`tcategory_id` AS `tcategory_id`,`tc`.`tcategory_name` AS `tcategory_name`,`tc`.`tcategory_status` AS `tcategory_status`,`tc`.`create_date` AS `create_date`,`tc`.`modified_date` AS `modified_date`,`tc`.`display_order` AS `display_order`,`tc`.`is_deleted` AS `is_deleted`,`s`.`status_name` AS `status_name` from (`table_categories` `tc` join `status_details` `s` on((`s`.`status_id` = `tc`.`tcategory_status`))) where (`tc`.`is_deleted` = 0);

-- --------------------------------------------------------

--
-- Görünüm yapısı `v_table_details`
--
DROP TABLE IF EXISTS `v_table_details`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_table_details` AS select `td`.`table_id` AS `table_id`,`td`.`table_name` AS `table_name`,`td`.`table_category` AS `table_category`,`tc`.`tcategory_name` AS `table_category_name`,`tc`.`display_order` AS `table_category_display_order`,`td`.`table_status` AS `table_status`,`td`.`create_date` AS `create_date`,`td`.`modified_date` AS `modified_date`,`td`.`display_order` AS `display_order`,`td`.`is_deleted` AS `is_deleted`,`s`.`status_name` AS `status_name` from ((`table_details` `td` join `status_details` `s` on((`s`.`status_id` = `td`.`table_status`))) join `table_categories` `tc` on(((`tc`.`tcategory_id` = `td`.`table_category`) and (`tc`.`is_deleted` = 0) and (`tc`.`tcategory_status` <> 2))));

-- --------------------------------------------------------

--
-- Görünüm yapısı `v_table_orders`
--
DROP TABLE IF EXISTS `v_table_orders`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_table_orders` AS select `ac`.`account_id` AS `account_id`,`ac`.`account_type` AS `account_type`,`ac`.`account_owner` AS `account_owner`,`ac`.`account_status` AS `account_status`,`o2a`.`order_id` AS `order_id`,`p2o`.`product_id` AS `product_id`,`p2o`.`product_price` AS `product_price`,`p2o`.`amount` AS `amount`,`p2o`.`porsion` AS `porsion` from ((`orders_to_accounts` `o2a` left join `account_details` `ac` on((`ac`.`account_id` = `o2a`.`account_id`))) left join `product_to_order` `p2o` on((`p2o`.`order_id` = `o2a`.`order_id`)));

--
-- Dökümü yapılmış tablolar için kısıtlamalar
--

--
-- Tablo kısıtlamaları `account_details`
--
ALTER TABLE `account_details`
  ADD CONSTRAINT `account_details_ibfk_1` FOREIGN KEY (`account_status`) REFERENCES `status_details` (`status_id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  ADD CONSTRAINT `account_details_ibfk_2` FOREIGN KEY (`staff_id`) REFERENCES `staff` (`id`) ON DELETE NO ACTION ON UPDATE CASCADE;

--
-- Tablo kısıtlamaları `bank_instalments`
--
ALTER TABLE `bank_instalments`
  ADD CONSTRAINT `bank_instalments_ibfk_1` FOREIGN KEY (`bank_id`) REFERENCES `bank_details` (`bank_id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Tablo kısıtlamaları `bank_logs`
--
ALTER TABLE `bank_logs`
  ADD CONSTRAINT `bank_logs_ibfk_1` FOREIGN KEY (`bank_id`) REFERENCES `bank_details` (`bank_id`) ON DELETE NO ACTION ON UPDATE CASCADE;

--
-- Tablo kısıtlamaları `buy_details`
--
ALTER TABLE `buy_details`
  ADD CONSTRAINT `buy_details_ibfk_1` FOREIGN KEY (`suppliers_id`) REFERENCES `suppliers_details` (`suppliers_id`) ON DELETE SET NULL ON UPDATE CASCADE;

--
-- Tablo kısıtlamaları `buy_list`
--
ALTER TABLE `buy_list`
  ADD CONSTRAINT `buy_list_ibfk_1` FOREIGN KEY (`buy_id`) REFERENCES `buy_details` (`buy_id`) ON DELETE SET NULL ON UPDATE CASCADE,
  ADD CONSTRAINT `buy_list_ibfk_2` FOREIGN KEY (`goods_id`) REFERENCES `goods_details` (`goods_id`) ON DELETE SET NULL ON UPDATE CASCADE;

--
-- Tablo kısıtlamaları `buy_price`
--
ALTER TABLE `buy_price`
  ADD CONSTRAINT `buy_price_ibfk_1` FOREIGN KEY (`list_id`) REFERENCES `buy_list` (`list_id`) ON DELETE SET NULL ON UPDATE CASCADE;

--
-- Tablo kısıtlamaları `customer_payments`
--
ALTER TABLE `customer_payments`
  ADD CONSTRAINT `customer_payments_ibfk_1` FOREIGN KEY (`customer_id`) REFERENCES `customer_details` (`customer_id`) ON UPDATE NO ACTION;

--
-- Tablo kısıtlamaları `orders_in`
--
ALTER TABLE `orders_in`
  ADD CONSTRAINT `orders_in_ibfk_1` FOREIGN KEY (`order_id`) REFERENCES `order_details` (`order_id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  ADD CONSTRAINT `orders_in_ibfk_2` FOREIGN KEY (`table_id`) REFERENCES `table_details` (`table_id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  ADD CONSTRAINT `orders_in_ibfk_3` FOREIGN KEY (`staff_id`) REFERENCES `staff` (`id`) ON DELETE NO ACTION ON UPDATE CASCADE;

--
-- Tablo kısıtlamaları `orders_to_accounts`
--
ALTER TABLE `orders_to_accounts`
  ADD CONSTRAINT `orders_to_accounts_ibfk_1` FOREIGN KEY (`account_id`) REFERENCES `account_details` (`account_id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  ADD CONSTRAINT `orders_to_accounts_ibfk_2` FOREIGN KEY (`order_id`) REFERENCES `order_details` (`order_id`) ON DELETE NO ACTION ON UPDATE CASCADE;

--
-- Tablo kısıtlamaları `order_details`
--
ALTER TABLE `order_details`
  ADD CONSTRAINT `order_details_ibfk_1` FOREIGN KEY (`order_status`) REFERENCES `status_details` (`status_id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  ADD CONSTRAINT `order_details_ibfk_2` FOREIGN KEY (`staff_id`) REFERENCES `staff` (`id`) ON UPDATE CASCADE;

--
-- Tablo kısıtlamaları `payment_details`
--
ALTER TABLE `payment_details`
  ADD CONSTRAINT `payment_details_ibfk_1` FOREIGN KEY (`sell_id`) REFERENCES `sell_details` (`sell_id`) ON DELETE SET NULL ON UPDATE CASCADE;

--
-- Tablo kısıtlamaları `payment_to_bank`
--
ALTER TABLE `payment_to_bank`
  ADD CONSTRAINT `payment_to_bank_ibfk_1` FOREIGN KEY (`payment_id`) REFERENCES `payment_details` (`payment_id`) ON DELETE SET NULL ON UPDATE CASCADE,
  ADD CONSTRAINT `payment_to_bank_ibfk_2` FOREIGN KEY (`bank_id`) REFERENCES `bank_details` (`bank_id`) ON DELETE SET NULL ON UPDATE CASCADE;

--
-- Tablo kısıtlamaları `price_to_product`
--
ALTER TABLE `price_to_product`
  ADD CONSTRAINT `price_to_product_ibfk_1` FOREIGN KEY (`product_id`) REFERENCES `product_details` (`product_id`) ON DELETE NO ACTION ON UPDATE CASCADE;

--
-- Tablo kısıtlamaları `product_details`
--
ALTER TABLE `product_details`
  ADD CONSTRAINT `product_details_ibfk_1` FOREIGN KEY (`product_cat`) REFERENCES `category_details` (`cat_id`) ON DELETE SET NULL ON UPDATE CASCADE;

--
-- Tablo kısıtlamaları `product_return`
--
ALTER TABLE `product_return`
  ADD CONSTRAINT `product_return_ibfk_2` FOREIGN KEY (`product_id`) REFERENCES `goods_details` (`goods_id`) ON DELETE SET NULL ON UPDATE CASCADE;

--
-- Tablo kısıtlamaları `product_size_limits`
--
ALTER TABLE `product_size_limits`
  ADD CONSTRAINT `product_size_limits_ibfk_1` FOREIGN KEY (`product_id`) REFERENCES `product_details` (`product_id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Tablo kısıtlamaları `product_to_color`
--
ALTER TABLE `product_to_color`
  ADD CONSTRAINT `product_to_color_ibfk_1` FOREIGN KEY (`product_id`) REFERENCES `product_details` (`product_id`),
  ADD CONSTRAINT `product_to_color_ibfk_2` FOREIGN KEY (`color_id`) REFERENCES `color_details` (`color_id`);

--
-- Tablo kısıtlamaları `product_to_order`
--
ALTER TABLE `product_to_order`
  ADD CONSTRAINT `product_to_order_ibfk_1` FOREIGN KEY (`product_id`) REFERENCES `product_details` (`product_id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  ADD CONSTRAINT `product_to_order_ibfk_2` FOREIGN KEY (`order_id`) REFERENCES `order_details` (`order_id`) ON DELETE NO ACTION ON UPDATE CASCADE;

--
-- Tablo kısıtlamaları `sell_details`
--
ALTER TABLE `sell_details`
  ADD CONSTRAINT `sell_details_ibfk_2` FOREIGN KEY (`staff_id`) REFERENCES `staff` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `sell_details_ibfk_3` FOREIGN KEY (`account_id`) REFERENCES `account_details` (`account_id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Tablo kısıtlamaları `sell_list`
--
ALTER TABLE `sell_list`
  ADD CONSTRAINT `sell_list_ibfk_1` FOREIGN KEY (`sell_id`) REFERENCES `sell_details` (`sell_id`) ON DELETE SET NULL ON UPDATE CASCADE,
  ADD CONSTRAINT `sell_list_ibfk_2` FOREIGN KEY (`product_id`) REFERENCES `product_details` (`product_id`) ON DELETE SET NULL ON UPDATE CASCADE;

--
-- Tablo kısıtlamaları `suppliers_payment`
--
ALTER TABLE `suppliers_payment`
  ADD CONSTRAINT `suppliers_payment_ibfk_1` FOREIGN KEY (`suppliers_id`) REFERENCES `suppliers_details` (`suppliers_id`) ON DELETE SET NULL ON UPDATE CASCADE;

--
-- Tablo kısıtlamaları `table_details`
--
ALTER TABLE `table_details`
  ADD CONSTRAINT `table_details_ibfk_2` FOREIGN KEY (`table_status`) REFERENCES `status_details` (`status_id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  ADD CONSTRAINT `table_details_ibfk_3` FOREIGN KEY (`table_category`) REFERENCES `table_categories` (`tcategory_id`) ON DELETE NO ACTION ON UPDATE CASCADE;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
