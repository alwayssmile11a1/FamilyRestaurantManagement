#drop database familyrestaurant;
create schema familyrestaurant;
use familyrestaurant;



######## DISH TABLE ################
create table DISH
(
	DishID char(10) not null,
    DishName char(100),
    UnitPrice decimal,
    DishStatus bool, #removed or haven't been removed, true means haven't been removed
    
    primary key(DishID)
);

DELIMITER //
Create Procedure InsertDish(in _ID char(10), in _DishName char(100), in _UnitPrice decimal, in _Status bool)
Begin
	Insert into DISH values(_ID, _DishName, _UnitPrice, _Status);
End //
DELIMITER ;

DELIMITER //
Create Procedure UpdateDish(in _ID char(10), in _DishName char(100),in _UnitPrice decimal)
Begin
	update DISH set DishName=_DishName, UnitPrice=_UnitPrice where DishID=_ID;
End //
DELIMITER ;

DELIMITER //
Create Procedure FindDish(in _ID char(10), in _Status bool)
Begin	
   
    select *
    from DISH
    where DishID = _ID and DishStatus = _Status;
          	
End //
DELIMITER ;

DELIMITER //
Create Procedure FindDishes(in _ID char(10), in _DishName char(100), in _UnitPrice decimal, 
                            in _UnitPriceCompareType varchar(2), in _Status bool)
Begin	
    Create temporary table UnitPriceTable (DishID char(10));
    
    case _UnitPriceCompareType 
		when '=' then insert into UnitPriceTable select DishID from DISH where UnitPrice = _UnitPrice;
        when '>' then insert into UnitPriceTable select DishID from DISH where UnitPrice > _UnitPrice;
        when '>=' then insert into UnitPriceTable select DishID from DISH where UnitPrice >= _UnitPrice;
        when '<' then insert into UnitPriceTable select DishID from DISH where UnitPrice < _UnitPrice;
        when '<=' then insert into UnitPriceTable select DishID from DISH where UnitPrice <= _UnitPrice;
	end case;
	 
    select DishID As 'Mã Món Ăn', DishName As 'Tên Món Ăn', CONCAT('',Format(UnitPrice,0), ' đ') As 'Đơn Giá' 
    from DISH
    where DishID like CONCAT('%', _ID, '%') and DishName like CONCAT('%', _DishName, '%') and DishStatus = _Status 
                                                                    and DishID in (select DishID from UnitPriceTable);
	
    drop table UnitPriceTable;
End //
DELIMITER ;

DELIMITER //
Create Procedure UpdateDishStatus(in _ID char(10), in _Status bool)
Begin
	update DISH set DishStatus = _Status where DishID=_ID;
End //
DELIMITER ;



######## CUSTOMER TABLE ################
create table CUSTOMER
(
	CustomerID char(10) not null,
    CustomerName char(100),
    CustomerAddress char(100),
    Phone char(20),
    Email char(50),
    DebtAmount decimal,
    CustomerStatus bool, #removed or haven't been removed, true means haven't been removed
    primary key(CustomerID)
);


DELIMITER //
Create Procedure InsertCustomer(in _ID char(10), in _Name char(100),in _Address char(100), in _Phone char(20), 
								in _Email char(50),in _DebtAmount decimal, in _Status bool)
Begin
	insert into Customer values(_ID,_Name,_Address,_Phone,_Email,_DebtAmount,_Status);
End //
DELIMITER ;

DELIMITER //
Create Procedure UpdateCustomer(in _ID char(10), in _Name char(100),in _Address char(100), in _Phone char(20), in _Email char(50))
Begin
	update Customer set CustomerName=_Name,CustomerAddress=_Address, Phone=_Phone ,Email=_Email where CustomerID=_ID;
End //
DELIMITER ;

DELIMITER //
Create Procedure AddCustomerDebt(in _ID char(10), in _Amount decimal)
Begin
	update Customer set DebtAmount=DebtAmount + _Amount where CustomerID=_ID;
End //
DELIMITER ;

DELIMITER //
Create Procedure FindCustomer(in _ID char(10), in _Status bool)
Begin	
   
    select *
    from Customer
    where CustomerID = _ID and CustomerStatus = _Status;
          	
End //
DELIMITER ;

DELIMITER //
Create Procedure FindCustomers(in _ID char(10), in _Name char(100),in _Address char(100), in _Phone char(20), 
							in _Email char(50), in _DebtAmount decimal, in _DebtAmountCompareType varchar(2), in _Status bool)
