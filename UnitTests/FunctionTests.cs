using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BUS;
using DTO;
using GUI;

namespace UnitTests
{
    [TestFixture]
    class FunctionTests
    {

        //string GetRandomWord(int numberOfCharacter)
        //{
        //    StringBuilder word = new StringBuilder(); 
        //    for(int i = 0;i< numberOfCharacter; i++)
        //    {
        //        word.Append("H");
        //    }
        //    return word.ToString();
        //}

        [TestCase("EM101","Hello","Hello", "09123431456", "abc@gmail.com", true)]
        [TestCase("EM1010131232", "Hello", "Hello", "09123431456", "abc@gmail.com", false)]
        [TestCase("~", "Hello", "Hello", "09123431456", "abc@gmail.com", false)]
        [TestCase("", "Hello", "Hello", "09123431456", "abc@gmail.com", false)]
        [TestCase("EM10232310", "~", "Hello", "09123431456", "abc@gmail.com", false)]
        [TestCase("EM101", "", "Hello", "09123431456", "abc@gmail.com", false)]
        [TestCase("EM101", "Hello", "~", "09123431456", "abc@gmail.com", false)]
        [TestCase("EM101", "Hello", "Hello", "~", "abc@gmail.com", false)]
        [TestCase("EM101", "Hello", "Hello", "", "abc@gmail.com", false)]
        public void IsRightFormatTestFunction(string id, string name, string address, string phoneNumber, string email, bool result)
        {
            CustomerDTO customer = new CustomerDTO(id,name,address,phoneNumber,email,0);
            Assert.AreEqual(CustomerBUS.Instance.IsRightFormat(customer), result);

        }


        [TestCase("101", true)]
        [TestCase("a", false)]
        [TestCase("~", false)]
        [TestCase(null, false)]
        [TestCase("101a", false)]
        [TestCase("a~", false)]
        [TestCase("101a~", false)]
        public void IsNumberTestFunction(string text, bool result)
        {          
            Assert.AreEqual(UserControlEmployees.IsNumber(text), result);
        }
    }
}
