######## GOODS TABLE ################
create table GOODS
(
	GoodsID char(10) not null,
    GoodsName char(100),
    SupplierID char(10),
    UnitPrice decimal,
    Stock int,
    GoodsStatus bool, #removed or haven't been removed, true means haven't been removed
    
    primary key(GoodsID),
    foreign key(SupplierID) references SUPPLIER(SupplierID)
);


DELIMITER //
Create Procedure InsertGoods(in _ID char(10), in _Name char(100), in _SupplierID char(10), in _UnitPrice decimal, in _Stock int, in _Status bool)
Begin
	insert into Goods values(_ID,_Name,_SupplierID ,_UnitPrice,_Stock,_Status);
End //
DELIMITER ;

DELIMITER //
Create Procedure UpdateGoods(in _ID char(10), in _Name char(100), in _SupplierID char(10), in _UnitPrice int)
Begin
	update Goods set GoodsName=_Name, SupplierID = _SupplierID, UnitPrice = _UnitPrice where GoodsID=_ID;
End //
DELIMITER ;

DELIMITER //
Create Procedure AddGoodsStock(in _ID char(10), in _Amount decimal)
Begin
	update Goods set Stock=Stock + _Amount where GoodsID=_ID;
End //
DELIMITER ;

DELIMITER //
Create Procedure FindGoods(in _ID char(10), in _Status bool)
Begin	
   
    select *
    from Goods
    where GoodsID = _ID and GoodsStatus = _Status;
          	
End //
DELIMITER ;

DELIMITER //
Create Procedure FindGoods(in _ID char(10), in _Name char(100), in _SupplierID char(10), in _Stock decimal, in _StockCompareType varchar(2), in _Status bool)
Begin	
    Create temporary table StockTable (GoodsID char(10));
    
    case _StockCompareType 
		when '=' then insert into StockTable select GoodsID from Goods where Stock = _Stock;
        when '>' then insert into StockTable select GoodsID from Goods where Stock > _Stock;
        when '>=' then insert into StockTable select GoodsID from Goods where Stock >= _Stock;
        when '<' then insert into StockTable select GoodsID from Goods where Stock < _Stock;
        when '<=' then insert into StockTable select GoodsID from Goods where Stock <= _Stock;
	end case;

    
    select GoodsID As 'Mã Hàng Hóa', GoodsName As 'Tên Hàng Hóa', SupplierID As 'Mã Nhà Cung Cấp', CONCAT('',Format(UnitPrice,0), ' đ') As 'Đơn giá' , Stock As 'Số lượng tồn'
	from Goods 
	where GoodsID like CONCAT('%', _ID, '%') and GoodsName like CONCAT('%', _Name, '%') and SupplierID like CONCAT('%', _SupplierID, '%') 
                                                         and GoodsStatus = _Status and GoodsID in (select GoodsID from StockTable);
	
    drop table StockTable;
	
End //
DELIMITER ;


DELIMITER //
Create Procedure UpdateGoodsStatus(in _ID char(10), in _Status Bool)
Begin
	update Goods set GoodsStatus = _Status where GoodsID=_ID;
End //
DELIMITER ;
