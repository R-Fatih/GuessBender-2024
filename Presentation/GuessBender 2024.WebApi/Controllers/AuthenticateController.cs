using GuessBender_2024.Application.Features.Mediator.Commands.AuthorizationQueries;
using GuessBender_2024.Application.Features.Mediator.Queries.AuthorizationQueries;
using GuessBender_2024.Application.Features.Mediator.Results.AuthorizationResults;
using GuessBender_2024.Application.Tools;
using GuessBender_2024.Domain.Entities;
using GuessBender_2024.Persistance.Context;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.WebApi.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class AuthenticateController : ControllerBase
	{
		

		private readonly IMediator _mediator;

		public AuthenticateController( IMediator mediator)
		{
			

            _mediator = mediator;
		}



        [HttpPost]
        public async Task<IActionResult> Login(LoginQuery command)
        {
            var values = await _mediator.Send(command);
            if (values.IsExist)
                return Created("", JwtTokenGenerator.GenerateToken(values));
            else return BadRequest("Kullanıcı adı veya şifre hatalı");
        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordQuery query)
        {
            return Ok(await _mediator.Send(query));

        }
        [HttpPost("GenerateNewPassword")]
        public async Task<IActionResult> GenerateNewPassword(CreateNewPasswordQuery query)
        {
           
            return Ok(await _mediator.Send(query));

        }
        //      [HttpPost]
        //      [Route("loginadmin")]
        //      public async Task<IActionResult> LoginAsAdmin([FromBody] LoginModel model)
        //      {
        //          var user = await userManager.FindByNameAsync(model.Username);
        //          if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
        //          {
        //              var userRoles = await userManager.GetRolesAsync(user);

        //              var authClaims = new List<Claim>
        //              {
        //                  new Claim(ClaimTypes.Name, user.UserName),
        //                  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //			new Claim(JwtRegisteredClaimNames.Sub,user.Id)
        //		};
        //		if (!userRoles.Contains("Admin"))
        //		{
        //			return Unauthorized();
        //		}
        //              foreach (var userRole in userRoles)
        //              {
        //                  authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        //              }

        //              var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        //              var token = new JwtSecurityToken(
        //                  issuer: _configuration["JWT:ValidIssuer"],
        //                  audience: _configuration["JWT:ValidAudience"],
        //                  expires: DateTime.Now.AddDays(256),
        //                  claims: authClaims,
        //                  signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        //                  );

        //              return Ok(new
        //              {
        //                  token = new JwtSecurityTokenHandler().WriteToken(token),
        //                  expiration = token.ValidTo
        //              });
        //          }
        //          return Unauthorized();
        //      }

        //      [HttpPost]
        //[Route("register")]
        //public async Task<IActionResult> Register([FromBody] RegisterModel model)
        //{
        //	var userExists = await userManager.FindByNameAsync(model.Username);
        //	if (userExists != null)
        //		return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

        //	ApplicationUser user = new ApplicationUser()
        //	{
        //		Email = model.Email,
        //		SecurityStamp = Guid.NewGuid().ToString(),
        //		UserName = model.Username
        //	};
        //	var result = await userManager.CreateAsync(user, model.Password);
        //	if (!result.Succeeded)
        //		return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

        //	return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        //}

        //[HttpPost]
        //[Route("register-admin")]
        //public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        //{
        //	var userExists = await userManager.FindByNameAsync(model.Username);
        //	if (userExists != null)
        //		return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

        //	ApplicationUser user = new ApplicationUser()
        //	{
        //		Email = model.Email,
        //		SecurityStamp = Guid.NewGuid().ToString(),
        //		UserName = model.Username
        //	};
        //	var result = await userManager.CreateAsync(user, model.Password);
        //	if (!result.Succeeded)
        //		return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

        //	if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
        //		await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        //	if (!await roleManager.RoleExistsAsync(UserRoles.Comp))
        //		await roleManager.CreateAsync(new IdentityRole(UserRoles.Comp));

        //	if (await roleManager.RoleExistsAsync(UserRoles.Admin))
        //	{
        //		await userManager.AddToRoleAsync(user, UserRoles.Admin);
        //	}

        //	return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        //      }
        //[HttpPost]
        //[Route("register-")]
        //public async Task<IActionResult> RegisterWriter([FromBody] RegisterModel model)
        //{
        //	var userExists = await userManager.FindByNameAsync(model.Username);
        //	if (userExists != null)
        //		return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Bu kullanıcı zaten var!" });

        //	ApplicationUser user = new ApplicationUser()
        //	{
        //		Email = model.Email,
        //		SecurityStamp = Guid.NewGuid().ToString(),
        //		UserName = model.Username
        //	};
        //	var result = await userManager.CreateAsync(user, model.Password);
        //	if (!result.Succeeded)
        //		return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Kayıt hatası lütfen bilgileri kontrol edin." });

        //	if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
        //		await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        //	if (!await roleManager.RoleExistsAsync(UserRoles.Comp))
        //		await roleManager.CreateAsync(new IdentityRole(UserRoles.Comp));

        //	if (await roleManager.RoleExistsAsync(UserRoles.Comp))
        //	{
        //		await userManager.AddToRoleAsync(user, UserRoles.Comp);
        //	}

        //	return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        //}
    }
}