Begin	
    Create temporary table DebtAmountTable (CustomerID char(10));
    
    case _DebtAmountCompareType 
		when '=' then insert into DebtAmountTable select CustomerID from Customer where DebtAmount = _DebtAmount;
        when '>' then insert into DebtAmountTable select CustomerID from Customer where DebtAmount > _DebtAmount;
        when '>=' then insert into DebtAmountTable select CustomerID from Customer where DebtAmount >= _DebtAmount;
        when '<' then insert into DebtAmountTable select CustomerID from Customer where DebtAmount < _DebtAmount;
        when '<=' then insert into DebtAmountTable select CustomerID from Customer where DebtAmount <= _DebtAmount;
	end case;

    
    select CustomerID As 'Mã Khách Hàng', CustomerName As 'Họ Tên Khách Hàng', CustomerAddress As 'Địa Chỉ',Phone As 'Điện Thoại', Email As 'Email',
																										CONCAT('',Format(DebtAmount,0), ' đ') As 'Số Tiền Nợ' 
	from Customer 
	where CustomerID like CONCAT('%', _ID, '%') and  CustomerName like CONCAT('%', _Name, '%') and CustomerAddress like CONCAT('%', _Address, '%') and 
          Phone like CONCAT('%', _Phone, '%') and Email like CONCAT('%', _Email, '%') and CustomerStatus = _Status and CustomerID in (select CustomerID from DebtAmountTable);
	
    drop table DebtAmountTable;
	
End //
DELIMITER ;


DELIMITER //
Create Procedure UpdateCustomerStatus(in _ID char(10), in _Status Bool)
Begin
	update Customer set CustomerStatus = _Status where CustomerID=_ID;
End //
DELIMITER ;




######## STAFF TABLE ################
create table STAFFPOSITION
(
	PositionID char(10) not null,
    PositionName char(100),
    PayRate float,
    primary key(PositionID)
);

######## STAFF TABLE ################
create table STAFF
(
	StaffID char(10) not null,
    StaffName char(100),
    StaffAddress char(100),
    Phone char(20),
    Email char(50),
    PositionID char(10),
    Salary decimal,
    StaffStatus bool, #removed or haven't been removed, true means haven't been removed
    
    primary key(StaffID),
	foreign key(PositionID) references STAFFPOSITION(PositionID)
);


DELIMITER //
Create Procedure InsertStaff(in _ID char(10), in _Name char(100),in _Address char(100), in _Phone char(20), 
								in _Email char(50), in _PositionID char(10), in _Salary decimal, in _Status bool)
Begin
	insert into Staff values(_ID,_Name,_Address,_Phone,_Email,(select PositionID from STAFFPOSITION where PositionID=_PositionID),_Salary,_Status);
End //
DELIMITER ;

DELIMITER //
Create Procedure UpdateStaff(in _ID char(10), in _Name char(100),in _Address char(100), in _Phone char(20), in _Email char(50), in _PositionID char(10))
Begin
	update Staff set StaffName=_Name,StaffAddress=_Address, Phone=_Phone ,Email=_Email, PositionID = (select PositionID from STAFFPOSITION where PositionID=_PositionID) where StaffID=_ID;
End //
DELIMITER ;

DELIMITER //
Create Procedure AddStaffSalary(in _ID char(10), in _Amount decimal)
Begin
	update Staff set Salary=Salary + _Amount where StaffID=_ID;
End //
DELIMITER ;

DELIMITER //
Create Procedure FindStaff(in _ID char(10), in _Status bool)
Begin	
   
    select *
    from Staff
    where StaffID = _ID and StaffStatus = _Status;
          	
End //
DELIMITER ;

DELIMITER //
Create Procedure FindStaffs(in _ID char(10), in _Name char(100),in _Address char(100), in _Phone char(20), 
							in _Email char(50), in _PositionID char(10), in _Salary decimal, in _SalaryCompareType varchar(2), in _Status bool)
