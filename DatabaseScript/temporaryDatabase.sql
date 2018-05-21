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
	insert into Staff values(_ID,_Name,_Address,_Phone,_Email,_PositionID ,_Salary,_Status);
End //
DELIMITER ;

DELIMITER //
Create Procedure UpdateStaff(in _ID char(10), in _Name char(100),in _Address char(100), in _Phone char(20), in _Email char(50), int _PositionID char(10))
Begin
	update Staff set StaffName=_Name,StaffAddress=_Address, Phone=_Phone ,Email=_Email, PositionID = _PositionID where StaffID=_ID;
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
							in _Email char(50), in PositionID char(10), in _Salary decimal, in _SalaryCompareType varchar(2), in _Status bool)
Begin	
    Create temporary table SalaryTable (StaffID char(10));
    
    case _SalaryCompareType 
		when '=' then insert into SalaryTable select StaffID from Staff where Salary = _Salary;
        when '>' then insert into SalaryTable select StaffID from Staff where Salary > _Salary;
        when '>=' then insert into SalaryTable select StaffID from Staff where Salary >= _Salary;
        when '<' then insert into SalaryTable select StaffID from Staff where Salary < _Salary;
        when '<=' then insert into SalaryTable select StaffID from Staff where Salary <= _Salary;
	end case;

    
    select StaffID As 'Mã Nhân Viên', StaffName As 'Họ Tên Nhân Viên', StaffAddress As 'Địa Chỉ',Phone As 'Điện Thoại', Email As 'Email',
																										CONCAT('',Format(Salary,0), ' đ') As 'Lương tháng' 
	from Staff 
	where StaffID like CONCAT('%', _ID, '%') and  StaffName like CONCAT('%', _Name, '%') and StaffAddress like CONCAT('%', _Address, '%') and 
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