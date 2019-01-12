using demo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace demo.Models
{
    public class User
    {
        [Required]
        public string name { get; set; }

        [Required]
        [EmailAddress]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }


    }

    public class LoginUser
    {
        [Required]
        public String UserName { get; set; }
        [Required]
        public String Password { get; set; }
    }


    

}


public class UserInfo
{
    
    public List<User> getData()
    {
        List<User> lstUser = new List<User>();
        lstUser.Add(new User { UserName = "demo1@gmail.com", name = "demo1" });
        lstUser.Add(new User { UserName = "demo2@gmail.com", name = "demo2" });
        lstUser.Add(new User { UserName = "demo3@gmail.com", name = "demo3" });
        lstUser.Add(new User { UserName = "demo4@gmail.com", name = "demo4" });
        return lstUser;
    }

}