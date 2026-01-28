using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using universitybusinesslayer.Dtos;
using universitydatalayer.data;
using universitydatalayer.entities;

namespace universitybusinesslayer.business
{
    public class userbusiness
    {
        private readonly userdata _userdata;
        private readonly jwtsettings _jwtsettings;
        public userbusiness(userdata userdata, IOptions<jwtsettings> jwtsettings)
        {
            _userdata = userdata;
            _jwtsettings = jwtsettings.Value;
        }
        public async Task<string> login(logindto login)
        {
            var user = await _userdata.getuserbyemailandpassword(login.email, login.password);
            if (user==null)
            {
                return null;
            }
            var permissions = await _userdata.getuserpermissions(user.id);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                new Claim(ClaimTypes.Role, user.role.Trim().ToLower())
            };
            foreach (var perm in permissions)
            {
                claims.Add(new Claim("permission", perm.name));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtsettings.key));
            var creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtsettings.issuer,
                audience: _jwtsettings.audience,
                claims: claims,
                expires: DateTime.Now.AddHours(_jwtsettings.durationinhours),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        public async Task<userdto>addnewuser(newuserdto newuserdto)
        {
            var user = new user();
            user.email= newuserdto.email;
            user.password= newuserdto.password;
            user.role= newuserdto.role;

            int userid = await _userdata.addnewuser(user);

            if(userid!=0)
            {
                if(user.role=="admin")
                {
                    await _userdata.addpermissions(userid, newuserdto.permissionids);
                }
                userdto userdto = new userdto();
                userdto.id=userid;
                userdto.email = user.email;
                userdto.password= user.password;
                userdto.role= user.role;
                return userdto;
            }
            else
            {
                return null;
            }
        }
        public async Task<bool>updateuser(int userid,updateuserdto updateuserdto)
        {
            bool exists=await _userdata.exists(userid);
            if (!exists)
                throw new Exception("user is not found");

            user user = new user
            {
                id = userid,
                email = updateuserdto.email,
                role = updateuserdto.role
            };
            if(updateuserdto.permissionids!=null)
            {
                await _userdata.replaceuserpermissions(userid, updateuserdto.permissionids);
            }

            return await _userdata.updateuser(user);

        }
        public async Task<bool>changepassword(int userid,changepassworddto changepassworddto)
        {
            var user = await _userdata.getuserbyid(userid);
            if (user == null)
                throw new Exception("user is not found");

            bool valid = _userdata.varifypassword(user, user.password, changepassworddto.oldpassword);

            if (!valid)
                throw new Exception("old password is wrong");

            return await _userdata.changepassword(userid,changepassworddto.newpassword);
                
            
        }
        public async Task<bool>deleteuser(int userid)
        {
            bool exist=await _userdata.exists(userid);
            if (!exist)
                throw new Exception("user is not found");
            return await _userdata.deleteuser(userid);
        }
    }
}