Begin	
    Create temporary table SalaryTable (StaffID char(10));
    
    case _SalaryCompareType 
		when '=' then insert into SalaryTable select StaffID from Staff where Salary = _Salary;
        when '>' then insert into SalaryTable select StaffID from Staff where Salary > _Salary;
        when '>=' then insert into SalaryTable select StaffID from Staff where Salary >= _Salary;
        when '<' then insert into SalaryTable select StaffID from Staff where Salary < _Salary;
        when '<=' then insert into SalaryTable select StaffID from Staff where Salary <= _Salary;
	end case;

    
    select StaffID As 'Mã Nhân Viên', StaffName As 'Họ Tên Nhân Viên', StaffAddress As 'Địa Chỉ',Phone As 'Điện Thoại', Email As 'Email', PositionID As 'Mã Vị Trí',
																										CONCAT('',Format(Salary,0), ' đ') As 'Lương tháng' 
	from Staff 
	where StaffID like CONCAT('%', _ID, '%') and PositionID like CONCAT('%', _PositionID, '%') and  StaffName like CONCAT('%', _Name, '%') and StaffAddress like CONCAT('%', _Address, '%') and 
          Phone like CONCAT('%', _Phone, '%') and Email like CONCAT('%', _Email, '%') and StaffStatus = _Status and StaffID in (select StaffID from SalaryTable);
	
    drop table SalaryTable;
	
End //
DELIMITER ;


DELIMITER //
Create Procedure UpdateStaffStatus(in _ID char(10), in _Status Bool)
Begin
	update Staff set StaffStatus = _Status where StaffID=_ID;
End //
DELIMITER ;



######## SUPPLIER TABLE ################
create table SUPPLIER
(
	SupplierID char(10) not null,
    SupplierName char(100),
    SupplierAddress char(100),
    Phone char(20),
    Email char(50),
    SupplierStatus bool, #removed or haven't been removed, true means haven't been removed
    
    primary key(SupplierID)
);


DELIMITER //
Create Procedure InsertSupplier(in _ID char(10), in _Name char(100),in _Address char(100), in _Phone char(20), 
								in _Email char(50), in _Status bool)
Begin
	insert into Supplier values(_ID,_Name,_Address,_Phone,_Email,_Status);
End //
DELIMITER ;

DELIMITER //
Create Procedure UpdateSupplier(in _ID char(10), in _Name char(100),in _Address char(100), in _Phone char(20), in _Email char(50))
Begin
	update Supplier set SupplierName=_Name,SupplierAddress=_Address, Phone=_Phone ,Email=_Email where SupplierID=_ID;
End //
DELIMITER ;

DELIMITER //
Create Procedure FindSupplier(in _ID char(10), in _Status bool)
Begin	
    select *
    from Supplier
    where SupplierID = _ID and SupplierStatus = _Status;
          	
End //
DELIMITER ;

DELIMITER //
Create Procedure FindSuppliers(in _ID char(10), in _Name char(100),in _Address char(100), in _Phone char(20), 
							in _Email char(50), in _Status bool)
Begin	    
    select SupplierID As 'Mã Nhà Cung Cấp', SupplierName As 'Tên Nhà Cung Cấp', SupplierAddress As 'Địa Chỉ',Phone As 'Điện Thoại', Email As 'Email'
																										
	from Supplier 
	where SupplierID like CONCAT('%', _ID, '%') and  SupplierName like CONCAT('%', _Name, '%') and SupplierAddress like CONCAT('%', _Address, '%') and 
          Phone like CONCAT('%', _Phone, '%') and Email like CONCAT('%', _Email, '%') and SupplierStatus = _Status;
	
End //
DELIMITER ;


DELIMITER //
Create Procedure UpdateSupplierStatus(in _ID char(10), in _Status Bool)
Begin
	update Supplier set SupplierStatus = _Status where SupplierID=_ID;
End //
DELIMITER ;


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
	insert into Goods values(_ID,_Name,(select SupplierID from SUPPLIER where SupplierID=_SupplierID) ,_UnitPrice,_Stock,_Status);
End //
DELIMITER ;

