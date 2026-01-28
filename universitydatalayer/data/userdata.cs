using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using universitydatalayer.dbcontext;
using universitydatalayer.entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using PasswordVerificationResult = Microsoft.AspNetCore.Identity.PasswordVerificationResult;

namespace universitydatalayer.data
{
    public class userdata
    {
        private readonly appdbcontext _context;
        private readonly PasswordHasher<user> _hasher=new PasswordHasher<user>();
        public userdata(appdbcontext context)
        {
            _context = context;
        }
        public string hashpassword(user user,string password)
        {
            return _hasher.HashPassword(user, password);
        }
        public bool varifypassword(user user,string hashedpassword,string providedpassword)
        {
            var result=_hasher.VerifyHashedPassword(user, hashedpassword, providedpassword);
            return result == PasswordVerificationResult.Success;
        }
        public async Task<user>getuserbyemailandpassword(string email,string password)
        {
            var user=await _context.users.FirstOrDefaultAsync(u=>u.email==email);
            if(user!=null)
            {
                bool valid =  varifypassword(user, user.password, password);
                if(valid)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
                
        }
        public async Task<int>addnewuser(user newuser)
        {
            newuser.password =  hashpassword(newuser, newuser.password);
            _context.users.Add(newuser);
            await _context.SaveChangesAsync();

            return newuser.id;
            
        }
        public async Task<List<permission>>getuserpermissions(int userid)
        {
            return await _context.userpermissions.Where(up => up.userid == userid).
                Select(up => up.permission).ToListAsync();
        }
        public async Task addpermissions(int userid,List<int>permissionids)
        {
            foreach(var pid in  permissionids)
            {
                _context.userpermissions.Add(new userpermission
                {
                    userid = userid,
                    permissionid = pid
                });
            }
            await _context.SaveChangesAsync();
        }
        public async Task<bool>updateuser(user user)
        {
            var existinguser = await _context.users.FirstOrDefaultAsync(u => u.id == user.id);

            existinguser.email= user.email;
            existinguser.role= user.role;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task replaceuserpermissions(int userid,List<int>permissionids)
        {
            var oldpermissions = await _context.userpermissions.Where(up => up.userid == userid).ToListAsync();
            _context.userpermissions.RemoveRange(oldpermissions);

            var newpermissions = permissionids.Select(p =>
            new userpermission
            {
                userid = userid,
                permissionid = p
            });

            _context.userpermissions.AddRange(newpermissions);
            await _context.SaveChangesAsync();
        }
        public async Task<bool>exists(int userid)
        {
            return await _context.users.AnyAsync(u=>u.id== userid);
        }
        public async Task<user>getuserbyid(int userid)
        {
            return await _context.users.FirstOrDefaultAsync(u => u.id == userid);
        }
        public async Task<bool>changepassword(int userid,string password)
        {
            var user=await _context.users.FirstOrDefaultAsync(u=>u.id == userid);
            user.password = hashpassword(user, password);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool>deleteuser(int userid)
        {
            var user=await _context.users.FirstOrDefaultAsync(u=>u.id==userid);
            _context.users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