DELIMITER //
Create Procedure UpdateGoods(in _ID char(10), in _Name char(100), in _SupplierID char(10), in _UnitPrice int)
Begin
	update Goods set GoodsName=_Name, SupplierID = (select SupplierID from SUPPLIER where SupplierID=_SupplierID) , UnitPrice = _UnitPrice where GoodsID=_ID;
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
Create Procedure FindGoods(in _ID char(10), in _Name char(100), in _SupplierID char(10), in _UnitPrice decimal, in _UnitPriceCompareType varchar(2), in _Stock int, in _StockCompareType varchar(2), in _Status bool)
Begin	
    Create temporary table StockTable (GoodsID char(10));
    
    case _StockCompareType 
		when '=' then insert into StockTable select GoodsID from Goods where Stock = _Stock;
        when '>' then insert into StockTable select GoodsID from Goods where Stock > _Stock;
        when '>=' then insert into StockTable select GoodsID from Goods where Stock >= _Stock;
        when '<' then insert into StockTable select GoodsID from Goods where Stock < _Stock;
        when '<=' then insert into StockTable select GoodsID from Goods where Stock <= _Stock;
	end case;
	
    Create temporary table UnitPriceTable (GoodsID char(10));
    
    case _UnitPriceCompareType 
		when '=' then insert into UnitPriceTable select GoodsID from Goods where UnitPrice = _UnitPrice;
        when '>' then insert into UnitPriceTable select GoodsID from Goods where UnitPrice > _UnitPrice;
        when '>=' then insert into UnitPriceTable select GoodsID from Goods where UnitPrice >= _UnitPrice;
        when '<' then insert into UnitPriceTable select GoodsID from Goods where UnitPrice < _UnitPrice;
        when '<=' then insert into UnitPriceTable select GoodsID from Goods where UnitPrice <= _UnitPrice;
	end case;
    
    select GoodsID As 'Mã Hàng Hóa', GoodsName As 'Tên Hàng Hóa', SupplierID As 'Mã Nhà Cung Cấp', CONCAT('',Format(UnitPrice,0), ' đ') As 'Đơn giá' , Stock As 'Số lượng tồn'
	from Goods 
	where GoodsID like CONCAT('%', _ID, '%') and GoodsName like CONCAT('%', _Name, '%') and SupplierID like CONCAT('%', _SupplierID, '%') 
                                                         and GoodsStatus = _Status and GoodsID in (select GoodsID from StockTable) and GoodsID in (select GoodsID from UnitPriceTable);
	
    drop table StockTable;
    drop table UnitPriceTable;
	
End //
DELIMITER ;


DELIMITER //
Create Procedure UpdateGoodsStatus(in _ID char(10), in _Status Bool)
Begin
	update Goods set GoodsStatus = _Status where GoodsID=_ID;
End //
DELIMITER ;

######## GOODSRECEIPT TABLE ################
create table GOODSRECEIPT
(
	GoodsReceiptID char(10) not null,
    ReceivedDate date,
    StaffID char(10),
    
    primary key(GoodsReceiptID),
    foreign key(StaffID) references STAFF(StaffID)
);


########## GOODSRECEIPTDETAIL TABLE ################
create table GOODSRECEIPTDETAIL
(
	GoodsReceiptDetailID char(10) not null,
    GoodsReceiptID char(10),
    GoodsID char(10),
    ReceivedQuantity int,
    
    primary key(GoodsReceiptDetailID),
    foreign key(GoodsReceiptID) references GOODSRECEIPT(GoodsReceiptID),
    foreign key(GoodsID) references GOODS(GoodsID)
);



######## SALESRECEIPT TABLE ################
create table SALESRECEIPT
(
	ReceiptID char(10) not null,
    CustomerID char(10),
    DateCosted date,
    StaffID char(10),
    
    primary key(ReceiptID),
    foreign key(CustomerID) references CUSTOMER(CustomerID),
    foreign key(StaffID) references STAFF(StaffID)
);


######## SALESRECEIPTDETAIL TABLE ################
create table SALESRECEIPTDETAIL
(
	ReceiptDetailID char(10) not null,
    ReceiptID char(10),
    DishID char(10),
    SalesQuantity int,
    
    primary key(ReceiptDetailID),
    foreign key(DishID) references DISH(DishID),
    foreign key(ReceiptID) references SALESRECEIPT(ReceiptID)
);

######## INCOMESTATEMENT TABLE ################
create table INCOMESTATEMENT
(
	IncomeStatementID char(10) not null,
    StatementMonth int,
    StatementYear int,
    
    primary key(IncomeStatementID)
);

######## INCOMEDETAIL TABLE ################
create table INCOMEDETAIL
(
	IncomeDetailID char(10) not null,
	IncomeStatementID char(10),
    CustomerID char(10),
    IncomeAmount decimal,
    
    primary key(IncomeDetailID),
    foreign key(CustomerID) references CUSTOMER(CustomerID),
    foreign key(IncomeStatementID) references INCOMESTATEMENT(IncomeStatementID)
);


######## RULES TABLE ################
create table RULES
(
    MinStockBeforeImporting int,
    MaxDebtAmount decimal
);

#Insert into RULES values(0,300,0,0,False)




                                      
                                      
                                      
